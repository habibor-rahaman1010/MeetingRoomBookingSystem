using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class SigninModel
    {
        [Required]
        public string Pin { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [TempData]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
