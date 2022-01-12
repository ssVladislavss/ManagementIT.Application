using System.Collections.Generic;

namespace ManagementIt.Core.Models.AppModels
{
    public class CreateOrEditToItModel
    {
        public CreateOrEditToItModel() { }
        public CreateOrEditToItModel(List<ApplicationStateModel> state,
                                     List<ApplicationPriorityModel> priority,
                                     List<ApplicationTypeModel> type)
        {
            SelectPriority = priority;
            SelectState = state;
            SelectType = type;
        }

        public CreateOrEditToItModel(List<ApplicationStateModel> state,
                                     List<ApplicationPriorityModel> priority,
                                     List<ApplicationTypeModel> type,
                                     ApplicationToItModel toIt)
        {
            Id = toIt.Id;
            Name = toIt.Name;
            Content = toIt.Content;
            Note = toIt.Note;
            Contact = toIt.Contact;
            ApplicationTypeId = toIt.TypeId;
            ApplicationPriorityId = toIt.PriorityId;
            DepartamentId = toIt.DepartamentId;
            RoomId = toIt.RoomId;
            EmployeeId = toIt.EmployeeId;
            StateId = toIt.StateId;
            EmployeeFullName = toIt.EmployeeFullName;
            DepartamentName = toIt.DepartmentName;
            RoomName = toIt.RoomName;

            Type = toIt.Type;
            Priority = toIt.Priority;
            State = toIt.State;

            SelectPriority = priority;
            SelectState = state;
            SelectType = type;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string Contact { get; set; }
        public ApplicationTypeModel Type { get; set; }
        public ApplicationPriorityModel Priority { get; set; }
        public ApplicationStateModel State { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ApplicationPriorityId { get; set; }
        public int DepartamentId { get; set; }
        public int RoomId { get; set; }
        public int EmployeeId { get; set; }
        public int StateId { get; set; }

        public string DepartamentName { get; set; }
        public string RoomName { get; set; }
        public string EmployeeFullName { get; set; }

        
        public List<ApplicationPriorityModel> SelectPriority { get; set; }
        public List<ApplicationTypeModel> SelectType { get; set; }
        public List<ApplicationStateModel> SelectState { get; set; }
    }
}
