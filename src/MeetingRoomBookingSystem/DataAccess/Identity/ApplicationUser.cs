using Microsoft.AspNetCore.Identity;

namespace DataAccess.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Pin { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
