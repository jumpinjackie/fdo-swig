﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner64
{
    class Program
    {
        static void Main(string[] args)
        {
            NUnit.ConsoleRunner.Runner.Main(new string[] { "UnitTest64.dll" });
        }
    }
}
