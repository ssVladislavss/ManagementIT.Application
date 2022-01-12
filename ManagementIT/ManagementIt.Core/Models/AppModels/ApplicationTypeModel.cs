using ManagementIt.Core.Domain;

namespace ManagementIt.Core.Models.AppModels
{
    public class ApplicationTypeModel : BaseEntity
    {
        public ApplicationTypeModel(){}
        public ApplicationTypeModel(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }

        public static ApplicationTypeModel GetAppTypeModel(string name, int id = 0) =>
            new ApplicationTypeModel(name, id);
    }
}
