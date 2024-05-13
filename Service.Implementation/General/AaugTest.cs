using AAUG.Service.Interfaces.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAUG.Service.General
{
    public class AaugTest : IAaugTest
    {
        public string Hello(string test)
        {
            test = test.ToLower();
            return test;
        }
    }
}
