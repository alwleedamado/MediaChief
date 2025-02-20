using FFMpegCore;

namespace VideoChief.Media.Convertors
{
    public class AudioConvertor : ConvertorBase
    {
        private readonly string _input;
        private readonly string _codec;
        private readonly int _bitrate;

        public AudioConvertor(string input, string codec, int bitrate) : base(input)
        {
            _input = input;
            _codec = codec;
            _bitrate = bitrate;
        }

        public override async Task Convert(string outputDir)
        {

            if (File.Exists(Input))
            {
                if (string.IsNullOrEmpty(outputDir))
                    outputDir = Path.Combine(Path.GetDirectoryName(Input)!, "OrbitOutput");
                if (!Path.Exists(outputDir)) Directory.CreateDirectory(outputDir);
                var fileInfo = new FileInfo(Input);
                var outputFile = $"{outputDir}/{fileInfo.Name.Split('.')[0]}.{_codec.ToLower()}";
                await FFMpegArguments
                    .FromFileInput(fileInfo)
                    .OutputToFile(outputFile, overwrite: true, delegate (FFMpegArgumentOptions options)
                    {
                        options.DisableChannel(FFMpegCore.Enums.Channel.Video);
                        options.WithAudioCodec(_codec);
                        options.WithAudioBitrate(_bitrate);
                    }).ProcessAsynchronously();
            }
        }
    }
}
