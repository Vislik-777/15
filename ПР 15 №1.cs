using System;

// Интерфейс для выполнения работы
public interface IWorker
{
    void Work();
}

// Интерфейс для зарядки
public interface ICharger
{
    void Charge();
}

// Класс робота, реализующий оба интерфейса
public class Robot : IWorker, ICharger
{
    public string Name { get; private set; }
    public int EnergyLevel { get; private set; } // Уровень энергии от 0 до 100

    // Конструктор
    public Robot(string name, int initialEnergy)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя робота не может быть пустым.", nameof(name));
        if (initialEnergy < 0 || initialEnergy > 100)
            throw new ArgumentOutOfRangeException(nameof(initialEnergy), "Начальный уровень энергии должен быть от 0 до 100.");

        Name = name;
        EnergyLevel = initialEnergy;
        Console.WriteLine($"Робот '{Name}' создан с энергией: {EnergyLevel}");
    }

    // Реализация метода Work из интерфейса IWorker
    public void Work()
    {
        if (EnergyLevel == 0)
        {
            Console.WriteLine($"Робот '{Name}': Недостаточно энергии для работы. Текущая энергия: {EnergyLevel}");
            return;
        }

        EnergyLevel -= 20;
        if (EnergyLevel < 0)
        {
            EnergyLevel = 0; // Энергия не ниже 0
        }
        Console.WriteLine($"Робот '{Name}': выполнил работу. Текущая энергия: {EnergyLevel}");
    }

    // Реализация метода Charge из интерфейса ICharger
    public void Charge()
    {
        EnergyLevel += 50;
        if (EnergyLevel > 100)
        {
            EnergyLevel = 100; // Энергия не выше 100
        }
        Console.WriteLine($"Робот '{Name}': заряжен. Текущая энергия: {EnergyLevel}");
    }

    // Метод для вывода текущего состояния робота
    public void DisplayStatus()
    {
        Console.WriteLine($"Статус '{Name}': Энергия {EnergyLevel}");
    }
}

// Главный класс программы для демонстрации
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Задание 1: Роботы на заводе");

        // Создаем робота с начальным уровнем энергии
        Robot factoryRobot = new Robot("Вася", 70);

        Console.WriteLine("Последовательность действий");

        // Робот работает два раза
        factoryRobot.Work();
        factoryRobot.Work();

        // Робот заряжается
        factoryRobot.Charge();

        // Робот работает еще раз
        factoryRobot.Work();

        Console.WriteLine("Завершение демонстрации");
        Console.ReadLine(); // Ждем нажатия клавиши для закрытия консоли
    }
}