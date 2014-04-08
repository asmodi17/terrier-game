using SkillsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SkillsLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for IActionTest and is intended
    ///to contain all IActionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IActionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual IAction CreateIAction()
        {
            // TODO: Instantiate an appropriate concrete class.
            IAction target = null;
            return target;
        }

        /// <summary>
        ///A test for SetCheckModifier
        ///</summary>
        [TestMethod()]
        public void SetCheckModifierTest()
        {
            IAction target = CreateIAction(); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.SetCheckModifier(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetDelay
        ///</summary>
        [TestMethod()]
        public void SetDelayTest()
        {
            IAction target = CreateIAction(); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.SetDelay(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for checkModifier
        ///</summary>
        [TestMethod()]
        public void checkModifierTest()
        {
            IAction target = CreateIAction(); // TODO: Initialize to an appropriate value
            int actual;
            actual = target.checkModifier;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for delay
        ///</summary>
        [TestMethod()]
        public void delayTest()
        {
            IAction target = CreateIAction(); // TODO: Initialize to an appropriate value
            int actual;
            actual = target.delay;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for name
        ///</summary>
        [TestMethod()]
        public void nameTest()
        {
            IAction target = CreateIAction(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.name;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for type
        ///</summary>
        [TestMethod()]
        public void typeTest()
        {
            IAction target = CreateIAction(); // TODO: Initialize to an appropriate value
            actionTypes actual;
            actual = target.type;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
