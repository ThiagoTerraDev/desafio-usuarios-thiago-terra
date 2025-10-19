namespace api_rest_dotnet.DTOs
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
    }
}
