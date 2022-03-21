using System.Linq;
using System.Web;
using EvrenDev.Application.Enums.Language;

namespace EvrenDev.Application.DTOS.AutoComplete
{
    public class AutoCompleteRequest
    {
        public string Table { get; set; }

        public string Column { get; set; }

        public string Query { get; set; }

        public int LanguageId { get; set; } = Languages.List().Single(Lang => Lang.Favorite).Id;

        public string DecodedQuery => HttpUtility.UrlDecode(Query);
    }
}
