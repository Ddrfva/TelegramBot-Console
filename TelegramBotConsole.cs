using System;
using System.Linq;

namespace TelegramBotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = string.Empty;
            bool isNameEntered = false;
            bool waitingForName = false;
            bool isRunning = true;

            Console.WriteLine("Добро пожаловать в бот!\n");
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("/start - начать работу и ввести имя");
            Console.WriteLine("/help - показать справку");
            Console.WriteLine("/info - информация о программе");
            Console.WriteLine("/echo [текст] - повторить введенный текст");
            Console.WriteLine("/exit - выход из программы");
            Console.WriteLine();

            while (isRunning)
            {
                if (!waitingForName)
                {
                    Console.Write("Введите команду: ");
                }

                string input = Console.ReadLine();

                if (waitingForName)
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("\nИмя не может быть пустым. Попробуйте снова:");
                        continue;
                    }

                    if (input.StartsWith("/"))
                    {
                        Console.WriteLine($"\nОшибка: '{input}' - это команда, а не имя. Пожалуйста, введите ваше имя (без /):");
                        continue;
                    }

                    if (input.Length < 2)
                    {
                        Console.WriteLine("\nОшибка: имя должно содержать хотя бы 2 символа. Попробуйте снова:");
                        continue;
                    }

                    if (!input.All(c => char.IsLetter(c)))
                    {
                        Console.WriteLine("\nОшибка: имя должно содержать только буквы. Попробуйте снова:");
                        continue;
                    }

                    userName = input;
                    isNameEntered = true;
                    waitingForName = false;
                    Console.WriteLine($"\nПриятно познакомиться, {userName}!");
                    Console.WriteLine("Подсказка: теперь доступна команда /echo [текст]\n");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Пожалуйста, введите команду.\n");
                    continue;
                }

                if (input.StartsWith("/echo "))
                {
                    if (isNameEntered)
                    {
                        string echoText = input.Substring(6);
                        if (!string.IsNullOrWhiteSpace(echoText))
                        {
                            Console.WriteLine($"{userName}, вы написали: \"{echoText}\"\n");
                        }
                        else
                        {
                            Console.WriteLine($"{userName}, вы не ввели текст. Используйте: /echo текст\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                    }
                    continue;
                }

                switch (input)
                {
                    case "/start":
                        if (!isNameEntered)
                        {
                            waitingForName = true;
                            Console.WriteLine();
                            Console.Write("Введите ваше имя: ");
                        }
                        else
                        {
                            Console.WriteLine($"{userName}, вы уже ввели имя. Используйте другие команды.\n");
                        }
                        break;

                    case "/help":
                        Console.WriteLine();
                        Console.WriteLine("Справочная информация");
                        Console.WriteLine("/start - начать работу и ввести имя");
                        Console.WriteLine("/help - показать эту справку");
                        Console.WriteLine("/info - информация о программе");
                        Console.WriteLine("/echo [текст] - повторить введенный текст (доступна после ввода имени)");
                        Console.WriteLine("/exit - выход из программы");
                        PrintPersonalizedMessage(isNameEntered, userName, "это вся доступная информация на данный момент.");
                        break;

                    case "/info":
                        Console.WriteLine();
                        Console.WriteLine("Информация о программе");
                        Console.WriteLine("Версия: 2.0.0");
                        Console.WriteLine("Дата создания: 26.02.2026");
                        Console.WriteLine("Автор: Dorofeeva Daria");
                        PrintPersonalizedMessage(isNameEntered, userName, "спасибо, что пользуетесь ботом!");
                        break;

                    case "/echo":
                        Console.WriteLine();
                        if (isNameEntered)
                        {
                            Console.WriteLine($"{userName}, вы использовали команду /echo без текста. Используйте: /echo [текст]\n");
                        }
                        else
                        {
                            Console.WriteLine("Сначала введите /start, чтобы начать работу.\n");
                        }
                        break;

                    case "/exit":
                        Console.WriteLine();
                        if (isNameEntered)
                            Console.WriteLine($"До свидания, {userName}!");
                        else
                            Console.WriteLine("До свидания!");

                        Console.WriteLine("Программа завершена.");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда. Введите /help для списка команд.\n");
                        break;
                }
            }
        }
        static void PrintPersonalizedMessage(bool isNameEntered, string userName, string message)
        {
            if (isNameEntered)
                Console.WriteLine($"{userName}, {message}\n");
            else
                Console.WriteLine($"Совет: сначала введите /start\n");
        }
    }
}