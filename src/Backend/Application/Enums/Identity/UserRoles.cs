using System.Collections.Generic;
using System.Linq;
using EvrenDev.Application.Constants;

namespace EvrenDev.Application.Enums.Identity
{
    public class UserRoles
    {
        public static UserRoles SuperUser { get; } = new UserRoles(name: RoleNames.SUPER_USER, 
            label: "Super Admin", 
            favorite: false
        );

        public static UserRoles Administrator { get; } = new UserRoles(name: RoleNames.ADMINISTRATOR, 
            label: "Administrator", 
            favorite: false
        );

        public static UserRoles Moderator { get; } = new UserRoles(name: RoleNames.MODERATOR, 
            label: "Moderator", 
            favorite: false
        );

        public static UserRoles BasicUser { get; } = new UserRoles(name: RoleNames.BASIC_USER, 
            label: "Kullanıcı",
            favorite: true
        );

        public string Label { get; set; }
        public string Name { get; set; }
        public bool Favorite { get; set; }

        public UserRoles(string name, 
            string label,
            bool favorite)
        {
            Name = name;
            Label = label;
            Favorite = favorite;
        }

        public static IEnumerable<UserRoles> List()
        {
            return new[] { SuperUser, Administrator, Moderator, BasicUser };
        }

        public static UserRoles FromRoleName(string roleName)
        {
            if(!List().Any(role => string.Equals(role.Name, roleName)))
            {
                return List().FirstOrDefault(role => role.Favorite);
            }

            return List().Single(role => role.Name == roleName);
        }
    }
}