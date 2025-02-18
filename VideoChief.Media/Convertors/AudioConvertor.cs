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
                var regex = new Regex("file:///");
                var inputFile = regex.Replace(_input, (s) => "");
                if (File.Exists(inputFile))
                {
                    var dir = new FileInfo(inputFile).Directory!.FullName.ToString() + '/' + "mp3s";
                    if(!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    var output = inputFile.Split('.')[0] + '.' + _codec;
                    FFMpeg.ExtractAudio(inputFile, output);
                }
            });
        }
    }
}
