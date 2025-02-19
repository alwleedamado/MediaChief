using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    public class MediaConversionViewModel(MediaConverterBase converter):ViewModelBase
    {
        public ConversionType ConversionType => _converter.ConversionType;
        public List<MediaFile> Files => _converter.Files;
        private double _conversionPercentage;
        public double ConversionPercentage
        {
            get => _conversionPercentage;
            set => this.RaiseAndSetIfChanged(ref _conversionPercentage, value);
        }
        private ConversionState _isCompleted = ConversionState.Idle;
        public ConversionState State
        {
            get => _isCompleted;
            set => this.RaiseAndSetIfChanged(ref this._isCompleted, value);
        }
        public string ConversionTypeName => Enum.GetName(typeof(ConversionType), ConversionType)!;
        private readonly MediaConverterBase _converter = converter;

        public async Task StartConversion(string outputDir)
        {
            State = ConversionState.Started;
            _converter.SetProgressHandler(delegate(double value)
            {
                Dispatcher.UIThread.Post(() => ConversionPercentage = value);
                
            });
            await _converter.Convert(outputDir);  
            State = ConversionState.Completed;
        }
        
    }
}
