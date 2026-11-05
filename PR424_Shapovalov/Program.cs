using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shapovalov
{

    using System;

    struct Expense
    {
        public string Name;
        public double Price;
    }

    class Program
    {
        static void Main()
        {

            Console.Write("Минимум 2, максимум 40,\n");
            int n = Convert.ToInt32(Console.ReadLine());

            Expense[] expenses = new Expense[n];

            Console.WriteLine("[Название; Цена]");

            for (int i = 0; i < n; i++)
            {
                Console.Write($"{i + 1}: ");
                string[] parts = Console.ReadLine().Split(';');
                expenses[i] = new Expense
                {
                    Name = parts[0].Trim(),
                    Price = double.Parse(parts[1])
                };
            }

            while (true)
            {
                Console.WriteLine("\n1 - Вывод данных");
                Console.WriteLine("2 - Статистика");
                Console.WriteLine("3 - Сортировка по цене");
                Console.WriteLine("4 - Конвертация валюты");
                Console.WriteLine("5 - Поиск по названию");
                Console.WriteLine("0 - Выход (в окно)");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        for (int i = 0; i < n; i++)
                            Console.WriteLine($"{expenses[i].Name} — {expenses[i].Price} руб.");
                        break;

                    case "2":
                        double sum = 0, min = expenses[0].Price, max = expenses[0].Price;
                        foreach (var e in expenses)
                        {
                            sum += e.Price;
                            if (e.Price < min)
                            {
                                min = e.Price;
                            }
                            if (e.Price > max)
                            {
                                max = e.Price;
                            }
                        }
                        Console.WriteLine($"Сумма {sum}");
                        Console.WriteLine($"Среднее {sum / n}");
                        Console.WriteLine($"Минимум {min}");
                        Console.WriteLine($"Максимум {max}");
                        break;

                    case "3":

                        for (int i = 0; i < n - 1; i++)
                            for (int j = 0; j < n - i - 1; j++)
                                if (expenses[j].Price > expenses[j + 1].Price)
                                {
                                    var a = expenses[j];
                                    expenses[j] = expenses[j + 1];
                                    expenses[j + 1] = a;
                                }
                        Console.WriteLine("Готово");
                        break;

                    case "4":
                        Console.Write("Курс");
                        double rate = double.Parse(Console.ReadLine());
                        Console.WriteLine("конвертация выполнена");

                        foreach (var e in expenses)
                            Console.WriteLine($"{e.Name}: {e.Price / rate}");
                        break;

                    case "5":
                        Console.Write("Строка");
                        string query = Console.ReadLine().ToLower();

                        foreach (var e in expenses)
                            if (e.Name.ToLower().Contains(query))
                                Console.WriteLine($"{e.Name} — {e.Price} руб.");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Написано неправильно");
                        break;
                }
            }
        }
    }
}
