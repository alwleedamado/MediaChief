using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Models;

namespace VideoChief.Models
{
    public interface IMediaConverter
    {
        ConversionType ConversionType { get; }
        List<MediaFile> Files { get; }
        Task Convert(string outputDir);
    }
}
