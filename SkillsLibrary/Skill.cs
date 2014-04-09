using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace GameLibrary
{
    public class Skill
    {
        // A Skill has a name
        public string Name { get; private set; }
        // A Skill has a SkillFamily
        public SkillFamily Family { get; private set; }
        // A Skill has a set of actions that can be performed
        HashSet<IAction> Actions;
        // Player skill ranks are stored within the player
        public Skill(string name, SkillFamily family, IEnumerable<IAction> actions, bool repetitiveImprove)
        {
            this.Actions = new HashSet<IAction>();
            this.Name = name;
            this.Family = family;
            this.Ranks = 0;

            foreach (IAction a in actions)
            {
                this.Actions.Add(a.Duplicate());
            }

            RepetitiveImprove = repetitiveImprove;
            RankXP = 0;
        }

        public IEnumerable<IAction> GetAllActions()
        {
            foreach (IAction a in Actions)
            {
                yield return a;
            }
        }
        public int Ranks { get; private set; }
        public bool RepetitiveImprove;
        public int RankXP;
        public int Mod { get; private set; }

        public void SetMod(Player p)
        {
            Mod = Ranks + Family.Ranks;

            switch (Family.Primary)
            {
                case stats.dexterity:
                    Mod += (int)(Family.Ratio * (double)p.Dexterity);
                    break;
                case stats.health:
                    Mod += (int)(Family.Ratio * (double)p.Health);
                    break;
                case stats.stamina:
                    Mod += (int)(Family.Ratio * (double)p.Stamina);
                    break;
                case stats.strength:
                    Mod += (int)(Family.Ratio * (double)p.Strength);
                    break;
                case stats.will:
                    Mod += (int)(Family.Ratio * (double)p.Will);
                    break;
            }

            switch (Family.Secondary)
            {
                case stats.dexterity:
                    Mod += (int)((1.0 - Family.Ratio) * (double)p.Dexterity);
                    break;
                case stats.health:
                    Mod += (int)((1.0 - Family.Ratio) * (double)p.Health);
                    break;
                case stats.stamina:
                    Mod += (int)((1.0 - Family.Ratio) * (double)p.Stamina);
                    break;
                case stats.strength:
                    Mod += (int)((1.0 - Family.Ratio) * (double)p.Strength);
                    break;
                case stats.will:
                    Mod += (int)((1.0 - Family.Ratio) * (double)p.Will);
                    break;
            }
        }

        public void ChangeRank(Player p, int rankMod)
        {
            Ranks += rankMod;
            SetMod(p);
            foreach (IAction a in Actions)
            {
                a.SetCheckModifier(Mod);
            }
            RankXP = 0;
        }

        // Idea: incorporate success vs. failure improvement.  Success should grant ~15% of normal chance
        // failur should grant 100% of normal chance.
        public void CheckForRepetitiveImprove(Player P, Random R, int opposingSkillMod, bool isSuccess)
        {
            int max = (Ranks < 10) ? Ranks * 10 : (int)Math.Pow((double)2.0, (double)(Ranks - 3));
            if (max <= 0) max = 10;
            double possibility = 1.0 + 2.0 * ((double)max - (double)RankXP) / (double)max; // scale from 1-3
            double chance = 1.0 / (Math.Log10(possibility)); // scale from approximately 2 to arbitrarily large number (if rankxp is high enough)
            // This possibility of chance being negative is small and implies an overflow.

            if (opposingSkillMod != 0)
            {
                if (opposingSkillMod > Mod)
                {
                    // the chance to improve is higher, so divide the chance before converting to an int
                    chance = chance * ((double)opposingSkillMod / ((double)opposingSkillMod + (double)Mod));
                }
                else if (opposingSkillMod < Mod)
                {
                    // the chance to improve is lower, so multiply the chance before converting to an int
                    chance = chance * ((double)opposingSkillMod + (double)Mod) / (double)opposingSkillMod;
                }
            }

            // the boolean isSuccess multiplies the value to be input to the R.Next method.  This gives the user
            // approximately 15% chance of improving vs a failure improvement check.
            if (isSuccess) chance *= 7.0;

            //Console.WriteLine(possibility.ToString() + " => " + chance.ToString());
            int chanceConverted = (chance < 0) ? 0 - (int)chance : (int)chance;
            int rand = R.Next(chanceConverted) + 2;
            //Console.WriteLine(rand + " " + chanceConverted);
            if (rand >= chanceConverted)
            {
                RankXP += 1;
                //Console.WriteLine(P.Name + " - " + Name + " RankXP: " + RankXP);
                if (RankXP >= max)
                {
                    ConsoleColor currentColor = Console.ForegroundColor;
                    if (P.IsPlayer) Console.ForegroundColor = ConsoleColor.Cyan;
                    else Console.ForegroundColor = ConsoleColor.DarkYellow;

                    ChangeRank(P, 1);

                    Console.WriteLine("{0}: {1} has improved. Ranks: {2}", P.Name, Name, Ranks);
                    Console.ForegroundColor = currentColor;
                }
            }
        }

        public Skill Duplicate()
        {
            return new Skill(Name, Family, Actions, RepetitiveImprove);
        }
    }

    public class SkillFamily
    {
        // A SkillFamily has a name
        public string Name { get; private set; }
        //public families type;
        // A SkillFamily has exactly two stats that are important to it, a Primary, and a Secondary
        public stats Primary { get; private set; }
        public stats Secondary { get; private set; }
        // A SkillFamily has a ratio of that importance for base modifiers on skills
        //      A Player has a SkillFamily which uses 3/4 Strength and 1/4 Dexterity
        //      The base modifier for any skill within that family is 3/4 of Player.Strength and 1/4 of Player.Dexterity
        // Ratio is the percentage of the modifier which is based on Primary
        //      Thus, 1 - Ratio gives the percentage of the modifier which is based on Secondary
        public double Ratio { get; private set; }

        // A SkillFamily doesn't contain any information that is specific.  This means that
        //      SkillFamily information is the responsibility of the program using this library.
        //      So lookups occur from the interface and not from within this library.
        //      SkillFamily ranks have the effect of adding 1 to any skill rank check of it's child skills
        public SkillFamily(string name, stats primary, stats secondary, double ratio)
        {
            this.Name = name;
            this.Primary = primary;
            this.Secondary = secondary;
            this.Ratio = ratio;
            this.Ranks = 0;
        }

        public int Ranks { get; private set; }

        /// <summary>
        /// Changes a Player's SkillFamily Ranks.  Ensure that after this is called, PlayerSpecificSkill.SetMod is called.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="i"></param>
        public void ChangeRank(int i)
        {
            Ranks += i;
        }

        public SkillFamily Duplicate()
        {
            return new SkillFamily(Name, Primary, Secondary, Ratio);
        }
    }
}
