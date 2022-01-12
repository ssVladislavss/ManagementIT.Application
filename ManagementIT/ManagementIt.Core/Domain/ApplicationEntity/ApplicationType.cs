using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Domain.ApplicationEntity
{
    //Тип заявки
    public class ApplicationType : BaseEntity
    {
        [Display(Name = "Тип")]
        public override string Name { get; set; }
    }
}
