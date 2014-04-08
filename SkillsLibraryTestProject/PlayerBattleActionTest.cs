﻿using SkillsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SkillsLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for PlayerBattleActionTest and is intended
    ///to contain all PlayerBattleActionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PlayerBattleActionTest
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
        ///A test for PlayerBattleAction Constructor
        ///</summary>
        [TestMethod()]
        public void PlayerBattleActionConstructorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            int delay = 0; // TODO: Initialize to an appropriate value
            battleActionTypes bType = new battleActionTypes(); // TODO: Initialize to an appropriate value
            PlayerBattleAction target = new PlayerBattleAction(name, delay, bType);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SetCheckModifier
        ///</summary>
        [TestMethod()]
        public void SetCheckModifierTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            int delay = 0; // TODO: Initialize to an appropriate value
            battleActionTypes bType = new battleActionTypes(); // TODO: Initialize to an appropriate value
            PlayerBattleAction target = new PlayerBattleAction(name, delay, bType); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.SetCheckModifier(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetDamage
        ///</summary>
        [TestMethod()]
        public void SetDamageTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            int delay = 0; // TODO: Initialize to an appropriate value
            battleActionTypes bType = new battleActionTypes(); // TODO: Initialize to an appropriate value
            PlayerBattleAction target = new PlayerBattleAction(name, delay, bType); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.SetDamage(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetDelay
        ///</summary>
        [TestMethod()]
        public void SetDelayTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            int delay = 0; // TODO: Initialize to an appropriate value
            battleActionTypes bType = new battleActionTypes(); // TODO: Initialize to an appropriate value
            PlayerBattleAction target = new PlayerBattleAction(name, delay, bType); // TODO: Initialize to an appropriate value
            int i = 0; // TODO: Initialize to an appropriate value
            target.SetDelay(i);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for bType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void bTypeTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            PlayerBattleAction_Accessor target = new PlayerBattleAction_Accessor(param0); // TODO: Initialize to an appropriate value
            battleActionTypes expected = new battleActionTypes(); // TODO: Initialize to an appropriate value
            battleActionTypes actual;
            target.bType = expected;
            actual = target.bType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for checkModifier
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void checkModifierTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            PlayerBattleAction_Accessor target = new PlayerBattleAction_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.checkModifier = expected;
            actual = target.checkModifier;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for damage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void damageTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            PlayerBattleAction_Accessor target = new PlayerBattleAction_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.damage = expected;
            actual = target.damage;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for delay
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void delayTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            PlayerBattleAction_Accessor target = new PlayerBattleAction_Accessor(param0); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.delay = expected;
            actual = target.delay;
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
            PlayerBattleAction_Accessor target = new PlayerBattleAction_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.name = expected;
            actual = target.name;
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
            PlayerBattleAction_Accessor target = new PlayerBattleAction_Accessor(param0); // TODO: Initialize to an appropriate value
            actionTypes expected = new actionTypes(); // TODO: Initialize to an appropriate value
            actionTypes actual;
            target.type = expected;
            actual = target.type;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
