using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoChief.ViewModels;

namespace VideoChief.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(OnActivated);

    }
    private IEnumerable<IDisposable> OnActivated()
    {
        var disposables = new CompositeDisposable()
        {
            ViewModel!.Interaction.RegisterHandler(DoOpenConversionDialogAsync),
            ViewModel!.OutputDirInteraction.RegisterHandler(ImportFolder)
        };
        return disposables;
    }
    private async Task ImportFolder(InteractionContext<Unit, string?> ctx)
    {
        var directories = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            AllowMultiple = false,
        });
        if (directories.Count > 0)
        {
            Regex regex = UriRegex();
            var ret = directories[0].Path.LocalPath;
            ctx.SetOutput(ret);
        }
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

    [GeneratedRegex("file:///")]
    private static partial Regex UriRegex();
}
