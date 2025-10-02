using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OOP_Lab14
{
    class Program
    {
        private static System.Timers.Timer _timer;
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1\n");
            Info.ShowProcesses();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Задание 2\n");
            Info.ShowDomains();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Задание 3\n");
            Info.ShowSimpleNumbers();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Задание 4\n");
            Info.ThoThreads();
            Console.WriteLine("---------------------------");

            StartTimer();

            Console.ReadLine();
        }
        private static void StartTimer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            Console.WriteLine("Таймер запущен..");
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Время: {0:HH:mm:ss.ff}", e.SignalTime);
        }
    }
}