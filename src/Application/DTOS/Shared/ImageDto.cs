namespace EvrenDev.Application.DTOS.Shared
{
    public class ImageDto
    {
        public string FileName { get; set; }
        public string Thumbnail { get; set; }
        public string Original { get; set; }
    }

    public class ImageFunctions {
        internal static ImageDto Get(string folder, 
            string fileName) {
            return new ImageDto
            {
                FileName = fileName,
                Thumbnail = $"/uploads/images/{folder}/thumbs/{fileName}",
                Original = $"/uploads/images/{folder}/{fileName}"
            };
        }
    }  
}