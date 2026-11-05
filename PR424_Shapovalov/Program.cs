using Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PR424_Shapovalov
{
    partial class Product
    {
        public void PrintInfo()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Название: " + ProductName);
            Console.WriteLine("Цена: " + Price);
            Console.WriteLine("В наличии: " + StockQuantity);
            Console.WriteLine();
        }
    }

    partial class PickupPoint
    {
        public void PrintInfo()
        {
            Console.WriteLine("ID: " + Id + " | " + PickupPointName + " | " + Address);
        }
    }

    partial class CartItem
    {
        public void PrintInfo()
        {
            Product product = Core.Context.Product.FirstOrDefault(x => x.Id == ProductId);

            Console.WriteLine("ID позиции: " + Id);
            Console.WriteLine("Товар: " + product.ProductName);
            Console.WriteLine("Цена: " + product.Price);
            Console.WriteLine("Количество: " + Quantity);
            Console.WriteLine("Сумма: " + product.Price * Quantity);
            Console.WriteLine();
        }
    }

    partial class Order
    {
        public decimal GetTotalPrice()
        {
            List<OrderItem> items = Core.Context.OrderItem.Where(x => x.OrderId == Id).ToList();
            return items.Sum(x => x.Price * x.Quantity);
        }

        public void PrintInfo()
        {
            PickupPoint point = Core.Context.PickupPoint.FirstOrDefault(x => x.Id == PickupPointId);
            List<OrderItem> items = Core.Context.OrderItem.Where(x => x.OrderId == Id).ToList();

            Console.WriteLine("Заказ номер: " + Id + " от " + CreatedAt.ToString("dd.MM.yyyy HH:mm"));
            Console.WriteLine("ПВЗ: " + point.PickupPointName + " (" + point.Address + ")");

            for (int i = 0; i < items.Count; i++)
            {
                int productId = items[i].ProductId;
                Product product = Core.Context.Product.FirstOrDefault(x => x.Id == productId);
                Console.WriteLine("  - " + product.ProductName + " x" + items[i].Quantity + " по " + items[i].Price);
            }

            Console.WriteLine("Итого: " + GetTotalPrice());
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static User currentUser = null;

        static void Main(string[] args)
        {
            bool exit = false;

            while (exit == false)
            {
                if (currentUser == null)
                {
                    exit = GuestMenu();
                }
                else
                {
                    exit = UserMenu();
                }
            }

            Console.WriteLine("Программа завершена.");
        }

        static bool GuestMenu()
        {
            Console.WriteLine("1. Регистрация");
            Console.WriteLine("2. Вход");
            Console.WriteLine("3. Просмотр товаров");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите команду: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CommandRegister();
            }
            else if (choice == "2")
            {
                CommandLogin();
            }
            else if (choice == "3")
            {
                CommandShowProducts();
            }
            else if (choice == "0")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Нет такой команды.");
            }

            return false;
        }

        static bool UserMenu()
        {
            Console.WriteLine("ПОЛЬЗОВАТЕЛЬ: " + currentUser.Nickname);
            Console.WriteLine("1. Просмотр товаров");
            Console.WriteLine("2. Добавить товар в корзину");
            Console.WriteLine("3. Показать корзину");
            Console.WriteLine("4. Удалить позицию из корзины");
            Console.WriteLine("5. Купить одну позицию из корзины");
            Console.WriteLine("6. Купить всю корзину");
            Console.WriteLine("7. Мои заказы");
            Console.WriteLine("8. Выйти из аккаунта");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите команду: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CommandShowProducts();
            }
            else if (choice == "2")
            {
                CommandAddToCart();
            }
            else if (choice == "3")
            {
                CommandShowCart();
            }
            else if (choice == "4")
            {
                CommandRemoveFromCart();
            }
            else if (choice == "5")
            {
                CommandBuyOne();
            }
            else if (choice == "6")
            {
                CommandBuyAll();
            }
            else if (choice == "7")
            {
                CommandShowOrders();
            }
            else if (choice == "8")
            {
                currentUser = null;
                Console.WriteLine("Вы вышли из аккаунта.");
            }
            else if (choice == "0")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Нет такой команды.");
            }

            return false;
        }

        static void CommandRegister()
        {
            Console.Write("Введите ник: ");
            string nickname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("Ник не может быть пустым.");
                return;
            }

            if (Core.Context.User.Any(x => x.Nickname == nickname))
            {
                Console.WriteLine("Пользователь с таким ником уже существует.");
                return;
            }

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Пароль не может быть пустым.");
                return;
            }

            Console.Write("Повторите пароль: ");
            string confirmPassword = Console.ReadLine();

            if (password != confirmPassword)
            {
                Console.WriteLine("Пароли не совпадают.");
                return;
            }

            AddUser(nickname, password);
            Console.WriteLine("Регистрация прошла успешно. Теперь войдите в аккаунт.");
        }

        static void AddUser(string nickname, string password)
        {
            User user = new User();
            user.Nickname = nickname;
            user.Password = password;

            Core.Context.User.Add(user);
            Core.Context.SaveChanges();
        }

        static void CommandLogin()
        {
            Console.Write("Введите ник: ");
            string nickname = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            User user = Core.Context.User.FirstOrDefault(x => x.Nickname == nickname && x.Password == password);

            if (user == null)
            {
                Console.WriteLine("Неверный ник или пароль.");
                return;
            }

            currentUser = user;
            Console.WriteLine("Добро пожаловать босс, " + user.Nickname + "!");
        }

        static void CommandShowProducts()
        {
            var products = Core.Context.Product.ToList();

            if (products.Count == 0)
            {
                Console.WriteLine("Товаров нет.");
                return;
            }

            Console.WriteLine("           СПИСОК ТОВАРОВ");

            for (int i = 0; i < products.Count; i++)
            {
                products[i].PrintInfo();
            }
        }

        static void CommandAddToCart()
        {
            Console.Write("Введите ID товара: ");
            if (int.TryParse(Console.ReadLine(), out int productId) == false)
            {
                Console.WriteLine("ID введён некорректно.");
                return;
            }

            Product product = Core.Context.Product.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                Console.WriteLine("Товар с таким ID не найден.");
                return;
            }

            Console.Write("Введите количество: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) == false || quantity <= 0)
            {
                Console.WriteLine("Количество введено некорректно.");
                return;
            }

            CartItem cartItem = Core.Context.CartItem.FirstOrDefault(x => x.UserId == currentUser.Id && x.ProductId == productId);
            int totalQuantity = quantity;

            if (cartItem != null)
            {
                totalQuantity = totalQuantity + cartItem.Quantity;
            }

            if (totalQuantity > product.StockQuantity)
            {
                Console.WriteLine("Недостаточно товара. В наличии: " + product.StockQuantity);
                return;
            }

            if (cartItem == null)
            {
                AddCartItem(productId, quantity);
            }
            else
            {
                cartItem.Quantity = totalQuantity;
                Core.Context.SaveChanges();
            }

            Console.WriteLine("Товар добавлен в корзину.");
        }

        static void AddCartItem(int productId, int quantity)
        {
            CartItem cartItem = new CartItem();
            cartItem.UserId = currentUser.Id;
            cartItem.ProductId = productId;
            cartItem.Quantity = quantity;

            Core.Context.CartItem.Add(cartItem);
            Core.Context.SaveChanges();
        }

        static List<CartItem> GetCart()
        {
            return Core.Context.CartItem
                .Where(x => x.UserId == currentUser.Id)
                .OrderBy(x => x.Id)
                .ToList();
        }

        static void CommandShowCart()
        {
            var cart = GetCart();

            if (cart.Count == 0)
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            Console.WriteLine("           КОРЗИНА");

            decimal total = 0;

            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].PrintInfo();

                int productId = cart[i].ProductId;
                Product product = Core.Context.Product.FirstOrDefault(x => x.Id == productId);
                total = total + product.Price * cart[i].Quantity;
            }

            Console.WriteLine("Итого: " + total);
        }

        static void CommandRemoveFromCart()
        {
            Console.Write("Введите ID позиции корзины для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id) == false)
            {
                Console.WriteLine("ID введён некорректно.");
                return;
            }

            CartItem cartItem = Core.Context.CartItem.FirstOrDefault(x => x.Id == id && x.UserId == currentUser.Id);

            if (cartItem == null)
            {
                Console.WriteLine("Позиция с таким ID не найдена.");
                return;
            }

            Core.Context.CartItem.Remove(cartItem);
            Core.Context.SaveChanges();

            Console.WriteLine("Позиция удалена из корзины.");
        }

        static void CommandBuyOne()
        {
            Console.Write("Введите ID позиции корзины для покупки: ");
            if (int.TryParse(Console.ReadLine(), out int id) == false)
            {
                Console.WriteLine("ID введён некорректно.");
                return;
            }

            CartItem cartItem = GetCart().FirstOrDefault(x => x.Id == id);

            if (cartItem == null)
            {
                Console.WriteLine("Позиция с таким ID не найдена.");
                return;
            }

            Checkout(new List<CartItem> { cartItem });
        }

        static void CommandBuyAll()
        {
            var cart = GetCart();

            if (cart.Count == 0)
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            Checkout(cart);
        }

        static PickupPoint ChoosePickupPoint()
        {
            var points = Core.Context.PickupPoint.ToList();

            if (points.Count == 0)
            {
                Console.WriteLine("Нет доступных ПВЗ.");
                return null;
            }

            Console.WriteLine("Доступные пункты выдачи:");

            for (int i = 0; i < points.Count; i++)
            {
                points[i].PrintInfo();
            }

            Console.Write("Введите ID пункта выдачи: ");
            if (int.TryParse(Console.ReadLine(), out int id) == false)
            {
                Console.WriteLine("ID введён некорректно.");
                return null;
            }

            PickupPoint point = points.FirstOrDefault(x => x.Id == id);

            if (point == null)
            {
                Console.WriteLine("Пункт выдачи с таким ID не найден.");
            }

            return point;
        }

        static void Checkout(List<CartItem> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                int productId = items[i].ProductId;
                Product product = Core.Context.Product.FirstOrDefault(x => x.Id == productId);

                if (items[i].Quantity > product.StockQuantity)
                {
                    Console.WriteLine("Недостаточно товара: " + product.ProductName +" (в наличии: " + product.StockQuantity + ")");
                    return;
                }
            }

            PickupPoint point = ChoosePickupPoint();

            if (point == null)
            {
                return;
            }

            Order order = new Order();
            order.UserId = currentUser.Id;
            order.PickupPointId = point.Id;
            order.CreatedAt = DateTime.Now;

            for (int i = 0; i < items.Count; i++)
            {
                int productId = items[i].ProductId;
                Product product = Core.Context.Product.FirstOrDefault(x => x.Id == productId);

                OrderItem orderItem = new OrderItem();
                orderItem.Order = order;
                orderItem.ProductId = productId;
                orderItem.Quantity = items[i].Quantity;
                orderItem.Price = product.Price;
                Core.Context.OrderItem.Add(orderItem);

                product.StockQuantity = product.StockQuantity - items[i].Quantity;
                Core.Context.CartItem.Remove(items[i]);
            }

            Core.Context.Order.Add(order);
            Core.Context.SaveChanges();

            Console.WriteLine("Заказ оформлен. Заберите его в пункте выдачи: " + point.PickupPointName);
        }

        static void CommandShowOrders()
        {
            Console.WriteLine("1. Сначала новые");
            Console.WriteLine("2. Сначала старые");
            Console.Write("Выберите сортировку: ");
            string choice = Console.ReadLine();

            List<Order> orders;

            if (choice == "1")
            {
                orders = Core.Context.Order.Where(x => x.UserId == currentUser.Id).OrderByDescending(x => x.CreatedAt).ToList();
            }
            else if (choice == "2")
            {
                orders = Core.Context.Order.Where(x => x.UserId == currentUser.Id).OrderBy(x => x.CreatedAt).ToList();
            }
            else
            {
                Console.WriteLine("Нет такой команды.");
                return;
            }

            if (orders.Count == 0)
            {
                Console.WriteLine("Заказов пока нет.");
                return;
            }

            Console.WriteLine("           МОИ ЗАКАЗЫ");

            for (int i = 0; i < orders.Count; i++)
            {
                orders[i].PrintInfo();
            }
        }
    }
}