namespace DotnetAPI.DTOs
{
    partial class UserForRegistrationDTO
    {
        // Check that the details are correct. e.g Typos.
        string Email { get; set; } = "";
        string Password { get; set; } = "";
        string PasswordConfirm { get; set; } = "";
    }
}