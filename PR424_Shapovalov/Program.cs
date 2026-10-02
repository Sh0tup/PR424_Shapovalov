using System;
using System.Collections.Generic;
using System.Linq;

enum Category
{
    Фантастика = 1,
    Детектив = 2,
    Ужасы = 3
}

class Product
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }
    public int Price { get; private set; }
    public Category Category { get; private set; }

   
    public Product(string code, string name, string author, int year, int price, Category category)
    {
        Code = code;
        Name = name;
        Author = author;
        Price = price;
        Year = year;
        Category = category;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Код: " + Code);
        Console.WriteLine("Название: " + Name);
        Console.WriteLine("Автор: " + Author);
        Console.WriteLine("Год издания: " + Year);
        Console.WriteLine("Цена: " + Price);
        Console.WriteLine("Жанр: " + Category);
    }
}

class Program
{
    static List<Product> products = new List<Product>();
    static int nextCode = 1;


    static void Main()
    {
        AddProduct("Война и мир", "Лев Толстой", 1869, 1200, Category.Фантастика);
        AddProduct("Анна Каренина", "Лев Толстой", 1877, 950, Category.Детектив);
        AddProduct("Воскресение", "Лев Толстой", 1899, 800, Category.Фантастика);
        AddProduct("Преступление и наказание", "Фёдор Достоевский", 1866, 1100, Category.Детектив);
        AddProduct("Идиот", "Фёдор Достоевский", 1869, 1000, Category.Ужасы);

        while (true)
        {
            Console.WriteLine("     МАГАЗИН");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Поиск книги");
            Console.WriteLine("4. Сортировка по авторам");
            Console.WriteLine("5. Сортировка по годам");
            Console.WriteLine("6. Вывести все книги");
            Console.WriteLine("7. Минимальная цена книги и максимальная");
            Console.WriteLine("8. Количество книг каждого автора");
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
                CommandSearch();
            }
            else if (choice == "4")
            {
                SortAuthor();
            }
            else if (choice == "5")
            {
                SortYear();
            }
            else if (choice == "6")
            {
                CommandShowAll();
            }
            else if (choice == "7")
            {
                MinMax();
            }
            else if (choice == "8")
            {
                CountAuthor();
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

        Console.WriteLine("Введите автора: ");
        string author = Console.ReadLine();

        Console.WriteLine("Введите год издания: ");
        if (int.TryParse(Console.ReadLine(), out int year) == false || (year / 1000 <= 1 || year / 1000 >= 2))
        {
            Console.WriteLine("Число введено некорректно");
            return;
        }

        Console.WriteLine("Введите цену: ");
        int.TryParse(Console.ReadLine(), out int price);
        if (price < 0)
        {
            Console.WriteLine("Цена не может быть отрицательной.");
            return;
        }

        Console.Write("Жанр (1-Фантастика, 2-Детектив, 3-Ужасы): ");
        Category category = (Category)int.Parse(Console.ReadLine());

        if (category == 0)
        {
            Console.WriteLine("Категория не выбрана.");
            return;
        }
        AddProduct(name, author, year, price, category);
        Console.WriteLine("Товар добавлен.");
    }

    static void AddProduct( string name, string author, int year, int price, Category category)
    {
        string code = nextCode.ToString();
        nextCode = nextCode + 1;

        Product product = new Product(code, name, author, year, price, category);
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

        Console.WriteLine("           СПИСОК КНИГ");

        for (int i = 0; i < products.Count; i++)
        {
            products[i].PrintInfo();
        }
    }

    static void MinMax()
    {
        var maxProduct = products.Max(x => x.Price);
        var minProduct = products.Min(x => x.Price);
        Console.WriteLine("Минимально стоящая книга: ");
        products.First(m=>m.Price > maxProduct).PrintInfo();
        foreach (var product in products)
        {
            if (product.Price == maxProduct || product.Price == minProduct)
            {
                product.PrintInfo();
            }
        }   
    }

    static void SortAuthor()
    {
        var sortAuthor = products.OrderBy(x => x.Author);
        foreach (var p in sortAuthor)
        {
            p.PrintInfo();
        }
    }

    static void SortYear()
    {
        var sortYear = products.OrderBy(x => x.Year);
        foreach (var p in sortYear)
        { 
            p.PrintInfo(); 
        }
    }
    
    static void CountAuthor()
    {
        var groups = products.GroupBy(x => x.Author);
        foreach (var group in groups)
        {
            Console.WriteLine($"{group.Key}: {group.Count()} книг");
        }
    }
}