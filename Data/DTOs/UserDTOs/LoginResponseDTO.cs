namespace Data.DTOs.UserDTOs
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
