using Contracts.Enums;
using System;

namespace ManagementIt.Core.Domain.ApplicationEntity
{
    public class ApplicationAction : BaseEntity
    {
        public int AppId { get; set; }
        public string Content { get; set; }
        public string StateName { get; set; }
        public string IniciatorFullName { get; set; }
        public int IniciatorId { get; set; }
        public int DeptId { get; set; }
        public DateTime EventDateAndTime { get; set; }
        public ActionType Action { get; set; }

        public ApplicationAction(){}

        public ApplicationAction(ApplicationToIt toIt, string iniciatorFullName, ActionType action, int iniciatorId = 0)
        {
            if (toIt != null)
            {
                AppId = toIt.Id;
                Content = toIt.Content;
                if(toIt.State != null) StateName = toIt.State.Name;
                IniciatorFullName = iniciatorFullName;
                EventDateAndTime = DateTime.Now;
                Name = toIt.Name;
                DeptId = toIt.DepartamentId;
                Action = action;
                IniciatorId = iniciatorId;
            }
        }

        public static string ActionTypeDescription(ActionType action)
        {
            return action switch
            {
                ActionType.Creation => "Создание",
                ActionType.Change => "Изменение",
                ActionType.StateChange => "Изменение состояния",
                ActionType.Deletion => "Удаление",
                ActionType.ChangeType => "Изменение типа",
                ActionType.ChangePriority => "Изменение приоритета",
                ActionType.ChangeRoom => "Изменение комнаты",
                ActionType.ChangeDepartment => "Изменение отделения",
                ActionType.ChangeEmployee => "Изменение сотрудника",
                ActionType.AddingArchive => "Добавление в архив",
                _ => "Неизвестное действие",
            };
        }
    }
}
