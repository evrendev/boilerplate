using System.Collections.Generic;
using System.Linq;

namespace EvrenDev.Application.Enums.Language
{
    public class MenuPositions
    {
        public static MenuPositions TopMenu { get; } = new MenuPositions(value:1, 
            label: "Top Menu"
        );

        public static MenuPositions Footer { get; } = new MenuPositions(value: 2, 
            label: "Footer Menu"
        );

        public int Value { get; set; }
        public string Label { get; set; }

        public MenuPositions(int value, 
            string label)
        {
            Value = value;
            Label = label;
        }

        public static IEnumerable<MenuPositions> List()
        {
            return new[] { TopMenu, Footer };
        }

        public static MenuPositions FromValue(int value)
        {
            return List().Single(menu => menu.Value == value);
        }
    }
}