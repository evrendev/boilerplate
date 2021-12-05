namespace EvrenDev.Application.DTOS.Shared
{
    public class FileDto
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class FileFunctions {
        internal static FileDto Get(string folder, 
            string fileName) {
            return new FileDto
            {
                Name = fileName,
                Path = $"/uploads/files/{folder}/{fileName}"
            };
        }
    }  
}