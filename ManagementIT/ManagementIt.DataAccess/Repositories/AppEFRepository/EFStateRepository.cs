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
    public class EFStateRepository : EFGenericRepository<ApplicationState>, IStateRepository
    {
        public EFStateRepository(AppDbContext context, ILogService service) : base(context, service) { }

        public async Task<ManagementITActionResult<ApplicationState>> GetStateOrDefault()
        {
            try
            {
                var result = await _context.ApplicationsState.FirstOrDefaultAsync(x => x.IsDefault == true);
                return ManagementITActionResult<ApplicationState>.IsSuccess(result);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<ApplicationState>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Не удалось найти модель || Модель: < {typeof(ApplicationState)} > || Параметр поиска: < IsDefault = true > || Описание: < {e.InnerException} >");
            }
        }
    }
}
