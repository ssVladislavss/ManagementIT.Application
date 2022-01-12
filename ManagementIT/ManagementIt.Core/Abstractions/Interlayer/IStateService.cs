using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.Core.Abstractions.Interlayer
{
    public interface IStateService
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationStateModel>>> GetAllAsync();
        Task<ManagementITActionResult<IEnumerable<ApplicationStateModel>>> GetByName(string name);
        Task<ManagementITActionResult<ApplicationStateModel>> GetByIdAsync(int id);
        Task<ManagementITActionResult> AddAsync(ApplicationStateModel model);
        Task<ManagementITActionResult> UpdateAsync(ApplicationStateModel model);
        bool ExistEntityByName(string name, int? Tid = null);
        Task<ManagementITActionResult> DeleteAsync(int stateId);
    }
}