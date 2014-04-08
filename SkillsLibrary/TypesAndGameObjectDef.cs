using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace GameLibrary
{
    public enum gameObjectTypes
    {
        player,
        room,
        weapon,
        armor,
        consumable
    }

    // Are these enough stats?
    // health has a direct effect on the amount of
    // damage that a player can take.
    public enum stats
    {
        dexterity,
        strength,
        stamina,
        will,
        health,
        HP,
        SP,
        WP,
        HPMax,
        SPMax,
        WPMax
    }

    public enum families
    {
        physicalAttack,
        physicalDefense,
        rangedAttack,
        magic,
        language,
        knowledge,
        thief,
        perception,
        crafting
    }

    public enum actionTypes
    {
        battleAction,
        skillAction
    }

    public enum battleActionTypes
    {
        attack,
        defend,
        buff,
        support
    }

    public enum weaponSlots
    {
        leftHand,
        rightHand,
        leftFoot,
        rightFoot,
        bothHands,
        bothFeet
    }

    public enum armorSlots
    {
        head,
        chest,
        arms,
        legs,
        shield
    }

    public enum damageTypes
    {
        bludgeon,
        slash,
        pierce,
        fire,
        ice,
        electric,
        magic,
    }

    public enum equipmentEnhancements
    {
        bless,
        curse,
        none
    }

    public abstract class GameObject
    {
        public abstract string Examine();
        public gameObjectTypes Type { get; private set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = (string.IsNullOrEmpty(value)) ? "" : value; }
        }
        public bool IsTargetable { get; private set; }

        public GameObject(gameObjectTypes type, string name, bool targetable)
        {
            Type = type;
            Name = name;
            IsTargetable = targetable;
        }

        public abstract GameObject Duplicate();
    }
}
