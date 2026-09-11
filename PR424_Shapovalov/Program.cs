using System;
using System.Collections.Generic;

// Категории товаров, заданы прямо в коде
enum Category
{
    Електроника = 1,
    Продукты = 2,
    Одежда = 3
}

class Product
{
    public string Code { get; private set; } // уникальный код, ставится автоматически
    public string Name { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }
    public Category Category { get; private set; }

    // "Остался ли товар на складе" — вычисляется само, менять извне нельзя
    public bool InStock
    {
        get
        {
            if (Quantity > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public Product(string code, string name, double price, int quantity, Category category)
    {
        Code = code;
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    public void AddQuantity(int amount)
    {
        Quantity = Quantity + amount;
    }

    public bool Sell(int amount)
    {
        if (amount > Quantity)
        {
            return false;
        }
        Quantity = Quantity - amount;
        return true;
    }

    public void PrintInfo()
    {
        string stockText;
        if (InStock)
        {
            stockText = "да";
        }
        else
        {
            stockText = "нет";
        }

        Console.WriteLine("Код: " + Code);
        Console.WriteLine("Название: " + Name);
        Console.WriteLine("Цена: " + Price);
        Console.WriteLine("Количество: " + Quantity);
        Console.WriteLine("На складе: " + stockText);
        Console.WriteLine("Категория: " + Category);
    }
}

class Program
{
    static List products = new List();
    static int nextCode = 1; // коды начинаются с 1

    static void Main()
    {
        AddProduct("Хлеб", 40, 10, Category.Продукты);
        AddProduct("Молоко", 90, 5, Category.Продукты);
        AddProduct("Футболка", 1200, 7, Category.Одежда);
        AddProduct("Джинсы", 3500, 3, Category.Одежда);
        AddProduct("Наушники", 2500, 4, Category.Електроника);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== МАГАЗИН =====");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("3. Заказать поставку");
            Console.WriteLine("4. Продать товар");
            Console.WriteLine("5. Поиск товара");
            Console.WriteLine("6. Показать все товары");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите команду: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CommandAdd();
            }
            else if (choice == "2")
            {
                CommandDelete();
            }
            else if (choice == "3")
            {
                CommandSupply();
            }
            else if (choice == "4")
            {
                CommandSell();
            }
            else if (choice == "5")
            {
                CommandSearch();
            }
            else if (choice == "6")
            {
                CommandShowAll();
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Нет такой команды.");
            }
        }

        Console.WriteLine("Программа завершена.");
    }

    static void CommandAdd()
    {
        Console.Write("Введите название: ");
        string name = Console.ReadLine();

        if (name == null || name.Trim().Length == 0)
        {
            Console.WriteLine("Название не может быть пустым.");
            return;
        }

        Console.WriteLine("Введите цену: ");
        name = Console.ReadLine();
        double.TryParse(name, out double price);
        if (price < 0)
        {
            Console.WriteLine("Цена не может быть отрицательной.");
            return;
        }

        Console.WriteLine("Введите количество: ");
        name = Console.ReadLine();
        int.TryParse(name, out int quantity);
        if (quantity < 0)
        {
            Console.WriteLine("Количество не может быть отрицательным.");
            return;
        }

        Category category =;
        if (category == 0)
        {
            Console.WriteLine("Категория не выбрана.");
            return;
        }

        AddProduct(name, price, quantity, category);
        Console.WriteLine("Товар добавлен.");
    }

    static void AddProduct(string name, double price, int quantity, Category category)
    {
        string code = nextCode.ToString();
        nextCode = nextCode + 1;

        Product product = new Product(code, name, price, quantity, category);
        products.Add(product);
    }
}