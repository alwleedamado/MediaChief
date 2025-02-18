using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoChief.Media.Convertors;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    internal class AudioConvversionTask : IMediaConverter
    {
        private readonly AudioCodec _codec;
        private readonly decimal _bitRate;
        private readonly decimal _sampleRate;

        public AudioConvversionTask(List<MediaFile> files, AudioCodec codec, decimal bitRate, decimal sampleRate)
        {
            Files = files;
            _codec = codec;
            _bitRate = bitRate;
            _sampleRate = sampleRate;
        }

        public ConversionType ConversionType => ConversionType.Audio;

        public List<MediaFile> Files { get; private set; } = [];

        public async Task Convert()
        {
            Task[] tasks = new Task[Files.Count];
            int i = 0;
            foreach (var file in Files)
            {
                var convertor = new AudioConvertor(file.Path, _codec.ToString());
                tasks[i++] = convertor.Convert();
            }
            await Task.WhenAll(tasks);
        }
    }
}
