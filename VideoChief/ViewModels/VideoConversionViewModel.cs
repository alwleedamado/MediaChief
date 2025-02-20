using ReactiveUI;
using System;
using VideoChief.Media.Models;

namespace VideoChief.ViewModels
{
    public class VideoConversionViewModel : ViewModelBase
    {
        public string[] AudioCodecs { get; } = Enum.GetNames(typeof(AudioCodec));
        public string[] VideoCodecs { get; } = Enum.GetNames(typeof(VideoCodec));
        private string _codec = VideoCodec.H264.ToString();
        public string Codec
        {
            get => _codec;
            set => this.RaiseAndSetIfChanged(ref _codec, value);
        }

        private double _frameRate;
        public double FrameRate
        {
            get => _frameRate;
            set => this.RaiseAndSetIfChanged(ref _frameRate, value);
        }

        private int _bitrate;
        public int Bitrate
        {
            get => _bitrate;
            set => this.RaiseAndSetIfChanged(ref _bitrate, value);
        }

        private string _audioCodec = VideoChief.Media.Models.AudioCodec.AAC.ToString();
        public string AudioCodec
        {
            get => _audioCodec;
            set => this.RaiseAndSetIfChanged(ref _audioCodec, value);
        }
    }
}
