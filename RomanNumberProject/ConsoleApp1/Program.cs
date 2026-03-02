using System;
using RomanNumberLib;

namespace RomanNumberDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Создание чисел");
            var a = new RomanNumber("XIV");
            var b = new RomanNumber("V");
            var c = new RomanNumber("MCMLXXXIV");

            Console.WriteLine($"   a = {a} (это {a.ToInt()})");
            Console.WriteLine($"   b = {b} (это {b.ToInt()})");
            Console.WriteLine($"   c = {c} (это {c.ToInt()})");
            Console.WriteLine();

            Console.WriteLine("Простые операции");
            Console.WriteLine($"   {a} + {b} = {a + b} (должно быть XIX = 19)");
            Console.WriteLine($"   {c} - {a} = {c - a} (должно быть MCMLXX = 1970)");
            Console.WriteLine($"   {a} * {b} = {a * b} (должно быть LXX = 70)");
            Console.WriteLine($"   {c} / {b} = {c / b} (должно быть CCCXCVI = 396)");
            Console.WriteLine();

            Console.WriteLine("Сравнение");
            var x = new RomanNumber("X");
            var y = new RomanNumber("V");
            var z = new RomanNumber("X");

            Console.WriteLine($"   {x} > {y} : {x > y} (должно быть True)");
            Console.WriteLine($"   {x} < {y} : {x < y} (должно быть False)");
            Console.WriteLine($"   {x} == {z} : {x == z} (должно быть True)");
            Console.WriteLine($"   {x} != {y} : {x != y} (должно быть True)");
            Console.WriteLine();

            Console.WriteLine("Сортировка");
            var numbers = new[]
            {
                new RomanNumber("XIV"),
                new RomanNumber("III"),
                new RomanNumber("IX"),
                new RomanNumber("M"),
                new RomanNumber("V")
            };

            Console.WriteLine("   До сортировки: " + string.Join(", ", numbers));
            Array.Sort(numbers);
            Console.WriteLine("   После сортировки: " + string.Join(", ", numbers));
            Console.WriteLine();

            Console.WriteLine("Валидация");
            TestCreation("IV");
            TestCreation("IIII");
            TestCreation("MCMX");
            TestCreation("XD");
            Console.WriteLine();

            Console.WriteLine("Преобразование:");
            RomanNumber fromInt = 42;
            Console.WriteLine($"   int 42 стал римским: {fromInt}");

            int backToInt = (int)fromInt;
            Console.WriteLine($"   обратно в int: {backToInt}");
            Console.WriteLine();

            Console.WriteLine("Ручной ввод");
            Console.WriteLine("   Введите римское число (например XIV, V, MCMLXXXIV)");
            Console.WriteLine("   или 'exit' для выхода");
            Console.WriteLine();

            while (true)
            {
                Console.Write("   > ");
                string input = Console.ReadLine().Trim().ToUpper();

                if (input == "EXIT")
                {
                    break;
                }

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                try
                {
                    var userNum = new RomanNumber(input);
                    Console.WriteLine($"      ✓ {input} -> {userNum} (это {userNum.ToInt()})");

                    Console.WriteLine($"      Сложение с X: {userNum} + X = {userNum + new RomanNumber("X")}");
                    Console.WriteLine($"      Умножение на II: {userNum} * II = {userNum * new RomanNumber("II")}");
                    Console.WriteLine($"      Сравнение с V: {userNum} > V = {userNum > new RomanNumber("V")}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"      ✗ Ошибка: {ex.Message}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("   Программа завершена");
        }

        static void TestCreation(string roman)
        {
            try
            {
                var num = new RomanNumber(roman);
                Console.WriteLine($"   ✓ {roman} -> {num} (корректно)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"   ✗ {roman} -> Ошибка: {ex.Message}");
            }
        }
    }
}