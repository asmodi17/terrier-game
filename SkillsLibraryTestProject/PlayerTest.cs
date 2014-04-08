using SkillsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SkillsLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for PlayerTest and is intended
    ///to contain all PlayerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PlayerTest
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
        ///A test for Player Constructor
        ///</summary>
        [TestMethod()]
        public void PlayerConstructorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool isTargetable = false; // TODO: Initialize to an appropriate value
            IEnumerable<Skill> skills = null; // TODO: Initialize to an appropriate value
            Player target = new Player(name, isTargetable, skills);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Examine
        ///</summary>
        [TestMethod()]
        public void ExamineTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool isTargetable = false; // TODO: Initialize to an appropriate value
            IEnumerable<Skill> skills = null; // TODO: Initialize to an appropriate value
            Player target = new Player(name, isTargetable, skills); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Examine();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetActions
        ///</summary>
        [TestMethod()]
        public void GetActionsTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool isTargetable = false; // TODO: Initialize to an appropriate value
            IEnumerable<Skill> skills = null; // TODO: Initialize to an appropriate value
            Player target = new Player(name, isTargetable, skills); // TODO: Initialize to an appropriate value
            Skill s = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<IAction> actual;
            actual = target.GetActions(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBattleActions
        ///</summary>
        [TestMethod()]
        public void GetBattleActionsTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool isTargetable = false; // TODO: Initialize to an appropriate value
            IEnumerable<Skill> skills = null; // TODO: Initialize to an appropriate value
            Player target = new Player(name, isTargetable, skills); // TODO: Initialize to an appropriate value
            Skill s = null; // TODO: Initialize to an appropriate value
            IEnumerable<PlayerBattleAction> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<PlayerBattleAction> actual;
            actual = target.GetBattleActions(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSkill
        ///</summary>
        [TestMethod()]
        public void GetSkillTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool isTargetable = false; // TODO: Initialize to an appropriate value
            IEnumerable<Skill> skills = null; // TODO: Initialize to an appropriate value
            Player target = new Player(name, isTargetable, skills); // TODO: Initialize to an appropriate value
            string name1 = string.Empty; // TODO: Initialize to an appropriate value
            Skill expected = null; // TODO: Initialize to an appropriate value
            Skill actual;
            actual = target.GetSkill(name1);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for dexterity
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void dexterityTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.dexterity = expected;
            actual = target.dexterity;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for health
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void healthTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.health = expected;
            actual = target.health;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isPlayer
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void isPlayerTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.isPlayer = expected;
            actual = target.isPlayer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isTargetable
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void isTargetableTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.isTargetable = expected;
            actual = target.isTargetable;
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
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.name = expected;
            actual = target.name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for stamina
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void staminaTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.stamina = expected;
            actual = target.stamina;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for strength
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void strengthTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.strength = expected;
            actual = target.strength;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for type
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void typeTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            gameObjectTypes expected = new gameObjectTypes(); // TODO: Initialize to an appropriate value
            gameObjectTypes actual;
            target.type = expected;
            actual = target.type;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for will
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void willTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Player_Accessor target = new Player_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.will = expected;
            actual = target.will;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
