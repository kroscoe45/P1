using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace P1
{
    internal class P1
    {
        private const int NUM_OBJ = 30;
        static void Main(string[] args)
        {
			int[] sizes = {1, 10, 100, 13, 15, 40, 80, 27, 72, -10, 0, 20, 50};
			int[] powers = {10, 2, 0, -10, 20, 7, 384379, -10000, 1000000, 20, 3, 99, 205};
			int[] materialCapacities = {30, 200, 30, 1000, 40, 6000, 30, 50, 10, 40, 904, 305, 200, 40};
			lumen[] lumens = new lumen[NUM_OBJ];

			for(int i = 0; i < NUM_OBJ; i++)
			{
				if ((i + 1) % 3 == 0)
				{
					lumens[i] = new lumen(sizes[i % sizes.Length] + i % 3, powers[i % powers.Length], materialCapacities[i % materialCapacities.Length] + i % 7);
				}
				else if((i + 1) % 7 == 0)
				{
					lumens[i] = new lumen(sizes[i % sizes.Length] + i % 5, powers[i % powers.Length] + i % 9, materialCapacities[i % materialCapacities.Length] + i % 4000);
				}
				else {
					lumens[i] = new lumen(sizes[i % sizes.Length], powers[i % powers.Length], materialCapacities[i % materialCapacities.Length]);
				}
			}

			
			using(StreamWriter output = new StreamWriter("./lumenDriverOutput.txt")) { 
				output.WriteLine("Number of lumens: " + lumens.Length);
				for(int i = 0; i < lumens.Length; i++)
				{
					output.WriteLine("Lumen number " + (i + 1));
					if ((i + 1) % 3 == 0)
						output.WriteLine("Lumen init vals: [" + (sizes[i % sizes.Length] + i % 3) + ", " + (powers[i % powers.Length]) + ", " + (materialCapacities[i % materialCapacities.Length] + i % 7) + "]");
					else if((i + 1) % 7 == 0)
						output.WriteLine("Lumen init vals: [" + (sizes[i % sizes.Length] + i % 5) + ", " + (powers[i % powers.Length] + i % 9) + ", " + (materialCapacities[i % materialCapacities.Length] + i % 4000) + "]");
					else
						output.WriteLine("Lumen init vals: [" + (sizes[i % sizes.Length]) + ", " + (powers[i % powers.Length]) + ", " + (materialCapacities[i % materialCapacities.Length]) + "]");
					
					lumens[i].reEvaluateState();
					output.WriteLine(new string('-', 15));
					output.WriteLine("Lumen state is: " + (lumens[i].isActive() ? (lumens[i].isStable() ? "Stable" : "Erratic") : "Inactive"));
					
					glowLumen(lumens[i], output);
					output.WriteLine("Reset call returned: " + (lumens[i].reset() ? "True" : "False"));
					deactivateLumen(lumens[i], output);
					zapLumen(lumens[i], output);

					output.WriteLine("Second reset call returned: " + (lumens[i].reset() ? "True" : "False"));
					output.WriteLine("\n");
				}
			}
		}
		private static void glowLumen(lumen test, StreamWriter output) 
		{
			output.Write("Next 5 glow calls return: ");
			for(int j = 0; j < 4; ++j) {
				output.Write(test.glow() + ", ");
			}
			output.WriteLine(test.glow());
		}
		private static void deactivateLumen(lumen test, StreamWriter output) 
		{
			if (test.isActive()) {
				int counter = 0;
				while (test.isActive()) {
					counter++;
					test.glow();
				}
				output.WriteLine("Took " + counter + " glow calls for lumen to become inactive");
			}
		}
		private static void zapLumen(lumen test, StreamWriter output) {
			test.zap();
			output.WriteLine("Zap restores inactive lumen: " + (test.isActive() ? "True" : "False"));
			while (test.isStable()) {
				test.zap();
			}
			output.WriteLine("Zap makes lumen erratic: " + (!test.isStable() ? "True" : "False"));
		}
	}
}