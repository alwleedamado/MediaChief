using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Models;

namespace VideoChief.Models
{
    public abstract class MediaConverterBase(List<MediaFile> files)
    {
        public abstract ConversionType ConversionType { get; }
        public List<MediaFile> Files { get; } = files;

        public Action<double>? ProgressEventHandler;

        public abstract Task Convert(string outputDir);

        public void SetProgressHandler(Action<double> handler)
        {
            ProgressEventHandler = handler;
        }
    }
}
