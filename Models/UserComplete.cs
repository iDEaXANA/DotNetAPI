namespace DotnetAPI.Models
{
    public partial class UserComplete
    {
        public int UserId { get; set; } // Makes it a property instead of a field.
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; } // decimal defaults to 0
        public decimal AvgSalary { get; set; } // May need to be removed
    }
}