namespace VideoChief.Media.Models
{
    public class MediaFile(string path)
    {
        public string Path { get; } = path;
        public string Extension
        {
            get
            {
                var fileInfo = new FileInfo(Path);
                return fileInfo.Extension;
            }
        }
    }
}
