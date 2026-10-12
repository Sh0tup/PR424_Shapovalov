using System;

namespace PR424_Shapovalov
{
    public static class Battle
    {
        private static Random rnd = new Random();

        public static void Fight(Hero hero, Enemy enemy)
        {
            Console.WriteLine($"На вас напал: {enemy.GetType().Name} (HP: {enemy.Health}, Атака: {enemy.Attack})");
            bool heroFrozen = false;

            while (hero.IsAlive == true && enemy.IsAlive == true)
            {
                if (heroFrozen)
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    heroFrozen = false;
                }
                else
                {
                    HeroAttack(hero, enemy);
                    if (!enemy.IsAlive) break;
                }

                EnemyAttack(hero, enemy, ref heroFrozen);

                Console.WriteLine($"Ваше HP: {hero.Health}/{hero.MaxHealth}");
                Console.WriteLine($"HP врага: {enemy.Health}");
            }
            Console.WriteLine(hero.IsAlive ? "Враг повержен!" : "Вы погибли");
        }

        private static void HeroAttack(Hero hero, Enemy enemy)
        {
            decimal raw = hero.Weapon.Attack * (1 - enemy.Defense / 100m);
            if (rnd.Next(100) < 20)
            {
                int damage = Math.Max(1, (int)Math.Round(raw * 2));
                Console.WriteLine($"КРИТ! Вы наносите {damage * 2} урона!");
                enemy.Health -= damage;
            } 
            else
            {
                int damage = Math.Max(1, (int)Math.Round(raw * 1));
                Console.WriteLine($"Вы наносите {damage} урона.");
            }
        }

        private static void EnemyAttack(Hero hero, Enemy enemy, ref bool heroFrozen)
        {
            if (enemy.FreezeChance > 0 && (decimal)rnd.NextDouble() < enemy.FreezeChance)
            {
                if (hero.Armor.Magic)
                {
                    Console.WriteLine("Магическая броня защитила вас от заморозки!");
                }
                else
                {
                    heroFrozen = true;
                    Console.WriteLine("Враг замораживает вас!");
                }
            }

            bool isCrit = rnd.NextDouble() < (double)enemy.CritChance;
            decimal raw = enemy.Attack * (isCrit ? 2m : 1m);

            if (enemy.IgnoresArmor)
            {
                int dmg = Math.Max(1, (int)Math.Round(raw));
                hero.TakeDamage(dmg);
                Console.WriteLine($"Враг игнорирует броню и наносит {dmg} урона!");
                return;
            }

            double blockFactor = 0.7 + rnd.NextDouble() * 0.3;
            decimal blocked = hero.Armor.Defense * (decimal)blockFactor;
            int damage = Math.Max(1, (int)Math.Round(raw * (1 - blocked / 100m)));
            hero.TakeDamage(damage);

            Console.WriteLine(isCrit? $"Враг наносит КРИТИЧЕСКИЙ удар: {damage} урона (блок {blocked}%)!": $"  Враг наносит {damage} урона (блок {blocked}%).");
        }
    }
}
