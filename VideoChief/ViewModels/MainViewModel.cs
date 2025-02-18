using FFMpegCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public Interaction<ConversionViewModel, MediaConversionViewModel> Interaction { get; }
    public ObservableCollection<MediaConversionViewModel> Conversions { get; } = [];
    public ICommand ConvertVideoCommand { get; }
    public ICommand ConvertAudioCommand { get; }
    public ICommand StartConversionCommand { get; }
    public MainViewModel()
    {
        Interaction = new Interaction<ConversionViewModel, MediaConversionViewModel>();

        ConvertAudioCommand = ReactiveCommand.CreateFromTask(async () => await OpenConversionDialog(ConversionType.Audio));
        ConvertVideoCommand = ReactiveCommand.CreateFromTask(async () => await OpenConversionDialog(ConversionType.Video));
        StartConversionCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            foreach(var conversion in Conversions)
            {
                await conversion.StartConversion();
            }
        });

        GlobalFFOptions.Configure(o =>
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            o.BinaryFolder = Path.Combine(baseDirectory, "bin");
            o.WorkingDirectory = baseDirectory;
            o.TemporaryFilesFolder = Path.Combine(baseDirectory, "temp");
        });
    }

    private async Task OpenConversionDialog(ConversionType conversionType)
    {
        var dialog = new ConversionViewModel(conversionType);
        var result = await Interaction.Handle(dialog);
        Conversions.Clear();
        if (result != null)
        {
            Conversions.Add(result);
        }
    }
}
