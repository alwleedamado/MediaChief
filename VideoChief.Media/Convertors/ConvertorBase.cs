namespace VideoChief.Media.Convertors
{
    public abstract class ConvertorBase(string input)
    {
        protected readonly string Input = input;
        public abstract Task Convert(string outputDir);
        
        protected string GetOutputDirectory(string outputDir, string extension)
        {
            if (string.IsNullOrEmpty(outputDir))
                outputDir = Path.Combine(Path.GetDirectoryName(Input)!, "OrbitOutput");
            if (!Path.Exists(outputDir)) Directory.CreateDirectory(outputDir);
            var fileInfo = new FileInfo(Input);
            var outputFile = $"{outputDir}/{fileInfo.Name.Split('.')[0]}.{extension}";
            return outputFile;
        }
    }
}
