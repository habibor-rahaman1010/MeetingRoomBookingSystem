using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Pin { get; set; } = string.Empty;
        public string Designation {  get; set; } = string.Empty;
        public bool Status {  get; set; }
        public Guid DepartmentId { get; set; }
    }
}
