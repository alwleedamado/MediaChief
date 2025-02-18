using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using VideoChief.ViewModels;

namespace VideoChief.Views;

public partial class ConversionView : ReactiveWindow<ConversionViewModel>
{
    public ConversionView()
    {
        InitializeComponent();
        this.WhenActivated(OnActivated);
    }

    private IEnumerable<IDisposable> OnActivated()
    {
        var disposables = new CompositeDisposable
        {
            ViewModel!.ImportFilesInteraction.RegisterHandler(BrowseFiles),
            ViewModel!.ImportFolderInteraction.RegisterHandler(ImportFolder),
            ViewModel!.ConvertCommand.Subscribe(Close)
        };
        return disposables;
    }

    private async Task BrowseFiles(InteractionContext<Unit, List<string>> ctx)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
        {
            AllowMultiple = true,
            FileTypeFilter = [
                new FilePickerFileType("Video"){ Patterns = ["*.mp4", "*.mkv", "*.flv", "*.webm", "*.avi"] },
                new FilePickerFileType("Audio"){ Patterns = ["*.mp3", "*.ogg", "*.acc"] }]
        });
        if (files.Count > 0)
        {
            var paths = files.Select(x => x.Path.ToString()).ToList();
            ctx.SetOutput(paths);
        }

    }

    private async Task ImportFolder(InteractionContext<Unit, List<string>> ctx)
    {
        var dir = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            AllowMultiple = false,

        });
        if (dir.Count > 0)
        {
            if (Directory.Exists(dir[0].Path.AbsolutePath.ToString()))
            {

                var files = Directory.EnumerateFiles(dir[0].Path.AbsolutePath.ToString());
                var acceptedFiles = new List<string>();
                foreach (var file in files)
                {
                    if (IsSupportedMedia(new FileInfo(file).Extension))
                        acceptedFiles.Add(file);
                }
                ctx.SetOutput(acceptedFiles);
            }
        }
    }

    private bool IsSupportedMedia(string ext)
    {
        List<string> exts = [".mp4", ".mkv", ".flv", ".webm", ".mp3", ".aac", ".ogg"];
        return exts.Contains(ext);
    }
}