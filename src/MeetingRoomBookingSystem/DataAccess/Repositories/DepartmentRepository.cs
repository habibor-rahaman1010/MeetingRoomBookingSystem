using DataAccess.Data;
using Domain.Entities;
using Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : Repository<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(MRBSDbContext mrbsDbcontext) : base(mrbsDbcontext)
        {
        }
    }
}
