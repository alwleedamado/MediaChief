using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    internal class VideoConversionTask : MediaConverterBase
    {

        public VideoConversionTask(List<MediaFile> files):base(files)
        {
        }

        public override ConversionType ConversionType => ConversionType.Video;

        public override async Task Convert(string outputDir)
        {
            await Task.CompletedTask;
        }
    }
}
