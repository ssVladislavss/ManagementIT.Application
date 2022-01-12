using ManagementIt.Core.Domain.ApplicationEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementIt.Core.Domain
{
    public abstract class ApplicationBaseEntity : BaseEntity
    {
        public ApplicationPriority Priority { get; set; }
        public ApplicationType Type { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public int DepartamentId { get; set; }
        public int RoomId { get; set; }
        public int EmployeeId { get; set; }
        public string Contact { get; set; }
        public ApplicationState State { get; set; }
        public bool OnDelete { get; set; }
        
        public string EmployeeFullName { get; set; }
        public string DepartmentName { get; set; }
        public string RoomName { get; set; }

        public int IniciatorId { get; set; }
        public string IniciatorFullName { get; set; }
    }
}
