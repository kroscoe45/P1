using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    internal class P1
    {
        private const int NUM_OBJ = 50; 
        static int Main(string[] args)
        {
            lumen[] lumArr = new lumen[NUM_OBJ];
            for(int i = 0; i < NUM_OBJ; ++i)
            {
                lumArr[i] = new lumen(i, i * i, i);
            }
            return 0;
        }
    }
}