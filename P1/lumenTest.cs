using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1 {
	internal class lumenTest {
		public bool testGlow(lumen obj, int expected) {
			return expected == obj.glow();
		}
		public bool testActive(lumen obj, int glowCount) {
			for(int i = 0; i < glowCount; i++) {
				obj.glow();
			}
			return obj.isActive();
		}
		public bool testStable(int power) {
			lumen obj = new lumen(_power : power);
			obj.zap();
		}
	}
}	