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
using System.Text.RegularExpressions;
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
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            AllowMultiple = true,
            FileTypeFilter = [
                new FilePickerFileType("Video"){ Patterns = ["*.mp4", "*.mkv", "*.flv", "*.webm", "*.avi"] },
                new FilePickerFileType("Audio"){ Patterns = ["*.mp3", "*.ogg", "*.acc"] }]
        });
        if (files.Count > 0)
        {
            Regex regex = UriRegex();

            var paths = files.Select(x =>
            {
                var path = x.Path.ToString();
                return regex.Replace(path, s => "");
            }).ToList();
            ctx.SetOutput(paths);
        }

    }

    private async Task ImportFolder(InteractionContext<Unit, List<string>> ctx)
    {
        var directories = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            AllowMultiple = true,

        });

        if (directories.Count > 0)
        {
            Regex regex = UriRegex();

            foreach (var directory in directories)
            {
                var directoryPath = regex.Replace(directory.Path.AbsolutePath.ToString(), (s) => "");

                if (Directory.Exists(directoryPath))
                {

                    var files = Directory.EnumerateFiles(directoryPath);
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
    }

    private static bool IsSupportedMedia(string ext)
    {
        if (string.IsNullOrEmpty(ext))
        {
            throw new ArgumentException($"'{nameof(ext)}' cannot be null or empty.", nameof(ext));
        }

        List<string> exts = [".mp4", ".mkv", ".flv", ".webm", ".mp3", ".aac", ".ogg"];
        return exts.Contains(ext);
    }

    [GeneratedRegex("file:///")]
    private static partial Regex UriRegex();
}