using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts.Enums;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.Core.Abstractions.Interlayer
{
    public interface IActionService
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetAllAsync();
        Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetByToItIdAsync(int apptoitId);
        Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetByName(string name);
        Task<ManagementITActionResult<ApplicationActionModel>> GetByIdAsync(int actionId, ActionType action = 0);
        Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetByActionTypeAsync(ActionType action);
        Task<ManagementITActionResult> DeleteAll();
        Task<ManagementITActionResult> DeleteSelected(List<int> ids);
    }
}