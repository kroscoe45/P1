using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* QUESTIONS
 * 1. Do we need an active state to initialize the lumen object to active, or can we 
 * initialie it to any non-inactive state such as stable? Would it be more optimal to
 * have two booleans rather than four enums so that an object can be both active and 
 * stable?
 * 2.
 */

/*DESIGN CHOICES
 * 1. Power is supplied directly to the lumen object, such that there is no way a 
 * lumen object can be switched off if the power is above the POWER_MIN constant.
 * Because of this we do not need an INACTIVE state.
 * 2. 
 * 
 * 
 */

namespace P1
{
	public class lumen
  {
    const int POWER_DECREMENT = -3;
		const int POWER_MAX = 100;
		const int POWER_MIN = 0;
		const int BRIGHTNESS_DECREMENT = 3;
    const int GLOWCOUNT_THRESHHOLD = 10;
		enum States
    {
      INACTIVE,
      STABLE,
      ERRATIC
    }
		private States state;
    private int brightness;
    private readonly int size;
    private int power;
    private int dimness;
    private int glowRequestCount;
    public lumen(int _size)
    {
			size = _size;
			init();
    }
    private void init()
    {
			state = States.STABLE;
			power = POWER_MAX;
			brightness = 0;
			dimness = 0;
		}
		//adds change to power
		//to decrement power, pass a negative value
		private void modifyPower(int change)
		{
			if(power + change <= POWER_MIN){
				power = POWER_MIN;
				state = States.INACTIVE;
			}
			else if (power + change >= POWER_MAX) {
				power = POWER_MAX;
				state = States.STABLE;
			}
			else {
				power += change;
				if(state == States.STABLE || state == States.ERRATIC)
					state = States.ERRATIC;
			}
		}
    public int glow()
    {
      modifyPower(POWER_DECREMENT);
      glowRequestCount++;
			if (state == States.INACTIVE)
				return dimness;
			else if (state == States.STABLE)
				return brightness * size;
			else
		     return power * size;
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