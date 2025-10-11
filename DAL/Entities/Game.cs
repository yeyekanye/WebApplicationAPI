namespace DAL.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        // 🆕 Шлях до папки із зображеннями гри
        public string ImageFolderPath { get; set; } = string.Empty;
    }
}
