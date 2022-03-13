using System.Collections.Generic;
using System.Linq;

namespace EvrenDev.Application.Enums.Language
{
    public class Languages
    {
        public static Languages Turkish { get; } = new Languages(id: 1, 
            label: "Türkçe", 
            url: "tr-TR", 
            favorite: false
        );

        public static Languages English { get; } = new Languages(id: 2, 
            label: "English", 
            url: "en-US",
            favorite: true
        );

        public int Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public bool Favorite { get; set; }

        public Languages(int id, 
            string label,
            string url,
            bool favorite)
        {
            Id = id;
            Label = label;
            Url = url;
            Favorite = favorite;
        }

        public static IEnumerable<Languages> List()
        {
            return new[] { Turkish, English };
        }

        public static Languages FromId(int id)
        {
            if(!List().Any(language => language.Id == id))
            {
                return List().FirstOrDefault(language => language.Favorite);
            }

            return List().FirstOrDefault(language => language.Id == id);
        }

        public static Languages FromUrl(string url)
        {
            if(!List().Any(l => string.Equals(l.Url, url)))
            {
                return List().FirstOrDefault(language => language.Favorite);
            }

            return List().FirstOrDefault(language => string.Equals(language.Url, url, System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}