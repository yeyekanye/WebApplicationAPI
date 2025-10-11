namespace Application.DTOs
{
    public class CreateGameDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ImageFolderPath { get; set; } = string.Empty;
    }
}
