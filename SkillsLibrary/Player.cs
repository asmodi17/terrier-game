using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace GameLibrary
{
    public class Player : GameObject
    {
        public Random R { get; private set; }
        public bool IsPlayer { get; private set; }

        private bool _autoRetaliate;
        /// <summary>
        /// AutoRetaliate is a property which enables retaliation when hostile actions
        /// are taken against the player
        /// </summary>
        public bool AutoRetaliate
        {
            get { return _autoRetaliate; }
            set
            {
                if (value == true) AutoAttack = value;
                _autoRetaliate = value;
            }
        }

        /// <summary>
        /// If AutoAttack is set, then a hostile action by the player towards the target
        /// will repeat until the target is no longer present.  This property can also
        /// be set by setting AutoRetaliate to true.
        /// </summary>
        public bool AutoAttack { get; set; }

        public override GameObject Duplicate()
        {
            // Players can not be Duplicated
            return (GameObject)this;
        }

        public string Desc { get; private set; }
        public override string Examine()
        {
            if (string.IsNullOrEmpty(Desc)) GenDesc();
            return Desc;
        }

        public void GenDesc()
        {
            string equipString = "";
            Armor head = GetEquippedArmor(armorSlots.head);
            Armor chest = GetEquippedArmor(armorSlots.chest);
            Armor arms = GetEquippedArmor(armorSlots.arms);
            Armor legs = GetEquippedArmor(armorSlots.legs);
            if (IsPlayer)
            {
                if (head == null)
                {
                    equipString += "You are not wearing any head protection.\n";
                }
                else
                {
                    equipString += "You are wearing the " + head.Name + " on your head.\n";
                }

                if (chest == null)
                {
                    equipString += "You are not wearing any body armor.\n";
                }
                else
                {
                    equipString += "You are wearing the " + chest.Name + " on your chest.\n";
                }

                if (arms == null)
                {
                    equipString += "You are not wearing any arm protection.\n";
                }
                else
                {
                    equipString += "You are wearing the " + arms.Name + " which cover your arms.\n";
                }

                if (legs == null)
                {
                    equipString += "You are not wearing any leg protection.\n";
                }
                else
                {
                    equipString += "You are wearing the " + legs.Name + " which cover your legs.\n";
                }
            }
            else
            {

            }

            // shield and hands may interfere... Perhaps we should have some logic set up for that...
            // any logic should be taken care of in EquipItem
            // There are now 6 options for shields and weapons LH, RH, LF, RF, BothHands, BothFeet
            equipString += GetDescForWeaponsLegsAndShields();

            Desc = equipString;
        }

        // This may be working properly.
        private string GetDescForWeaponsLegsAndShields()
        {
            string result = "";

            GameObject LH = null;
            GameObject RH = null;
            GameObject LF = null;
            GameObject RF = null;
            GameObject BothHands = null;
            GameObject BothFeet = null;

            foreach (GameObject i in GetEquippedItems())
            {
                if (i.Type == gameObjectTypes.armor)
                {
                    if (((Armor)i).Slot == armorSlots.legs)
                    {
                        BothFeet = i;
                    }
                }
                else
                {
                    // handle the weapon and shield cases
                    if (i.Type == gameObjectTypes.weapon)
                    {
                        switch (((Weapon)i).Slot)
                        {
                            case (weaponSlots.leftHand):
                                LH = i;
                                break;
                            case (weaponSlots.rightHand):
                                RH = i;
                                break;
                            case (weaponSlots.leftFoot):
                                LF = i;
                                break;
                            case (weaponSlots.rightFoot):
                                RF = i;
                                break;
                            case (weaponSlots.bothHands):
                                BothHands = i;
                                break;
                            case (weaponSlots.bothFeet):
                                BothFeet = i;
                                break;
                            default:
                                throw new ArgumentException("Weapon slots can only be LH, RH, LF, RF, BothHands, or BothFeet");
                        }
                    }
                }
            }

            // Now handle the results in a sane order: LH, RH, BothHands, LF, RF, BothFeet
            if (LH != null)
            {
                if (LH.Type == gameObjectTypes.weapon)
                {
                    result += (IsPlayer) ? "You are wielding " + LH.Name + " in your left hand.\n" : Name + " is wielding " + LH.Name + " in their left hand.\n";
                }
                else
                {
                    result += (IsPlayer) ? "You are holding " + LH.Name + " in your left hand.\n" : Name + " is holding " + LH.Name + " in their left hand.\n";
                }
            }
            if (LH == null && BothHands == null) result += (IsPlayer) ? "You are using your left fist as a weapon.\n" : Name + " is using their left fist as a weapon.\n";

            if (RH != null)
            {
                if (RH.Type == gameObjectTypes.weapon)
                {
                    result += (IsPlayer) ? "You are wielding " + RH.Name + " in your right hand.\n" : Name + " is wielding " + RH.Name + " in their right hand.\n";
                }
                else
                {
                    result += (IsPlayer) ? "You are holding " + RH.Name + " in your right hand.\n" : Name + " is holding " + RH.Name + " in their right hand.\n";
                }
            }
            if (RH == null && BothHands == null) result += (IsPlayer) ? "You are using your right fist as a weapon.\n" : Name + " is using their right fist as a weapon.\n";

            if (BothHands != null)
            {
                if (BothHands.Type == gameObjectTypes.weapon)
                {
                    result += (IsPlayer) ? "You are wielding " + BothHands.Name + " with both hands.\n" : Name + " is wielding " + BothHands.Name + " with both hands.\n";
                }
                else
                {
                    result += (IsPlayer) ? "You are holding " + BothHands.Name + " with both hands.\n" : Name + " is holding " + BothHands.Name + " with both hands.\n";
                }
            }
            // Take into account LF, RF, and BothFeet before claiming that the player is using fists as a weapon
            bool LFWeapon = false;
            bool RFWeapon = false;
            bool BothFeetWeapon = false;
            if (LF != null) if (LF.Type == gameObjectTypes.weapon) LFWeapon = true;
            if (RF != null) if (RF.Type == gameObjectTypes.weapon) RFWeapon = true;
            if (BothFeet != null) if (BothFeet.Type == gameObjectTypes.weapon) BothFeetWeapon = true;

            if (BothHands == null && LH == null && RH == null && !(LFWeapon || RFWeapon || BothFeetWeapon)) result += (IsPlayer) ? "You are using your fists as a weapon.\n" : Name + " is using their fists as a weapon.\n";

            // LF and RF are only valid for weapons.  BothFeet can be valid for either weapons or armor
            if (LF != null)
            {
                if (LF.Type == gameObjectTypes.weapon)
                {
                    result += (IsPlayer) ? "You are wielding " + LF.Name + " with your left foot.\n" : Name + " is wielding " + LF.Name + " with their left foot.\n";
                }
            }

            if (RF != null)
            {
                if (RF.Type == gameObjectTypes.weapon)
                {
                    result += (IsPlayer) ? "You are wielding " + RF.Name + " with your right foot.\n" : Name + " is wielding " + RF.Name + " with their left foot.\n";
                }
            }

            if (BothFeet != null)
            {
                if (BothFeet.Type == gameObjectTypes.weapon)
                {
                    result += (IsPlayer) ? "You are wielding " + BothFeet.Name + ".\n" : Name + " is wielding " + BothFeet.Name + ".\n";
                }
                else
                {
                    result += (IsPlayer) ? "You are wearing the " + BothFeet.Name + ".\n" : Name + " is wearing the " + BothFeet.Name + ".\n";
                }
            }

            return result;
        }

        private HashSet<GameObject> Inventory;

        public IEnumerable<string> InventoryItemNames()
        {
            foreach (GameObject g in Inventory)
            {
                yield return g.Name;
            }
        }

        /// <summary>
        /// AddItem adds an item to the Player's inventory.  That must be an Item, and not a player or a room
        /// </summary>
        /// <param name="g"></param>
        public void AddItemToInventory(GameObject g)
        {
            if (g.Type == gameObjectTypes.player || g.Type == gameObjectTypes.room)
            {
                throw new ArgumentException("Can't add a player or a room to a player's inventory");
            }

            Inventory.Add(g.Duplicate());
        }

        // It might be prudent to have unique keys for each item in the inventory.  This way a player
        // can have multiple items with the same name
        public void RemoveItemFromInventory(string name)
        {
            foreach (GameObject g in Inventory)
            {
                if (g.Name.Equals(name)) Inventory.Remove(g);
            }
        }

        /// <summary>
        /// GetNetSkills should be called whenever equipment changes.  This method ensures that equipped items
        /// have their appropriate skills added to the player's skill list.
        /// </summary>
        public void GetNewSkills()
        {
            foreach (GameObject g in GetEquippedItems())
            {
                bool found = false;
                foreach (Skill s in Skills)
                {
                    if (g.Type == gameObjectTypes.armor)
                    {
                        if (((Armor)g).AssociatedSkill.Name == s.Name){
                            found = true;
                        }
                    }
                    else if (g.Type == gameObjectTypes.weapon)
                    {
                        if (((Weapon)g).AssociatedSkill.Name == s.Name)
                        {
                            found = true;
                        }
                    }
                }
                if (found == false)
                {
                    if (g.Type == gameObjectTypes.armor) Skills.Add(((Armor)g).AssociatedSkill.Duplicate());
                    else Skills.Add(((Weapon)g).AssociatedSkill.Duplicate());
                }
            }
        }

        public GameObject GetItemInInventory(string name)
        {
            foreach (GameObject g in Inventory)
            {
                if (g.Name == name)
                {
                    return g;
                }
            }
            return null;
        }

        public string EquipArmor(string name)
        {
            GameObject g = GetItemInInventory(name);
            if (g == null) return "Could not equip " + name + " because it could not be found in the inventory.";
            else
            {
                if (g.Type == gameObjectTypes.armor)
                {
                    if (((Armor)g).Slot != armorSlots.shield)
                    {
                        Armor a = (Armor)g;
                        Armor equipped = GetEquippedArmor(a.Slot);
                        if (equipped == null)
                        {
                            a.Equipped = true;
                            GetNewSkills();
                            GenDesc();
                            return "You are now wearing " + a.Name + ".";
                        }
                        else
                        {
                            string result = " You removed " + equipped.Name + ".";
                            equipped.Equipped = false;
                            result += "\nYou are now wearing " + a.Name + ".";
                            a.Equipped = true;
                            GetNewSkills();
                            GenDesc();
                            return result;
                        }
                    }
                    else { GenDesc(); return "You must hold a shield in a hand."; }
                }
                else { GenDesc(); return "You cannot wear " + g.Name + "."; }
            }
        }

        public string EquipWeapon(string weapon, weaponSlots slot)
        {
            GameObject item = GetItemInInventory(weapon);
            if (item == null) return weapon + " could not be found.";
            else if (item.Type != gameObjectTypes.weapon)
            {
                return item.Name + " is not a weapon or shield.";
            }
            else return EquipWeaponFromInventory((Weapon)item, slot);
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            foreach (Skill s in Skills)
            {
                yield return s;
            }
        }

        public IEnumerable<SkillFamily> GetAllSkillFamilies()
        {
            foreach (SkillFamily s in SkillFamilies)
            {
                yield return s;
            }
        }

        private string EquipWeaponFromInventory(Weapon weapon, weaponSlots slot)
        {
            string result = "";
            //throw new NotImplementedException();
            if (weapon == null) return "There was no item to equip.\n";
            
            GameObject equipped = GetEquippedWeaponOrShield(slot);
            Armor equippedLegArmor = GetEquippedArmor(armorSlots.legs);

            if ((slot == weaponSlots.leftFoot || slot == weaponSlots.rightFoot || slot == weaponSlots.bothFeet) && equippedLegArmor != null)
            {
                equippedLegArmor.Equipped = false;
                result += "Removed " + equippedLegArmor.Name + ".\n";
            }

            if (equipped == null)
            {
                weapon.Equipped = true;
                weapon.Slot = slot;
                result += "You are now wielding " + weapon.Name + ".\n";
            }
            else
            {
                ((Weapon)equipped).Equipped = false;
                result += "Removed " + equipped.Name + ".\n";
                weapon.Equipped = true;
                result += "You are now wielding " + weapon.Name + ".\n";
            }

            // The goal here is to set up a single attack action which sets its delay based on equipped weapons.
            // This means we need to have two calls to SetAttackActionDelay which will determine the delay,
            // one when a player is created, and one here in EquipWeaponFromInventory.
            GetNewSkills();
            SetAttackActionDelay();
            GenDesc();
            return result;
        }

        public IEnumerable<GameObject> GetEquippedItems()
        {
            foreach (GameObject i in Inventory)
            {
                if (i.Type == gameObjectTypes.armor)
                {
                    if (((Armor)i).Equipped) yield return i;
                }
                else if (i.Type == gameObjectTypes.weapon)
                {
                    if (((Weapon)i).Equipped) yield return i;
                }
            }
        }

        public Armor GetEquippedArmor(armorSlots armorType)
        {
            foreach (GameObject i in GetEquippedItems())
            {
                if (i.Type == gameObjectTypes.armor)
                {
                    if (((Armor)i).Slot == armorType)
                    {
                        return (Armor)i;
                    }
                }
            }

            return null;
        }

        public GameObject GetEquippedWeaponOrShield(weaponSlots weaponType)
        {
            foreach (GameObject g in GetEquippedItems())
            {
                if (g.Type == gameObjectTypes.weapon)
                {
                    if (((Weapon)g).Slot == weaponType) return g;
                }
            }

            return null;
        }

        // A Player should have stats
        // When primary stats change, all Skill ChangeRanks method should be called with
        // a rankMod of 0.  This will ensure that the updated stats increases each Skill's mod.
        public int Dexterity { get; private set; }
        public int Strength { get; private set; }
        public int Stamina { get; private set; }
        public int Will { get; private set; }
        public int Health { get; private set; }
        // Health, Will and Stamina are all associated with Points that can be used by skills
        // and by attacks.  Using a skill will drain Stamina or Will points, getting damaged
        // will reduce Health points
        private int HP, HPMax, SP, SPMax, WP, WPMax;
        public double HPRatio { get; private set; }
        public double SPRatio { get; private set; }
        public double WPRatio { get; private set; }

        // damage can be either positive or negative and can affect any stat
        // 
        /// <summary>
        /// SetStat is used to modify all stats.  When a primary stat changes, the ChangeRanks 
        /// method is called for all skills with a rankMod of 0 to update the checkmodifier with
        /// the new stats.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="damage">Positive damage is added to the stat.  Negative damage is subtracted from the stat</param>
        /// <returns>If the damage taken is to one of the points, then return the value of the ratio. Otherwise, return 0.0</returns>
        public double SetStat(stats stat, int damage)
        {
            // When a player takes damage, update the appropriate stat.
            int mod = 0;
            switch (stat)
            {
                case stats.dexterity:
                    Dexterity = (Dexterity + damage > 0) ? Dexterity + damage : 1;
                    foreach (Skill s in GetAllSkills())
                    {
                        s.ChangeRank(this, 0);
                    }
                    break;
                case stats.health:
                    //Health = (Health + damage > 0) ? Health + damage : 1;
                    mod = 0;
                    if (Health + damage <= 0)
                    {
                        // When Health + damage <= 0, damage is negative.  Prevent a negative or 0 Health stat (which makes no sense).
                        // and determine how to modify HPMax, which starts at 1000 and goes up with each Health increase by 10.
                        // However, it is possible that HPMax has been increased with an item or potion, so don't remove that change.
                        mod = 1 - Health;
                        Health = 1;
                    }
                    else
                    {
                        Health = Health + damage;
                        mod = damage;
                    }
                    HPMax += mod * 10;
                    HPRatio = (double)HP / (double)HPMax;
                    foreach (Skill s in GetAllSkills())
                    {
                        s.ChangeRank(this, 0);
                    }
                    break;
                case stats.HP:
                    HP = (HP + damage > HPMax) ? HPMax : HP + damage;
                    if (HP < 0) HP = 0;
                    HPRatio = (double)HP / (double)HPMax;
                    return HPRatio;
                case stats.HPMax:
                    HPMax = (HPMax + damage > 1000) ? HPMax + damage : 1000;
                    HPRatio = (double)HP / (double)HPMax;
                    return HPRatio;
                case stats.SP:
                    SP = (SP + damage > SPMax) ? SPMax : SP + damage;
                    if (SP < 0) SP = 0;
                    SPRatio = (double)SP / (double)SPMax;
                    return SPRatio;
                case stats.SPMax:
                    SPMax = (SPMax + damage > 1000) ? SPMax + damage : 1000;
                    SPRatio = (double)SP / (double)SPMax;
                    return SPRatio;
                case stats.stamina:
                    //Stamina = (Stamina + damage > 0) ? Stamina + damage : 1;
                    mod = 0;
                    if (Stamina + damage <= 0)
                    {
                        // When Health + damage <= 0, damage is negative.  Prevent a negative or 0 Health stat (which makes no sense).
                        // and determine how to modify HPMax, which starts at 1000 and goes up with each Health increase by 10.
                        // However, it is possible that HPMax has been increased with an item or potion, so don't remove that change.
                        mod = 1 - Stamina;
                        Stamina = 1;
                    }
                    else
                    {
                        Stamina = Stamina + damage;
                        mod = damage;
                    }
                    SPMax += mod * 10;
                    SPRatio = (double)SP / (double)SPMax;
                    foreach (Skill s in GetAllSkills())
                    {
                        s.ChangeRank(this, 0);
                    }
                    break;
                case stats.strength:
                    Strength = (Strength + damage > 0) ? Strength + damage : 1;
                    foreach (Skill s in GetAllSkills())
                    {
                        s.ChangeRank(this, 0);
                    }
                    break;
                case stats.will:
                    //Will = (Will + damage > 0) ? Will + damage : 1;
                    mod = 0;
                    if (Will + damage <= 0)
                    {
                        // When Health + damage <= 0, damage is negative.  Prevent a negative or 0 Health stat (which makes no sense).
                        // and determine how to modify HPMax, which starts at 1000 and goes up with each Health increase by 10.
                        // However, it is possible that HPMax has been increased with an item or potion, so don't remove that change.
                        mod = 1 - Will;
                        Will = 1;
                    }
                    else
                    {
                        Will = Will + damage;
                        mod = damage;
                    }
                    WPMax += mod * 10;
                    WPRatio = (double)WP / (double)WPMax;
                    foreach (Skill s in GetAllSkills())
                    {
                        s.ChangeRank(this, 0);
                    }
                    break;
                case stats.WP:
                    WP = (WP + damage > WPMax) ? WPMax : WP + damage;
                    if (WP < 0) WP = 0;
                    WPRatio = (double)WP / (double)WPMax;
                    return WPRatio;
                case stats.WPMax:
                    WPMax = (WPMax + damage > 1000) ? WPMax + damage : 1000;
                    WPRatio = (double)WP / (double)WPMax;
                    return WPRatio;
                default:
                    throw new ArgumentException("The stat supplied is not currently supported");
            }

            return 0.0;
        }

        // skills should be initialized along with the player.
        private HashSet<Skill> Skills;
        private HashSet<SkillFamily> SkillFamilies;

        /// <summary>
        /// Can be used to create a Player Character (PC) or a Non-Player Character (NPC)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isTargetable">Set to False if the NPC cannot be attacked</param>
        /// <param name="skills"></param>
        /// <param name="skillFamilies"></param>
        /// <param name="isPlayer">Set to False if creating an NPC</param>
        /// <param name="r"></param>
        public Player(string name, bool isTargetable, IEnumerable<Skill> skills, IEnumerable<SkillFamily> skillFamilies, bool isPlayer, Random r)
            : base(gameObjectTypes.player, name, isTargetable)
        {
            this.Name = name;
            IsPlayer = isPlayer;

            this.Skills = new HashSet<Skill>();
            this.SkillFamilies = new HashSet<SkillFamily>();
            this.Inventory = new HashSet<GameObject>();

            R = r;            

            AutoAttack = true;
            AutoRetaliate = true;

            Strength = 5;//R.Next(10) + 1;
            Stamina = 5;// R.Next(10) + 1;
            Dexterity = 5;// R.Next(10) + 1;
            Health = 5;// R.Next(10) + 1;
            Will = 5;// R.Next(10) + 1;
            HP = HPMax = 1000 + Health * 10;
            HPRatio = (double)HP / (double)HPMax;
            SP = SPMax = 1000 + Stamina * 10;
            SPRatio = (double)SP / (double)SPMax;
            WP = WPMax = 1000 + Will * 10;
            WPRatio = (double)WP / (double)WPMax;
            
            foreach (SkillFamily sf in skillFamilies)
            {
                this.SkillFamilies.Add(sf.Duplicate());
            }

            foreach (Skill s in skills)
            {
                Skill skillToAdd = s.Duplicate();
                skillToAdd.SetMod(this);
                Skills.Add(skillToAdd);
            }

            DefendingAction = null;
            BuffingActions = new HashSet<PlayerBattleAction>();

            SetAttackActionDelay();
        }

        private void SetAttackActionDelay()
        {
            // Get the Attack Action from a player's Actions
            IAction attackAction = GetAction("Attack");
            if (attackAction.Type != actionTypes.battleAction)
            {
                throw new ArgumentException("Attack should always be a PlayerBattleAction.");
            }
            PlayerBattleAction attack = (PlayerBattleAction)attackAction;

            int count = 0;
            int delay = 0;

            foreach (Weapon w in GetWeapons("Attack"))
            {
                // Weapons should have a Delay property.
                count++;
                delay += w.Delay;
            }
            if (count > 1)
            {
                attack.SetDelay(delay / count);
            }
            else if (count == 1)
            {
                delay += 500;
                count++;
                attack.SetDelay(delay / count);
            }
            else
            {
                // Set the Attack Action delay to the default for Brawling.  500
                attack.SetDelay(500);
            }
            Console.WriteLine(Name + " Attack Delay set to " + attack.Delay.ToString());
        }

        /// <summary>
        /// Returns the skill with a given name.  Make sure to check for null on the returned Skill
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Skill GetSkill(string name)
        {
            foreach (Skill s in Skills)
            {
                if (s.Name.Equals(name)) return s;
            }

            return null;
        }

        public SkillFamily GetSkillFamily(string name)
        {
            foreach (SkillFamily s in SkillFamilies)
            {
                if (s.Name.Equals(name)) return s;
            }

            return null;
        }

        // Each skill has actions associated with it
        // We need to be able to get the actions available from each skill as necessary
        // So use the Skill.GetAllActions() IEnumerable to get them.

        //We also need to be able to get just the BattleActions from a given skill
        public IEnumerable<PlayerBattleAction> GetBattleActions(Skill s)
        {
            foreach (IAction a in s.GetAllActions())
            {
                if (a.Type == actionTypes.battleAction) yield return (PlayerBattleAction)a;
            }
        }

        public IAction GetAction(string name)
        {
            foreach (Skill s in GetAllSkills())
            {
                foreach (IAction a in s.GetAllActions())
                {
                    if (a.Name == name)
                    {
                        return a;
                    }
                }
            }

            return null;
        }

        public void PerformAction(IAction a)
        {
            switch (a.Type)
            {
                case actionTypes.battleAction:
                    PerformBattleAction((PlayerBattleAction)a);
                    break;
                case actionTypes.skillAction:
                    PerformSkillAction((SkillAction)a);
                    break;
                default:
                    throw new ArgumentException("That action type is not currently supported.");
            }
        }

        private void PerformBattleAction(PlayerBattleAction a)
        {
            if (a.CanPerform == true)
            {
                switch (a.BType)
                {
                    case battleActionTypes.attack:
                        PerformAttack((PlayerBattleAction)a);
                        break;
                    case battleActionTypes.buff:
                        PerformBuff((PlayerBattleAction)a);
                        break;
                    case battleActionTypes.defend:
                        PerformDefend((PlayerBattleAction)a);
                        break;
                    default:
                        throw new ArgumentException("That action type is not currently supported.");
                }
            }
        }

        private PlayerBattleAction DefendingAction;

        /// <summary>
        /// RemoveDefending Action should get called as soon as the DefendingAction.Timer elapses
        /// </summary>
        /// 
        public void RemoveDefendingAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            DefendingAction = null;
        }

        private void PerformDefend(PlayerBattleAction defendAction)
        {
            // A Defend action raises defense against a specific type of damage.  This should only last
            // Delay amount of time.  This means that a player can only be defending once at a time,
            // effects do not stack, and if another Defend is currently ongoing, that Defend should
            // either block the requested Defend, or be replaced by the current Defend
            // It is possible to have a Timer for effects that occur over time.

            if (defendAction.CanPerform)
            {
                if (DefendingAction != null) return;

                DefendingAction = defendAction;
                defendAction.CanPerform = false;
                defendAction.Timer.Elapsed += new System.Timers.ElapsedEventHandler(RemoveDefendingAction);
                defendAction.Timer.Start();
            }
        }

        // It is possible that a player would be able to have multiple Buff Actions
        // active at any given time.  This should probably be a collection.
        // It is also possible for a Buff to have a negative effect (i.e. poisons)
        private HashSet<PlayerBattleAction> BuffingActions;

        public void RemoveBuffingAction(object sender, System.Timers.ElapsedEventArgs e, PlayerBattleAction a)
        {
            BuffingActions.Remove(a);

            // TODO:
            // Need to actually perform the debuff.
        }

        private void PerformBuff(PlayerBattleAction buffingAction)
        {
            // A Buff action raises a certain stat for a specific amount of time.
            if (BuffingActions.Contains(buffingAction)) return;

            // TODO:
            // Need to actually perform the buff.

            BuffingActions.Add(buffingAction);
            buffingAction.CanPerform = false;
            buffingAction.Timer.Elapsed += (sender, e) => RemoveBuffingAction(sender, e, buffingAction);
            buffingAction.Timer.Start();
        }

        // Combat Skills:
        //  Defense Skills
        //  -   Parry - Reduces possibility of a hit - Requires a weapon/shield
        //  -   Dodge - Reduces possibility of a hit
        //  -   DamageReduction - Reduces damage dealt when hit
        //  Offense Skills
        //  -   Hit - Increases possibility of a hit
        //  -   Damage - Increases damage dealt when hit
        //  -   MultipleAttacks - Reduces multi-wielding penalties
        //  -   Critical - Increases Critical Chance and Critical Damage -- May not use this at all.
        //  Weapon Skills
        //  -   Bladed, Bludgeon, Unarmed
        //      -   All Weapon skills - increase possibility of hit, increase damage, increase critical chance and critical damage
        //  Armor Skills
        //  -   Armor, Shield, Unarmored
        //      -   All Armor skills - reduce possibility of hit, reduce damage

        // We want combat to occur regularly and we don't want it to be overly cumbersome
        // In order to handle this, we need to display Delay remaining time, or just tell the Player when they can perform
        // the action again.
        // Think WoW cooldowns.
        // AutoAttack should attack when Delay is over unless overridden by another Player Action.
        // Regardless of source of the attack (autoattack or otherwise) we need to perform the attack and see if the skills
        // are improved.

        private void PerformAttack(PlayerBattleAction a)
        {
            // Of primary concern here is that the playerBattleAction is null
            if (a == null) throw new ArgumentNullException("Attack Actions can not be null when attacking");
            if (a.Target == null) throw new ArgumentNullException("An Attack Action must have a non-null target");

            // Currently, only Players can be targetted with attacks.
            if (a.Target.Type != gameObjectTypes.player)
            {
                throw new ArgumentException("Attack actions must target players");
            }

            // TODO:
            // Need to take into account DefendingAction.

            // Roll for each Player.  Attacker and Defender both get rolls.  If an Attacker gets a 1, he always misses
            // Ties go to Defender.
            Player Defender = (Player)a.Target;
            int aRoll = R.Next(6) + 1;
            int hit = aRoll;
            int dRoll = Defender.R.Next(6) + 1;
            int block = dRoll;
            // If either player rolls a 6, they get to roll again
            while (aRoll == 6) { aRoll = R.Next(6) + 1; hit += aRoll; }
            while (dRoll == 6) { dRoll = Defender.R.Next(6) + 1; block += dRoll; }
            // Get Attacker's equipped weapons and determine a modifier
            hit += GetAttackModifier();
            // Get Defender's equipped weapons and armor and determine a modifier
            block += Defender.GetBlockModifier();

            //Console.WriteLine("{0} Hit value = {1}, {2} Block value = {3}", Name, hit, Defender.Name, block);
            // Next, start the timer.  This action cannot be performed again until the timer finishes.
            a.CanPerform = false;
            a.Timer.Start();
            
            // Now we have a hit roll and a block roll
            if (hit <= block)
            {
                // It was a miss
                // Failure produces more possibility of improvement than a success
                // When it is a hit, we need to increment the rankXP for all skills associated
                // This means we need to improve hit, damage, all weapon skills, and multiple attacks (if applicable)
                PerformHitImproveCheck(Defender, false);

                // There is also a chance for the defender to improve
                Defender.PerformDefenseImproveCheck(this, true);
            }
            else
            {
                // It was a hit
                PerformHitImproveCheck(Defender, true);

                // There is a chance for the defender to improve
                Defender.PerformDefenseImproveCheck(this, false);

                // Can't just do this.
                // Armor reduces damage. DamageReduction reduces damage
                // Damage skill increases damage
                // Will need to revamp this in the future.

                // Remember that when you call SetStat, SetStat adds the modifier to the stat.

                double multiplier = 1.0;

                if (Defender.HPRatio <= 1 && Defender.HPRatio > 0.9) { multiplier = 1.0; }

                else if (Defender.HPRatio <= 0.9 && Defender.HPRatio > 0.75) { multiplier = 1.25; }

                else if (Defender.HPRatio <= 0.75 && Defender.HPRatio > 0.5) { multiplier = 1.5; }

                else if (Defender.HPRatio <= 0.5 && Defender.HPRatio > 0.25) { multiplier = 2.0; }

                else if (Defender.HPRatio <= 0.25 && Defender.HPRatio > 0.1) { multiplier = 5.0; }

                else { multiplier = 10.0; }

                double HPModifier = -(hit - block);
                HPModifier *= multiplier;

                Defender.SetStat(stats.HP, (int)HPModifier);
                Console.WriteLine(Name + " hit " + Defender.Name + " for " + (-(int)HPModifier).ToString() + " damage.");
                Console.WriteLine(Defender.Name + " HP Ratio: " + Defender.HPRatio.ToString("F2"));
            }
        }

        private void PerformDefenseImproveCheck(Player Attacker, bool BlockSuccess)
        {
            HashSet<Skill> skillsToImprove = new HashSet<Skill>();
            int count = 0;
            foreach (Weapon w in GetWeapons("Parry"))
            {
                skillsToImprove.Add(GetSkill(w.AssociatedSkill.Name));
                count = count + 1;
            }

            skillsToImprove.Add(GetSkill("Dodge"));

            if (count >= 2)
            {
                skillsToImprove.Add(GetSkill("Multiple Attacks"));
                skillsToImprove.Add(GetSkill("Parry"));
            }
            else if (count == 0) skillsToImprove.Add(GetSkill("Brawling"));
            else skillsToImprove.Add(GetSkill("Parry"));

            foreach (Skill s in skillsToImprove)
            {
                Skill hitSkill = Attacker.GetSkill("Hit");
                // If brawling is being used, then we can check for dodge improvements twice (as the player
                // cannot use parry)
                if (count == 0 && s.Name.Equals("Dodge")) s.CheckForRepetitiveImprove(this, R, hitSkill.Mod, false);
                s.CheckForRepetitiveImprove(this, R, hitSkill.Mod, false);
            }
        }

        public void PerformHitImproveCheck(Player Defender, bool HitSuccess)
        {
            HashSet<Skill> skillsToImprove = new HashSet<Skill>();

            skillsToImprove.Add(GetSkill("Hit"));
            int count = 0;
            foreach (Weapon w in GetWeapons("Attack"))
            {
                skillsToImprove.Add(GetSkill(w.AssociatedSkill.Name));
                count = count + 1;
            }
            if (count >= 2) skillsToImprove.Add(GetSkill("Multiple Attacks"));
            else if (count == 0) skillsToImprove.Add(GetSkill("Brawling"));

            // As the RankXP of a given skill gets closer to the max XP for that rank, the possibility of a 
            // repetitive improve should decrease.  Ideally, we want the range for chance improvements to be
            // around 0.5 for RankXP==0, and 0.1 for RankXP==(MaxXP-1)
            // Log10(3) ~0.5, Log10(1.025) ~0.01
            // The range for an improvement should be 1.025 to 3
            foreach (Skill s in skillsToImprove)
            {
                Skill parry = Defender.GetSkill("Parry");
                Skill dodge = Defender.GetSkill("Dodge");
                int higherDefenderSkillMod = (parry.Ranks >= dodge.Ranks) ? parry.Mod : dodge.Mod;
                s.CheckForRepetitiveImprove(this, R, higherDefenderSkillMod, HitSuccess);
            }
        }

        public IEnumerable<Weapon> GetWeapons(string attackOrParry)
        {
            foreach (GameObject i in GetEquippedItems())
            {
                if (attackOrParry.Equals("Attack")) { if (i.Type == gameObjectTypes.weapon) yield return (Weapon)i; }
                else if (attackOrParry.Equals("Parry"))
                {
                    if (i.Type == gameObjectTypes.weapon)
                    {
                        Weapon w = (Weapon)i;
                        if (w.Slot != weaponSlots.bothFeet && w.Slot != weaponSlots.leftFoot && w.Slot != weaponSlots.rightFoot) yield return w;
                    }
                }
            }
        }

        //public double AttackRatio { get; set; }

        /// <summary>
        /// GetAttackModifier returns a modifier based on Weapon, Hit, and Multiple Attacks skills.
        /// We aren't really handling the Brawling case very well.  Current paradigm is to determine
        /// which weapons (if any) are currently equipped and performing the attacks in that manner.
        /// This isn't really valid, though.  What we should do, is take the 
        /// </summary>
        /// <returns></returns>
        public int GetAttackModifier()
        {
            int result = 0;
            Skill Hit = GetSkill("Hit");
            List<Weapon> attackingWeapons = new List<Weapon>();
            foreach (Weapon w in GetWeapons("Attack"))
            {
                attackingWeapons.Add(w);
            }
            // We know which weapon we are currently using.
            // This means that we are going to penalize the attack based on the number of weapons equipped.
            // From the block logic, we know that we want to have a logarithmicly increasing percentage
            if (attackingWeapons.Count == 1)
            {
                // single attacks
                Weapon w = attackingWeapons[0];
                Skill s = GetSkill(w.AssociatedSkill.Name);
                result += s.Mod + Hit.Mod;
            }
            else if (attackingWeapons.Count > 1)
            {
                foreach (Weapon w in attackingWeapons)
                {
                    // multiple attacks
                    Skill s = GetSkill(w.AssociatedSkill.Name);
                    result += (int)((double)s.Mod / (double)attackingWeapons.Count);
                    Skill MA = GetSkill("Multiple Attacks");
                    double hit = (double)Hit.Mod / (double)attackingWeapons.Count;
                    double weaponSkillMultiplier = 0.0;
                    if (MA.Ranks >= 10)
                    {
                        hit *= Math.Log10((double)MA.Ranks);
                        weaponSkillMultiplier = Math.Log10((double)MA.Ranks);
                    }
                    else
                    {
                        weaponSkillMultiplier = (double)(1.0 / (double)(attackingWeapons.Count));
                    }
                    result += (int)hit;
                    result += (int)(weaponSkillMultiplier * ((double)s.Ranks) / (2.0 * (double)attackingWeapons.Count));
                }
            }
            else
            {
                // a player can attack unarmed
                Skill s = GetSkill("Brawling");
                result += Hit.Mod + s.Mod;
            }
            
            return result;
        }

        private double DefenseRatio { get; set; }

        /// <summary>
        /// GetBlockModifier returns a check modifier.  It possibly contains Shield, Weapon, Parry, Dodge skills
        /// </summary>
        /// <returns></returns>
        public int GetBlockModifier()
        {
            int result = 0;
            // Blocking is essentially passive - It doesn't require a target.
            // This means that Armor is not used for Blocking, only for preventing damage.
            // Parry and Dodge provide the stat mod, everything else supplies their ranks.
            // Can't use Parry unless you have a weapon or a shield
            // While Parry and Dodge are both defense skills, only use one.
            // Eventually, a PC should be able to customize the ratio of Dodge/Parry
            // For now, though, use a hard set DefenseRatio of 0.55
            Skill Parry = GetSkill("Parry");
            Skill Dodge = GetSkill("Dodge");

            result += (int)(((double)Dodge.Mod) * (1 - DefenseRatio));
            
            // Both Parry and Dodge use the same SkillFamily which is included in Skill.Mod
            List<Weapon> ParryingItems = new List<Weapon>();
            foreach (Weapon w in GetWeapons("Parry"))
            {
                ParryingItems.Add(w);
            }

            // If multiple weapons are being used for Parry, then Multiple Attacks comes into play.
            // If no weapons are being used, then Parry doesn't get added
            if (ParryingItems.Count > 1)
            {
                // Efficacy should be low for lower ranks
                //      2 - Max Parry of 50%, Max Weapon/Shield Ranks use of 25%/weapon
                //      3 - Max Parry of 33%, Max Weapon/Shield Ranks use of 16%/weapon
                //      4 - Max Parry of 25%, Max Weapon/Shield Ranks use of 12.5%/weapon
                // Ranks for skills start at 1
                // So take (parry - 1)/(ParryingItems.Count) to get initial parry value
                // Now how does MA come into play? when MA is high enough, we should be able to get full parry and full Weapon/Shield Ranks
                // Take Log10(MA) to get the multiplier.  This means at MA == 100, we can have full parry for two weapons
                // while it takes MA == 10000 to have fully parry for 4 weapons.
                // If someone with MA == 10000 is using two weapons, they will have a 2x Parry bonus.  This seems pretty good.
                double parry = (double)Parry.Mod / (double)ParryingItems.Count;
                Skill MA = GetSkill("Multiple Attacks");
                if (MA.Ranks >= 10) parry *= Math.Log10((double)MA.Ranks);
                result += (int)(parry*DefenseRatio); // We need to add in the stat modifiers only once

                // Now that we have the Parry modifier, we need to add in the Weapon skills modifier
                // At MA == 100, we will have full parry, but only 50% of the player's weapon skills added.
                double weaponSkillMultiplier = (MA.Ranks >= 10) ? Math.Log10((double)MA.Ranks) : (double)(1.0/(double)ParryingItems.Count);
                
                foreach (Weapon i in ParryingItems)
                {
                    Skill s = GetSkill(i.AssociatedSkill.Name);
                    if (s == null) throw new Exception("There was a problem.  Somewhere along the lines the skill for " + i.Name + " got lost.");
                    result += (int)(weaponSkillMultiplier * ((double)s.Ranks) / (2.0 * (double)ParryingItems.Count));
                }
            }
            else if (ParryingItems.Count == 1)
            {
                // When creating the Block modifier, we only need to account for
                // the Parry Mod and the weapon ranks
                result += (int)((double)Parry.Mod * DefenseRatio);
                foreach (Weapon i in ParryingItems)
                {
                    Skill s = GetSkill((i.AssociatedSkill).Name);
                    if (s == null) throw new Exception("There was a problem.  Somewhere along the lines the skill for " + i.Name + " got lost.");
                    result += s.Ranks;
                }
            }
            else
            {
                // If no weapons are equipped, then use the Brawling skill and the Dodge Skill again.
                Skill s = GetSkill("Brawling");
                result += s.Mod + Dodge.Mod; // This means that Dodge will provide (1 + (1-DefenseRatio)) * Dodge.Mod-a hefty bonus.
            }

            return result;
        }

        public void PerformSkillAction(SkillAction a)
        {

        }
    }
}
