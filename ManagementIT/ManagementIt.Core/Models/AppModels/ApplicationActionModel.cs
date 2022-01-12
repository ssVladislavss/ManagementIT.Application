using System;
using System.ComponentModel.DataAnnotations;
using ManagementIt.Core.Domain;

namespace ManagementIt.Core.Models.AppModels
{
    public class ApplicationActionModel : BaseEntity
    {
        public ApplicationActionModel(){}
        public ApplicationActionModel(ApplicationToItModel toit, string userName)
        {
            if (toit != null)
            {
                AppId = toit.Id;
                Content = toit.Content;
                if (toit.State != null) StateName = toit.State.Name;
                IniciatorFullName = userName;
                EventDateAndTime = DateTime.Now;
                DeptId = toit.DepartamentId;
            }
        }
        public ApplicationActionModel(int apptoitId, string userName)
        {
            AppId = apptoitId;
            EventDateAndTime = DateTime.Now;
            IniciatorFullName = userName;
        }

        public int AppId { get; set; }
        public string Content { get; set; }
        public string StateName { get; set; }
        public string IniciatorFullName { get; set; }
        public int DeptId { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public int IniciatorId { get; set; }
        public string Action { get; set; }

        public static ApplicationActionModel GetActionModel(ApplicationToItModel toit, string userName) =>
            new ApplicationActionModel(toit, userName);
    }
}
