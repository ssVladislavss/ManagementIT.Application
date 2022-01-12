using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.Core.Abstractions.Interlayer
{
    public interface IApplicationService
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>>GetByDeptIdAsync(int deptId);
        Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetByTypeIdAsync(int id, string iniciator);
        Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetAllAsync();
        Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetAllForOnDeleteAsync();
        Task<ManagementITActionResult<ApplicationToItModel>> GetByIdAsync(int id);
        Task<ManagementITActionResult> AddAsync(ApplicationToItModel model, string iniciator);
        Task<ManagementITActionResult> UpdateAsync(ApplicationToItModel model, string iniciator);
        Task<ManagementITActionResult> UpdateStateAsync(EditToItStateModel model, string iniciator);
        Task<ManagementITActionResult> UpdatePriorityAsync(EditPriorityModel model, string iniciator);
        Task<ManagementITActionResult> UpdateEmployeeAsync(EditEmployeeModel model, string iniciator);
        Task<ManagementITActionResult> SetIniciatorAsync(SetIniciatorOrApplicationModel model);

        Task<ManagementITActionResult> UpdateEmployeeNameAsync(EditEmployeeFullNameModel model);
        Task<ManagementITActionResult> UpdateRoomNameAsync(int roomId, string roomName);
        Task<ManagementITActionResult> UpdateDepartmentNameAsync(int deptId, string departmentName);
        
        bool ExistEntityByName(string name, int? Tid = null);
        Task<ManagementITActionResult> ExistDependencyEntity(int roomId = 0, int departmentId = 0, int employeeId = 0);
        Task<ManagementITActionResult> OnDeleteAsync(OnDeleteApplicationModel model, string iniciator);
        Task<ManagementITActionResult> DeleteAsync(DeleteApplicationModel model, string iniciator);
        Task<ManagementITActionResult> DeleteRangeToItByOnDeleteAsync();

        Task<ManagementITActionResult<CreateOrEditToItModel>> GetCreateToItAsync();
        Task<ManagementITActionResult<CreateOrEditToItModel>> GetUpdateToItAsync(int appId);
    }
}