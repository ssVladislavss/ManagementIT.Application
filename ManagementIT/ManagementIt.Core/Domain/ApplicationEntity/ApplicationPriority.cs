using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Domain.ApplicationEntity
{
    public class ApplicationPriority : BaseEntity
    {
        public bool IsDefault { get; set; }
    }
}
