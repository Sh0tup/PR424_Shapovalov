using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class Weapon
{
    public string Name { get; set; }
    public int Attack {  get; set; }
    public bool Range {  get; set; }
    public Weapon (string name, int attack, bool range)
    {
        this.Name = name;
        this.Attack = attack;
        this.Range = range;
    }
}
public class Armor
{
    public string Name { get; set; }
    public decimal Defense {  get; set; }
    public bool Magic { get; set; }

    public Armor(string name, decimal defense, bool magic)
    {
        Name = name;
        Defense = defense;
        Magic = magic;
    }
}
public class Hero
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon {  get; set; }

    public Armor Armor { get; set; }

    public Hero(string name, int health, Weapon weapon, Armor armor)
    {
        Name = name;
        Health = health;
        Weapon = weapon;
        Armor = armor;
    }

}
public abstract class Enemy
{
    public abstract int Health { get; set; }
    public abstract int Attack { get; set; }
    public abstract int Defense { get; set; }
}

public class Goblin : Enemy
{
    public override int Health { get; set; }
    public override int Attack { get; set; }
    public override int Defense { get; set; }
}

namespace PR424_Shapovalov
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
