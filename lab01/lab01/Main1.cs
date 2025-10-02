using System;

namespace project01
{
    public static class Main1
    {
        static void Main()
        {

            int[][] mas = new int[][]
        {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5 },
                new int[] { 6, 7, 8, 9 },
                new int[] { 10 }
        };

            void st()
            {
                Console.WriteLine("Hello world");
            }

            st();


            for (int i = 0; i < mas.Length; i++)
            {
                Console.Write("Элементы подмассива " + (i + 1) + ": ");
                for (int j = 0; j < mas[i].Length; j++)
                {
                    Console.Write(mas[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

















































        //TaskNum1.Point1A();
        //TaskNum1.Point1B();
        //TaskNum1.Point1C();
        //TaskNum1.Point1D();
        //TaskNum1.Point1E();
        //TaskNum1.Point1F();
        //Console.ReadLine();

        //TaskNum2.Point2A();
        //TaskNum2.Point2B();
        //TaskNum2.Point2C();
        //TaskNum2.Point2D();
        //Console.ReadLine();

        //TaskNum3.Point3A();
        //TaskNum3.Point3B();
        //TaskNum3.Point3C();
        //Console.ReadLine();

        //TaskNum4.Point4();
        //TaskNum5.Point5();
        //TaskNum6.Point6();
    }
}