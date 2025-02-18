using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoChief.Media.Models;

namespace VideoChief.ViewModels
{
    public class AudioConvrsionViewModel : ViewModelBase
    {
        public string[] AudioCodecs { get; } = Enum.GetNames(typeof(AudioCodec));

        private AudioCodec _codec;
        public AudioCodec Codec
        {
            get { return _codec; }
            set { this.RaiseAndSetIfChanged(ref _codec, value); }
        }

        private decimal _bitrate;
        public decimal Bitrate
        {
            get { return _bitrate; }
            set { this.RaiseAndSetIfChanged(ref this._bitrate, value); }
        }

        private decimal _sampleRate;
        public decimal SampleRate
        {
            get { return _sampleRate; }
            set { this.RaiseAndSetIfChanged(ref _sampleRate, value); }
        }
    }
}
