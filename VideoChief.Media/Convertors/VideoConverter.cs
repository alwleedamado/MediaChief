using FFMpegCore;

namespace VideoChief.Media.Convertors
{
    public class VideoConverter(string input, string videoCodec, string audioCodec, int bitrate, double frameRate)
        : ConvertorBase(input)
    {
        private readonly string _videoCodec = GetFfVideoCodec(videoCodec);

        public override async Task Convert(string outputDir)
        {
            var container = _videoCodec.Equals("H264") ? "mpd4" : "mkv";

            var outputFile = GetOutputDirectory(outputDir, container);

            await FFMpegArguments.FromFileInput(Input)
                .OutputToFile(outputFile, overwrite: true, delegate (FFMpegArgumentOptions options)
                {
                    options.WithVideoCodec(_videoCodec);
                    options.WithVideoBitrate(bitrate);
                    options.WithFramerate(frameRate);
                    options.WithAudioCodec(audioCodec);
                }).ProcessAsynchronously();
        }
        
        private static string GetFfVideoCodec(string codec)
        {
            return codec switch
            {
                "H264" => "libx264",
                "H265" => "libx265",
                _ => throw new InvalidOperationException($"{codec} is not supported")
            };
        }
    }
}
