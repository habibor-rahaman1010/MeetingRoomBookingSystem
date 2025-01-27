using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class SigninModel
    {
        [Required(ErrorMessage = "Pin is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Pin must be between 4 and 10 characters.")]
        public string Pin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        public bool RememberPassword { get; set; }

        [TempData]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
