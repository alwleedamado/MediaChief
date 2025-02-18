using FFMpegCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    public class MediaConversionViewModel
    {
        public ConversionType ConversionType => _converter.ConversionType;
        public List<MediaFile> Files => _converter.Files;
        public string ConversionTypeName => Enum.GetName(typeof(ConversionType), ConversionType)!;
        private readonly IMediaConverter _converter;
        public MediaConversionViewModel(IMediaConverter converter)
        {
            _converter = converter;
        }
        public async Task StartConversion()
        {
            await _converter.Convert();  
        }
        
    }
}
