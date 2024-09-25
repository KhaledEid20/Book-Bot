namespace BookBot.DTOs
{
    public class AddBook
    {
        public int AuthorId { get; set; }
        public BookGenre Genre { get; set; }
        public string? BookName { get; set; }
        public string? publishDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public IFormFile ImageData { get; set; }
    }

}