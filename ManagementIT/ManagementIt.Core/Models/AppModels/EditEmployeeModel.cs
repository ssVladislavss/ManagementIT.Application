using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Models.AppModels
{
    public class EditEmployeeModel
    {
        public int AppId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string Contact { get; set; }

        public int IniciatorId { get; set; }
        public string IniciatorFullName { get; set; }

        public EditEmployeeModel() { }
        public EditEmployeeModel(int appId, int employeeId, string employeeFullName, string contact)
        {
            AppId = appId;
            EmployeeId = employeeId;
            EmployeeFullName = employeeFullName;
            Contact = contact;
        }
    }
}
