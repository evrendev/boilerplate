using System.Collections.Generic;
using System.Linq;

namespace EvrenDev.Application.Enums.Language
{
    public class Languages
    {
        public static Languages Turkish { get; } = new Languages(value: 1, 
            label: "Türkçe", 
            url: "tr-TR", 
            favorite: true
        );

        public static Languages English { get; } = new Languages(value: 2, 
            label: "English", 
            url: "en-US",
            favorite: false
        );

        public int Value { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public bool Favorite { get; set; }

        public Languages(int value, 
            string label,
            string url,
            bool favorite)
        {
            Value = value;
            Label = label;
            Url = url;
            Favorite = favorite;
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