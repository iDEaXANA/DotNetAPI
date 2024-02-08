namespace DotnetAPI.DTOs
{
    public partial class UserForLoginDTO
    {
        // Check that the details are correct. e.g Typos.
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}