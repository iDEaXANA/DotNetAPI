namespace DotnetAPI.DTOs
{
    public partial class UserForLoginDTO
    {
        // Check that the details are correct. e.g Typos.
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}