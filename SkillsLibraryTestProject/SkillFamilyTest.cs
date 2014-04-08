using SkillsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SkillsLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for SkillFamilyTest and is intended
    ///to contain all SkillFamilyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SkillFamilyTest
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


        /// <summary>
        ///A test for SkillFamily Constructor
        ///</summary>
        [TestMethod()]
        public void SkillFamilyConstructorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            stats primary = new stats(); // TODO: Initialize to an appropriate value
            stats secondary = new stats(); // TODO: Initialize to an appropriate value
            float ratio = 0F; // TODO: Initialize to an appropriate value
            SkillFamily target = new SkillFamily(name, primary, secondary, ratio);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ChangeRank
        ///</summary>
        [TestMethod()]
        public void ChangeRankTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            stats primary = new stats(); // TODO: Initialize to an appropriate value
            stats secondary = new stats(); // TODO: Initialize to an appropriate value
            float ratio = 0F; // TODO: Initialize to an appropriate value
            SkillFamily target = new SkillFamily(name, primary, secondary, ratio); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.ChangeRank(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for name
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void nameTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SkillFamily_Accessor target = new SkillFamily_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.name = expected;
            actual = target.name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for primary
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void primaryTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SkillFamily_Accessor target = new SkillFamily_Accessor(param0); // TODO: Initialize to an appropriate value
            stats expected = new stats(); // TODO: Initialize to an appropriate value
            stats actual;
            target.primary = expected;
            actual = target.primary;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ranks
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void ranksTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SkillFamily_Accessor target = new SkillFamily_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.ranks = expected;
            actual = target.ranks;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ratio
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void ratioTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SkillFamily_Accessor target = new SkillFamily_Accessor(param0); // TODO: Initialize to an appropriate value
            float expected = 0F; // TODO: Initialize to an appropriate value
            float actual;
            target.ratio = expected;
            actual = target.ratio;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for secondary
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void secondaryTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SkillFamily_Accessor target = new SkillFamily_Accessor(param0); // TODO: Initialize to an appropriate value
            stats expected = new stats(); // TODO: Initialize to an appropriate value
            stats actual;
            target.secondary = expected;
            actual = target.secondary;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
