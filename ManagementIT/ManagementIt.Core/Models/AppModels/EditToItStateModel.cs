using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Models.AppModels
{
    public class EditToItStateModel
    {
        [Required]
        public int AppId { get; protected set; }
        [Required]
        public int StateId { get; protected set; }

        public int IniciatorId { get; set; }
        public string IniciatorFullName { get; set; }

        public EditToItStateModel() { }
        public EditToItStateModel(int appId, int stateId)
        {
            AppId = appId;
            StateId = stateId;
        }

        public static EditToItStateModel GetEditToItStateModel(int appId, int stateId) => new EditToItStateModel(appId, stateId);
    }
}
