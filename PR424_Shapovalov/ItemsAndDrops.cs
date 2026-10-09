using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR424_Shapovalov
{
    public class Weapon
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public bool Range { get; set; }
        public Weapon(string name, int attack, bool range)
        {
            Name = name;
            Attack = attack;
            Range = range;
        }
    }
    public class Armor
    {
        public string Name { get; set; }
        public decimal Defense { get; set; }
        public bool Magic { get; set; }

        public Armor(string name, decimal defense, bool magic)
        {
            Name = name;
            Defense = defense;
            Magic = magic;
        }
    }
    public static class ItemsAndDrops
    {
        private static Random rnd = new Random();

        public static Weapon GenerateWeapon()
        {
            string[] names = { "Меч", "Топор", "Лук", "Кинжал", "Боевой посох" };
            string name = names[rnd.Next(names.Length)];
            int attack = rnd.Next(5, 21);
            bool range = rnd.Next(2) == 0;
            return new Weapon(name, attack, range);
        }

        public static Armor GenerateArmor()
        {
            string[] names = { "Кожаный доспех", "Кольчуга", "Латы", "Мантия мага" };
            string name = names[rnd.Next(names.Length)];
            decimal defense = rnd.Next(5, 21);
            bool magic = rnd.Next(2) == 0;
            return new Armor(name, defense, magic);
        }
        public static string RollDrop()
        {
            int roll = rnd.Next(100);
            if (roll < 40) return "Зелье";
            if (roll < 70) return "Оружие";
            return "Доспех";
        }
    }
}
