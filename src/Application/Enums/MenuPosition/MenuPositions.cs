using System.Collections.Generic;
using System.Linq;

namespace EvrenDev.Application.Enums.Language
{
    public class MenuPositions
    {
        public static MenuPositions Menu { get; } = new MenuPositions(1, "Menü");
        public static MenuPositions Footer { get; } = new MenuPositions(2, "Footer");

        public int Value { get; set; }
        public string Text { get; set; }

        public MenuPositions(int value, 
            string text)
        {
            Value = value;
            Text = text;
        }

        public static IEnumerable<MenuPositions> List()
        {
            return new[] { Menu, Footer };
        }

        public static MenuPositions FromValue(int value)
        {
            return List().Single(menu => menu.Value == value);
        }
    }
}