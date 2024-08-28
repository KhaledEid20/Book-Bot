from dotenv import load_dotenv;
import os
import requests
import telebot

load_dotenv()
API_KEY = os.getenv('API_KEY')
bot = telebot.TeleBot(API_KEY)


@bot.message_handler(commands = ['hello'])
def helloMessage(message):
    x = "Hello on our bot here we can provide you info about the books and authors,\n and there is will be more features in the coming versions"
    bot.send_message(message.chat.id,x)
@bot.message_handler(commands = ['book'])
def getBook(message):
    args = message.text.split()[1:]
    if not args :
        bot.send_message(message.chat.id,"please Enter the book name(required) and publish date(optional)")
        return
    name = args[0]
    publishDate = args[1] if len(args) > 1 else None

    params = {
        "Name" : name,
        "Pd" : publishDate
    }
    try:
        resp = requests.get("http://localhost:5082/api/Book/getBook", params=params)

        if resp.status_code == 200:
            data = resp.json()
            response_message = (
                f"Author Name: {data.get('authorName')}\n"
                f"Genre: {data.get('genre')}\n"
                f"Book Name: {data.get('bookName')}\n"
                f"Publish Date: {data.get('publishDate')}"
            )
        else:
            response_message = "Couldn't retrieve the information about the author."

    except Exception as e:
        response_message = f"An error occurred: {str(e)}"

    bot.send_message(message.chat.id, response_message)
bot.polling()
