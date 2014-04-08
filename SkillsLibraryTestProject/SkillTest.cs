using SkillsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SkillsLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for SkillTest and is intended
    ///to contain all SkillTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SkillTest
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
        ///A test for Skill Constructor
        ///</summary>
        [TestMethod()]
        public void SkillConstructorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            SkillFamily family = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> actions = null; // TODO: Initialize to an appropriate value
            Skill target = new Skill(name, family, actions);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ChangeRank
        ///</summary>
        [TestMethod()]
        public void ChangeRankTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            SkillFamily family = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> actions = null; // TODO: Initialize to an appropriate value
            Skill target = new Skill(name, family, actions); // TODO: Initialize to an appropriate value
            Player p = null; // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.ChangeRank(p, i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetAllActions
        ///</summary>
        [TestMethod()]
        public void GetAllActionsTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            SkillFamily family = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> actions = null; // TODO: Initialize to an appropriate value
            Skill target = new Skill(name, family, actions); // TODO: Initialize to an appropriate value
            IEnumerable<IAction> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> actual;
            actual = target.GetAllActions();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetMod
        ///</summary>
        [TestMethod()]
        public void SetModTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            SkillFamily family = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> actions = null; // TODO: Initialize to an appropriate value
            Skill target = new Skill(name, family, actions); // TODO: Initialize to an appropriate value
            Player p = null; // TODO: Initialize to an appropriate value
            target.SetMod(p);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for family
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void familyTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Skill_Accessor target = new Skill_Accessor(param0); // TODO: Initialize to an appropriate value
            SkillFamily expected = null; // TODO: Initialize to an appropriate value
            SkillFamily actual;
            target.family = expected;
            actual = target.family;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for mod
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void modTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Skill_Accessor target = new Skill_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.mod = expected;
            actual = target.mod;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for name
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void nameTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Skill_Accessor target = new Skill_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.name = expected;
            actual = target.name;
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
            Skill_Accessor target = new Skill_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.ranks = expected;
            actual = target.ranks;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
