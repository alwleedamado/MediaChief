using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Convertors;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    internal class AudioConvversionTask : MediaConverterBase
    {
        private readonly AudioCodec _codec;
        private readonly int _bitRate;

        public AudioConvversionTask(List<MediaFile> files, AudioCodec codec, int bitRate) : base(files)
        {
            _codec = codec;
            _bitRate = bitRate;
        }

        public override ConversionType ConversionType => ConversionType.Audio;
        public override async Task Convert(string outputDir)
        {
            int i = 1;
            foreach (var file in Files)
            {
                var convertor = new AudioConvertor(file.Path, _codec.ToString(), _bitRate);
                await convertor.Convert(outputDir);
                ProgressEventHandler?.Invoke(i / (double)Files.Count * 100);
                i++;
            }
        }
    }
}
