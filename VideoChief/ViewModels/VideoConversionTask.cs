using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    internal class VideoConversionTask : IMediaConverter
    {
        public VideoConversionTask(List<MediaFile> files)
        {
            Files = files;
        }

        public ConversionType ConversionType => ConversionType.Video;

        public List<MediaFile> Files {  get; private set; }

        public async Task Convert()
        {
            await Task.CompletedTask;
        }

        
    }
}
