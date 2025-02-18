using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoChief.Media.Convertors
{
    public abstract class ConvertorBase
    {
        protected readonly string Input;
        protected ConvertorBase(string input)
        {
            Input = input;
        }
        public abstract Task Convert();
    }
}
