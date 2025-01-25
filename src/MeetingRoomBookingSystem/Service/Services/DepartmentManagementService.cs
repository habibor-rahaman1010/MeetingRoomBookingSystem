using Domain.Entities;
using Domain.UnitOfWorkContracts;
using Service.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DepartmentManagementService : IDepartmentManagementService
    {
        private readonly IMRBSUnitOfWork _departmentUnitOfWork;
        public DepartmentManagementService(IMRBSUnitOfWork departmentUnitOfWork)
        {
            _departmentUnitOfWork = departmentUnitOfWork;
        }

        public async Task<IList<Department>> GetDepartmentsAsync()
        {
            return await _departmentUnitOfWork.DepartmentRepository.GetAllAsync();
        }
    }
}
