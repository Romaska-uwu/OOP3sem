using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static class Info
{
    private static string name_file = "text.txt";
    private static Mutex mutex = new();
    public static void ShowProcesses()
    {
        var allProcesses = Process.GetProcesses();

        foreach (var process in allProcesses)
        {
            try
            {
                Console.WriteLine($"Id процесса - {process.Id}\n" +
                              $"Имя процесса - {process.ProcessName}\n" +
                              $"Приоритет процесса - {process.BasePriority}\n" +
                              $"Время запуска процесса - {process.StartTime}\n" +
                              $"Текущее состояние процесса - {process.Responding}\n" +
                              $"Время работы процесса - {process.TotalProcessorTime}\n" +
                              $"Время загрузки процесса - {process.UserProcessorTime}\n");
            }
            catch (Exception e)
            {

            }
        }
        Console.WriteLine($"Количество процессов - {allProcesses.Length}");
    }

    public static void ShowDomains()
    {

        AppDomain domain = AppDomain.CurrentDomain;
        Console.WriteLine($"Имя домена - {domain.FriendlyName}");
        Console.WriteLine("Базовый каталог:\n" + domain.BaseDirectory);
        Console.WriteLine($"Детали конфигурации - {domain.SetupInformation}");
        Console.WriteLine("Сборки, загруженные в домен:");

        Assembly[] assemblies = domain.GetAssemblies();
        foreach (Assembly asm in assemblies)
        {
            Console.WriteLine(asm.GetName().Name);
        }

    }

    public static void ShowSimpleNumbers()
    {
        var first = new Thread(PrintSimpleNumbers);
        first.Start();
        first.Name = "Simple_numbers";
        first.Join();
        Console.WriteLine();

    }
    public static void ShowInfo(object thread)
    {
        var z = thread as Thread;
        Console.WriteLine($"Имя потока - {z.Name}\n" +
                          $"Приоритет потока - {z.Priority}\n" +
                          $"Статус потока - {z.ThreadState}\n" +
                          $"Поток фоновый - {z.IsBackground}\n" +
                          $"Поток запущен - {z.IsAlive}\n" +
                          $"Поток приостановлен - {z.IsThreadPoolThread}\n");
    }
    public static void PrintSimpleNumbers()
    {
        var first = new Thread(ShowInfo);
        first.Start(Thread.CurrentThread);
        first.Join();

        Console.WriteLine("Введите n:");
        int n = int.Parse(Console.ReadLine());
        for (int i = 2; i < n; i++)
        {
            bool isPrime = true;
            for (int j = 2; j < i; j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
        }
    }

    public static void ThoThreads()
    {
        var first = new Thread(OddNumbers) { Priority = ThreadPriority.Highest };
        var two = new Thread(EvenNumbers);

        first.Start();
        two.Start();
        FirstlyEvenSecondlyOdd();
        ShowOneByOne();
    }
    private static void ShowOneByOne()
    {
        var mutex = new Mutex();
        var even = new Thread(ShowEvenNumbers);
        var odd = new Thread(ShowOddNumbers);
        odd.Start();
        even.Start();
        even.Join();
        odd.Join();

        void ShowEvenNumbers()
        {
            for (var i = 0; i < 15; i++)
            {
                mutex.WaitOne();
                if (i % 2 == 0)
                    Console.Write(i + " ");
                mutex.ReleaseMutex();
            }
        }

        void ShowOddNumbers()
        {
            for (var i = 0; i < 10; i++)
            {
                mutex.WaitOne();
                Thread.Sleep(200);
                if (i % 2 != 0)
                    Console.Write(i + " ");
                mutex.ReleaseMutex();
            }
        }
    }
    private static void FirstlyEvenSecondlyOdd()
    {
        var objectToLock = new object();
        var even = new Thread(ShowEvenNumbers);
        var odd = new Thread(ShowOddNumbers);
        even.Start();
        odd.Start();
        even.Join();
        odd.Join();

        void ShowEvenNumbers()
        {
            lock (objectToLock)
            {
                for (var i = 0; i < 15; i++)
                {
                    if (i % 2 == 0)
                        Console.Write(i + " ");
                }
            }
        }

        void ShowOddNumbers()
        {
            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                if (i % 2 != 0)
                    Console.Write(i + " ");
            }
        }
    }
    public static void OddNumbers()
    {
        string txt = string.Empty;
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(200);
            if (i % 2 != 0)
            {
                txt += i + " ";
                Console.WriteLine(i + " ");
            }

        }
        txt += "\n";
        File.AppendAllText(name_file, txt);

    }
    public static void EvenNumbers()
    {
        string txt = string.Empty;
        for (int i = 0; i < 15; i++)
        {
            Thread.Sleep(300);
            if (i % 2 == 0)
            {
                txt += i + " ";
                Console.WriteLine(i + " ");
            }

        }
        txt += "\n";
        File.AppendAllText(name_file, txt);

    }

    public static void ReadFile()
    {
        var file = File.ReadAllText(name_file);
        Console.WriteLine(file);
    }
}