// Copyright (C) 2014 Phillip Bradbury
// This program comes with ABSOLUTELY NO WARRANTY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace GameLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random R = new Random((int)DateTime.Now.Ticks & 0x000FFFFF);
            Dictionary<families, SkillFamily> skillFamilies = new Dictionary<families,SkillFamily>();
            GenerateSkillFamilies(out skillFamilies);
            Dictionary<string, Skill> skills = new Dictionary<string, Skill>();
            GenerateSkills(skillFamilies, out skills);

            Dictionary<string, Skill> initialPlayerSkills = InitializePlayerSkillSet(skills);

            Weapon sword = new Weapon("Sword", "Just a sword", weaponSlots.leftHand, damageTypes.slash, equipmentEnhancements.none, skills["Sword"], 1000, false);
            Weapon sword2 = new Weapon("Sword 2", "Just another sword", weaponSlots.leftHand, damageTypes.slash, equipmentEnhancements.none, skills["Sword"], 1000, false);
            Weapon sword3 = (Weapon)sword.Duplicate();
            sword3.Name = "Sword 3";
            Weapon shield = new Weapon("Shield", "Just a shield", weaponSlots.rightHand, damageTypes.bludgeon, equipmentEnhancements.none, skills["Shield"], 1500, true);


            Player A = new Player("Player A", true, initialPlayerSkills.Values, skillFamilies.Values, true, R);
            Player B = new Player("Player B", true, initialPlayerSkills.Values, skillFamilies.Values, true, R);

            /*A.GetSkill("Dodge").ChangeRank(B, 100);
            A.GetSkill("Brawling").ChangeRank(B, 100);
            A.GetSkill("Hit").ChangeRank(B, 100);

            B.GetSkill("Dodge").ChangeRank(B, 100);
            B.GetSkill("Brawling").ChangeRank(B, 100);
            B.GetSkill("Hit").ChangeRank(B, 100);*/

            A.AddItemToInventory(sword);
            A.AddItemToInventory(sword3);

            B.AddItemToInventory(sword2);
            B.AddItemToInventory(shield);

            Console.Write(A.EquipWeapon(sword.Name, weaponSlots.leftHand));
            Console.Write(A.EquipWeapon(sword3.Name, weaponSlots.rightHand));
            Console.Write(B.EquipWeapon(sword2.Name, weaponSlots.rightHand));
            Console.Write(B.EquipWeapon(shield.Name, weaponSlots.leftHand));

            // It shouldn't matter which attack action is used.
            // TODO: modify the GetAction and other methods to handle only
            //      one attack action.  This will use all weapons currently
            //      equipped and modify its own delay whenever equipped items
            //      change.
            IAction someAction = A.GetAction("Attack");
            if (someAction != null) {
                if (someAction.Type == actionTypes.battleAction)
                {
                    ((PlayerBattleAction)someAction).Target = B;
                }
            }

            IAction someOtherAction = B.GetAction("Attack");
            if (someOtherAction != null) {
                if (someOtherAction.Type == actionTypes.battleAction)
                {
                    ((PlayerBattleAction)someOtherAction).Target = A;
                }
            }

            IAction rallyA = A.GetAction("Rally");
            if (rallyA != null)
            {
                if (rallyA.Type == actionTypes.battleAction)
                {
                    ((PlayerBattleAction)rallyA).Target = A;
                }
            }

            IAction rallyB = B.GetAction("Rally");
            if (rallyB != null)
            {
                if (rallyB.Type == actionTypes.battleAction)
                {
                    ((PlayerBattleAction)rallyB).Target = B;
                }
            }

            A.PerformAction(someAction);

            //Console.WriteLine(A.HPRatio);
            //Console.WriteLine(B.HPRatio);

            if (B.AutoRetaliate == true)
            {
                B.PerformAction(someOtherAction);

                // First, if AutoAttack is false for the player, then the
                // attack is a single action.  If AutoRetaliate is set for the target,
                // then they will reciprocate.  If AutoRetaliate is set, then it
                // must also set AutoAttack.

                while (B.HPRatio > 0.0 && A.HPRatio > 0.0)
                {
                    if (A.AutoAttack == true)
                    {
                        A.PerformAction(someAction);
                    }

                    if (B.AutoAttack == true)
                    {
                        B.PerformAction(someOtherAction);
                    }

                    A.PerformAction(rallyA);
                    B.PerformAction(rallyB);
                }
            }

            ListPlayerStats(A);
            ListAvailableSkills(A);
            ListPlayerStats(B);
            ListAvailableSkills(B);

            Console.ReadKey();
        }

        private static void ListPlayerStats(Player A)
        {
            Console.WriteLine(A.Name + " Stats:");
            Console.WriteLine("Strength: " + A.Strength.ToString());
            Console.WriteLine("Dexterity: " + A.Dexterity.ToString());
            Console.WriteLine("Health: " + A.Health.ToString());
            Console.WriteLine("Stamina: " + A.Stamina.ToString());
            Console.WriteLine("Will: " + A.Will.ToString());
        }

        private static void ListAvailableSkills(Player A)
        {
            Console.WriteLine(A.Name + " Skills:");
            foreach (Skill s in A.GetAllSkills())
            {
                Console.WriteLine(s.Name + ":  Ranks:" + s.Ranks.ToString() + ", Mod:" + s.Mod.ToString());
            }
        }

        private static void ListAvailableActions(Player A)
        {
            foreach (Skill s in A.GetAllSkills())
            {
                foreach (IAction i in s.GetAllActions())
                {
                    Console.WriteLine(i.Name);
                }
            }
        }

        static void GenerateSkillFamilies(out Dictionary<families, SkillFamily> skillfamilies)
        {
            // Need to generate skill families for player generation.
            /*
            physicalAttack,
            physicalDefense,
            rangedAttack,
            magic,
            language,
            knowledge,
            thief,
            perception,
            crafting*/

            skillfamilies = new Dictionary<families, SkillFamily>();

            SkillFamily physicalAttack = new SkillFamily("Physical Attack", stats.strength, stats.dexterity, 0.65);
            SkillFamily physicalDefend = new SkillFamily("Physical Defend", stats.dexterity, stats.dexterity, 1.0);
            SkillFamily rangedAttack = new SkillFamily("Ranged Attack", stats.strength, stats.dexterity, 0.65);
            SkillFamily magic = new SkillFamily("Magic", stats.will, stats.will, 1.0);
            SkillFamily language = new SkillFamily("Language", stats.will, stats.will, 1.0);
            SkillFamily knowledge = new SkillFamily("Knowledge", stats.will, stats.will, 1.0);
            SkillFamily thief = new SkillFamily("Thief", stats.dexterity, stats.will, 0.75);
            SkillFamily perception = new SkillFamily("Perception", stats.will, stats.will, 1.0);
            SkillFamily crafting = new SkillFamily("Crafting", stats.dexterity, stats.will, 0.75);

            skillfamilies.Add(families.physicalAttack, physicalAttack);
            skillfamilies.Add(families.physicalDefense, physicalDefend);
            skillfamilies.Add(families.rangedAttack, rangedAttack);
            skillfamilies.Add(families.magic, magic);
            skillfamilies.Add(families.language, language);
            skillfamilies.Add(families.knowledge, knowledge);
            skillfamilies.Add(families.thief, thief);
            skillfamilies.Add(families.perception, perception);
            skillfamilies.Add(families.crafting, crafting);
        }

        static void GenerateSkills(Dictionary<families, SkillFamily> sf, out Dictionary<string, Skill> skills)
        {
            skills = new Dictionary<string, Skill>();
            // Combat Skills:
            //  Defense Skills
            //  -   Parry - Reduces possibility of a hit - Requires a weapon/shield -- Need this
            //  -   Dodge - Reduces possibility of a hit -- Need this
            //  Offense Skills
            //  -   Hit - Increases possibility of a hit -- Need this
            // General Skills
            //  -   MultipleAttacks - Reduces multi-wielding penalties -- Need this
            //  Weapon Skills
            //  -   Bladed, Bludgeon, Unarmed, Shield
            //      -   All Weapon skills - increase possibility of hit, increase damage, increase critical chance and critical damage -- Need This
            //  Armor Skills
            //  -   Armor, Unarmored
            //      -   All Armor skills - reduce possibility of hit, reduce damage -- Not Currently used
            
            // So currently this will generate: Parry, Dodge, Hit, MultipleAttacks, Sword, Unarmed, Shield
            // Unimplemented: Armor, Damage, DamageReduction, and Critical
            Skill Parry = new Skill("Parry", sf[families.physicalDefense], new List<IAction>(), true);
            Skill Dodge = new Skill("Dodge", sf[families.physicalDefense], new List<IAction>(), true);
            Skill Hit = new Skill("Hit", sf[families.physicalAttack], new List<IAction>(), true);
            Skill MultipleAttacks = new Skill("Multiple Attacks", sf[families.physicalAttack], new List<IAction>(), true);

            PlayerBattleAction swordAttack = new PlayerBattleAction("Attack", 1000, battleActionTypes.attack);
            List<IAction> skillActions = new List<IAction>();
            skillActions.Add(swordAttack);
            Skill Sword = new Skill("Sword", sf[families.physicalAttack], skillActions, true);
            
            PlayerBattleAction fistAttack = new PlayerBattleAction("Attack", 500, battleActionTypes.attack);
            skillActions = new List<IAction>();
            skillActions.Add(fistAttack);
            Skill Brawling = new Skill("Brawling", sf[families.physicalAttack], skillActions, true);

            PlayerBattleAction shieldAttack = new PlayerBattleAction("Attack", 1500, battleActionTypes.attack);
            skillActions = new List<IAction>();
            skillActions.Add(shieldAttack);
            Skill Shield = new Skill("Shield", sf[families.physicalDefense], skillActions, true);

            PlayerBattleAction greaveAttack = new PlayerBattleAction("Attack", 750, battleActionTypes.attack);
            skillActions = new List<IAction>();
            skillActions.Add(greaveAttack);
            Skill GreaveBlades = new Skill("Greave Blades", sf[families.physicalAttack], skillActions, true);

            PlayerBattleAction haftedAttack = new PlayerBattleAction("Attack", 1250, battleActionTypes.attack);
            skillActions = new List<IAction>();
            skillActions.Add(haftedAttack);
            Skill Hafted = new Skill("Hafted", sf[families.physicalAttack], skillActions, true);

            BuffAction rally = new BuffAction("Rally", 10000, 5000, stats.strength, 5);
            skillActions = new List<IAction>();
            skillActions.Add(rally);
            Skill Support = new Skill("Support", sf[families.knowledge], skillActions, false);

            skills.Add("Parry", Parry);
            skills.Add("Dodge", Dodge);
            skills.Add("Hit", Hit);
            skills.Add("Multiple Attacks", MultipleAttacks);
            skills.Add("Sword", Sword);
            skills.Add("Brawling", Brawling);
            skills.Add("Shield", Shield);
            skills.Add("Greave Blades", GreaveBlades);
            skills.Add("Hafted", Hafted);
            skills.Add("Support", Support);
        }

        static Dictionary<string, Skill> InitializePlayerSkillSet(Dictionary<string, Skill> skills)
        {
            // A Player doesn't know about weapon skills until they have the weapon.
            // This is also true of other skills that are not yet implemented.
            // These skills are created using the GenerateSkills method so that they exist, but cannot be used
            // until the items necessary are equipped
            // with equipped items, there is a player method Player.GetNewSkills which checks newly equipped
            // items for skills which are not in the Player's current skill list.

            Dictionary<string, Skill> result = new Dictionary<string, Skill>();

            foreach (string s in skills.Keys)
            {
                // Add the restricted skills to this if statement
                if (!(s.Equals("Sword") || s.Equals("Shield") || s.Equals("Greave Blades") || s.Equals("Hafted")))
                {
                    result.Add(s, skills[s]);
                }
            }

            return result;
        }

        static void PrintSomePlayerInfo(Player p)
        {
            Console.WriteLine(p.Desc);
            Console.WriteLine("Stats:");
            Console.WriteLine("Strength: " + p.Strength);
            Console.WriteLine("Stamina: " + p.Stamina);
            Console.WriteLine("Dexterity: " + p.Dexterity);
            Console.WriteLine("Health: " + p.Health);
            Console.WriteLine("Will: " + p.Will + "\n");
            Console.WriteLine("SkillFamilies:");
            Console.WriteLine("--------------");
            foreach (SkillFamily s in p.GetAllSkillFamilies())
            {
                Console.WriteLine(s.Name + ": " + s.Ranks.ToString());
            }
            Console.WriteLine("\nSkills:");
            Console.WriteLine("--------------");
            foreach (Skill s in p.GetAllSkills())
            {
                Console.WriteLine(s.Name + ": " + s.Ranks.ToString() + " - " + s.Mod.ToString());
            }
        }
    }
}
