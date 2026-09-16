using System;
using System.Collections.Generic;

enum Category
{
    Електроника = 1,
    Продукты = 2,
    Одежда = 3
}

class Product
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }
    public Category Category { get; private set; }

    public bool InStock // осталось ли что-то
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

    public void RemoveQuantity(int amount)
    {
        Quantity = Quantity - amount;
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
    static List<Product> products = new List<Product>();
    static int nextCode = 1;

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

        Console.WriteLine("Введите цену: ");
        name = Console.ReadLine();
        double.TryParse(name, out double price);
        if (price < 0)
        {
            price = Math.Abs(price);
        }

        Console.WriteLine("Введите количество: ");
        name = Console.ReadLine();
        int.TryParse(name, out int quantity);
        if (quantity < 0)
        {
            Console.WriteLine("Количество не может быть отрицательным.");
            return;
        }

        Console.Write("Категория (1-Электроника, 2-Продукты, 3-Одежда): ");
        Category category = (Category)int.Parse(Console.ReadLine());

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

    public static void CommandDelete()
    {
        Console.Write("Введите код товара для удаления: ");
        string code = Console.ReadLine();

        int index = -1;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == code)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Товар с таким кодом не найден.");
            return;
        }

        Console.WriteLine($"Товар {products[index].Name} удалён.");
        products.RemoveAt(index);
    }

    static void CommandSupply()
    {
        Console.Write("Введите код товара: ");
        string code = Console.ReadLine();

        int index = -1;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == code)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Товар с таким кодом не найден.");
            return;
        }

        Console.Write("Введите количество для поставки: ");
        int amount = int.Parse(Console.ReadLine());

        products[index].AddQuantity(amount);
        Console.WriteLine($"Поставка добавлена.");
    }

    static void CommandSell()
    {
        Console.Write("Введите код товара: ");
        string code = Console.ReadLine();

        int index = -1;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == code)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("Товар с таким кодом не найден.");
            return;
        }

        Console.Write("Введите количество для продажи: ");
        int amount = int.Parse(Console.ReadLine());

        if (products[index].Quantity < amount)
        {
            Console.WriteLine("Недостаточно товара на складе.");
            return;
        }

        products[index].RemoveQuantity(amount);
        double total = products[index].Price * amount;
        Console.WriteLine($"Продано {amount} шт. на сумму {total}. Осталось: {products[index].Quantity}");
    }

    static void CommandSearch()
    {
        Console.Write("Введите код или название товара или категорию (Электроника, Продукты, Одежда): ");
        string query = Console.ReadLine();

        bool found = false;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == query || products[i].Name == query || products[i].Category.ToString() == query)
            {
                products[i].PrintInfo();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    static void CommandShowAll()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст.");
            return;
        }

        Console.WriteLine("             СПИСОК ТОВАРОВ");

        for (int i = 0; i < products.Count; i++)
        {
            products[i].PrintInfo();
        }
    }
}