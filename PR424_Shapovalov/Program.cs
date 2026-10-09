using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using PR424_Shapovalov;
public class Hero
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon {  get; set; }
    public int MaxHealth { get; set; }
    public Armor Armor { get; set; }

    public Hero(string name, int health, Weapon weapon, Armor armor)
    {
        Name = name;
        Health = health;
        MaxHealth = health;
        Weapon = weapon;
        Armor = armor;
    }
    static void OpenChest(Hero hero)
    {
        Console.WriteLine("Вы нашли сундук!");

        string drop = ItemsAndDrops.RollDrop();

        if (drop == "Зелье")
        {
            hero.Health = hero.MaxHealth;
            Console.WriteLine("В сундуке лечебное зелье! Здоровье полностью восстановлено.");
            return;
        }

        if (drop == "Оружие")
        {
            Weapon newWeapon = ItemsAndDrops.GenerateWeapon();
            Console.WriteLine($"В сундуке: {newWeapon.Name} (Атака: {newWeapon.Attack}, Дальнее: {newWeapon.Range})");
            Console.WriteLine($"У вас:    {hero.Weapon.Name} (Атака: {hero.Weapon.Attack}, Дальнее: {hero.Weapon.Range})");
            Console.WriteLine ("Взять новое оружие? (1 — взять, 2 — выбросить): ");
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
        Console.WriteLine("Взять новый доспех? (1 — взять, 2 — выбросить): ");
        if (Console.ReadLine() == "1")
        {
            hero.Armor = newArmor;
            Console.WriteLine($"Экипировано: {newArmor.Name}");
        }
        else Console.WriteLine("Вы выбросили предмет.");
    }
}

namespace PR424_Shapovalov
{
    internal class Program
    {
        static void Main()
        {

        }
    }
}
