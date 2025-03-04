using ReactiveUI;
using System;
using VideoChief.Media.Models;

namespace VideoChief.ViewModels
{
    public class AudioConvrsionViewModel : ViewModelBase
    {
        public string[] AudioCodecs { get; } = Enum.GetNames(typeof(AudioCodec));
        public int[] Bitrates { get; } = [124, 196, 320];


        private string _codec = AudioCodec.Mp3.ToString();
        public string Codec
        {
            get { return _codec; }
            set { this.RaiseAndSetIfChanged(ref _codec, value); }
        }

        private int _bitrate = 124;
        public int Bitrate
        {
            get { return _bitrate; }
            set { this.RaiseAndSetIfChanged(ref this._bitrate, value); }
        }
    }
}
