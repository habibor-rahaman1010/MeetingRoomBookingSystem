using DataAccess.Data;
using Domain.Entities;
using Domain.RepositoryContracts;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : Repository<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(MRBSDbContext mrbsDbcontext) : base(mrbsDbcontext)
        {
        }
    }
}
