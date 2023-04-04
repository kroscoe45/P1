using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    internal class P1
    {
        static int Main(string[] args)
        {
            lumen test = new lumen(_power: 0);
            Console.WriteLine(test.isActive());
            return 0;
        }
    }
}