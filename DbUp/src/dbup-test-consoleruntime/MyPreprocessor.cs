using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUPTest
{
    class MyPreprocessor : IScriptPreprocessor
    {
        public string Process(string contents)
        {
            var ss = contents;
            return contents;
        }
    }
}
