using System;

namespace PR424_Shapovalov
{
    public static class Battle
    {
        private static Random rnd = new Random();

        public static void Fight(Hero hero, Enemy enemy)
        {
            Console.WriteLine($"На вас напал: {enemy.GetType().Name} (HP: {enemy.Health}, ATK: {enemy.Attack})");

            if (hero.Weapon.Range)
            {
                Console.WriteLine("Вы открываете бой выстрелом издалека!");
                HeroAttack(hero, enemy, 1m);
                if (!enemy.IsAlive)
                {
                    Console.WriteLine("Враг повержен ещё до того, как подошёл!");
                    return;
                }
            }

            bool heroFrozen = false;

            while (hero.IsAlive && enemy.IsAlive)
            {
                bool isDefending = false;

                if (heroFrozen)
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    heroFrozen = false;
                }
                else
                {
                    int action = ChooseAction(hero.Weapon.Range);

                    if (action == 1)
                    {
                        HeroAttack(hero, enemy, 1m);
                        if (!enemy.IsAlive) break;
                    }
                    else if (action == 2)
                    {
                        isDefending = true;
                        Console.WriteLine("Вы занимаете оборонительную стойку (броня эффективнее вдвое).");
                    }
                    else
                    {
                        isDefending = true;
                        Console.WriteLine("Вы стреляете из-за щита (урон снижен вдвое).");
                        HeroAttack(hero, enemy, 0.5m);
                        if (!enemy.IsAlive) break;
                    }
                }
                EnemyAttack(hero, enemy, ref heroFrozen, isDefending);

                Console.WriteLine($"  Ваше HP: {hero.Health}/{hero.MaxHealth} | HP врага: {enemy.Health}");
            }

            Console.WriteLine(hero.IsAlive ? "Враг повержен!" : "Вы погибли...");
        }
        private static int ChooseAction(bool hasRangedWeapon)
        {
            while (true)
            {
                Console.Write(hasRangedWeapon
                    ? "Ваш ход (1 — атаковать, 2 — стойка, 3 — выстрел из-за щита [-50% урона]): "
                    : "Ваш ход (1 — атаковать, 2 — защищаться): ");
                string input = Console.ReadLine();
                if (input == "1") return 1;
                if (input == "2") return 2;
                if (input == "3" && hasRangedWeapon) return 3;
                Console.WriteLine("Введите корректный номер действия.");
            }
        }

        private static void HeroAttack(Hero hero, Enemy enemy, decimal damageFactor)
        {
            bool isCrit = rnd.Next(100) < 20;
            decimal raw = hero.Weapon.Attack * damageFactor * (1 - enemy.Defense / 100m);
            int damage = Math.Max(1, (int)Math.Round(raw * (isCrit ? 2m : 1m)));
            enemy.Health -= damage;

            Console.WriteLine(isCrit? $"  КРИТ! Вы наносите {damage} урона!" : $"  Вы наносите {damage} урона.");
        }

        private static void EnemyAttack(Hero hero, Enemy enemy, ref bool heroFrozen, bool heroDefending)
        {
            if (enemy.FreezeChance > 0 && rnd.NextDouble() < (double)enemy.FreezeChance)
            {
                if (hero.Armor.Magic)
                {
                    Console.WriteLine("  Магическая броня защитила вас от заморозки!");
                }
                else
                {
                    heroFrozen = true;
                    Console.WriteLine("  Враг замораживает вас!");
                }
            }

            bool isCrit = rnd.NextDouble() < (double)enemy.CritChance;
            decimal raw = enemy.Attack * (isCrit ? 2m : 1m);

            if (enemy.IgnoresArmor)
            {
                int dmg = Math.Max(1, (int)Math.Round(raw));
                hero.TakeDamage(dmg);
                Console.WriteLine($"  Враг игнорирует броню и наносит {dmg} урона!");
                return;
            }

            double blockFactor = 0.7 + rnd.NextDouble() * 0.3;
            decimal armorDefense = hero.Armor.Defense * (heroDefending ? 2m : 1m);
            decimal blocked = Math.Min(armorDefense * (decimal)blockFactor, 100m);
            int damage = Math.Max(1, (int)Math.Round(raw * (1 - blocked / 100m)));
            hero.TakeDamage(damage);

            Console.WriteLine(isCrit ? $"  Враг наносит КРИТИЧЕСКИЙ удар: {damage} урона (блок {blocked}%)!" : $"  Враг наносит {damage} урона (блок {blocked}%).");
        }
    }
}
