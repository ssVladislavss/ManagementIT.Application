using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.Core.Abstractions.Interlayer
{
    public interface IPriorityService
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>> GetAllAsync();
        Task<ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>> GetByName(string name);
        Task<ManagementITActionResult<ApplicationPriorityModel>> GetByIdAsync(int id);
        Task<ManagementITActionResult> AddAsync(ApplicationPriorityModel model);
        Task<ManagementITActionResult> UpdateAsync(ApplicationPriorityModel model);
        bool ExistEntityByName(string name, int? Tid = null);
        Task<ManagementITActionResult> DeleteAsync(int priorityId);
    }
}