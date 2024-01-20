namespace DotnetAPI.Models
{
    public partial class UserSalary
    {
        public int UserId { get; set; } // Makes it a property instead of a field.
        public decimal Salary { get; set; }
        public decimal AvgSalary { get; set; } // decimal defaults to 0
    }
}