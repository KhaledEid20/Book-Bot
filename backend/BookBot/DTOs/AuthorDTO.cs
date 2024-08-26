namespace BookBot.DTOs
{
    public class AuthorDTO
    {
        public string AuthorName { get; set; } = String.Empty;
        public string birthDate { get; set; } = String.Empty;
        public string? deadDate { get; set; }
        public string? Nationality { get; set; }
        public string brief { get; set; } = String.Empty;
    }
}
