using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace GameLibrary
{
    public class Consumable : GameObject
    {
        public string Desc { get; private set; }

        public override string Examine() { return Desc; }

        public Consumable(string name, string desc)
            : base(gameObjectTypes.consumable, name, false)
        {
            this.Desc = desc;
        }

        public override GameObject Duplicate()
        {
            //throw new NotImplementedException();
            Consumable result = new Consumable(Name, Desc);
            return result;
        }
    }

    public abstract class Equipment : GameObject
    {
        public equipmentEnhancements Enhanced { get; private set; }

        public void EnhanceEquipment(equipmentEnhancements enh)
        {
            Enhanced = enh;
        }

        public Skill AssociatedSkill { get; private set; }

        private string _Desc;
        public string Desc
        {
            get { return _Desc; }
            private set { _Desc = (string.IsNullOrEmpty(value)) ? "" : value; }
        }

        public override string Examine() { return Desc; }

        // the Player will have to perform checking to verify that the weapon can be equipped
        public bool Equipped;

        public Equipment(string name, gameObjectTypes type, equipmentEnhancements enh, Skill s, string desc)
            : base(type, name, false)
        {
            Enhanced = enh;
            AssociatedSkill = s;
            Desc = desc;
            Equipped = false;
        }
    }

    public class Weapon : Equipment
    {
        public weaponSlots Slot { get; set; }
        public damageTypes DType { get; private set; }
        public bool IsShield { get; private set; }

        public override GameObject Duplicate()
        {
            return new Weapon(Name, Desc, Slot, DType, Enhanced, AssociatedSkill.Duplicate(), IsShield);
        }

        public Weapon(string name, string desc, weaponSlots slot, damageTypes dType, equipmentEnhancements enhanced, Skill s, bool isShield)
            : base(name, gameObjectTypes.weapon, enhanced, s, desc)
        {
            this.Slot = slot;
            this.DType = dType;
            this.IsShield = isShield;
        }
    }

    public class Armor : Equipment
    {
        public armorSlots Slot;

        public override GameObject Duplicate()
        {
            return new Armor(Name, Desc, Slot, Enhanced, AssociatedSkill.Duplicate());
        }

        public Armor(string name, string desc, armorSlots slot, equipmentEnhancements enhanced, Skill s)
            : base(name, gameObjectTypes.armor, enhanced, s, desc)
        {
            this.Name = name;
            this.Slot = slot;
        }
    }

    public class Shield : Weapon
    {
        /// <summary>
        /// Constraints for shield slots is to be either LH, RH, or Both Hands
        /// </summary>
        public Shield(string name, string desc, weaponSlots shieldSlot, equipmentEnhancements enhanced, Skill s)
            : base(name, desc, shieldSlot, damageTypes.bludgeon, enhanced, s, true) //base(name, desc, armorSlots.shield, enhanced, s)
        {
        }
    }
}
