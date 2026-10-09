using System;

namespace PR424_Shapovalov
{
    public class Hero
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }

        public bool IsAlive => Health > 0;

        public Hero(string name, int health, Weapon weapon, Armor armor)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Weapon = weapon;
            Armor = armor;
        }

        public void HealFull()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
        }
    }
}
