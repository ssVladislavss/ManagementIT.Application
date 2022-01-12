using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementIt.Core.Abstractions.AppRepository
{
    public interface IApplicationTOITRepository : IGenericRepository<ApplicationToIt>
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByDepartamentIdAsync(int depId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByEmployeeIdAsync(int employeeId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByIniciatorIdAsync(int iniciatorId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByRoomIdAsync(int roomId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByApplicationTypeIdAsync(int typeId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAllApplicationToItAsync();
        Task<ManagementITActionResult<ApplicationToIt>> GetApplicationToItByIdAsync(int applicationToItId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetToItForOnDeleteAsync();
        Task<ManagementITActionResult> ExistDependencyEntity(int roomId = 0, int departmentId = 0, int employeeId = 0);
    }
}
