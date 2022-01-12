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
using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.Domain.MongoEntity;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;
using ManagementIt.DataAccess.DataBase;

namespace ManagementIt.DataAccess.InterlayerRepositories
{
    public class InterlayerState : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _service;

        public InterlayerState(IStateRepository stateRepository, IMapper mapper, ILogService service)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationStateModel>>> GetAllAsync()
        {
            var states = await _stateRepository.GetAllEntitiesAsync();
            if (states.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationStateModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, "Состояний заявок нет", null, states.AspNetException);
            
            if (!states.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationStateModel>>
                    .Fail(new []{ TypeOfErrors.NoContent }, "Состояний заявок нет", null, "Запрос выполнен. Данных нет");
            
            var response = _mapper.Map<IEnumerable<ApplicationStateModel>>(states.Data);
            return ManagementITActionResult<IEnumerable<ApplicationStateModel>>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationStateModel>>> GetByName(string name)
        {
            var states = await _stateRepository.GetEntitiesByNameAsync(name);
            if (!states.Any())
            {
                //await _service.Create(LogMessage.GetLogMessage($"{ManagementItConstants.ConstantState.controller}",
                //    $"Запрос выполнен. Данных нет", NotificationType.Error, principal?.Identity?.Name));

                return ManagementITActionResult<IEnumerable<ApplicationStateModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, $"По названию [{name}], ничего не найдено", null);
            }

            var response = _mapper.Map<IEnumerable<ApplicationStateModel>>(states);
            return ManagementITActionResult<IEnumerable<ApplicationStateModel>>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult<ApplicationStateModel>> GetByIdAsync(int id)
        {
            var result = await _stateRepository.GetEntityByIdAsync(id);
            if (result.AspNetException != null) return ManagementITActionResult<ApplicationStateModel>
                    .Fail(result.Errors, result.ErrorDescription, null, result.AspNetException);
            if (result.Data == null) return ManagementITActionResult<ApplicationStateModel>
                    .Fail(result.Errors, result.ErrorDescription, null, $"Состояние не найдено || ID < {id} >");

            var response = _mapper.Map<ApplicationStateModel>(result.Data);
            return ManagementITActionResult<ApplicationStateModel>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult> AddAsync(ApplicationStateModel model)
        {
            var entity = _mapper.Map<ApplicationState>(model);

            if (entity.IsDefault)
            {
                var exist = await _stateRepository.GetStateOrDefault();
                if (exist.Data != null)
                {
                    exist.Data.IsDefault = false;
                    var result = await _stateRepository.UpdateEntityAsync(exist.Data);
                    if (result.AspNetException != null) return result;
                    else if (!result.Success)
                        return ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null,
                            $"Запрос на создание дефолтного состояния || Ошибка при изменении имеющегося состояния по умолчанию || " +
                            $"ID имеющегося состояния по умолчанию: < {exist.Data.Id} > , Name: < {exist.Data.Name} > || " +
                            $"Входной параметр Name: < {model.Name} >");
                }
            }

            return await _stateRepository.AddEntityAsync(entity);
        }

        public async Task<ManagementITActionResult> UpdateAsync(ApplicationStateModel model)
        {
            var state = await _stateRepository.GetEntityByIdAsync(model.Id);
            if (state.AspNetException != null)
                return ManagementITActionResult.Fail(state.Errors, state.ErrorDescription, state.AspNetException);
            if (state.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                    $"Запрос на изменение данных состояния || Данные не найдены || ID: < {model.Id} > || Name: < {model.Name} >");

            if (model.IsDefault)
            {
                var exist = await _stateRepository.GetStateOrDefault();
                if (exist.AspNetException != null) return exist;
                else if (exist.Data != null)
                {
                    exist.Data.IsDefault = false;
                    var result = await _stateRepository.UpdateEntityAsync(exist.Data);
                    if (result.AspNetException != null) return result;
                    else if (!result.Success)
                        return ManagementITActionResult.Fail(new[] { TypeOfErrors.UpdateEntityError }, null,
                            $"Запрос на изменение дефолтного состояния || Ошибка при изменении имеющегося состояния по умолчанию || " +
                            $"ID имеющегося состояния по умолчанию: < {exist.Data.Id} > , Name: < {exist.Data.Name} > || " +
                            $"Входной параметр ID: < {model.Id} > Name: < {model.Name} >");
                }
            }

            state.Data.BGColor = model.BGColor;
            state.Data.State = model.State;
            state.Data.Name = model.Name;
            state.Data.IsDefault = model.IsDefault;
            
            return await _stateRepository.UpdateEntityAsync(state.Data);
        }

        public bool ExistEntityByName(string name, int? Tid = null) => _stateRepository.ExistEntityByName(name, Tid);
        
        public async Task<ManagementITActionResult> DeleteAsync(int stateId)
        {
            if(stateId == 1 || stateId == 2 || stateId == 3) return ManagementITActionResult
                      .Fail(new[] { TypeOfErrors.DeletionEntityError }, null,
                      $"Запрос на удаление состояния || Модель: < {typeof(ApplicationState)} > || ID: < {stateId} > || Вы не можете удалить это состояние, оно является фиксированным");
            var entity = await _stateRepository.GetEntityByIdAsync(stateId);
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                    $"Ошибка при удалении состояния || данные не найдены || ID < {stateId} >");
            if(entity.Data.IsDefault) return ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null,
                $"Ошибка при удалении состояния || Это состояние является дефолтным, его нельзя удалить || ID < {stateId} > || Name: < {entity.Data.Name} >");

            return await _stateRepository.DeleteEntityAsync(entity.Data);
        }
    }
}
