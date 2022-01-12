using ManagementIt.Core.Domain;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ManagementIt.Core.Models.AppModels
{
    public class ApplicationStateModel : BaseEntity
    {
        public string State { get; set; }
        public string BGColor { get; set; }
        public bool IsDefault { get; set; }

        public ApplicationStateModel(){}

        public ApplicationStateModel(string name, string bgcolor, bool isDefault = false, int id = 0)
        {
            Id = id;
            Name = name;
            BGColor = bgcolor;
            IsDefault = isDefault;
        }

        public static ApplicationStateModel GetState(string name, string bgcolor, bool isDefault = false,int id = 0) =>
            new ApplicationStateModel(name, bgcolor, isDefault, id);
    }
}
