using System.Collections.Generic;

namespace EvrenDev.Application.Constants
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

        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }

        public static class Department
        {
            public const string View = "Permissions.Settings.Department.View";
            public const string Create = "Permissions.Settings.Department.Create";
            public const string Edit = "Permissions.Settings.Department.Edit";
            public const string Delete = "Permissions.Settings.Department.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Settings.Users.View";
            public const string Create = "Permissions.Settings.Users.Create";
            public const string Edit = "Permissions.Settings.Users.Edit";
            public const string Delete = "Permissions.Settings.Users.Delete";
        }

        public static class Content
        {
            public const string View = "Permissions.Content.View";
            public const string Create = "Permissions.Content.Create";
            public const string Edit = "Permissions.Content.Edit";
            public const string Delete = "Permissions.Content.Delete";
        }
    }
}