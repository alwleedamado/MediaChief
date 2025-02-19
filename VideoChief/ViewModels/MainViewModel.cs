using FFMpegCore;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoChief.Models;

namespace VideoChief.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private string _outputDir = "";
    public string OutputDir
    {
        get => _outputDir;
        set => this.RaiseAndSetIfChanged(ref _outputDir, value);
    }
    public Interaction<ConversionViewModel, MediaConversionViewModel> Interaction { get; }
    public Interaction<Unit, string?> OutputDirInteraction { get; }
    public ObservableCollection<MediaConversionViewModel> Conversions { get; } = [];
    public ICommand ConvertVideoCommand { get; }
    public ICommand ConvertAudioCommand { get; }
    public ICommand StartConversionCommand { get; }
    public ICommand SelectOutputCommand { get; }
    public MainViewModel()
    {
        Interaction = new Interaction<ConversionViewModel, MediaConversionViewModel>();
        OutputDirInteraction = new Interaction<Unit, string?>();
        ConvertAudioCommand = ReactiveCommand.CreateFromTask(async () => await OpenConversionDialog(ConversionType.Audio));
        ConvertVideoCommand = ReactiveCommand.CreateFromTask(async () => await OpenConversionDialog(ConversionType.Video));
        StartConversionCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            foreach (var conversion in Conversions)
            {
                await conversion.StartConversion(OutputDir);
            }
        });
        SelectOutputCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var result = await OutputDirInteraction.Handle(Unit.Default);
            if(!string.IsNullOrEmpty(result))
                OutputDir = result;
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
