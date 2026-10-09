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
    public decimal Defens {  get; set; }
    public bool Magic { get; set; }

    public Armor(string name, decimal defens, bool magic)
    {
        Name = name;
        Defens = defens;
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

namespace PR424_Shapovalov
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
