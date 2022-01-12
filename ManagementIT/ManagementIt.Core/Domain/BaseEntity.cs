using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Domain
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public virtual string Name { get; set; }
    }
}
