namespace DotnetAPI
{
    public partial class Users
    {
        public int UserId { get; set; } // Makes it a property instead of a field.
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Gender { get; set; } = "";
        public bool Active { get; set; }
    }
}