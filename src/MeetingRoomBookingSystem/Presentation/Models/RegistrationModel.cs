using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using DataAccess.RazorUtility;

namespace Presentation.Models
{
    public class RegistrationModel
    {
        public Guid Id { get; set; }
        public RegistrationModel()
        {
            Departments = new List<SelectListItem>();

        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Pin")]
        public string Pin { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }


        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; } = string.Empty;

        public IList<SelectListItem> Departments { get; private set; }


        public void SetDepartmentsValues(IList<Department> departments)
        {
            Departments = RazorUtility.ConvertDepartments(departments);
        }

    }
}
