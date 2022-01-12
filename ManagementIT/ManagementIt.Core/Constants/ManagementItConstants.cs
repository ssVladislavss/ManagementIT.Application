using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Constants
{
    public static class ManagementItConstants
    {
        public const string messageSuccess = "Запрос обработан";
        public const string messageError = "Произошла ошибка";
        public static string NotificationQueueNameIT = "NotificationQueueNameIT";

        public class ConstantPriority
        {
            public const string controller = "Admin/ApplicationPriorityController";
        }
        public class ConstantState
        {
            public const string controller = "Admin/ApplicationStateController";
        }
        public class ConstantApplication
        {
            public const string adminController = "Admin/ApplicationToITController";
            public const string controller = "ApplicationToITController";
        }
        public class ConstantType
        {
            public const string controller = "Admin/ApplicationTypeController";
        }
        public class ConstantDepartament
        {
            public const string controller = "Admin/DepartamentController";
        }
        public class ConstantEmployee
        {
            public const string controller = "Admin/EmployeeController";
            public const string serverAddress = "https://localhost:9001";
            public const string registerAddress = "Employee/register";
            public const string setRoleAddress = "Employee/add-user-role";
            public const string updateRoleAddress = "Employee/update-role";
            public const string resetPasswordAddress = "Employee/password-reset";
        }
        public class ConstantPosition
        {
            public const string controller = "Admin/PositionController";
        }
        public class ConstantSubdivision
        {
            public const string controller = "Admin/SubdivisionController";
        }
        public class ConstantRole
        {
            public const string controller = "Admin/RoleController";
            public const string serverAddress = "https://localhost:9001";
            public const string updateAddress = "Role/update";
            public const string createAddress = "Role/create";


            public const string addingError =
                "Роль была добавлена на сервер авторизации. Произошла ошибка добавления в базу Вашего сервера, сделайте повторный запрос на сервер авторизации для синхронизации списка ролей";
        }
        public class ConstantBuilding
        {
            public const string controller = "Admin/BuildingController";
        }
        public class ConstantRoom
        {
            public const string controller = "Admin/RoomController";
        }
        public class ConstantAction
        {
            public const string controller = "Admin/ApplicationActionController";
        }

        public class ConstantDbContext
        {
            public const string Context = "База данных";
        }
    }
}
