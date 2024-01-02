using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tf_Demo_Mvc.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
