using System.ComponentModel.DataAnnotations;

namespace TF_Demo.Models
{
    public class Tf_Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
