using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoChief.Media.Models;
using VideoChief.Models;

namespace VideoChief.ViewModels
{
    public class MediaConversionViewModel(IMediaConverter converter)
    {
        public ConversionType ConversionType => _converter.ConversionType;
        public List<MediaFile> Files => _converter.Files;
        public string ConversionTypeName => Enum.GetName(typeof(ConversionType), ConversionType)!;
        private readonly IMediaConverter _converter = converter;

        public async Task StartConversion(string outputDir)
        {
            await _converter.Convert(outputDir);  
        }
        
    }
}
