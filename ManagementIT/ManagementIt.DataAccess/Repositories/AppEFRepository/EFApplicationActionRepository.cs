using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.DataAccess.DataBase;
using ManagementIt.DataAccess.Repositories.TRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.MongoEntity;
using ManagementIt.Core.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ManagementIt.DataAccess.Repositories.AppEFRepository
{
    public class EFApplicationActionRepository : EFGenericRepository<ApplicationAction>, IApplicationActionRepository
    {
        public EFApplicationActionRepository(AppDbContext context, ILogService service) : base(context, service) { }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationAction>>> GetActionByToItIdAsync(int apptoitId)
        {
            try
            {
                var response = await _context.ApplicationsAction.Where(x => x.AppId == apptoitId).ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationAction>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationAction>>
                    .Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске истории заявки по ID заявки < {typeof(ApplicationAction)} > | ApplicationID < {apptoitId} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<ApplicationAction>> GetActionByIdAsync(int actionId, ActionType action = 0)
        {
            try
            {
                if (action == 0)
                {
                    var response = await _context.ApplicationsAction.Where(x => x.Id == actionId).FirstOrDefaultAsync();
                    return ManagementITActionResult<ApplicationAction>.IsSuccess(response);
                }
                else
                {
                    var response = await _context.ApplicationsAction.FirstOrDefaultAsync(x => x.Id == actionId && x.Action == action);
                    return ManagementITActionResult<ApplicationAction>.IsSuccess(response);
                }
            }
            catch (Exception e)
            {
                return ManagementITActionResult<ApplicationAction>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске истории заявки по её ID || < {typeof(ApplicationAction)} > || ActionID < {actionId} > || {e.InnerException} >");
            }
        }

        public override async Task<IEnumerable<ApplicationAction>> GetEntitiesByNameAsync(string name)
        {
            try
            {
                var response = await _context.ApplicationsAction.Where(x => x.StateName.ToLower().Trim() == name.ToLower().Trim()).ToListAsync();
                return response;
            }
            catch (Exception e)
            {
                await _service.Create(LogMessage.GetLogMessage(ManagementItConstants.ConstantDbContext.Context,
                    $"Ошибка при поиске истории заявки по названию < {typeof(ApplicationAction)} > | ActionName < {name} > || {e.InnerException} >",
                    NotificationType.Error));
                return null;
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationAction>>> GetActionByEnumTypeAsync(ActionType action)
        {
            try
            {
                var response = await _context.ApplicationsAction.Where(x => x.Action == action).ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationAction>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationAction>>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске истории заявок по ActionType < {typeof(ApplicationAction)} > | ActionType < {action} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<ApplicationAction>> GetActionByToItIdAndActionTypeAsync(int appId, ActionType action)
        {
            try
            {

                var response = await _context.ApplicationsAction.FirstOrDefaultAsync(x => x.AppId == appId && x.Action == action);
                return ManagementITActionResult<ApplicationAction>.IsSuccess(response);

            }
            catch (Exception e)
            {
                return ManagementITActionResult<ApplicationAction>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске истории заявки по ID заявки || < {typeof(ApplicationAction)} > || ApplicationId < {appId} > || {e.InnerException} >");
            }
        }
    }
}
