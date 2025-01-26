using Domain.Entities;

namespace Service.ServicesContract
{
    public interface IDepartmentManagementService
    {
        Task<IList<Department>> GetDepartmentsAsync();
    }
}
