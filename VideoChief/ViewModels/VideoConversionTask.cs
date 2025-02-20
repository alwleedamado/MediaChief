using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Convertors;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    internal class VideoConversionTask : MediaConverterBase
    {
        private readonly string _videoCodec;
        private readonly string _audioCodec;
        private readonly int _bitrate;
        private readonly double _frameRate;

        public VideoConversionTask(List<MediaFile> files, string videoCodec, string audioCodec, int bitrate, double frameRate) : base(files)
        {
            _videoCodec = videoCodec;
            _audioCodec = audioCodec;
            _bitrate = bitrate;
            _frameRate = frameRate;
        }

        public override ConversionType ConversionType => ConversionType.Video;

        public override async Task Convert(string outputDir)
        {
            int i = 1;
            foreach (var file in Files)
            {
                var converter = new VideoConverter(file.Path, _videoCodec, _audioCodec, _bitrate, _frameRate);
                await converter.Convert(outputDir);
                ProgressEventHandler?.Invoke(i / (double)Files.Count * 100);
                i++;
            }
        }
    }
}
