using System;

// Интерфейс для получения цены
public interface IPriceable
{
    string Name { get; } // Добавляем Name для удобства вывода, хотя в задании явно не сказано, но полезно
    decimal Price { get; }
}

// Интерфейс для получения срока гарантии
public interface IWarranty
{
    int WarrantyMonths { get; } // Срок гарантии в месяцах
}

// Класс Телефона, реализует оба интерфейса
public class Phone : IPriceable, IWarranty
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int WarrantyMonths { get; private set; }

    public Phone(string name, decimal price, int warrantyMonths)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя товара не может быть пустым.", nameof(name));
        if (price < 0) throw new ArgumentException("Цена не может быть отрицательной.", nameof(price));
        if (warrantyMonths < 0) throw new ArgumentException("Срок гарантии не может быть отрицательным.", nameof(warrantyMonths));

        Name = name;
        Price = price;
        WarrantyMonths = warrantyMonths;
    }
}

// Класс Ноутбука, реализует только интерфейс IPriceable
public class Laptop : IPriceable
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public Laptop(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя товара не может быть пустым.", nameof(name));
        if (price < 0) throw new ArgumentException("Цена не может быть отрицательной.", nameof(price));

        Name = name;
        Price = price;
    }
}

// Главный класс программы для демонстрации
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Задание 3: Магазин техники");

        // Создаем экземпляры товаров
        Phone iphone = new Phone("iPhone 15 Pro", 120000m, 24);
        Laptop macbook = new Laptop("MacBook Air M2", 150000m);
        Phone samsung = new Phone("Samsung Galaxy S24", 90000m, 18);
        Laptop dell = new Laptop("Dell XPS 15", 135000m);

        // Объединяем их в массив IPriceable, так как все товары имеют цену
        IPriceable[] items = { iphone, macbook, samsung, dell };

        decimal totalCost = 0m;

        Console.WriteLine("Информация о товарах");
        foreach (var item in items)
        {
            Console.WriteLine($"Товар: {item.Name}");
            Console.WriteLine($"Цена: {item.Price:C}");
            totalCost += item.Price;

            // Проверяем, реализует ли товар интерфейс IWarranty
            if (item is IWarranty warrantyItem)
            {
                Console.WriteLine($"Гарантия: {warrantyItem.WarrantyMonths} месяцев.");
            }
            else
            {
                Console.WriteLine("Гарантия: Не предусмотрена.");
            }
        }

        Console.WriteLine($"Итог");
        Console.WriteLine($"Общая стоимость всех товаров: {totalCost:C}");

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadLine();
    }
}