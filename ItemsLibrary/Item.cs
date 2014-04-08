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
        public weaponSlots Slot { get; private set; }
        public damageTypes DType { get; private set; }

        public Weapon(string name, string desc, weaponSlots slot, damageTypes dType, equipmentEnhancements enhanced, Skill s)
            : base(name, gameObjectTypes.weapon, enhanced, s, desc)
        {
            this.Slot = slot;
            this.DType = dType;
        }
    }

    public class Armor : Equipment
    {
        public armorSlots Slot;

        public Armor(string name, string desc, armorSlots slot, equipmentEnhancements enhanced, Skill s)
            : base(name, gameObjectTypes.armor, enhanced, s, desc)
        {
            this.Name = name;
            this.Slot = slot;
        }
    }

    public class Shield : Armor
    {
        /// <summary>
        /// Constraints for shield slots is to be either LH, RH, or Both Hands
        /// </summary>
        public weaponSlots ShieldSlot;

        public Shield(string name, string desc, weaponSlots shieldSlot, equipmentEnhancements enhanced, Skill s)
            : base(name, desc, armorSlots.shield, enhanced, s)
        {
            this.ShieldSlot = shieldSlot;
        }
    }
}
