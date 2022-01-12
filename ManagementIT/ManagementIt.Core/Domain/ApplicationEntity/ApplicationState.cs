using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Domain.ApplicationEntity
{
    //Состояние заявки
    public class ApplicationState : BaseEntity
    {
        public string State { get; set; }
        public string BGColor { get; set; }
        public bool IsDefault { get; set; }
    }
}
