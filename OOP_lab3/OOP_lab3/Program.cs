using System;
using System.Linq;

namespace OneDimensionalArray
{
    // Задание 1: Определение класса "Одномерный массив"
    public class OneDimensionalArray
    {
        public int Kari { get; set;}
        public OneDimensionalArray(int kari) { Kari = kari;}

       public static bool operator <=(OneDimensionalArray a, OneDimensionalArray b)
        {
            return a.Kari <= b.Kari;
        }
        public static bool operator >=(OneDimensionalArray a, OneDimensionalArray b)
        {
            return a.Kari >= b.Kari;
        }

        public static bool AddKari(this string srt)
        {
            if(int.TryParse(srt, out int number))
            {
                return true;
            }
            return false;
        }
























        private int[] array;

        // Конструктор
        public OneDimensionalArray(int[] array)
        {
            this.array = array;
        }

        // Индексатор
        public int this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        // Перегрузка оператора * (умножение массивов)
        public static OneDimensionalArray operator *(OneDimensionalArray a1, OneDimensionalArray a2)
        {
            int[] result = a1.array.Zip(a2.array, (x, y) => x * y).ToArray();
            return new OneDimensionalArray(result);
        }

        // Перегрузка оператора true (если массив не содержит отрицательных элементов)
        public static bool operator true(OneDimensionalArray a)
        {
            return a.array.All(x => x >= 0);
        }

        public static bool operator false(OneDimensionalArray a)
        {
            return a.array.Any(x => x < 0);
        }

        // Перегрузка оператора приведения к int (возвращает размер массива)
        public static explicit operator int(OneDimensionalArray a)
        {
            return a.array.Length;
        }

        // Перегрузка оператора == (проверка на равенство)
        public static bool operator ==(OneDimensionalArray a1, OneDimensionalArray a2)
        {
            return a1.array.SequenceEqual(a2.array);
        }

        public static bool operator !=(OneDimensionalArray a1, OneDimensionalArray a2)
        {
            return !(a1 == a2);
        }

        // Перегрузка оператора < (сравнение)
        public static bool operator <(OneDimensionalArray a1, OneDimensionalArray a2)
        {
            return a1.array.Sum() < a2.array.Sum();
        }

        public static bool operator >(OneDimensionalArray a1, OneDimensionalArray a2)
        {
            return a1.array.Sum() > a2.array.Sum();
        }

        // Печать массива
        public override string ToString()
        {
            return string.Join(", ", array);
        }

        public override bool Equals(object obj)
        {
            return obj is OneDimensionalArray array && this == array;
        }

        public override int GetHashCode()
        {
            return array.GetHashCode();
        }

        // Задание 2: Вложенный объект Production
        public class Production
        {
            public int Id { get; set; }
            public string OrganizationName { get; set; }

            public Production(int id, string organizationName)
            {
                Id = id;
                OrganizationName = organizationName;
            }
        }

        public Production ProductionInfo { get; set; }

        // Задание 3: Вложенный класс Developer
        public class Developer
        {
            public string FullName { get; set; }
            public int Id { get; set; }
            public string Department { get; set; }

            public Developer(string fullName, int id, string department)
            {
                FullName = fullName;
                Id = id;
                Department = department;
            }
        }

        public Developer DeveloperInfo { get; set; }

        public int[] GetArray()
        {
            return array;
        }
    }

    // Задание 4: Статический класс StatisticOperation
    public static class StatisticOperation
    {
        // Сумма элементов
        public static int Sum(OneDimensionalArray array)
        {
            return array.GetArray().Sum();
        }

        // Разница между максимальным и минимальным
        public static int DifferenceMaxMin(OneDimensionalArray array)
        {
            var arr = array.GetArray();
            return arr.Max() - arr.Min();
        }

        // Подсчёт количества элементов
        public static int CountElements(OneDimensionalArray array)
        {
            return array.GetArray().Length;
        }

        // Задание 5: Методы расширения
        // Проверка на содержание определённого символа в строке
        public static bool ContainsSymbol(this string str, char symbol)
        {
            return str.Contains(symbol);
        }

        // Удаление отрицательных элементов
        public static OneDimensionalArray RemoveNegative(this OneDimensionalArray array)
        {
            return new OneDimensionalArray(array.GetArray().Where(x => x >= 0).ToArray());
        }
    }

    // Основной класс программы
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Задание 1: Работа с классом и перегрузками --- \n");
            var array1 = new OneDimensionalArray(new[] { -1, 2, 3 });
            var array2 = new OneDimensionalArray(new[] { 4, 5, 6 });
            Console.WriteLine("Первый массив: " + array1);
            Console.WriteLine("Второй массив: " + array2);

            var multipliedArray = array1 * array2;
            Console.WriteLine("Результат умножения массивов: " + multipliedArray);

            if (array1)
                Console.WriteLine("Массив 1 не содержит отрицательных элементов");

            int size = (int)array1;
            Console.WriteLine("Размер массива 1: " + size);

            Console.WriteLine("Массивы равны: " + (array1 == array2));
            Console.WriteLine("Массив 1 меньше массива 2: " + (array1 < array2));

            Console.WriteLine("\n--- Задание 2: Работа с вложенным объектом Production ---\n");
            array1.ProductionInfo = new OneDimensionalArray.Production(1, "Организация ООП");
            Console.WriteLine("Id: " + array1.ProductionInfo.Id + ", Имя организации: " + array1.ProductionInfo.OrganizationName);

            Console.WriteLine("\n--- Задание 3: Работа с вложенным классом Developer ---\n");
            array1.DeveloperInfo = new OneDimensionalArray.Developer("Адамович Карианна", 101, "IT");
            Console.WriteLine("Разработчик: " + array1.DeveloperInfo.FullName + " Id: " + array1.DeveloperInfo.Id + " Отдел: " + array1.DeveloperInfo.Department);

            Console.WriteLine("\n--- Задание 4: Статические методы ---\n");
            Console.WriteLine("Сумма элементов: " + StatisticOperation.Sum(array1));
            Console.WriteLine("Разница между макс и мин: " + StatisticOperation.DifferenceMaxMin(array1));
            Console.WriteLine("Количество элементов: " + StatisticOperation.CountElements(array1));

            Console.WriteLine("\n--- Задание 5: Методы расширения ---\n");
            string str = "Hello, world!";
            Console.WriteLine("Содержит 'o': " + str.ContainsSymbol('o'));

            var arrayWithoutNegatives = array1.RemoveNegative();
            Console.WriteLine("Массив без отрицательных элементов: " + arrayWithoutNegatives);
        }
    }
}
