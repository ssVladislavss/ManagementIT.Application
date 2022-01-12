using ManagementIt.Core.Domain;
using ManagementIt.Core.Domain.ApplicationEntity;

namespace ManagementIt.Core.Models.AppModels
{
    public class ApplicationPriorityModel : BaseEntity
    {
        public bool IsDefault { get; set; }

        public ApplicationPriorityModel(){}
        public ApplicationPriorityModel(string name, int id = 0, bool isDefault = false)
        {
            Id = id;
            Name = name;
            IsDefault = isDefault;
        }

        public static ApplicationPriorityModel GetAppPriorityModel(string name, int id = 0) =>
            new ApplicationPriorityModel(name, id);
    }
}