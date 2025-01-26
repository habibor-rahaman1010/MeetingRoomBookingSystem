using Domain.Entities;
using Domain.UnitOfWorkContracts;
using Service.ServicesContract;

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
