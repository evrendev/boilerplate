using System;
using System.Collections.Generic;
using System.Linq;
using EvrenDev.Application.Constants;

namespace EvrenDev.Application.Enums.Identity
{
    public class UserRoles
    {
        public static UserRoles SuperUser { get; } = new UserRoles(val: RoleNames.SUPER_USER, 
            text: "Süper Yönetici", 
            level: 20
        );

        public static UserRoles Administrator { get; } = new UserRoles(val: RoleNames.ADMINISTRATOR, 
            text: "Yönetici", 
            level: 10
        );

        public static UserRoles Editor { get; } = new UserRoles(val: RoleNames.EDITOR, 
            text: "Editör", 
            level: 5
        );

        public static UserRoles BasicUser { get; } = new UserRoles(val: RoleNames.BASIC_USER, 
            text: "Kullanıcı", 
            level: 1
        );

        public string Text { get; set; }
        public string Value { get; set; }
        public int Level { get; set; }

        public UserRoles(string val, 
            string text,
            int level)
        {
            Value = val;
            Text = text;
            Level = level;
        }

        public static IEnumerable<UserRoles> List()
        {
            return new[] { SuperUser, Administrator, Editor, BasicUser};
        }

        public static UserRoles FromString(string roleString)
        {
            return List().Single(r => string.Equals(r.Text, roleString, StringComparison.OrdinalIgnoreCase));
        }

        public static UserRoles FromValue(string value)
        {
            return List().Single(r => r.Value == value);
        }

        public static IEnumerable<UserRoles> FromLevel(int level)
        {
            return List().Where(role => role.Level >= level).ToList();
        }
    }
}