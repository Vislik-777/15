using System;
// Интерфейс для внесения и снятия денег
public interface IDepositableWithdrawable
{
    string AccountId { get; } // Уникальный идентификатор счета
    decimal Balance { get; } // Текущий баланс счета

    bool Deposit(decimal amount); // Метод для пополнения счета
    bool Withdraw(decimal amount); // Метод для снятия денег со счета
}

// Интерфейс для перевода денег на другой счет
public interface ITransferable
{
    // Метод для перевода денег с текущего счета на другой целевой счет
    bool Transfer(IDepositableWithdrawable destinationAccount, decimal amount);
}

// Класс банковского счета, реализующий оба интерфейса
public class BankAccount : IDepositableWithdrawable, ITransferable
{
    public string AccountId { get; private set; }
    public decimal Balance { get; private set; }

    // Конструктор счета
    public BankAccount(string accountId, decimal initialBalance = 0m)
    {
        if (string.IsNullOrWhiteSpace(accountId))
            throw new ArgumentException("Идентификатор счета не может быть пустым.", nameof(accountId));
        if (initialBalance < 0)
            throw new ArgumentException("Начальный баланс не может быть отрицательным.", nameof(initialBalance));

        AccountId = accountId;
        Balance = initialBalance;
        Console.WriteLine($"Счет '{AccountId}' создан с начальным балансом: {Balance:C}");
    }

    // Реализация метода Deposit из IDepositableWithdrawable
    public bool Deposit(decimal amount)
    {
        // Проверка: Сумма > 0
        if (amount <= 0)
        {
            Console.WriteLine($"Ошибка (Счет '{AccountId}'): Сумма для пополнения должна быть больше нуля. (Попытка: {amount:C})");
            return false;
        }

        Balance += amount;
        Console.WriteLine($"Счет '{AccountId}': Пополнено {amount:C}. Текущий баланс: {Balance:C}");
        return true;
    }

    // Реализация метода Withdraw из IDepositableWithdrawable
    public bool Withdraw(decimal amount)
    {
        // Проверка: Сумма > 0
        if (amount <= 0)
        {
            Console.WriteLine($"Ошибка (Счет '{AccountId}'): Сумма для снятия должна быть больше нуля. (Попытка: {amount:C})");
            return false;
        }

        // Проверка: На счету хватает денег
        if (Balance < amount)
        {
            Console.WriteLine($"Ошибка (Счет '{AccountId}'): Недостаточно средств для снятия {amount:C}. Текущий баланс: {Balance:C}");
            return false;
        }

        Balance -= amount;
        Console.WriteLine($"Счет '{AccountId}': Снято {amount:C}. Текущий баланс: {Balance:C}");
        return true;
    }

    // Реализация метода Transfer из ITransferable
    public bool Transfer(IDepositableWithdrawable destinationAccount, decimal amount)
    {
        if (destinationAccount == null)
        {
            Console.WriteLine($"Ошибка при переводе со счета '{AccountId}': Целевой счет не может быть пустым.");
            return false;
        }

        // Дополнительная проверка, чтобы не переводить на тот же счет
        if (destinationAccount.AccountId == this.AccountId)
        {
            Console.WriteLine($"Ошибка при переводе со счета '{AccountId}': Нельзя перевести средства на тот же счет.");
            return false;
        }
        // Встроенные проверки в Withdraw и Deposit позаботятся о сумме > 0 и достаточности средств.
        // При переводе: снимает с себя, кладёт на другой счёт.
        Console.WriteLine($"Попытка перевода {amount:C} со счета '{AccountId}' на счет '{destinationAccount.AccountId}'...");
        bool withdrawSuccessful = Withdraw(amount); // Снимаем с текущего счета
        if (withdrawSuccessful)
        {
            bool depositSuccessful = destinationAccount.Deposit(amount); // Кладем на целевой счет
            if (depositSuccessful)
            {
                Console.WriteLine($"Перевод {amount:C} со счета '{AccountId}' на счет '{destinationAccount.AccountId}' успешно выполнен.");
                return true;
            }
            else
            {
                // В реальной системе здесь нужна логика отката (возврата денег на исходный счет),
                // чтобы избежать потери средств, если снятие прошло, а пополнение - нет.
                Console.WriteLine($"!!! ВНИМАНИЕ: Снятие средств со счета '{AccountId}' успешно, но пополнение счета '{destinationAccount.AccountId}' не удалось. Требуется ручное вмешательство/откат!");
                // Пример отката (для демонстрации, но в реальной системе нужна более надежная транзакция):
                // this.Deposit(amount); 
                return false;
            }
        }
        else
        {
            // Сообщение об ошибке уже было выведено методом Withdraw
            Console.WriteLine($"Перевод со счета '{AccountId}' не выполнен.");
            return false;
        }
    }
}

// Главный класс программы для демонстрации
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Задание 5: Банк и переводы");

        // Создаем два счета
        BankAccount accountUser1 = new BankAccount("User1_Acc", 1500m);
        BankAccount accountUser2 = new BankAccount("User2_Acc", 300m);

        Console.WriteLine("Исходные балансы");
        Console.WriteLine($"Баланс счета '{accountUser1.AccountId}': {accountUser1.Balance:C}");
        Console.WriteLine($"Баланс счета '{accountUser2.AccountId}': {accountUser2.Balance:C}");

        Console.WriteLine("Делаем успешный перевод с одного счета на другой");
        decimal transferAmount = 500m;
        if (accountUser1.Transfer(accountUser2, transferAmount))
        {
            Console.WriteLine("Операция перевода завершена успешно.");
        }
        else
        {
            Console.WriteLine("Операция перевода завершена с ошибкой.");
        }

        Console.WriteLine("Попытка перевода с недостаточным балансом");
        decimal largeTransferAmount = 1500m; // У User1 осталось 1000м, пытаемся перевести 1500м
        if (accountUser1.Transfer(accountUser2, largeTransferAmount))
        {
            Console.WriteLine("Операция перевода завершена успешно.");
        }
        else
        {
            Console.WriteLine("Операция перевода завершена с ошибкой (как и ожидалось).");
        }

        Console.WriteLine("Попытка перевода на тот же счет");
        if (accountUser1.Transfer(accountUser1, 100m))
        {
            Console.WriteLine("Операция перевода завершена успешно.");
        }
        else
        {
            Console.WriteLine("Операция перевода завершена с ошибкой (как и ожидалось).");
        }

        Console.WriteLine(" Итоговые балансы");
        Console.WriteLine($"Баланс счета '{accountUser1.AccountId}': {accountUser1.Balance:C}");
        Console.WriteLine($"Баланс счета '{accountUser2.AccountId}': {accountUser2.Balance:C}");

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadLine();
    }
}