using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* CLASS INVARIANTS
 * 'power' variable will always be an integer between POWER_MIN and POWER_MAX
 * The array 'initialValues' will not change after the object has been created
 */

/* QUESTIONS
 * 1. Do we need an active state to initialize the lumen object to active, or can we 
 * initialie it to any non-inactive state such as stable? Would it be more optimal to
 * have two booleans rather than four enums so that an object can be both active and 
 * stable?
 * 2.
 */

/*DESIGN CHOICES
 * 1.Power is supplied directly to the lumen object, such that there is no way a 
 * lumen object can be switched off if the power is above the POWER_MIN constant.
 * Because of this we do not need an INACTIVE state.
 * 2.The number returned by the glow() function when the state is ERRATIC approaches
 * the number that will be returned when state is stable. This way there can be
 * a smooth transition from low to high power without any sharp jumps at full
 * power.
 * 3.
 * 
 * 
 */


//brightness: arbitrary maximum brightness that the object is capable of producing when full power
//power: the percent that the lumen object is powered (0 - 100)
//size: how large the lumen object is
namespace P1
{
	public class lumen
	{
		private const int POWER_DECREMENT = 1;
        private const int POWER_MAX = 100;
        private const int POWER_MIN = 0;
        private const int BRIGHTNESS_DECREMENT = 1;
        private const int GLOWCOUNT_THRESHHOLD = 10;
		enum States
		{
			INACTIVE,
			STABLE,
			ERRATIC
		}
		private States state;
        private int glowRequestCount;

        private readonly int[] initialValues = new int[3]; //[size, brightness, power]
        private readonly int size;
        private int brightness;
		private int power;
		private int dimness;
		public lumen(int _size = 10, int _brightness = 100, int _power = 100)
		{
            initialValues[0] = size = _size;
			initialValues[1] = _brightness;
			initialValues[2] = _power;
            init();
		}
		private void init()
		{
            brightness = initialValues[1];
			power = 0;
            setPower(initialValues[2]);
			dimness = POWER_MAX - power;
            state = States.STABLE;
        }
		private void setPower(int newPower)
		{
			power = Math.Clamp(newPower, POWER_MIN, POWER_MAX);
			switch (power)
			{
				case POWER_MIN:
					state = States.INACTIVE;
					break;
				case POWER_MAX:
					state = States.STABLE;
					break;
				default:
					state = States.ERRATIC;
					break;
			}
		}
		public int glow()
		{
			setPower(power - POWER_DECREMENT);
			glowRequestCount++;
			if (state == States.INACTIVE)
                return dimness;
			else if (state == States.STABLE)
				return size * brightness;

			//as the state approaches stable (fully powered) the return value
			//will also approach the return value for the stable state
			else
				return (power / POWER_MAX) * size * brightness;
		}
		public void reset()
		{
			if (glowRequestCount > GLOWCOUNT_THRESHHOLD && power > 0)
				init();
			else
				brightness -= BRIGHTNESS_DECREMENT;
		}
	}
}


/* IMPLEMENTATION INVARIANTS
 * 1.  glow() will return numbers in a smooth pattern from low to high power
 * 2.  The reset() function will only reset the lumen object to the original
 *     values if glow() has been called more than GLOWCOUNT_THRESHOLD times
 *     the state will be INACTIVE of the power is POWER_MIN, STABLE if the
 * 3.  power is POWER_MAX, and ERRATIC if it is between those two values. The
 *     value of power can never not be between or equal to POWER_MIN and 
 *     POWER_MAX.
 * 4.  The 'dimness' variable is updated to be  the inverse of the 'power' 
 *     variable. As power decreases the dimness increases
 */