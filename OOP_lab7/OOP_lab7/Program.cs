using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OOP_lab7
{
    //Задание 1: Универсальный интерфейс с операциями
    public interface IOperations<T>
    {
        void Add(T item);
        void Remove(T item);
        void Display();
    }

    // Задание 2: Универсальный класс CollectionType<T>
    public class CollectionType<T> : IOperations<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
            Console.WriteLine($"Добавлено: {item}");
        }

        public void Remove(T item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                Console.WriteLine($"Удалено: {item}");
            }
            else
            {
                Console.WriteLine($"Элемент {item} не найден в коллекции.");
            }
        }

        public void Display()
        {
            Console.WriteLine("Элементы коллекции:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public T Find(Predicate<T> predicate)
        {
            return items.Find(predicate);
        }

        // Задание 5: Сохранение в файл (JSON)
        public void SaveToFile(string filePath)
        {
            try
            {
                var json = JsonSerializer.Serialize(items);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Данные успешно сохранены в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        // Загрузка из файла (JSON)
        public void LoadFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                    Console.WriteLine("Данные успешно загружены из файла.");
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            }
        }
    }

    // Пользовательский класс из лабораторной №4
    public class Software
    {
        public string Name { get; set; }

        public Software(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Программное обеспечение: {Name}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Работа с универсальным классом ---");

            // Работа с коллекцией целых чисел
            var intCollection = new CollectionType<int>();
            intCollection.Add(10);
            intCollection.Add(20);
            intCollection.Remove(10);
            intCollection.Display();

            // Работа с коллекцией строк
            var stringCollection = new CollectionType<string>();
            stringCollection.Add("Привет");
            stringCollection.Add("Мир");
            stringCollection.Remove("Мир");
            stringCollection.Display();

            // Работа с пользовательским классом
            var softwareCollection = new CollectionType<Software>();
            var software1 = new Software("Антивирус");
            var software2 = new Software("Офис");

            softwareCollection.Add(software1);
            softwareCollection.Add(software2);
            softwareCollection.Display();

            // Поиск элемента
            var foundSoftware = softwareCollection.Find(s => s.Name.Contains("Анти"));
            Console.WriteLine(foundSoftware != null
                ? $"Найдено: {foundSoftware}"
                : "Элемент не найден.");

            // Сохранение и загрузка данных
            string filePath = "collection.json";
            softwareCollection.SaveToFile(filePath);
            softwareCollection.LoadFromFile(filePath);
        }
    }
}
