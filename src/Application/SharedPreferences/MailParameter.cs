namespace EvrenDev.Application.SharedPreferences
{
    public class MailParameter
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
        public string PlainTextContent { get; set; } = null;
    }
}