using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Models.AppModels
{
    public class DeleteApplicationModel
    {
        public int Id { get; set; }
        public int IniciatorId { get; set; }
        public string IniciatorFullName { get; set; }
    }
}
