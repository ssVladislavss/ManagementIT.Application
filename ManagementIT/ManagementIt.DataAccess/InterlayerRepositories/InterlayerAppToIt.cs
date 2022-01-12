using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Enums;
using IdentityModel;
using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.Domain.MongoEntity;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.DataAccess.InterlayerRepositories
{
    public class InterlayerAppToIt : IApplicationService
    {
        private readonly IApplicationTOITRepository _applicationToitRepository;
        private readonly IMapper _mapper;
        private readonly IPriorityRepository _priorityService;
        private readonly IGenericRepository<ApplicationType> _typeService;
        private readonly IStateRepository _stateService;
        private readonly IApplicationActionRepository _applicationActionRepository;

        public InterlayerAppToIt(IApplicationTOITRepository applicationToitRepository, IMapper mapper,
            IGenericRepository<ApplicationType> typeRepository,
            IPriorityRepository priorityRepository,
            IStateRepository stateRepository,
            IApplicationActionRepository applicationActionRepository)
        {
            _applicationToitRepository = applicationToitRepository;
            _mapper = mapper;
            _priorityService = priorityRepository ?? throw new ArgumentNullException(nameof(priorityRepository));
            _typeService = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
            _stateService = stateRepository ?? throw new ArgumentNullException(nameof(stateRepository));
            _applicationActionRepository = applicationActionRepository ?? throw new ArgumentNullException(nameof(applicationActionRepository));
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetByDeptIdAsync(int deptId)
        {
            var apps = await _applicationToitRepository.GetAppByDepartamentIdAsync(deptId);
            if (apps.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(apps.Errors, apps.ErrorDescription, null, apps.AspNetException);

            if (!apps.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, null, null, "Запрос выполнен. Данных нет");

            var select = apps.Data.Where(x => x.OnDelete == false).ToList();
            if (!select.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NotActiveApplication }, null, null, $"Все заявки этого департамента стоят на удалении || DeptId < {deptId} >");

            var response = _mapper.Map<IEnumerable<ApplicationToItModel>>(select);
            return ManagementITActionResult<IEnumerable<ApplicationToItModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetByTypeIdAsync(int id, string iniciator)
        {
            var apps = await _applicationToitRepository.GetAppByApplicationTypeIdAsync(id);
            if (apps.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(apps.Errors, apps.ErrorDescription, null, apps.AspNetException);

            if (!apps.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, null, null, "Запрос выполнен. Данных нет");

            var select = apps.Data.Where(x => x.OnDelete == false).ToList();
            if (!select.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NotActiveApplication }, null, null, $"Все заявки этого типа стоят на удалении || ID типа <{id}>");

            var response = _mapper.Map<IEnumerable<ApplicationToItModel>>(select);
            return ManagementITActionResult<IEnumerable<ApplicationToItModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetAllAsync()
        {
            var apps = await _applicationToitRepository.GetAllApplicationToItAsync();
            if (apps.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(apps.Errors, apps.ErrorDescription, null, apps.AspNetException);

            if (!apps.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, null, null, $"Получение списка всех заявок || Модель: < {typeof(ApplicationToIt)} > || Список заявок пуст");

            var select = apps.Data.Where(x => x.OnDelete == false).ToList();
            if (!select.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NotActiveApplication }, null, null,
                    $"Запрос на получение всех заявок || Модель: < {typeof(ApplicationToIt)} > || Все заявки находятся в архиве");

            var response = _mapper.Map<IEnumerable<ApplicationToItModel>>(select);
            return ManagementITActionResult<IEnumerable<ApplicationToItModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationToItModel>>> GetAllForOnDeleteAsync()
        {
            var apps = await _applicationToitRepository.GetToItForOnDeleteAsync();
            if (apps.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(apps.Errors, apps.ErrorDescription, null, apps.AspNetException);

            if (!apps.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationToItModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, null, null, $"Получение списка заявок из архива || Модель: < {typeof(ApplicationToIt)} > || Архив пуст");

            var response = _mapper.Map<IEnumerable<ApplicationToItModel>>(apps.Data);
            return ManagementITActionResult<IEnumerable<ApplicationToItModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<ApplicationToItModel>> GetByIdAsync(int id)
        {
            var result = await _applicationToitRepository.GetApplicationToItByIdAsync(id);
            if (result.AspNetException != null)
                return ManagementITActionResult<ApplicationToItModel>
                    .Fail(result.Errors, result.ErrorDescription, null, result.AspNetException);

            if (result.Data == null)
                return ManagementITActionResult<ApplicationToItModel>
                    .Fail(new[] { TypeOfErrors.NotFound }, null, null, $"Получение заявки по её идентификатору || Модель: < {typeof(ApplicationToIt)} > || ID: < {id} >  || Заявка не найдена");

            if (result.Data.OnDelete)
                return ManagementITActionResult<ApplicationToItModel>
                    .Fail(new[] { TypeOfErrors.OnDelete }, null, null, $"Получение заявки по её идентификатору || Модель: < {typeof(ApplicationToIt)} > || ID: < {id} >  || Заявка находится в архиве, для получения информации, перейдите в архив");

            var response = _mapper.Map<ApplicationToItModel>(result.Data);
            return ManagementITActionResult<ApplicationToItModel>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult> AddAsync(ApplicationToItModel model, string iniciator)
        {
            var entity = _mapper.Map<ApplicationToIt>(model);
            ManagementITActionResult<ApplicationPriority> priority = null;
            ManagementITActionResult<ApplicationState> state = null;

            priority = model.PriorityId != 0
                ? await _priorityService.GetEntityByIdAsync(model.PriorityId)
                : await _priorityService.GetDefaultPriority();

            state = model.StateId != 0
                ? await _stateService.GetEntityByIdAsync(model.StateId)
                : await _stateService.GetStateOrDefault();

            var type = await _typeService.GetEntityByIdAsync(model.TypeId);

            if (model.DepartamentId != 0) entity.DepartamentId = model.DepartamentId;
            else
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistDepartament }, null,
                    $"Ошибка при добавлении заявки || Идентификатор отделения не может быть равен нолю || ID <{model.DepartamentId}>");

            if (priority.AspNetException != null) return priority;
            else if (priority.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotexistPriority }, null,
                    $"Ошибка при добавлении заявки || Не найден приоритет по ID <{model.PriorityId}> || Или не найден дефолтный приоритет");
            else entity.Priority = priority.Data;

            if (model.RoomId != 0) entity.RoomId = model.RoomId;
            else
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistRoom }, null,
                    $"Ошибка при добавлении заявки || Id комнаты не может быть равен нолю || ID <{model.RoomId}>");

            if (state.AspNetException != null) return state;
            else if (state.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistState }, null,
                    $"Ошибка при добавлении заявки || Не найдено состояние по ID <{model.StateId}> || Или не найдено состояние по умолчанию");
            else entity.State = state.Data;

            if (type.AspNetException != null) return type;
            else if (type.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistType }, null,
                    $"Ошибка при добавлении заявки || Не найден тип по ID <{model.TypeId}>");
            else entity.Type = type.Data;

            var result = await _applicationToitRepository.AddEntityAsync(entity);

            if (!result.Success) return result;

            //if (string.IsNullOrEmpty(iniciator)) return ManagementITActionResult.IsSuccess();

            var action = new ApplicationAction(entity, model.IniciatorFullName, ActionType.Creation, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);

            return ManagementITActionResult.IsSuccess();
        }

        public virtual async Task<ManagementITActionResult> UpdateAsync(ApplicationToItModel model, string iniciator)
        {
            var entity = await _applicationToitRepository.GetApplicationToItByIdAsync(model.Id);
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound },
                    $"Произошла ошибка при изменении данных заявки || заявка не найдена || ID < {model.Id} >");

            var type = await _typeService.GetEntityByIdAsync(model.TypeId);

            if (model.DepartamentId != 0) entity.Data.DepartamentId = model.DepartamentId;
            else
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistDepartament }, null,
                    $"Ошибка при изменении заявки || Идентификатор отделения не может быть равен нолю || ID < {model.DepartamentId} >");

            if (model.RoomId != 0) entity.Data.RoomId = model.RoomId;
            else
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistRoom }, null,
                    $"Ошибка при изменении заявки || Id комнаты не может быть равен нолю || ID <{model.RoomId}>");

            if (type.AspNetException != null) return type;
            else if (type.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistType }, null,
                    $"Ошибка при изменении заявки || Не найден тип по ID <{model.TypeId}>");
            else entity.Data.Type = type.Data;

            entity.Data.Name = model.Name;
            //entity.Data.Contact = model.Contact;
            entity.Data.Content = model.Content;
            entity.Data.Note = model.Note;

            entity.Data.DepartmentName = model.DepartmentName;
            entity.Data.RoomName = model.RoomName;


            var result = await _applicationToitRepository.UpdateEntityAsync(entity.Data);
            if (!result.Success) return result;

            //if (string.IsNullOrEmpty(iniciator)) return ManagementITActionResult.IsSuccess();

            var action = new ApplicationAction(entity.Data, model.IniciatorFullName, ActionType.Change, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);

            return ManagementITActionResult.IsSuccess();
        }

        public virtual async Task<ManagementITActionResult> UpdateStateAsync(EditToItStateModel model, string iniciator)
        {
            var entity = await _applicationToitRepository.GetApplicationToItByIdAsync(model.AppId);
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                    $"Произошла ошибка при изменении состояния заявки || заявка не найдена || ID < {model.AppId} >");

            var state = await _stateService.GetEntityByIdAsync(model.StateId);

            if (state.AspNetException != null) return state;
            else if (state.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistState }, null,
                    $"Ошибка при изменении состояния заявки || Не найдено состояние || ID <{model.StateId}>");
            else entity.Data.State = state.Data;

            var result = await _applicationToitRepository.UpdateEntityAsync(entity.Data);
            if (!result.Success) return result;

            //if (string.IsNullOrEmpty(iniciator)) return ManagementITActionResult.IsSuccess();

            var action = new ApplicationAction(entity.Data, model.IniciatorFullName, ActionType.StateChange, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);

            return ManagementITActionResult.IsSuccess();
        }

        public async Task<ManagementITActionResult> UpdatePriorityAsync(EditPriorityModel model, string iniciator)
        {
            var entity = await _applicationToitRepository.GetApplicationToItByIdAsync(model.AppId);
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                    $"Произошла ошибка при изменении приоритета заявки || Заявка не найдена || ID < {model.AppId} >");

            var priority = await _priorityService.GetEntityByIdAsync(model.PriorityId);

            if (priority.AspNetException != null) return priority;
            else if (priority.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistState }, null,
                    $"Ошибка при изменении заявки || Не найден приоритет по ID <{model.PriorityId}>");
            else entity.Data.Priority = priority.Data;

            var result = await _applicationToitRepository.UpdateEntityAsync(entity.Data);
            if (!result.Success) return result;

            //if (string.IsNullOrEmpty(iniciator)) return ManagementITActionResult.IsSuccess();

            var action = new ApplicationAction(entity.Data, model.IniciatorFullName, ActionType.ChangePriority, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);

            return ManagementITActionResult.IsSuccess();
        }

        public virtual bool ExistEntityByName(string name, int? Tid = null) => _applicationToitRepository.ExistEntityByName(name, Tid);

        public virtual async Task<ManagementITActionResult> OnDeleteAsync(OnDeleteApplicationModel model, string iniciator)
        {
            var entity = await _applicationToitRepository.GetApplicationToItByIdAsync(model.Id);
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound },
                    $"Не удалось поставить заявку на удаление || ID < {model.Id} > || Заявка не найдена");

            entity.Data.OnDelete = true;
            var result = await _applicationToitRepository.UpdateEntityAsync(entity.Data);
            if (!result.Success) return result;

            //if (string.IsNullOrEmpty(iniciator)) return ManagementITActionResult.IsSuccess();

            var action = new ApplicationAction(entity.Data, model.IniciatorFullName, ActionType.AddingArchive, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);

            return ManagementITActionResult.IsSuccess();
        }

        public virtual async Task<ManagementITActionResult> DeleteAsync(DeleteApplicationModel model, string iniciator)
        {
            var entity = await _applicationToitRepository.GetApplicationToItByIdAsync(model.Id);
            if (entity.AspNetException != null)
                return ManagementITActionResult.Fail(entity.Errors, entity.ErrorDescription, entity.AspNetException);

            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, entity.ErrorDescription, $"Заявка не найдена || ID < {model.Id} >");

            if (!entity.Data.OnDelete)
                return
                    ManagementITActionResult.Fail(new[] { TypeOfErrors.ErrorFlagForDeletion }, "Вы не можете удалить эту заявку",
                    $"Попытка удалить рабочую заявку || Для удаления необходимо установить флаг OnDelete в true || ID заявки < {model.Id} >");

            var result = await _applicationToitRepository.DeleteEntityAsync(entity.Data);
            if (!result.Success) return result;

            var action = new ApplicationAction(entity.Data, model.IniciatorFullName, ActionType.Deletion, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);

            return ManagementITActionResult.IsSuccess();
        }

        public async Task<ManagementITActionResult> DeleteRangeToItByOnDeleteAsync()
        {
            var entities = await _applicationToitRepository.GetToItForOnDeleteAsync();
            if (entities.AspNetException != null)
                return ManagementITActionResult.Fail(entities.Errors, entities.ErrorDescription, entities.AspNetException);

            if (!entities.Data.Any())
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NoContent }, "Нет заявок на удалении", $"В базе нет ниодной заявки, стоящей на удалении");

            return await _applicationToitRepository.DeleteRangeAsync(entities.Data);
        }

        public async Task<ManagementITActionResult<CreateOrEditToItModel>> GetCreateToItAsync()
        {

            var priority = await _priorityService.GetAllEntitiesAsync();
            var type = await _typeService.GetAllEntitiesAsync();
            var state = await _stateService.GetAllEntitiesAsync();

            if (priority.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(priority.Errors, priority.ErrorDescription, null, priority.AspNetException);
            else if (!priority.Data.Any())
                return ManagementITActionResult<CreateOrEditToItModel>.Fail
                    (new[] { TypeOfErrors.NotexistPriority }, priority.ErrorDescription, null, "Нельзя создать заявку || Нет ниодного приоритета");

            if (type.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(type.Errors, type.ErrorDescription, null, type.AspNetException);
            else if (!type.Data.Any())
                return ManagementITActionResult<CreateOrEditToItModel>.Fail
                    (new[] { TypeOfErrors.NotExistType }, type.ErrorDescription, null, "Нельзя создать заявку || Нет ниодного типа");

            if (state.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(state.Errors, state.ErrorDescription, null, state.AspNetException);
            else if (!state.Data.Any())
                return ManagementITActionResult<CreateOrEditToItModel>.Fail
                    (new[] { TypeOfErrors.NotExistState }, state.ErrorDescription, null, "Нельзя создать заявку || Нет ниодного состояния");

            var priorityModel = _mapper.Map<List<ApplicationPriorityModel>>(priority.Data);
            var typeModel = _mapper.Map<List<ApplicationTypeModel>>(type.Data);
            var stateModel = _mapper.Map<List<ApplicationStateModel>>(state.Data);

            var response = new CreateOrEditToItModel(stateModel, priorityModel, typeModel);

            return ManagementITActionResult<CreateOrEditToItModel>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult<CreateOrEditToItModel>> GetUpdateToItAsync(int appId)
        {
            var toIt = await _applicationToitRepository.GetApplicationToItByIdAsync(appId);

            if (toIt.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(toIt.Errors, toIt.ErrorDescription, null,
                    toIt.AspNetException);
            if (toIt.Data == null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(new[] { TypeOfErrors.NotFound }, "Произошла внутренняя ошибка", null,
                    $"ID < {appId} > Возникла ошибка. Заявка не найдена");

            if (toIt.Data.OnDelete)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(new[] { TypeOfErrors.OnDelete }, "Вы не можете изменять эту заявку", null,
                    $"ID < {appId} > Нельзя изменять заявку поставленную на удаление");

            var priority = await _priorityService.GetAllEntitiesAsync();
            var type = await _typeService.GetAllEntitiesAsync();
            var state = await _stateService.GetAllEntitiesAsync();

            var priorityModel = _mapper.Map<List<ApplicationPriorityModel>>(priority.Data);
            var typeModel = _mapper.Map<List<ApplicationTypeModel>>(type.Data);
            var stateModel = _mapper.Map<List<ApplicationStateModel>>(state.Data);
            var toItModel = _mapper.Map<ApplicationToItModel>(toIt.Data);

            var response = new CreateOrEditToItModel(stateModel, priorityModel, typeModel, toItModel);

            return ManagementITActionResult<CreateOrEditToItModel>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult> ExistDependencyEntity(int roomId = 0, int departmentId = 0, int employeeId = 0) =>
            await _applicationToitRepository.ExistDependencyEntity(roomId, departmentId, employeeId);

        public async Task<ManagementITActionResult> UpdateEmployeeNameAsync(EditEmployeeFullNameModel model)
        {
            var entitties = await _applicationToitRepository.GetAppByEmployeeIdAsync(model.EmployeeId);
            if (entitties.AspNetException == null)
            {
                if (entitties.Data.Any())
                {
                    foreach (var application in entitties.Data)
                    {
                        application.EmployeeFullName = model.EmployeeFullName;
                    }
                    await _applicationToitRepository.UpdateRangeEntityAsync(entitties.Data);
                }
            }

            var appOrIniciator = await _applicationToitRepository.GetAppByIniciatorIdAsync(model.IniciatorId);
            if (appOrIniciator.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(appOrIniciator.Errors, appOrIniciator.ErrorDescription, null,
                    appOrIniciator.AspNetException);
            if (!appOrIniciator.Data.Any())
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(new[] { TypeOfErrors.NoContent }, "Произошла внутренняя ошибка", null,
                    $"Поиск заявок по ID инициатора создания || Заявки не найдены || iniciatorId: < {model.IniciatorId} > || Нет заявок созданных этим сотрудником");

            foreach (var application in appOrIniciator.Data)
            {
                if (application.IniciatorId == model.IniciatorId)
                {
                    application.IniciatorFullName = model.IniciatorFullName;
                    application.Contact = model.Contact;
                }
            }
            return await _applicationToitRepository.UpdateRangeEntityAsync(entitties.Data);
        }

        public async Task<ManagementITActionResult> UpdateRoomNameAsync(int roomId, string roomName)
        {
            var entitties = await _applicationToitRepository.GetAppByRoomIdAsync(roomId);
            if (entitties.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(entitties.Errors, entitties.ErrorDescription, null,
                    entitties.AspNetException);
            if (!entitties.Data.Any())
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(new[] { TypeOfErrors.NoContent }, "Произошла внутренняя ошибка", null,
                    $"Поиск заявок по ID помещения || Заявки не найдены || roomId: < {roomId} >");

            foreach (var application in entitties.Data)
            {
                application.RoomName = roomName;
            }
            return await _applicationToitRepository.UpdateRangeEntityAsync(entitties.Data);
        }

        public async Task<ManagementITActionResult> UpdateDepartmentNameAsync(int deptId, string departmentName)
        {
            var entitties = await _applicationToitRepository.GetAppByDepartamentIdAsync(deptId);
            if (entitties.AspNetException != null)
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(entitties.Errors, entitties.ErrorDescription, null,
                    entitties.AspNetException);
            if (!entitties.Data.Any())
                return ManagementITActionResult<CreateOrEditToItModel>.Fail(new[] { TypeOfErrors.NoContent }, "Произошла внутренняя ошибка", null,
                    $"Поиск заявок по ID отделения || Заявки не найдены || departmentID: < {deptId} >");

            foreach (var application in entitties.Data)
            {
                application.DepartmentName = departmentName;
            }
            return await _applicationToitRepository.UpdateRangeEntityAsync(entitties.Data);
        }

        public async Task<ManagementITActionResult> UpdateEmployeeAsync(EditEmployeeModel model, string iniciator)
        {
            var application = await _applicationToitRepository.GetApplicationToItByIdAsync(model.AppId);

            if (application.AspNetException != null) return application;
            else if (application.Data == null) return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                 $"Запрос на изменение сотрудника у заявки || Модель: < {typeof(ApplicationToIt)} > || ID заявки: < {model.AppId} > || Заявка не найдена");
            if (application.Data.State.Id == 1)
            {
                var state = await _stateService.GetEntityByIdAsync(2); // Под Id = 2 всегда находится состояние (Выполняется)
                if (state.AspNetException != null) return state;
                else if (state.Data == null) return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotExistState }, null,
                     $"Назначение исполнителя заявки || Произошла ошибка || Не найдено состояние 'Выполняется' || ID состояния < 2 > || ID заявки < {model.AppId} > || Возможно первоначальные данные не прошли инициализацию");
                application.Data.State = state.Data;
            }

            application.Data.EmployeeId = model.EmployeeId;
            application.Data.EmployeeFullName = model.EmployeeFullName;

            var result = await _applicationToitRepository.UpdateEntityAsync(application.Data);
            if (!result.Success) return result;

            var action = new ApplicationAction(application.Data, model.IniciatorFullName, ActionType.ChangeEmployee, model.IniciatorId);
            await _applicationActionRepository.AddEntityAsync(action);
            return ManagementITActionResult.IsSuccess();
        }

        public async Task<ManagementITActionResult> SetIniciatorAsync(SetIniciatorOrApplicationModel model)
        {
            var application = await _applicationToitRepository.GetApplicationToItByIdAsync(model.AppId);
            if (application.AspNetException != null) return application;
            else if (application.Data == null) return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                 $"Запись инициатора в данные заявки || Модель: < {typeof(ApplicationToIt)} > || ID: < {model.AppId} > || Заявка не найдена");

            application.Data.IniciatorId = model.IniciatorId;
            application.Data.IniciatorFullName = model.IniciatorFullName;
            application.Data.Contact = model.Contact;

            var result = await _applicationToitRepository.UpdateEntityAsync(application.Data);
            if (!result.Success) return result;

            var action = await _applicationActionRepository.GetActionByToItIdAndActionTypeAsync(model.AppId, ActionType.Creation);
            if (action.AspNetException == null)
            {
                if (action.Data != null)
                {
                    action.Data.IniciatorFullName = model.IniciatorFullName;
                    action.Data.IniciatorId = model.IniciatorId;
                    await _applicationActionRepository.UpdateEntityAsync(action.Data);
                }
            }
            return ManagementITActionResult.IsSuccess();
        }
    }
}
