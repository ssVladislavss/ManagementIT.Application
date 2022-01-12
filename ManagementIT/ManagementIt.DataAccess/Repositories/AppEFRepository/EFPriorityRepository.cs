using Contracts.Enums;
using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.ResponseModels;
using ManagementIt.DataAccess.DataBase;
using ManagementIt.DataAccess.Repositories.TRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.DataAccess.Repositories.AppEFRepository
{
    public class EFPriorityRepository : EFGenericRepository<ApplicationPriority>, IPriorityRepository
    {
        public EFPriorityRepository(AppDbContext context, ILogService service) : base(context, service){}

        public async Task<ManagementITActionResult<ApplicationPriority>> GetDefaultPriority()
        {
            try
            {
                var result = await _context.ApplicationsPriority.FirstOrDefaultAsync(x => x.IsDefault == true);
                return ManagementITActionResult<ApplicationPriority>.IsSuccess(result);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<ApplicationPriority>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Не удалось найти модель || Модель: < {typeof(ApplicationPriority)} > || Параметр поиска: < IsDefault = true > || Описание: < {e.InnerException} >");
            }
        }
    }
}
