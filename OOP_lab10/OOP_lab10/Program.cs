using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqLab
{
    // Класс Vector для варианта 1
    public class Vector
    {
        public List<int> Components { get; set; }

        public Vector(IEnumerable<int> components)
        {
            Components = components.ToList();
        }

        public double Magnitude => Math.Sqrt(Components.Sum(c => c * c));

        public override string ToString()
        {
            return $"[{string.Join(", ", Components)}] (|v| = {Magnitude:F2})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1: Работа с массивом месяцев
            string[] months = { "June", "July", "May", "December", "January", "February", "March", "April", "August", "September", "October", "November" };

            Console.WriteLine("\nМесяцы с длиной строки равной 4:");
            var query1 = months.Where(m => m.Length == 4);
            foreach (var month in query1)
            {
                Console.WriteLine(month);
            }

            Console.WriteLine("\nЛетние и зимние месяцы:");
            var query2 = months.Where(m => new[] { "June", "July", "August", "December", "January", "February" }.Contains(m));
            foreach (var month in query2)
            {
                Console.WriteLine(month);
            }

            Console.WriteLine("\nМесяцы в алфавитном порядке:");
            var query3 = months.OrderBy(m => m);
            foreach (var month in query3)
            {
                Console.WriteLine(month);
            }

            Console.WriteLine("\nМесяцы с буквой 'u' и длиной имени >= 4:");
            var query4 = months.Where(m => m.Contains('u') && m.Length >= 4);
            foreach (var month in query4)
            {
                Console.WriteLine(month);
            }

            // Задание 2: Работа с коллекцией Vector
            var vectors = new List<Vector>
            {
                new Vector(new[] { 1, 0, 0 }),
                new Vector(new[] { 0, 0, 0 }),
                new Vector(new[] { -1, 1, 0 }),
                new Vector(new[] { 3, 4, 0 }),
                new Vector(new[] { 1, 1, 1 }),
                new Vector(new[] { 2, 0, 0 }),
                new Vector(new[] { -3, -4, 5 }),
                new Vector(new[] { 0, 0, 7 }),
                new Vector(new[] { 0, 5, 0 }),
                new Vector(new[] { 0, -1, -1 })
            };

            // Количество векторов, содержащих 0
            var vectorsWithZero = vectors.Count(v => v.Components.Contains(0));
            Console.WriteLine($"\nКоличество векторов, содержащих 0: {vectorsWithZero}");

            // Список векторов с наименьшим модулем
            var minMagnitude = vectors.Min(v => v.Magnitude);
            var vectorsWithMinMagnitude = vectors.Where(v => Math.Abs(v.Magnitude - minMagnitude) < 1e-5);
            Console.WriteLine("\nВекторы с наименьшим модулем:");
            foreach (var vector in vectorsWithMinMagnitude)
            {
                Console.WriteLine(vector);
            }

            // Вектор максимальной длины
            var maxVector = vectors.OrderByDescending(v => v.Magnitude).First();
            Console.WriteLine($"\nМаксимальный вектор: {maxVector}");

            // Первый вектор с отрицательным значением
            var firstNegativeVector = vectors.FirstOrDefault(v => v.Components.Any(c => c < 0));
            Console.WriteLine($"\nПервый вектор с отрицательным значением: {firstNegativeVector}");

            // Упорядоченный список векторов по размеру
            Console.WriteLine("\nУпорядоченный список векторов по модулю:");
            var orderedVectors = vectors.OrderBy(v => v.Magnitude);
            foreach (var vector in orderedVectors)
            {
                Console.WriteLine(vector);
            }

            // Задание 4: Запрос с 5 операторами LINQ
            Console.WriteLine("\nЗапрос с 5 операторами LINQ:");
            var customQuery = vectors
                .Where(v => v.Components.Count == 3)  // Условие
                .OrderBy(v => v.Magnitude)           // Упорядочивание
                .GroupBy(v => v.Components.Sum() > 0) // Группировка
                .SelectMany(g => g)                  // Проекция
                .Take(3);                            // Разбиение

            foreach (var vector in customQuery)
            {
                Console.WriteLine(vector);
            }

            // Задание 5: Оператор Join
            Console.WriteLine("\nЗапрос с оператором Join:");
            var labels = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            var joinedQuery = vectors.Zip(labels, (vector, label) => new { Label = label, Vector = vector });
            foreach (var pair in joinedQuery)
            {
                Console.WriteLine($"{pair.Label}: {pair.Vector}");
            }
        }
    }
}
