using System;
using RomanNumberLib; // ЭТО ВАЖНО! Ссылка на нашу библиотеку

namespace RomanNumberDemo // Имя пространства должно совпадать с именем проекта
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрация работы с римскими числами ===\n");

            // 1. Создание чисел
            Console.WriteLine("1. СОЗДАНИЕ ЧИСЕЛ:");
            var a = new RomanNumber("XIV");  // 14
            var b = new RomanNumber("V");    // 5
            var c = new RomanNumber("MCMLXXXIV"); // 1984

            Console.WriteLine($"   a = {a} (это {a.ToInt()})");
            Console.WriteLine($"   b = {b} (это {b.ToInt()})");
            Console.WriteLine($"   c = {c} (это {c.ToInt()})");
            Console.WriteLine();

            // 2. Арифметические операции
            Console.WriteLine("2. АРИФМЕТИЧЕСКИЕ ОПЕРАЦИИ:");
            Console.WriteLine($"   {a} + {b} = {a + b} (должно быть XIX = 19)");
            Console.WriteLine($"   {c} - {a} = {c - a} (должно быть MCMLXX = 1970)");
            Console.WriteLine($"   {a} * {b} = {a * b} (должно быть LXX = 70)");
            Console.WriteLine($"   {c} / {b} = {c / b} (должно быть CCCXCVI = 396)");
            Console.WriteLine();

            // 3. Сравнение
            Console.WriteLine("3. СРАВНЕНИЕ:");
            var x = new RomanNumber("X");   // 10
            var y = new RomanNumber("V");   // 5
            var z = new RomanNumber("X");   // 10

            Console.WriteLine($"   {x} > {y} : {x > y} (должно быть True)");
            Console.WriteLine($"   {x} < {y} : {x < y} (должно быть False)");
            Console.WriteLine($"   {x} == {z} : {x == z} (должно быть True)");
            Console.WriteLine($"   {x} != {y} : {x != y} (должно быть True)");
            Console.WriteLine();

            // 4. Работа со списком (сортировка!)
            Console.WriteLine("4. СОРТИРОВКА РИМСКИХ ЧИСЕЛ:");
            var numbers = new[]
            {
                new RomanNumber("XIV"),   // 14
                new RomanNumber("III"),   // 3
                new RomanNumber("IX"),    // 9
                new RomanNumber("M"),     // 1000
                new RomanNumber("V")      // 5
            };

            Console.WriteLine("   До сортировки: " + string.Join(", ", numbers));
            Array.Sort(numbers); // Работает благодаря IComparable!
            Console.WriteLine("   После сортировки: " + string.Join(", ", numbers));
            Console.WriteLine();

            // 5. Валидация (проверка на ошибки)
            Console.WriteLine("5. ПРОВЕРКА ВАЛИДАЦИИ:");

            TestCreation("IV");   // Правильно: 4
            TestCreation("IIII"); // Ошибка! (должна быть IV)
            TestCreation("MCMX"); // Правильно: 1910
            TestCreation("XD");   // Ошибка! (так нельзя писать)

            Console.WriteLine();

            // 6. Неявное преобразование (для удобства)
            Console.WriteLine("6. НЕЯВНОЕ ПРЕОБРАЗОВАНИЕ:");
            RomanNumber fromInt = 42; // Неявно из int
            Console.WriteLine($"   int 42 стал римским: {fromInt}");

            int backToInt = (int)fromInt; // Явно в int
            Console.WriteLine($"   обратно в int: {backToInt}");

            Console.WriteLine("\n=== Демонстрация завершена ===");
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