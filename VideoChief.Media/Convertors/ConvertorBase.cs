namespace VideoChief.Media.Convertors
{
    public abstract class ConvertorBase(string input)
    {
        protected readonly string Input = input;
        public abstract Task Convert(string outputDir);
    }
}
