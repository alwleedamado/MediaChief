using FFMpegCore;
using FFMpegCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoChief.Media.Convertors
{
    public class AudioConvertor : ConvertorBase
    {
        private readonly string _input;
        private string _codec;
        public AudioConvertor(string input, string codec) : base(input)
        {
            _input = input;
            _codec = codec;
        }

        public override Task Convert()
        {

            return Task.Run(() =>
            {
                if (File.Exists(Input))
                {
                    var outputDir = Path.Combine(Path.GetDirectoryName(Input)!, "OrbitOutput");
                    if(!Path.Exists(outputDir)) Directory.CreateDirectory(outputDir);
                    var fileInfo = new FileInfo(Input);
                    var outputFile = $"{outputDir}/{fileInfo.Name.Split('.')[0]}.{_codec.ToLower()}";
                    FFMpeg.ExtractAudio(Input, outputFile);
                }
            });
        }
    }
}
