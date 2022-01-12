using ApplicationContracts.ViewModels.Application.ActionModels;
using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using ApplicationContracts.ViewModels.Application.Priority;
using ApplicationContracts.ViewModels.Application.StateModels;
using ApplicationContracts.ViewModels.Application.TypeModels;
using AutoMapper;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.Models.AppModels;

namespace ManagementIt.WebHost.AutoMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region ApplicationAction

            CreateMap<ApplicationAction, ApplicationActionModel>()
                .ForMember(x => x.Content, dest => dest.MapFrom(src => src.Content))
                .ForMember(x => x.AppId, dest => dest.MapFrom(src => src.AppId))
                .ForMember(x => x.EventDateAndTime, dest => dest.MapFrom(src => src.EventDateAndTime))
                .ForMember(x => x.DeptId, dest => dest.MapFrom(src => src.DeptId))
                .ForMember(x => x.IniciatorFullName, dest => dest.MapFrom(src => src.IniciatorFullName))
                .ForMember(x => x.Action, dest => dest.MapFrom(src => ApplicationAction.ActionTypeDescription(src.Action)))
                .ForMember(x => x.IniciatorId, dest => dest.MapFrom(src => src.IniciatorId))
                .ForMember(x => x.StateName, dest => dest.MapFrom(src => src.StateName));
            CreateMap<ApplicationActionModel, ApplicationAction>()
                .ForMember(x => x.Content, dest => dest.MapFrom(src => src.Content))
                .ForMember(x => x.AppId, dest => dest.MapFrom(src => src.AppId))
                .ForMember(x => x.EventDateAndTime, dest => dest.MapFrom(src => src.EventDateAndTime))
                .ForMember(x => x.DeptId, dest => dest.MapFrom(src => src.DeptId))
                .ForMember(x => x.IniciatorFullName, dest => dest.MapFrom(src => src.IniciatorFullName))
                .ForMember(x => x.StateName, dest => dest.MapFrom(src => src.StateName));

            #endregion

            #region ApplicationPriority
            CreateMap<ApplicationPriority, ApplicationPriorityModel>()
                .ForMember("Id", dest => dest.MapFrom(x => x.Id))
                .ForMember("IsDefault", dest => dest.MapFrom(x => x.IsDefault))
                .ForMember("Name", dest => dest.MapFrom(x => x.Name));
            CreateMap<ApplicationPriorityModel, ApplicationPriority>()
                .ForMember("Id", dest => dest.MapFrom(x => x.Id))
                .ForMember("IsDefault", dest => dest.MapFrom(x => x.IsDefault))
                .ForMember("Name", dest => dest.MapFrom(x => x.Name));
            #endregion

            #region ApplicationType

            CreateMap<ApplicationType, ApplicationTypeModel>()
                .ForMember("Id", dest => dest.MapFrom(x => x.Id))
                .ForMember("Name", dest => dest.MapFrom(x => x.Name));
            CreateMap<ApplicationTypeModel, ApplicationType>()
                .ForMember("Id", dest => dest.MapFrom(x => x.Id))
                .ForMember("Name", dest => dest.MapFrom(x => x.Name));

            #endregion

            #region ApplicationState

            CreateMap<ApplicationState, ApplicationStateModel>()
                .ForMember("Id", dest => dest.MapFrom(src => src.Id))
                .ForMember("Name", dest => dest.MapFrom(src => src.Name))
                .ForMember("State", dest => dest.MapFrom(src => src.State))
                .ForMember("BGColor", dest => dest.MapFrom(src => src.BGColor));
            CreateMap<ApplicationStateModel, ApplicationState>()
                .ForMember("Id", dest => dest.MapFrom(src => src.Id))
                .ForMember("Name", dest => dest.MapFrom(src => src.Name))
                .ForMember("State", dest => dest.MapFrom(src => src.State))
                .ForMember("BGColor", dest => dest.MapFrom(src => src.BGColor));

            #endregion

            #region ApplicationToIt

            CreateMap<ApplicationToIt, ApplicationToItModel>()
                .ForMember("Priority", dest => dest.MapFrom(src => src.Priority))
                .ForMember("Type", dest => dest.MapFrom(src => src.Type))
                .ForMember("Content", dest => dest.MapFrom(src => src.Content))
                .ForMember("Note", dest => dest.MapFrom(src => src.Note))
                .ForMember("DepartamentId", dest => dest.MapFrom(src => src.DepartamentId))
                .ForMember("RoomId", dest => dest.MapFrom(src => src.RoomId))
                .ForMember("EmployeeId", dest => dest.MapFrom(src => src.EmployeeId))
                .ForMember("Contact", dest => dest.MapFrom(src => src.Contact))
                .ForMember("State", dest => dest.MapFrom(src => src.State))
                .ForMember("Name", dest => dest.MapFrom(src => src.Name))
                .ForMember("PriorityId", dest => dest.MapFrom(src => src.Priority.Id))
                .ForMember("TypeId", dest => dest.MapFrom(src => src.Type.Id))
                .ForMember("OnDelete", dest => dest.MapFrom(src => src.OnDelete))
                .ForMember("StateId", dest => dest.MapFrom(src => src.State.Id))
                .ForMember(x => x.RoomName, dest => dest.MapFrom(src => src.RoomName))
                .ForMember(x => x.EmployeeFullName, dest => dest.MapFrom(src => src.EmployeeFullName))
                .ForMember(x => x.DepartmentName, dest => dest.MapFrom(src => src.DepartmentName))
                .ForMember(x => x.IniciatorFullName, dest => dest.MapFrom(src => src.IniciatorFullName))
                .ForMember(x => x.IniciatorId, dest => dest.MapFrom(src => src.IniciatorId))
                .ForMember("Id", dest => dest.MapFrom(src => src.Id));
            CreateMap<ApplicationToItModel, ApplicationToIt>()
                .ForMember("Priority", dest => dest.MapFrom(src => src.Priority))
                .ForMember("Type", dest => dest.MapFrom(src => src.Type))
                .ForMember("Content", dest => dest.MapFrom(src => src.Content))
                .ForMember("Note", dest => dest.MapFrom(src => src.Note))
                .ForMember("DepartamentId", dest => dest.MapFrom(src => src.DepartamentId))
                .ForMember("RoomId", dest => dest.MapFrom(src => src.RoomId))
                .ForMember("EmployeeId", dest => dest.MapFrom(src => src.EmployeeId))
                .ForMember("Contact", dest => dest.MapFrom(src => src.Contact))
                .ForMember("State", dest => dest.MapFrom(src => src.State))
                .ForMember("Name", dest => dest.MapFrom(src => src.Name))
                .ForMember("OnDelete", dest => dest.MapFrom(src => src.OnDelete))
                .ForMember(x => x.RoomName, dest => dest.MapFrom(src => src.RoomName))
                .ForMember(x => x.EmployeeFullName, dest => dest.MapFrom(src => src.EmployeeFullName))
                .ForMember(x => x.DepartmentName, dest => dest.MapFrom(src => src.DepartmentName))
                .ForMember(x => x.IniciatorFullName, dest => dest.MapFrom(src => src.IniciatorFullName))
                .ForMember(x => x.IniciatorId, dest => dest.MapFrom(src => src.IniciatorId))
                .ForMember("Id", dest => dest.MapFrom(src => src.Id));

            #endregion



            #region ApplicationToItModel in ApplicationToItViewModel

            CreateMap<ApplicationToItModel, ApplicationToItViewModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Content, x => x.MapFrom(src => src.Content))
                .ForMember(dest => dest.State, x => x.MapFrom(src => src.State))
                .ForMember(dest => dest.DepartamentId, x => x.MapFrom(src => src.DepartamentId))
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Note, x => x.MapFrom(src => src.Note))
                .ForMember(dest => dest.Priority, x => x.MapFrom(src => src.Priority))
                .ForMember(dest => dest.RoomId, x => x.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomName, x => x.MapFrom(src => src.RoomName))
                .ForMember(dest => dest.EmployeeFullName, x => x.MapFrom(src => src.EmployeeFullName))
                .ForMember(dest => dest.DepartamentName, x => x.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.OnDelete, x => x.MapFrom(src => src.OnDelete))
                .ForMember(dest => dest.Type, x => x.MapFrom(src => src.Type));

            #endregion

            #region CreateApplicationToITViewModel in ApplicationToItModel

            CreateMap<CreateApplicationToITViewModel, ApplicationToItModel>()
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Content, x => x.MapFrom(src => src.Content))
                .ForMember(dest => dest.DepartamentId, x => x.MapFrom(src => src.DepartamentId))
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Note, x => x.MapFrom(src => src.Note))
                .ForMember(dest => dest.RoomId, x => x.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomName, x => x.MapFrom(src => src.RoomName))
                .ForMember(dest => dest.EmployeeFullName, x => x.MapFrom(src => src.EmployeeFullName))
                .ForMember(dest => dest.PriorityId, x => x.MapFrom(src => src.ApplicationPriorityId))
                .ForMember(dest => dest.TypeId, x => x.MapFrom(src => src.ApplicationTypeId))
                .ForMember(dest => dest.StateId, x => x.MapFrom(src => src.StateId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.DepartmentName, x => x.MapFrom(src => src.DepartamentName));

            #endregion

            #region UpdateApplicationViewModel in ApplicationToItModel

            CreateMap<UpdateApplicationViewModel, ApplicationToItModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Content, x => x.MapFrom(src => src.Content))
                .ForMember(dest => dest.Note, x => x.MapFrom(src => src.Note))
                .ForMember(dest => dest.DepartmentName, x => x.MapFrom(src => src.DepartamentName))
                .ForMember(dest => dest.RoomName, x => x.MapFrom(src => src.RoomName))
                .ForMember(dest => dest.EmployeeFullName, x => x.MapFrom(src => src.EmployeeFullName))
                .ForMember(dest => dest.RoomId, x => x.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.DepartamentId, x => x.MapFrom(src => src.DepartamentId))
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.PriorityId, x => x.MapFrom(src => src.ApplicationPriorityId))
                .ForMember(dest => dest.StateId, x => x.MapFrom(src => src.StateId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.TypeId, x => x.MapFrom(src => src.ApplicationTypeId));

            #endregion

            #region ApplicationActionModel in ActionViewModel

            CreateMap<ApplicationActionModel, ActionViewModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.AppId, x => x.MapFrom(src => src.AppId))
                .ForMember(dest => dest.ContentApp, x => x.MapFrom(src => src.Content))
                .ForMember(dest => dest.DateOrTime, x => x.MapFrom(src => src.EventDateAndTime))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.Action, x => x.MapFrom(src => src.Action))
                .ForMember(dest => dest.UserNameIniciator, x => x.MapFrom(src => src.IniciatorFullName))
                .ForMember(dest => dest.DeptId, x => x.MapFrom(src => src.DeptId));

            #endregion

            #region ApplicationTypeModel in ApplicationTypeViewModel

            CreateMap<ApplicationTypeModel, ApplicationTypeViewModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name));

            #endregion

            #region ApplicationPriorityModel in ApplicationPriorityViewModel

            CreateMap<ApplicationPriorityModel, ApplicationPriorityViewModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsDefault, x => x.MapFrom(src => src.IsDefault))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name));

            #endregion

            #region ApplicationStateModel in ApplicationStateViewModel

            CreateMap<ApplicationStateModel, ApplicationStateViewModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.BGColor, x => x.MapFrom(src => src.BGColor));

            #endregion

            #region CreateOrEditToItModel in CreateApplicationToITViewModel

            CreateMap<CreateOrEditToItModel, CreateApplicationToITViewModel>()
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Content, x => x.MapFrom(src => src.Content))
                .ForMember(dest => dest.StateId, x => x.MapFrom(src => src.StateId))
                .ForMember(dest => dest.DepartamentId, x => x.MapFrom(src => src.DepartamentId))
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Note, x => x.MapFrom(src => src.Note))
                .ForMember(dest => dest.ApplicationPriorityId, x => x.MapFrom(src => src.ApplicationPriorityId))
                .ForMember(dest => dest.RoomId, x => x.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.ApplicationTypeId, x => x.MapFrom(src => src.ApplicationTypeId))
                .ForMember(dest => dest.SelectState, x => x.MapFrom(src => src.SelectState))
                .ForMember(dest => dest.SelectType, x => x.MapFrom(src => src.SelectType))
                .ForMember(dest => dest.Type, x => x.MapFrom(src => src.Type))
                .ForMember(dest => dest.Priority, x => x.MapFrom(src => src.Priority))
                .ForMember(dest => dest.State, x => x.MapFrom(src => src.State))
                .ForMember(dest => dest.SelectPriority, x => x.MapFrom(src => src.SelectPriority));

            #endregion

            #region CreateOrEditToItModel in UpdateApplicationViewModel

            CreateMap<CreateOrEditToItModel, UpdateApplicationViewModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Content, x => x.MapFrom(src => src.Content))
                .ForMember(dest => dest.Note, x => x.MapFrom(src => src.Note))
                .ForMember(dest => dest.StateId, x => x.MapFrom(src => src.StateId))
                .ForMember(dest => dest.DepartamentId, x => x.MapFrom(src => src.DepartamentId))
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ApplicationPriorityId, x => x.MapFrom(src => src.ApplicationPriorityId))
                .ForMember(dest => dest.RoomId, x => x.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.ApplicationTypeId, x => x.MapFrom(src => src.ApplicationTypeId))
                .ForMember(dest => dest.Type, x => x.MapFrom(src => src.Type))
                .ForMember(dest => dest.Priority, x => x.MapFrom(src => src.Priority))
                .ForMember(dest => dest.State, x => x.MapFrom(src => src.State))
                .ForMember(dest => dest.SelectState, x => x.MapFrom(src => src.SelectState))
                .ForMember(dest => dest.SelectType, x => x.MapFrom(src => src.SelectType))
                .ForMember(dest => dest.SelectPriority, x => x.MapFrom(src => src.SelectPriority));

            #endregion

            #region UpdateEmployeeFullNameRequest in EditEmployeeFullNameModel

            CreateMap<UpdateEmployeeFullNameRequest, EditEmployeeFullNameModel>()
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.EmployeeFullName, x => x.MapFrom(src => src.EmployeeFullName))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact));

            #endregion

            #region OnDeleteApplicationViewModel in OnDeleteApplicationModel

            CreateMap<OnDeleteApplicationViewModel, OnDeleteApplicationModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName));

            #endregion

            #region DeleteApplicationViewModel in DeleteApplicationModel

            CreateMap<DeleteApplicationViewModel, DeleteApplicationModel>()
                .ForMember(dest => dest.Id, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName));

            #endregion

            #region EditPriorityOrApplicationViewModel in EditPriorityModel

            CreateMap<EditPriorityOrApplicationViewModel, EditPriorityModel>()
                .ForMember(dest => dest.AppId, x => x.MapFrom(src => src.AppId))
                .ForMember(dest => dest.PriorityId, x => x.MapFrom(src => src.PriorityId))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName));

            #endregion

            #region EditToItStateViewModel in EditToItStateModel

            CreateMap<EditToItStateViewModel, EditToItStateModel>()
                .ForMember(dest => dest.AppId, x => x.MapFrom(src => src.AppId))
                .ForMember(dest => dest.StateId, x => x.MapFrom(src => src.StateId))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName));

            #endregion

            #region UpdateEmployeeOrApplicationRequest in EditEmployeeModel

            CreateMap<UpdateEmployeeOrApplicationRequest, EditEmployeeModel>()
                .ForMember(dest => dest.AppId, x => x.MapFrom(src => src.AppId))
                .ForMember(dest => dest.EmployeeId, x => x.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact))
                .ForMember(dest => dest.EmployeeFullName, x => x.MapFrom(src => src.EmployeeFullName))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName));

            #endregion

            #region SetIniciatorOrApplicationRequest in SetIniciatorOrApplicationModel

            CreateMap<SetIniciatorOrApplicationRequest, SetIniciatorOrApplicationModel>()
                .ForMember(dest => dest.AppId, x => x.MapFrom(src => src.AppId))
                .ForMember(dest => dest.IniciatorId, x => x.MapFrom(src => src.IniciatorId))
                .ForMember(dest => dest.IniciatorFullName, x => x.MapFrom(src => src.IniciatorFullName))
                .ForMember(dest => dest.Contact, x => x.MapFrom(src => src.Contact));

            #endregion
        }
    }
}
