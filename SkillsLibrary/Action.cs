using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace GameLibrary
{
    public interface IAction
    {
        string Name { get; }
        int Delay { get; }
        actionTypes Type { get; }
        int CheckModifier { get; }

        void SetDelay(int i);
        void SetCheckModifier(int i);
        IAction Duplicate();
    }

    public class PlayerBattleAction : IAction
    {
        // A BattleAction is an action that takes place within a battle
        // What types of BattleActions?
        //      Attack:  Requires a target.  Attempts to hit and subsequently deal damage to target
        //      Defend:  Prepare for incoming attacks.  Adds a bonus to defense rolls
        //      Buff:    Requires a target.  Perform some type of buffing to aid in next attack(s)
        //      Support: Requires a target.  Perform some type of healing
        //      Item:    Requires a target.  This allows a change in equipment, use of a potion, etc.

        // Is the name the only identifying feature?  What else should the class know about?
        // An action is not performed by a skill, it is only contained by the skill.
        // Therefore, the action itself doesn't know anything about what it is doing
        // This means that the player and Game must determine what to do with the action once it is
        // requested.

        // Damage is relative, and can be negative or positive.
        // Negative damage indicates healing
        // Positive damage indicates damage.
        public string Name { get; private set; }

        // There is a certain amount of time that must pass after the action
        // is initiated before the player can perform another action
        // An Action has a delay
        public int Delay { get; private set; }
        public battleActionTypes BType { get; private set; }
        public actionTypes Type { get; private set; }

        // Damage is relative, and can be negative or positive.
        // Negative damage indicates healing
        // Positive damage indicates damage.
        public int Damage { get; private set; }

        // checkModifier is obtained and modified whenever a skill's modifier
        // is changed.
        // This means that when a stat is changed, we need to go through the Player's skills
        // and perform a SetMod
        public int CheckModifier { get; private set; }
        public void SetCheckModifier(int i) { CheckModifier = i; }

        public void SetDamage(int i) { Damage = i; }
        public void SetDelay(int i) { Delay = i; Timer.Interval = (double)Delay; }

        // A BattleAction has a target
        public GameObject Target;
        public PlayerBattleAction(string name, int delay, battleActionTypes bType)
        {
            this.Name = name;
            this.Delay = delay;
            this.BType = bType;
            this.Target = null;
            Type = actionTypes.battleAction;
            CanPerform = true;
            Timer = new System.Timers.Timer((double)delay);
            Timer.Elapsed += new System.Timers.ElapsedEventHandler(ResetCanPerform);
        }

        public bool CanPerform;
        public void ResetCanPerform(object sender, System.Timers.ElapsedEventArgs e)
        {
            Timer.Stop();
            CanPerform = true;
        }

        public System.Timers.Timer Timer;

        public virtual IAction Duplicate()
        {
            PlayerBattleAction result = new PlayerBattleAction(Name, Delay, BType);
            
            return result;
        }

        // Can an action be performed without the Player performing the action?
        // The Player's Action check modifier is calculated by the Skill when
        // Ranks and Stats change.  This means that damage is independent of
        // weapons, and is entirely a property of skill and stats.
        // How then is damage calculated?  Weapon Skill vs. Armor Skill?
        // The Action itself can't deal damage, only the player can deal damage.
    }

    public class BuffAction : PlayerBattleAction
    {
        public System.Timers.Timer BuffTimer;
        public int BuffDelay { get; private set; }
        public void SetBuffDelay(int i) { BuffDelay = i; BuffTimer.Interval = (double)i; }
        public stats Stat { get; private set; }
        // BuffActions can only be performed on Players

        public void PerformBuff()
        {
            // Since BuffActions can only be performed on Players, the Target must be set and unset.
            Player p = (Player)Target;
            if (p != null)
            {
                p.SetStat(Stat, Damage);
            }
            else
            {
                throw new ArgumentNullException("The target of a BuffAction cannot be null.");
            }
        }

        public void PerformDeBuff()
        {
            // Since BuffActions can only be performed on Players, the Target must be set and unset.
            Player p = (Player)Target;
            if (p != null)
            {
                p.SetStat(Stat, -Damage);
            }
            else
            {
                throw new ArgumentNullException("The target of a BuffAction cannot be null.");
            }
        }

        public BuffAction(string name, int actionDelay, int buffDelay, stats stat, int damage)
            : base(name, actionDelay, battleActionTypes.buff)
        {
            Stat = stat;
            BuffTimer = new System.Timers.Timer((double)buffDelay);
            BuffDelay = buffDelay;
            SetDamage(damage);
        }

        public override IAction Duplicate()
        {
            PlayerBattleAction result = new BuffAction(Name, Delay, BuffDelay, Stat, Damage);

            return result;
        }
    }

    public class SkillAction : IAction
    {
        public string Name { get; private set; }
        public int Delay { get; private set; }
        public actionTypes Type { get; private set; }
        public GameObject Target;
        public int CheckModifier { get; private set; }

        public void SetDelay(int i) { Delay = i; }
        public void SetCheckModifier(int i) { CheckModifier = i; }

        public SkillAction(string name, int delay)
        {
            this.Name = name;
            this.Delay = delay;
            Type = actionTypes.skillAction;
            this.Target = null;
        }

        public IAction Duplicate()
        {
            IAction result = new SkillAction(Name, Delay);
            return result;
        }
    }
}
