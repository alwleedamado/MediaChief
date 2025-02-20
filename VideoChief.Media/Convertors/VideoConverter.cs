using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoChief.Media.Convertors
{
    public class VideoConverter : ConvertorBase
    {
        private readonly string _videoCodec;
        private readonly string _audioCodec;
        private readonly int _bitrate;
        private readonly double _frameRate;

        public VideoConverter(string input, string videoCodec, string audioCodec, int bitrate, double rameRate) : base(input)
        {
            _videoCodec = GetFfVideoCodec(videoCodec);
            _audioCodec = audioCodec;
            _bitrate = bitrate;
            _frameRate = rameRate;
        }

        public override async Task Convert(string outputDir)
        {
            if (string.IsNullOrEmpty(outputDir))
                outputDir = Path.Combine(Path.GetDirectoryName(Input)!, "OrbitOutput");
            if (!Path.Exists(outputDir)) Directory.CreateDirectory(outputDir);
            var fileInfo = new FileInfo(Input);
            var container = _videoCodec.Equals("H264") ? "mpd4" : "mkv";
            var outputFile = $"{outputDir}/{fileInfo.Name.Split('.')[0]}.{container}";

            await FFMpegArguments.FromFileInput(Input)
                .OutputToFile(outputFile, overwrite: true, delegate (FFMpegArgumentOptions options)
                {
                    options.WithVideoCodec(_videoCodec);
                    options.WithVideoBitrate(_bitrate);
                    options.WithFramerate(_frameRate);
                    options.WithAudioCodec(_audioCodec);
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
