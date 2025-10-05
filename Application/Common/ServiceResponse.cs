namespace Application.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;     // Чи успішна операція
        public string Message { get; set; } = string.Empty; // Повідомлення (помилка або інфо)
        public T? Data { get; set; }                  // Дані (DTO, список і т.д.)
    }
}
