using DataAccess.Data;
using Domain.RepositoryContracts;
using Domain.UnitOfWorkContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class MRBSUnitOfWork : UnitOfWork, IMRBSUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; private set; }

        
        public MRBSUnitOfWork(MRBSDbContext mrbsDbContext,
            IDepartmentRepository departmentRepository)
            : base(mrbsDbContext)
        {
            DepartmentRepository = departmentRepository;

        }
        
    }
}
     