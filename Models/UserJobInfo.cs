namespace DotnetAPI.Models
{
    public partial class UserJobInfo
    {
        public int UserId { get; set; } // Makes it a property instead of a field.
        public string JobTitle { get; set; } = "";
        public string Department { get; set; } = "";
    }
}