using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LabWork
{
    // Класс Автомобиль
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(string brand, string model, int year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Brand} {Model} ({Year})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Задание 1: Коллекция автомобилей ---");

            // Коллекция автомобилей (Dictionary<TKey, TValue>)
            var carCollection = new Dictionary<int, Car>();
            carCollection.Add(1, new Car("Toyota", "Camry", 2020));
            carCollection.Add(2, new Car("Honda", "Civic", 2018));
            carCollection.Add(3, new Car("Ford", "Focus", 2019));

            // Вывод коллекции
            Console.WriteLine("Все автомобили:");
            foreach (var car in carCollection)
            {
                Console.WriteLine($"Ключ: {car.Key}, Значение: {car.Value}");
            }

            // Удаление автомобиля
            carCollection.Remove(2);
            Console.WriteLine("\nПосле удаления:");
            foreach (var car in carCollection)
            {
                Console.WriteLine($"Ключ: {car.Key}, Значение: {car.Value}");
            }

            // Поиск автомобиля по ключу
            if (carCollection.TryGetValue(3, out var foundCar))
            {
                Console.WriteLine($"\nНайдено: {foundCar}");
            }

            Console.WriteLine("\n--- Задание 2: Универсальная коллекция ---");

            // Универсальная коллекция List<int>
            var intList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine("Исходная коллекция List<int>:");
            Console.WriteLine(string.Join(", ", intList));

            // Удаление 3 элементов
            intList.RemoveRange(0, 3);
            Console.WriteLine("\nПосле удаления первых 3 элементов:");
            Console.WriteLine(string.Join(", ", intList));

            // Добавление элементов
            intList.Add(11);
            intList.Insert(0, 0);
            Console.WriteLine("\nПосле добавления элементов:");
            Console.WriteLine(string.Join(", ", intList));

            // Вторая коллекция (Dictionary)
            var intDictionary = new Dictionary<int, int>();
            for (int i = 0; i < intList.Count; i++)
            {
                intDictionary[i] = intList[i];
            }

            Console.WriteLine("\nСодержимое Dictionary<int, int>:");
            foreach (var item in intDictionary)
            {
                Console.WriteLine($"Ключ: {item.Key}, Значение: {item.Value}");
            }

            // Поиск значения в словаре
            var searchValue = 5;
            var found = intDictionary.ContainsValue(searchValue);
            Console.WriteLine(found ? $"\nЗначение {searchValue} найдено в Dictionary." : $"\nЗначение {searchValue} не найдено.");

            Console.WriteLine("\n--- Задание 3: ObservableCollection ---");

            // Наблюдаемая коллекция
            var observableCars = new ObservableCollection<Car>();
            observableCars.CollectionChanged += ObservableCars_CollectionChanged;

            // Добавление элементов
            observableCars.Add(new Car("BMW", "X5", 2021));
            observableCars.Add(new Car("Audi", "A4", 2022));

            // Удаление элемента
            observableCars.RemoveAt(0);
        }

        private static void ObservableCars_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("\nЭлемент добавлен:");
                foreach (Car newItem in e.NewItems)
                {
                    Console.WriteLine(newItem);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("\nЭлемент удален:");
                foreach (Car oldItem in e.OldItems)
                {
                    Console.WriteLine(oldItem);
                }
            }
        }
    }
}
