using System;
using PR424_Shapovalov;

namespace PR424_Shapovalov
{
    internal class Program
    {
        private static Random rnd = new Random();

        static void Main()
        {
            Hero hero = new Hero("Герой", 100,
                new Weapon("Ржавый меч", 5, false),
                new Armor("Тряпичная броня", 2, false));

            Console.WriteLine("Игра началась!");

            for (int step = 1; step <= 10 && hero.IsAlive; step++)
            {
                Console.WriteLine($"Шаг {step}");

                if (rnd.Next(2) == 0)
                    OpenChest(hero);
                else
                    Battle.Fight(hero, EnemyGenerator.Generate());

                Console.WriteLine($"HP: {hero.Health}/{hero.MaxHealth}\nОружие: {hero.Weapon.Name}\nБроня: {hero.Armor.Name}");
            }

            Console.WriteLine(hero.IsAlive ? $"Вы прошли все испытания" : "Герой погиб");
        }

        static void OpenChest(Hero hero)
        {
            Console.WriteLine("Вы нашли сундук!");

            string drop = ItemsAndDrops.RollDrop();

            if (drop == "Зелье")
            {
                hero.HealFull();
                Console.WriteLine("В сундуке лечебное зелье! Здоровье полностью восстановлено.");
                return;
            }

            if (drop == "Оружие")
            {
                Weapon newWeapon = ItemsAndDrops.GenerateWeapon();
                Console.WriteLine($"В сундуке: {newWeapon.Name} (Атака: {newWeapon.Attack}, Дальнее: {newWeapon.Range})");
                Console.WriteLine($"У вас:    {hero.Weapon.Name} (Атака: {hero.Weapon.Attack}, Дальнее: {hero.Weapon.Range})");
                Console.Write("Взять новое оружие? (1 — взять, 2 — выбросить): ");
                if (Console.ReadLine() == "1")
                {
                    hero.Weapon = newWeapon;
                    Console.WriteLine($"Экипировано: {newWeapon.Name}");
                }
                else Console.WriteLine("Вы выбросили предмет.");
                return;
            }

            Armor newArmor = ItemsAndDrops.GenerateArmor();
            Console.WriteLine($"В сундуке: {newArmor.Name} (Защита: {newArmor.Defense}, Магический: {newArmor.Magic})");
            Console.WriteLine($"У вас:    {hero.Armor.Name} (Защита: {hero.Armor.Defense}, Магический: {hero.Armor.Magic})");
            Console.Write("Взять новый доспех? (1 — взять, 2 — выбросить): ");
            if (Console.ReadLine() == "1")
            {
                hero.Armor = newArmor;
                Console.WriteLine($"Экипировано: {newArmor.Name}");
            }
            else Console.WriteLine("Вы выбросили предмет.");
        }
    }
}
