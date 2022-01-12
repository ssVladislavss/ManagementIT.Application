using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Enums;
using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.MongoEntity;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.DataAccess.InterlayerRepositories
{
    public class InterlayerAction : IActionService
    {
        private readonly IApplicationActionRepository _actionRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _service;

        public InterlayerAction(IApplicationActionRepository actionRepository,
            IMapper mapper,
            ILogService service)
        {
            _actionRepository = actionRepository ?? throw new ArgumentNullException(nameof(actionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetAllAsync()
        {
            var actions = await _actionRepository.GetAllEntitiesAsync();
            if (actions.AspNetException != null) 
                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.Fail(new[] { TypeOfErrors.InternalServerError }, $"", null, actions.AspNetException);
            if (!actions.Data.Any())
            {
                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>
                    .Fail(new []{ TypeOfErrors.NoContent }, "Истории нет", null, "Запрос выполнен. Данных нет.");
            }

            var response = _mapper.Map<IEnumerable<ApplicationActionModel>>(actions.Data);
            return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetByToItIdAsync(int apptoitId)
        {
            var actions = await _actionRepository.GetActionByToItIdAsync(apptoitId);
            if (actions.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.Fail(actions.Errors, actions.ErrorDescription, null, actions.AspNetException);

            if (!actions.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>
                    .Fail(new []{ TypeOfErrors.NoContent }, "Истории нет", null, $"Запрос выполнен. Данных нет || ID заявки <{apptoitId}>");
            
            var response = _mapper.Map<IEnumerable<ApplicationActionModel>>(actions.Data);
            return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetByName(string name)
        {
            var actions = await _actionRepository.GetEntitiesByNameAsync(name);
            if (!actions.Any())
            {
                //await _service.Create(LogMessage.GetLogMessage($"{ManagementItConstants.ConstantAction.controller}", $"Запрос выполнен. Данных нет",
                //                                                   NotificationType.Error, principal?.Identity?.Name));

                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>
                    .Fail(new []{ TypeOfErrors.NoContent }, $"По названию [{name}], ничего не найдено", null);
            }

            var response = _mapper.Map<IEnumerable<ApplicationActionModel>>(actions);
            return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<ApplicationActionModel>> GetByIdAsync(int actionId, ActionType action = 0)
        {
            var result = await _actionRepository.GetActionByIdAsync(actionId, action);
            
            if (result.AspNetException != null)
                return ManagementITActionResult<ApplicationActionModel>.Fail(result.Errors, null, null, result.AspNetException);
            if (result.Data == null)
                return ManagementITActionResult<ApplicationActionModel>
                    .Fail(new []{ TypeOfErrors.NotFound }, "Истории заявки под указанным идентификатором нет", null,
                    $"Запрос выполнен || Данные не найдены || Входные параметры: < actionId = {actionId} > || < action = {action} >");
            
            var response = _mapper.Map<ApplicationActionModel>(result.Data);
            return ManagementITActionResult<ApplicationActionModel>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult> DeleteAll()
        {
            var actions = await _actionRepository.GetAllEntitiesAsync();
            if (actions.AspNetException != null)
                return ManagementITActionResult.Fail(actions.Errors, actions.ErrorDescription, actions.AspNetException);

            if (!actions.Data.Any())
                return ManagementITActionResult.Fail(new []{ TypeOfErrors.NoContent }, "Нет ниодной записи", "Не удалось удалить историю || нет ниодной записи");
            
            return await _actionRepository.DeleteRangeAsync(actions.Data);
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationActionModel>>> GetByActionTypeAsync(ActionType action)
        {
            var result = await _actionRepository.GetActionByEnumTypeAsync(action);
            if (result.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.Fail(result.Errors, null, null, result.AspNetException);
            if (!result.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationActionModel>>
                    .Fail(new[] { TypeOfErrors.NotFound }, null, null,
                    $"Запрос выполнен || Данные не найдены || Входной параметр: action < {action} >");

            var response = _mapper.Map<IEnumerable<ApplicationActionModel>>(result.Data);
            return ManagementITActionResult<IEnumerable<ApplicationActionModel>>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult> DeleteSelected(List<int> ids)
        {
            var actions = await _actionRepository.GetEntitiesByIdsAsync(ids);
            if (actions.AspNetException != null)
                return ManagementITActionResult.Fail(actions.Errors, null, actions.AspNetException);
            if (!actions.Data.Any())
            {
                var log = string.Empty;
                foreach (var id in ids)
                {
                    log += id.ToString() + ", ";
                }
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                    $"Запрос на удаление списка моделей || Модель: < {typeof(ApplicationActionModel)} > || Входной параметр, список Id: < {log} >");
            }

            return await _actionRepository.DeleteRangeAsync(actions.Data); 
        }
    }
}
