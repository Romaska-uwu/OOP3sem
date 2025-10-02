using System;
using System.Collections.Generic;

// Задание 1
namespace LabWork
{
    
    public abstract class BaseSoftware
    {
        public abstract string GetDescription(); 
    }

    
    public interface ICloneable
    {
        bool DoClone(); 
    }

    
    public class Software : BaseSoftware, ICloneable
    {
        public string Name { get; set; }

        public Software(string name)
        {
            Name = name;
        }

        public override string GetDescription()
        {
            return $"Программное обеспечение: {Name}";
        }

        public bool DoClone()
        {
            Console.WriteLine("Клонирование программного обеспечения...");
            return true;
        }

        public override string ToString()
        {
            return $"Программное обеспечение - Имя: {Name}";
        }
    }

    
    public class OperationSet : Software
    {
        public string OperationType { get; set; }

        public OperationSet(string name, string operationType) : base(name)
        {
            OperationType = operationType;
        }

        public override string GetDescription()
        {
            return $"Набор операций: {Name}, Тип операции: {OperationType}";
        }

        public override string ToString()
        {
            return $"Набор операций - Имя: {Name}, Тип операции: {OperationType}";
        }
    }

    public class TextProcessor : Software
    {
        public string Format { get; set; }

        public TextProcessor(string name, string format) : base(name)
        {
            Format = format;
        }

        public override string GetDescription()
        {
            return $"Текстовый процессор: {Name}, Формат: {Format}";
        }

        public override string ToString()
        {
            return $"Текстовый процессор - Имя: {Name}, Формат: {Format}";
        }
    }

    
    public class Word : TextProcessor
    {
        public Word(string name) : base(name, "DOCX") { }

        public override string ToString()
        {
            return $"Word - Имя: {Name}, Формат: DOCX";
        }
    }

    public class Virus : Software
    {
        public string VirusType { get; set; }

        public Virus(string name, string virusType) : base(name)
        {
            VirusType = virusType;
        }

        public override string GetDescription()
        {
            return $"Вирус: {Name}, Тип вируса: {VirusType}";
        }

        public override string ToString()
        {
            return $"Вирус - Имя: {Name}, Тип вируса: {VirusType}";
        }
    }

    public class CConficker : Virus
    {
        public CConficker(string name) : base(name, "Conficker") { }

        public override string ToString()
        {
            return $"CConficker - Имя: {Name}, Тип вируса: Conficker";
        }
    }

    public class Toy : Software
    {
        public string ToyType { get; set; }

        public Toy(string name, string toyType) : base(name)
        {
            ToyType = toyType;
        }

        public override string GetDescription()
        {
            return $"Игрушка: {Name}, Тип игрушки: {ToyType}";
        }

        public override string ToString()
        {
            return $"Игрушка - Имя: {Name}, Тип игрушки: {ToyType}";
        }
    }

    public class Minesweeper : Toy
    {
        public Minesweeper(string name) : base(name, "Головоломка") { }

        public override string ToString()
        {
            return $"Сапер - Имя: {Name}, Тип игрушки: Головоломка";
        }
    }


    public class Developer : Software
    {
        public string Language { get; set; }

        public Developer(string name, string language) : base(name)
        {
            Language = language;
        }

        public override string GetDescription()
        {
            return $"Разработчик: {Name}, Язык: {Language}";
        }

        public override string ToString()
        {
            return $"Разработчик - Имя: {Name}, Язык: {Language}";
        }
    }


    public class Printer
    {
        public void IAmPrinting(BaseSoftware software)
        {
            Console.WriteLine(software.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1
            Console.WriteLine("Задание 1: Иерархия классов");

            
            List<BaseSoftware> softwareList = new List<BaseSoftware>
            {
                new Software("Антивирус"),
                new OperationSet("Набор операций 1", "Безопасность"),
                new TextProcessor("Текстовый процессор", "PDF"),
                new Word("Текстовый процессор Word"),
                new Virus("Троян", "Троян"),
                new CConficker("Вирус CConficker"),
                new Toy("Игрушечная машина", "Удаленное управление"),
                new Minesweeper("Игра Сапер"),
                new Developer("Джон Доу", "C#")
            };

            
            Printer printer = new Printer();

            
            foreach (var item in softwareList)
            {
                printer.IAmPrinting(item);
            }

            // Задание 5
            Console.WriteLine("\nЗадание 5: Демонстрация интерфейсов и абстрактных классов");

            Software softwareObj = new Software("Новое программное обеспечение");
            ICloneable cloneableObj = softwareObj;

            
            cloneableObj.DoClone();

            // Задание 6
            Console.WriteLine("\nЗадание 6: Переопределение ToString()");

            foreach (var item in softwareList)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}