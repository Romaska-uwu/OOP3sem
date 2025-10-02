using System;
public static class TaskNum3
{
    public static void Point3A()
    {
        Console.WriteLine("\n\nЗаданние 3а \n");
        int[,] matrix = new int[3, 3]
        {
                { 2, 34, 5 },
                { 4, 23, 78 },
                { 12, 67, 24}
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(matrix[i, j] + "    ");
            }
            Console.WriteLine();
        }
    }

    public static void Point3B()
    {
        Console.WriteLine("\n\nЗадание 3b \n");
        string[] stringArray = new string[] { "lorem", "ipsum", "hello", "world" };
        Console.Write("Массив слов: ");
        foreach (string str in stringArray)
            Console.Write(str + " ");
        Console.WriteLine($"\nДлина массива: {stringArray.Length}");

        Console.WriteLine("Введите цифру от 1 до 4: ");
        int numWord = Convert.ToInt32(Console.ReadLine()) - 1;
        if (numWord < 0 || numWord > 4)
            return;
        Console.WriteLine("Слово: ");
        string word = Console.ReadLine() ?? "";

        for (int i = 0; i < stringArray.Length; i++)
        {
            if (numWord == i)
            {
                stringArray[i] = word;
                break;
            }
        }
        Console.Write("Новый массив слов: ");
        foreach (string str in stringArray)
            Console.Write(str + " ");
    }

    public static void Point3C()
    {
        Console.WriteLine("\n\nЗадание 3с \n");
        int[][] array = new int[3][];

        for (int i = 0; i < array.Length; i++)
            array[i] = new int[i + 2];

        Console.WriteLine("Введите элементы массивов массива array: ");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine($"Массив {i + 1}: ");
            for (int j = 0; j < array[i].Length; j++)
            {
                array[i][j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        Console.WriteLine("Результат:");
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                Console.Write(array[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public static void Point3D()
    {
        Console.WriteLine("\n\nЗадание 3d \n");
        var array3D = new[] { 1, 2, 3, 4, 5 };
        var string3D = "Hello, World!";

        Console.WriteLine("Тип переменной array: " + array3D.GetType());
        Console.WriteLine("Тип переменной str: " + string3D.GetType());
    }
}