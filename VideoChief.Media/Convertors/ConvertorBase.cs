using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoChief.Media.Convertors
{
    public abstract class ConvertorBase(string input)
    {
        protected readonly string Input = input;
        public abstract Task Convert();
    }
}
