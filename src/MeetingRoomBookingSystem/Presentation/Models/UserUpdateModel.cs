using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class UserUpdateModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pin is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Pin should be between 4 and 10 characters.")]
        public string Pin { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Department name can't be longer than 100 characters.")]
        public string Department { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Designation name can't be longer than 100 characters.")]
        public string Designation { get; set; } = string.Empty;
    }
}
