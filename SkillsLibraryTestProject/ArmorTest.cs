using SkillsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SkillsLibraryTestProject
{
    
    
    /// <summary>
    ///This is a test class for ArmorTest and is intended
    ///to contain all ArmorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ArmorTest
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
        ///A test for Armor Constructor
        ///</summary>
        [TestMethod()]
        public void ArmorConstructorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string desc = string.Empty; // TODO: Initialize to an appropriate value
            armorSlots slot = new armorSlots(); // TODO: Initialize to an appropriate value
            equipmentEnhancements enhanced = new equipmentEnhancements(); // TODO: Initialize to an appropriate value
            Skill s = null; // TODO: Initialize to an appropriate value
            Armor target = new Armor(name, desc, slot, enhanced, s);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Examine
        ///</summary>
        [TestMethod()]
        public void ExamineTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string desc = string.Empty; // TODO: Initialize to an appropriate value
            armorSlots slot = new armorSlots(); // TODO: Initialize to an appropriate value
            equipmentEnhancements enhanced = new equipmentEnhancements(); // TODO: Initialize to an appropriate value
            Skill s = null; // TODO: Initialize to an appropriate value
            Armor target = new Armor(name, desc, slot, enhanced, s); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Examine();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for enhanceArmor
        ///</summary>
        [TestMethod()]
        public void enhanceArmorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string desc = string.Empty; // TODO: Initialize to an appropriate value
            armorSlots slot = new armorSlots(); // TODO: Initialize to an appropriate value
            equipmentEnhancements enhanced = new equipmentEnhancements(); // TODO: Initialize to an appropriate value
            Skill s = null; // TODO: Initialize to an appropriate value
            Armor target = new Armor(name, desc, slot, enhanced, s); // TODO: Initialize to an appropriate value
            equipmentEnhancements enh = new equipmentEnhancements(); // TODO: Initialize to an appropriate value
            target.enhanceArmor(enh);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for associatedSkill
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void associatedSkillTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
            Skill expected = null; // TODO: Initialize to an appropriate value
            Skill actual;
            target.associatedSkill = expected;
            actual = target.associatedSkill;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for desc
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void descTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.desc = expected;
            actual = target.desc;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for enhanced
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void enhancedTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
            equipmentEnhancements expected = new equipmentEnhancements(); // TODO: Initialize to an appropriate value
            equipmentEnhancements actual;
            target.enhanced = expected;
            actual = target.enhanced;
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
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
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
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.name = expected;
            actual = target.name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for slot
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SkillsLibrary.dll")]
        public void slotTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
            armorSlots expected = new armorSlots(); // TODO: Initialize to an appropriate value
            armorSlots actual;
            target.slot = expected;
            actual = target.slot;
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
            Armor_Accessor target = new Armor_Accessor(param0); // TODO: Initialize to an appropriate value
            gameObjectTypes expected = new gameObjectTypes(); // TODO: Initialize to an appropriate value
            gameObjectTypes actual;
            target.type = expected;
            actual = target.type;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
