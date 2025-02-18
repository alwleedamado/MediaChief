using Avalonia.Controls;
using Avalonia.Metadata;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using VideoChief.ViewModels;

namespace VideoChief.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => action(ViewModel!.Interaction.RegisterHandler(DoOpenConversionDialogAsync)));

    }

    private async Task DoOpenConversionDialogAsync(InteractionContext<ConversionViewModel, MediaConversionViewModel> interaction)
    {
        var dialog = new ConversionView
        {
            DataContext = interaction.Input
        };
        var result = await dialog.ShowDialog<MediaConversionViewModel>(this);
        interaction.SetOutput(result);
    }
}
