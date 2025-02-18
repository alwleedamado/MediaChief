using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using VideoChief.Models;
using System.Reactive.Linq;
using VideoChief.Media.Models;

namespace VideoChief.ViewModels
{
    public class ConversionViewModel : ViewModelBase
    {
        private ConversionType _conversionType;
        public ConversionType ConversionType
        {
            get => _conversionType;
            set => this.RaiseAndSetIfChanged(ref _conversionType, value);
        }

        private readonly ViewModelBase _selectedConversionView;
        public ViewModelBase SelectedConversionView => _selectedConversionView;

        public ICommand ImportFolderCommand { get; }
        public ICommand ImportFilesCommand { get; }
        public ReactiveCommand<Unit, MediaConversionViewModel> ConvertCommand { get; }
        public Interaction<Unit, List<string>> ImportFilesInteraction { get; }
        public Interaction<Unit, List<string>> ImportFolderInteraction { get; }

        public ObservableCollection<MediaFile> Files { get; } = [];
        public ConversionViewModel(ConversionType conversionType)
        {
            ImportFilesInteraction = new Interaction<Unit, List<string>>();
            ImportFolderInteraction = new Interaction<Unit, List<string>>();

            _conversionType = conversionType;
            ImportFolderCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var files = await ImportFolderInteraction.Handle(Unit.Default);
                Files.Clear();
                foreach (var file in files)
                {
                    Files.Add(new MediaFile(file));
                }
            });
            ImportFilesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var files = await ImportFilesInteraction.Handle(Unit.Default);
                foreach(var file in files)
                {
                    Files.Add(new MediaFile(file));
                }
            });
            ConvertCommand = ReactiveCommand.Create<MediaConversionViewModel>(() =>
            {
                return conversionType == ConversionType.Audio ?
                 CreateAudioTask()
                 : CreateVideoTask();
            });

            _selectedConversionView = conversionType == ConversionType.Audio ?
                new AudioConvrsionViewModel()
                : new VideoConversionViewModel();
        }

        private MediaConversionViewModel CreateAudioTask()
        {
            var vm = _selectedConversionView as AudioConvrsionViewModel;

            var task =  new AudioConvversionTask(Files.ToList(), vm.Codec, vm.Bitrate, vm.SampleRate);
            return new MediaConversionViewModel(task);
        }

        private MediaConversionViewModel CreateVideoTask()
        {
            var task =  new VideoConversionTask(Files.ToList());
            return new MediaConversionViewModel(task);
        }
    }
}
