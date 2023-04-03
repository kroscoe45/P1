using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace P1
{
	public class zappedErraticObject : Exception {
		public zappedErraticObject(){}
		public zappedErraticObject(string message) : base(message) { }

	}
	public class lumen
	{
		private enum States
		{
			INACTIVE,
			STABLE,
			ERRATIC
		}
		private States state;
    private int glowCount;
		private readonly int size;
		private readonly int initialPower;
		private readonly int maxStablePower;
		private readonly int initialBrightness;
		private readonly int resetThreshold;
		private readonly int materialCapacity; //how much power the object can hold per 1 size before becoming erratic
		private int dimness;
		private int brightness;
		private int power;
		public lumen(int _size = 10, int _power = 100, int _materialCapacity = 10)
		{
			state = States.STABLE;
      size = _size;
			power = initialPower = _power;
			setBrightness();
			initialBrightness = brightness;
			resetThreshold = Math.Clamp(power, 1, power);
			materialCapacity = _materialCapacity;
			maxStablePower = materialCapacity * size;
		}
		private void setBrightness() {
			brightness = size * power;
			dimness = initialBrightness / Math.Clamp(brightness, 1, brightness);
		}
		private void updateState() {
			state = States.INACTIVE;
			if (power >=0)
				state = States.STABLE;
			if (power >= materialCapacity * size)
				state = States.ERRATIC;
		}
		public int glow()
		{
			glowCount++;
			power--;
			updateState();
			setBrightness();
			if (state == States.INACTIVE)
				return dimness;
			else if (state == States.STABLE)
				return brightness;
			else {
				return power % 2 == 0 ? (maxStablePower * size) / 2 : brightness;
			}
		}
		public bool reset()
		{
			updateState();
			if (glowCount > resetThreshold && power > 0) {
				state = States.STABLE;
				power = initialPower;
				setBrightness();
				return true;
			}
			else {
				power--;
				setBrightness();
				return false;
			}
		}
		public bool isActive() { return state != States.INACTIVE; }
		public bool isStable() { return state == States.STABLE; }
		public void zap() {
			updateState();
			if (state == States.ERRATIC)
				throw new zappedErraticObject("Cannot zap an erratic lumen object!");
			power += initialPower;
			updateState();
		}
	}
}