using System;
using System.Collections.Generic;

// Интерфейс для атаки
public interface IAttacker
{
    string Name { get; } // Добавляем свойство Name для более информативного вывода
    void Attack();
}

// Интерфейс для лечения
public interface IHealer
{
    string Name { get; } // Добавляем свойство Name для более информативного вывода
    void Heal();
}

// Класс Воина, умеет только атаковать
public class Warrior : IAttacker
{
    public string Name { get; private set; }

    public Warrior(string name)
    {
        Name = name;
    }

    // Реализация метода Attack из IAttacker
    public void Attack()
    {
        Console.WriteLine($"{Name}: Рубит мечом!");
    }
}

// Класс Мага, умеет и атаковать, и лечить
public class Mage : IAttacker, IHealer
{
    public string Name { get; private set; }

    public Mage(string name)
    {
        Name = name;
    }

    // Реализация метода Attack из IAttacker
    public void Attack()
    {
        Console.WriteLine($"{Name}: Призывает огненный шар!");
    }

    // Реализация метода Heal из IHealer
    public void Heal()
    {
        Console.WriteLine($"{Name}: Лечит союзников!");
    }
}

// Главный класс программы для демонстрации
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Задание 4: Игроки и способности");

        // Создаем экземпляры игроков
        Warrior aragorn = new Warrior("Арагорн");
        Mage gandalf = new Mage("Гэндальф");
        Warrior legolas = new Warrior("Леголас"); // Добавим еще одного воина для разнообразия
        Mage merlin = new Mage("Мерлин");


        List<object> allPlayers = new List<object> { aragorn, gandalf, legolas, merlin };

        Console.WriteLine("Сначала - все, кто умеет атаковать");
        foreach (var player in allPlayers)
        {
            if (player is IAttacker attacker) // Проверяем, реализует ли игрок IAttacker
            {
                attacker.Attack(); // Вызываем метод Attack
            }
        }

        Console.WriteLine("Потом - только тех, кто умеет лечить");
        foreach (var player in allPlayers)
        {
            if (player is IHealer healer) // Проверяем, реализует ли игрок IHealer
            {
                healer.Heal(); // Вызываем метод Heal
            }
        }

        Console.WriteLine("Завершение демонстрации");
        Console.ReadLine(); // Ждем нажатия клавиши для закрытия консоли
    }
}