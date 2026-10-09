using System;

namespace PR424_Shapovalov
{
    public abstract class Enemy
    {
        public abstract int Health { get; set; }
        public abstract int Attack { get; set; }
        public abstract decimal Defense { get; set; }

        public virtual bool IgnoresArmor => false;
        public virtual decimal FreezeChance => 0m;
        public virtual decimal CritChance => 0m;
    }

    public class Goblin : Enemy
    {
        public const decimal BaseCritChance = 0.15m;

        public override int Health { get; set; }
        public override int Attack { get; set; }
        public override decimal Defense { get; set; }
        public override decimal CritChance => BaseCritChance;

        public Goblin(int health, int attack, decimal defense)
        {
            Health = health;
            Attack = attack;
            Defense = defense;
        }
    }

    public class Skeleton : Enemy
    {
        public override int Health { get; set; }
        public override int Attack { get; set; }
        public override decimal Defense { get; set; }
        public override bool IgnoresArmor => true;

        public Skeleton(int health, int attack, decimal defense)
        {
            Health = health;
            Attack = attack;
            Defense = defense;
        }
    }

    public class Mage : Enemy
    {
        public const decimal BaseFreezeChance = 0.15m;

        public override int Health { get; set; }
        public override int Attack { get; set; }
        public override decimal Defense { get; set; }
        public override decimal FreezeChance => BaseFreezeChance;

        public Mage(int health, int attack, decimal defense)
        {
            Health = health;
            Attack = attack;
            Defense = defense;
        }
    }

    public class VVG : Goblin
    {
        public override decimal CritChance => BaseCritChance + 0.10m;

        public VVG(int hp, int atk, decimal def)
            : base((int)Math.Round(hp * 2.0m), (int)Math.Round(atk * 1.5m), def * 1.2m)
        {
        }
    }

    public class Kovalsky : Skeleton
    {
        public Kovalsky(int hp, int atk, decimal def)
            : base((int)Math.Round(hp * 2.5m), (int)Math.Round(atk * 1.3m), def * 1.4m)
        {
        }
    }
    public class ArchmageCpp : Mage
    {
        public override decimal FreezeChance => BaseFreezeChance + 0.10m;

        public ArchmageCpp(int hp, int atk, decimal def)
            : base((int)Math.Round(hp * 1.8m), (int)Math.Round(atk * 1.6m), def * 1.1m)
        {
        }
    }

    public class PestovSMinusMinus : Skeleton
    {
        public override decimal FreezeChance => Mage.BaseFreezeChance + 0.15m;

        public PestovSMinusMinus(int hp, int atk, decimal def)
            : base((int)Math.Round(hp * 1.3m), (int)Math.Round(atk * 1.8m), def * 0.6m)
        {
        }
    }
    public static class EnemyGenerator
    {
        private static Random rnd = new Random();

        public static Enemy Generate()
        {
            int hp = rnd.Next(40, 81);
            int atk = rnd.Next(8, 18);
            int def = rnd.Next(5, 21);
            int roll = rnd.Next(100);

            if (roll < 10)
            {
                if (roll < 3) return new VVG(hp, atk, def);
                if (roll < 5) return new Kovalsky(hp, atk, def);
                if (roll < 7) return new ArchmageCpp(hp, atk, def);
                return new PestovSMinusMinus(hp, atk, def);
            }

            int type = rnd.Next(3);
            if (type == 0) return new Goblin(hp, atk, def);
            if (type == 1) return new Skeleton(hp, atk, def);
            return new Mage(hp, atk, def);
        }
    }
}