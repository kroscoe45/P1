/* NAME: Kyle Roscoe
 * LAST REVISED: 4-4-23
 * REVISION HISTORY: https://github.com/kroscoe45/P1
 * CLASS INVARIANTS
 * 1.  The state variable of a lumen object may only be either: INACTIVE(0), STABLE(1) or ERRATIC(2).
 * 2.  All values stored in the class are initialized in the constructor.
 * 3.  Default values are defined for size, power, and materialCapacity
 * 4.  size and materialCapacity are stable and constant throughout the object's lifetime
 * 5.  State tracking and transitioning is handled by the class, not the user
 * 6.  All objects are initialized as STABLE, even when power is 0, until a public function 
 *     other than isActive() or isStable is called.
 * 7.  Error processing: a zappedErraticObject exception is thrown when the user calls zap()
 *     when the object is in the ERRATIC state. This is up to the user to catch and handle.
 * 8.  dimness will be at it's maximum value when brightness is at it's minimum value.
 * 9.  The constructor will ensure that invalid numbers are not assigned to internal data.
 *     The user may pass in garbage or invalid values to the constructor but the object will
 *     be constructed with valid values in all fields.
 */


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
		private readonly int resetThreshold;
        //how much power the object can hold per 1 unit of size before becoming erratic
        private readonly int materialCapacity;
		private readonly int maxStableBrightness;
		private int dimness;
		private int brightness;
		private int power;
		public lumen(int _size = 10, int _power = 100, int _materialCapacity = 10) {
			state = States.STABLE;
			size = _size <= 1 ? 1 : _size;
			power = initialPower = _power <= 0 ? 0 : _power;
            setBrightness();
			resetThreshold = size;
			materialCapacity = _materialCapacity <= 0 ? 0 : _materialCapacity;
			maxStablePower = materialCapacity * size;
			maxStableBrightness = maxStablePower * size;
        }
		private void setBrightness() {
			brightness = size * power;
			dimness = brightness > maxStableBrightness ? 0 : maxStableBrightness - brightness;
		}
		private void decPower() { power = power - 1 <= 0 ? 0 : power - 1; }
		private void updateState() {
			state = States.INACTIVE;
			if (power > 0)
				state = States.STABLE;
			if (power > materialCapacity * size)
				state = States.ERRATIC;
		}
		public int glow() {
			glowCount++;
            setBrightness();
			decPower();
            updateState();
			if (state == States.INACTIVE)
				return dimness;
			if (state == States.STABLE)
				return brightness;
			return power % 2 == 0 ? maxStableBrightness : brightness;
		}
		public void reEvaluateState() { updateState(); }
		public bool reset() {
			updateState();
			if (glowCount >= resetThreshold && power > 0) {
				glowCount = 0;
				state = States.STABLE;
				power = initialPower;
				setBrightness();
				return true;
			}
			decPower();
			setBrightness();
			return false;
		}
		public bool isActive() { return state != States.INACTIVE; }
		public bool isStable() { return state == States.STABLE; }
		//precondition: state is not erratic (exception will be thrown)
		public void zap() {
			updateState();
			if (state == States.ERRATIC)
				throw new zappedErraticObject("Cannot zap an erratic lumen object!");
			power += initialPower < 1 ? 1 : initialPower;
			updateState();
		}
		//postcondition: object may now be active or unstable
	}
}

/* IMPLEMENTATION INVARIANTS
 * 1.  State is stored as an enum to allow for extended functionality if needed.
 *     state is computed and set only in the updateState() method.
 * 2.  Size must be a positive number (or else object would not exist), power must be non-zero
 *     (negative power functionality not defined), materialCapacity must be non-negative.
 * 3.  When the state is erratic, the glow() method will return values alternating between the 
 *     maximum stable brightness value and the real current brightness.
 * 4.  updateState() will make the state inactive if the power is 0, stable if the power is
 *     withing the bounds set by materialCapacity, and otherwise unstable.
 */