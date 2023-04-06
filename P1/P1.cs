/* NAME: Kyle Roscoe
 * LAST REVISED: 4-4-23
 * REVISION HISTORY: https://github.com/kroscoe45/P1
 * OS: Windows 10
 * USER INTERFACE: User makes lumen objects by passing size, brightness, and power parameters.
 * DRIVER DESCRIPTION:	Makes many lumens with varying parameters, and glows them and then resets them.
 *						glowLumen() makes 5 glow() calls to the lumen obj - should print the same value
 *						5 times if the lumen is inactive.
 *						zapLumen() adds the initial lumen power or 1 to the lumen (whichever is greater)
 *						This may alter the lumen's state
 *						deactivateLumen() will glow the lumen until it can be reset.
 *						Driver avoids making calls to zap() when object is erratic to avoid errors.
 * Assumptions: Assumed that the lumen() obj takes integers as constructor parameters
*/

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
        private const int NUM_OBJ = 10;
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
			for(int i = 0; i < lumens.Length; i++)
			{
				Console.WriteLine("Lumen number " + (i + 1));
				if ((i + 1) % 3 == 0)
					Console.WriteLine("Lumen created with [" + (sizes[i % sizes.Length] + i % 3) + ", " + (powers[i % powers.Length]) + ", " + (materialCapacities[i % materialCapacities.Length] + i % 7) + "]");
				else if((i + 1) % 7 == 0)
					Console.WriteLine("Lumen created with [" + (sizes[i % sizes.Length] + i % 5) + ", " + (powers[i % powers.Length] + i % 9) + ", " + (materialCapacities[i % materialCapacities.Length] + i % 4000) + "]");
				else
					Console.WriteLine("Lumen created with [" + (sizes[i % sizes.Length]) + ", " + (powers[i % powers.Length]) + ", " + (materialCapacities[i % materialCapacities.Length]) + "]");
					
				lumens[i].reEvaluateState();
				Console.WriteLine("Lumen is currently " + (lumens[i].isActive() ? (lumens[i].isStable() ? "stable" : "erratic") : "inactive"));
				glowLumen(lumens[i]);
				Console.WriteLine("Lumen back to start? " + (lumens[i].reset() ? "Yes" : "No"));
				deactivateLumen(lumens[i]);
				zapLumen(lumens[i]);
                Console.WriteLine("Lumen back to start? " + (lumens[i].reset() ? "Yes" : "No"));
                Console.WriteLine("Lumen is currently " + (lumens[i].isActive() ? (lumens[i].isStable() ? "stable" : "erratic") : "inactive"));
                Console.WriteLine("\n");
			}
	
		}
		private static void glowLumen(lumen test) 
		{
			Console.Write("Next 5 glow calls return: ");
			for(int j = 0; j < 4; ++j) {
				Console.Write(test.glow() + ", ");
			}
			Console.WriteLine(test.glow());
		}
		private static void deactivateLumen(lumen test) 
		{
			if (test.isActive()) {
				int counter = 0;
				while (test.isActive()) {
					counter++;
					test.glow();
				}
				Console.WriteLine("Took " + counter + " glow calls for lumen to become inactive");
			}
		}
		private static void zapLumen(lumen test) {
			test.zap();
			Console.WriteLine("Zapped lumen! Is it active? " + (test.isActive() ? "Yes!" : "No!"));
			while (test.isStable()) {
				test.zap();
			}
			Console.WriteLine("Zapped again. Is it erratic? " + (!test.isStable() ? "Yes!" : "No!"));
		}
	}
}