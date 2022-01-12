using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Models.AppModels
{
    public class SetIniciatorOrApplicationModel
    {
        public int AppId { get; set; }
        public int IniciatorId { get; set; }
        public string IniciatorFullName { get; set; }
        public string Contact { get; set; }
    }
}
