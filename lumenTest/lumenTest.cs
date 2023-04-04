namespace P1
{
    [TestClass]
    public class lumenTest
    {
        [TestMethod]
        public void zapInvalid() {
            bool pass = false;
            lumen test = new lumen();
            for(int i = 0; i < 5; i++)
            {
                test.glow();
            }
            try {
                test.zap();
                test.zap();
                test.zap();
                test.zap();
                test.zap();
            }
            catch(zappedErraticObject e)
            {
                pass = true;
            }
            Assert.IsTrue(pass);
        }
        [TestMethod]
        public void zapValid()
        {
            lumen test = new lumen(10, 10, 10);
            for (int i = 0; i < 5; i++)
            {
                test.glow();
            }
            try
            {
                for(int i = 0; i < 5; ++i)
                {
                    test.zap();
                }
            }
            catch (zappedErraticObject)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void isStableTest() {
            lumen test = new lumen();
            test.glow();
            Assert.IsTrue(test.isStable());
            test.zap();
            Assert.IsFalse(test.isStable());
        }
        [TestMethod]
        public void isActiveTest()
        {
            lumen test = new lumen(_power: 1);
            Assert.IsTrue(test.isActive());
            test.glow();
            test.glow();
            Assert.IsFalse(test.isActive());
        }
        [TestMethod]
        public void resetValid(){
            lumen test = new lumen(_size : 5);
            for (int i = 0; i < 6; ++i)
                test.glow();
            Assert.IsTrue(test.reset());
            Assert.IsFalse(test.reset());
        }
        [TestMethod]
        public void resetDecPower()
        {
            lumen test = new lumen();
            int preReset = test.glow();
            Assert.IsFalse(test.reset());
            int postReset = test.glow();
            Assert.IsTrue(postReset < preReset);
        }
        [TestMethod]
        public void erraticGlow()
        {
            lumen test = new lumen();
            test.zap();
            int val1 = test.glow();
            int val2 = test.glow();
            while (!test.isStable())
            {
                Assert.IsTrue(Math.Abs(val1 - val2) >= 10);
                val1 = test.glow();
                val2 = test.glow();
            }
        }
        [TestMethod]
        public void glowStateTransition()
        {
            lumen test = new lumen();
            while(test.isActive()) { test.glow(); }
            int inactiveGlowVal = test.glow();
            Assert.IsFalse(test.isActive());
            int val = test.glow();
            for(int i = 0; i < 100; ++i){ Assert.AreEqual(val, inactiveGlowVal); }
            test.zap();
            Assert.IsTrue(test.isActive());
            int val2 = test.glow();
            Assert.AreNotEqual(val, val2);
            test.zap();
            Assert.IsFalse(test.isStable());
        }
        [TestMethod]
        public void initInactive()
        {
            lumen test = new lumen(_power : 0);
            Assert.IsTrue(test.isActive());
            test.glow();
            Assert.IsFalse(test.isActive());
        }
    }
}