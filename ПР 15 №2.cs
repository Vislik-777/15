using System;

// Интерфейс для включения и выключения устройств
public interface ISwitchable
{
    void On();
    void Off();
}

// Интерфейс для установки уровня (например, яркости, скорости)
public interface ILevelAdjustable
{
    void SetLevel(int level);
}

// Класс Лампы
public class Lamp : ISwitchable, ILevelAdjustable
{
    public string Name { get; private set; }
    public int BrightnessLevel { get; private set; } // Уровень от 0 до 100

    public Lamp(string name)
    {
        Name = name;
        BrightnessLevel = 0; // По умолчанию выключена
        DisplayStatus();
    }

    // Реализация метода On из ISwitchable
    public void On()
    {
        BrightnessLevel = 100;
        Console.Write($"{Name}: ");
        DisplayStatus();
    }

    // Реализация метода Off из ISwitchable
    public void Off()
    {
        BrightnessLevel = 0;
        Console.Write($"{Name}: ");
        DisplayStatus();
    }

    // Реализация метода SetLevel из ILevelAdjustable
    public void SetLevel(int level)
    {
        if (level < 0) BrightnessLevel = 0;
        else if (level > 100) BrightnessLevel = 100;
        else BrightnessLevel = level;

        Console.Write($"{Name}: Установлен уровень ");
        DisplayStatus();
    }

    // Выводит текущее состояние лампы
    public void DisplayStatus()
    {
        Console.WriteLine($"{(BrightnessLevel > 0 ? "Включена" : "Выключена")}, Яркость: {BrightnessLevel}%");
    }
}

// Класс Вентилятора
public class Fan : ISwitchable
{
    public string Name { get; private set; }
    public bool IsOn { get; private set; }

    public Fan(string name)
    {
        Name = name;
        IsOn = false; // По умолчанию выключен
        DisplayStatus();
    }

    // Реализация метода On из ISwitchable
    public void On()
    {
        IsOn = true;
        Console.Write($"{Name}: ");
        DisplayStatus();
    }

    // Реализация метода Off из ISwitchable
    public void Off()
    {
        IsOn = false;
        Console.Write($"{Name}: ");
        DisplayStatus();
    }

    // Выводит текущее состояние вентилятора
    public void DisplayStatus()
    {
        Console.WriteLine($"{(IsOn ? "Включен" : "Выключен")}");
    }
}

// Главный класс программы для демонстрации
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Задание 2: Умный дом");

        // Создаем устройства
        Lamp livingRoomLamp = new Lamp("Лампа в гостиной");
        Fan bedroomFan = new Fan("Вентилятор в спальне");

        Console.WriteLine("Проходим по всем устройствам: включить -> выключить");

        // Создаем массив устройств, реализующих ISwitchable
        ISwitchable[] switchableDevices = { livingRoomLamp, bedroomFan };

        foreach (var device in switchableDevices)
        {
            Console.WriteLine($"Устройство: {((dynamic)device).Name}"); // Приводим к dynamic, чтобы получить Name
            device.On();
            device.Off();
        }

        Console.WriteLine("Дополнительно для лампы: установить уровень");

        // Для лампы дополнительно устанавливаем уровень
        // Проверяем, реализует ли устройство ILevelAdjustable
        if (livingRoomLamp is ILevelAdjustable adjustableLamp)
        {
            adjustableLamp.SetLevel(30); // Устанавливаем уровень 30%
            adjustableLamp.SetLevel(85); // Устанавливаем уровень 85%
            livingRoomLamp.Off(); // Выключаем лампу после всех действий
        }

        Console.WriteLine("Завершение демонстрации");
        Console.ReadLine(); // Ждем нажатия клавиши для закрытия консоли
    }
}