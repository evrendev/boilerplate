using System.Collections.Generic;
using System.Linq;

namespace EvrenDev.Application.Enums.Language
{
    public class Languages
    {
        public static Languages Turkish { get; } = new Languages(1, "Türkçe", "tr");
        public static Languages English { get; } = new Languages(2, "English", "en");

        public int Value { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public Languages(int value, 
            string text,
            string url)
        {
            Value = value;
            Text = text;
            Url = url;
        }

        public static IEnumerable<Languages> List()
        {
            return new[] { Turkish, English };
        }

        public static Languages FromValue(int value)
        {
            return List().Single(language => language.Value == value);
        }

        public static Languages FromUrl(string url)
        {
            return List().Single(language => string.Equals(language.Url, url, System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}