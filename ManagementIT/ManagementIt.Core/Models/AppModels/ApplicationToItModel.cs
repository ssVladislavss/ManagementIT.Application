using System;
using ManagementIt.Core.Domain;

namespace ManagementIt.Core.Models.AppModels
{
    public class ApplicationToItModel : BaseEntity
    {
        public int PriorityId { get; set; }
        public int TypeId { get; set; }
        public int DepartamentId { get; set; }
        public int RoomId { get; set; }
        public int EmployeeId { get; set; }
        public int StateId { get; set; }
        public ApplicationPriorityModel Priority { get; set; }
        public ApplicationTypeModel Type { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public string Contact { get; set; }
        public ApplicationStateModel State { get; set; }
        public bool OnDelete { get; set; }

        public string EmployeeFullName { get; set; }
        public string DepartmentName { get; set; }
        public string RoomName { get; set; }

        public string IniciatorFullName { get; set; }
        public int IniciatorId { get; set; }

        public ApplicationToItModel(){}
        public ApplicationToItModel(string name, string content, string note, string contact,
            ApplicationPriorityModel priority = null, ApplicationTypeModel type = null,
            ApplicationStateModel state = null, int id = 0, bool onDelete = false)

        {
            Id = id;
            Name = name;
            Content = content;
            Note = note;
            Contact = contact;
            Priority = priority;
            Type = type;
            State = state;
            OnDelete = onDelete;
            if (Priority != null) PriorityId = Priority.Id;
            if (Type != null) TypeId = Type.Id;
            RoomId = RoomId;
            DepartamentId = DepartamentId;
            EmployeeId = EmployeeId;
            if (State != null) StateId = State.Id;
        }

        public ApplicationToItModel(string name, string content, string note, string contact,
            int priorityId, int typeId, int deptId, string employeeFullName, string departmentName, string roomName,
            int roomId, int employeeId, int stateId, int id = 0, bool onDelete = false, string iniciatorFullName = null)
        {
            Id = id;
            Name = name;
            Content = content;
            Note = note;
            Contact = contact;
            PriorityId = priorityId;
            TypeId = typeId;
            DepartamentId = deptId;
            EmployeeId = employeeId;
            StateId = stateId;
            RoomId = roomId;
            OnDelete = onDelete;
            EmployeeFullName = employeeFullName;
            DepartmentName = departmentName;
            RoomName = roomName;
            IniciatorFullName = iniciatorFullName;
        }

        public static ApplicationToItModel GeApplicationToItModel(string name, string content, string note,string contact,
            ApplicationPriorityModel priority = null, ApplicationTypeModel type = null, ApplicationStateModel state = null, int id = 0, bool onDelete = false) =>
            new ApplicationToItModel(name, content, note, contact, priority, type, state, id, onDelete);
    }
}
