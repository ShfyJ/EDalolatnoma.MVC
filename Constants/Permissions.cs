using System.Collections.Generic;

namespace EDalolatnoma.MVC.Constants
{
public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static class Company
    {
        public const string View = "Доступ: Организации-> Просмотр";
        public const string Create = "Доступ: Организации-> Создание ";
        public const string Edit = "Доступ: Организации-> Редактирование";
        public const string Delete = "Доступ: Организации-> Удаление";
    }


        public static class ViewAll
        {
            public const string View = "Доступ: Видет все организации";
            
        }
       /* public static class UsersList
        {
            public const string View = "Доступ: Пользователи-> Просмотр";
            public const string Create = "Доступ: Пользователи-> Создание ";
            public const string Edit = "Доступ: Пользователи-> Редактирование";
            public const string Delete = "Доступ: Пользователи-> Удаление";
        } */
       /* public static class RolesList
        {
            public const string View = "Доступ: Роли-> Просмотр";
            public const string Create = "Доступ: Роли-> Создание ";
            public const string Edit = "Доступ: Роли-> Редактирование";
            public const string Delete = "Доступ: Роли-> Удаление";
            
        }*/
        public static class PermissionList
        {
            public const string View = "Доступ: Управление доступом ";
        }

        public static class Fields
        {
            public const string View = "Доступ: Месторождение(Площад)-> Просмотр";
            public const string Create = "Доступ: Месторождение(Площад)-> Создание ";
            public const string Edit = "Доступ: Месторождение(Площад)-> Редактирование";
            public const string Delete = "Доступ: Месторождение(Площад)-> Удаление";
        }
        public static class Wells
        {
            public const string View = "Доступ: Скважины-> Просмотр";
            public const string Create = "Доступ: Скважины-> Создание ";
            public const string Edit = "Доступ: Скважины-> Редактирование";
            public const string Delete = "Доступ: Скважины-> Удаление";
        }
        public static class KernoDb
        {
            public const string View = "Доступ: Кернохранилище-> Просмотр";
            public const string Create = "Доступ: Кернохранилище-> Создание ";
            public const string Edit = "Доступ: Кернохранилище-> Редактирование";
            public const string Delete = "Доступ: Кернохранилище-> Удаление";
        }
        public static class WellStatus
        {
            public const string View = "Доступ: Статус скважины-> Просмотр";
            public const string Create = "Доступ: Статус скважины-> Создание ";
            public const string Edit = "Доступ: Статус скважины-> Редактирование";
            public const string Delete = "Доступ: Статус скважины-> Удаление";
        }
    }
}