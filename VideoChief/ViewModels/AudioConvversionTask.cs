using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Convertors;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    internal class AudioConvversionTask : IMediaConverter
    {
        private readonly AudioCodec _codec;
        private readonly int _bitRate;

        public AudioConvversionTask(List<MediaFile> files, AudioCodec codec, int bitRate)
        {
            Files = files;
            _codec = codec;
            _bitRate = bitRate;
        }

        public ConversionType ConversionType => ConversionType.Audio;

        public List<MediaFile> Files { get; private set; } = [];

        public async Task Convert(string outputDir)
        {
            Task[] tasks = new Task[Files.Count];
            int i = 0;
            foreach (var file in Files)
            {
                var convertor = new AudioConvertor(file.Path, _codec.ToString(), _bitRate);
                tasks[i++] = convertor.Convert(outputDir);
            }
            await Task.WhenAll(tasks);
        }
    }
}
