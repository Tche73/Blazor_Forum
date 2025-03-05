namespace Blazor_Forum.Dto_s
{
    public class AuthResponseDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
}
