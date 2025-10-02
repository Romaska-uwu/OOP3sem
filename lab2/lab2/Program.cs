using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        // 2) Создание объектов класса Vector
        var vector1 = new Vector(new[] { 1, 2, 3 });
        var vector2 = new Vector(new[] { 4, 5, 6 });
        var vector3 = new Vector(3, 0); // Вектор с 3 элементами, инициализированными 0
        var vector4 = new Vector(); // Вектор по умолчанию

        // Вывод информации о векторах
        Console.WriteLine(vector1);
        Console.WriteLine(vector2);
        Console.WriteLine(vector3);
        Console.WriteLine(vector4);

        // Вызов свойств
        Console.WriteLine($"ID вектора 1: {vector1.Id}");
        Console.WriteLine($"Элемент по индексу 1 вектора 2: {vector2[1]}");

        // Изменение элемента
        vector2[1] = 10; // Изменение второго элемента в vector2
        Console.WriteLine($"Обновленный вектор 2: {vector2}");

        // Сравнение объектов
        Console.WriteLine($"Вектор 1 равен вектору 2: {vector1.Equals(vector2)}");

        // Проверка типа
        Console.WriteLine($"Тип вектора 1: {vector1.GetType()}");

        // 3) Создание массива объектов
        var vectors = new Vector[]
        {
            vector1,
            vector2,
            vector3,
            new Vector(new[] { 0, 0, 0 }) // Вектор, содержащий только нули
        };

        // a) Вывод векторов, содержащих 0
        var zeroVectors = vectors.Where(v => v.ContainsZero()).ToList();
        Console.WriteLine("Векторы, содержащие 0:");
        foreach (var v in zeroVectors)
        {
            Console.WriteLine(v);
        }

        // b) Список векторов с наименьшим модулем
        var minMagnitude = vectors.Min(v => v.Magnitude());
        var minMagnitudeVectors = vectors.Where(v => v.Magnitude() == minMagnitude).ToList();
        Console.WriteLine("\nВекторы с наименьшим модулем:");
        foreach (var v in minMagnitudeVectors)
        {
            Console.WriteLine(v);
        }

        // 4) Пример анонимного типа
        var anonymousVector = new { Id = vector1.Id, Elements = vector1, Description = "Это анонимный вектор." }; // Перевод строки
        Console.WriteLine($"Анонимный тип: Id = {anonymousVector.Id}, Элементы = [{string.Join(", ", anonymousVector.Elements)}], Описание = {anonymousVector.Description}"); // Перевод строки
    }
}
