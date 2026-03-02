using System;
using System.Collections.Generic;
using System.Text;

namespace RomanNumberLib
{
    /// <summary>
    /// Представляет римское число как полноценный тип данных.
    /// Поддерживает арифметические операции и сравнение.
    /// </summary>
    public readonly struct RomanNumber : IComparable<RomanNumber>, IEquatable<RomanNumber>
    {
        // Внутреннее представление - обычное целое число
        private readonly int _value;

        // Словарь для конвертации числа в римскую запись
        private static readonly (int Value, string Symbol)[] _romanMap = new[]
        {
            (1000, "M"),
            (900, "CM"),
            (500, "D"),
            (400, "CD"),
            (100, "C"),
            (90, "XC"),
            (50, "L"),
            (40, "XL"),
            (10, "X"),
            (9, "IX"),
            (5, "V"),
            (4, "IV"),
            (1, "I")
        };

        /// <summary>
        /// Создает римское число из строки (например, "XIV")
        /// </summary>
        /// <param name="roman">Строка с римским числом (только заглавные латинские буквы)</param>
        /// <exception cref="ArgumentException">Выбрасывается, если строка не является корректным римским числом</exception>
        public RomanNumber(string roman)
        {
            if (string.IsNullOrWhiteSpace(roman))
                throw new ArgumentException("Римское число не может быть пустым", nameof(roman));

            _value = ParseRoman(roman);
        }

        // Приватный конструктор для внутреннего использования (из int)
        private RomanNumber(int value)
        {
            if (value < 1 || value > 3999)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Римские числа поддерживаются только в диапазоне 1-3999");

            _value = value;
        }

        /// <summary>
        /// Парсит строку с римским числом
        /// </summary>
        private static int ParseRoman(string roman)
        {
            int total = 0;
            int previousValue = 0;

            // Идем по строке справа налево (так проще обрабатывать вычитание)
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                char c = roman[i];
                int currentValue = RomanCharToInt(c);

                if (currentValue == 0)
                    throw new ArgumentException($"Недопустимый символ в римском числе: {c}");

                // Если текущее значение меньше предыдущего, значит это вычитание (IV = 4)
                if (currentValue < previousValue)
                    total -= currentValue;
                else
                    total += currentValue;

                previousValue = currentValue;
            }

            // Дополнительная проверка: конвертируем обратно и сравниваем
            // Это отсекает случаи типа "IIII" (которые распарсятся, но это невалидная запись)
            string validation = ConvertToRoman(total);
            if (validation != roman)
                throw new ArgumentException($"Неверный формат римского числа: {roman}. Правильная запись: {validation}");

            return total;
        }

        /// <summary>
        /// Конвертирует один римский символ в число
        /// </summary>
        private static int RomanCharToInt(char c)
        {
            return c switch
            {
                'I' => 1,
                'V' => 5,
                'X' => 10,
                'L' => 50,
                'C' => 100,
                'D' => 500,
                'M' => 1000,
                _ => 0
            };
        }

        /// <summary>
        /// Конвертирует целое число в римскую запись
        /// </summary>
        private static string ConvertToRoman(int number)
        {
            if (number < 1 || number > 3999)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Римские числа поддерживаются только в диапазоне 1-3999");

            var result = new StringBuilder();
            int remaining = number;

            foreach (var pair in _romanMap)
            {
                while (remaining >= pair.Value)
                {
                    result.Append(pair.Symbol);
                    remaining -= pair.Value;
                }
            }

            return result.ToString();
        }

        // ============== АРИФМЕТИЧЕСКИЕ ОПЕРАЦИИ ==============

        /// <summary>
        /// Складывает два римских числа
        /// </summary>
        public static RomanNumber operator +(RomanNumber a, RomanNumber b)
        {
            return new RomanNumber(a._value + b._value);
        }

        /// <summary>
        /// Вычитает одно римское число из другого
        /// </summary>
        public static RomanNumber operator -(RomanNumber a, RomanNumber b)
        {
            return new RomanNumber(a._value - b._value);
        }

        /// <summary>
        /// Умножает два римских числа
        /// </summary>
        public static RomanNumber operator *(RomanNumber a, RomanNumber b)
        {
            return new RomanNumber(a._value * b._value);
        }

        /// <summary>
        /// Делит одно римское число на другое (целочисленное деление)
        /// </summary>
        public static RomanNumber operator /(RomanNumber a, RomanNumber b)
        {
            if (b._value == 0)
                throw new DivideByZeroException("Деление на ноль");

            return new RomanNumber(a._value / b._value);
        }

        // ============== ОПЕРАЦИИ СРАВНЕНИЯ ==============

        public static bool operator >(RomanNumber a, RomanNumber b) => a._value > b._value;
        public static bool operator <(RomanNumber a, RomanNumber b) => a._value < b._value;
        public static bool operator >=(RomanNumber a, RomanNumber b) => a._value >= b._value;
        public static bool operator <=(RomanNumber a, RomanNumber b) => a._value <= b._value;
        public static bool operator ==(RomanNumber a, RomanNumber b) => a._value == b._value;
        public static bool operator !=(RomanNumber a, RomanNumber b) => a._value != b._value;

        // ============== МЕТОДЫ СРАВНЕНИЯ ==============

        public int CompareTo(RomanNumber other) => _value.CompareTo(other._value);
        public bool Equals(RomanNumber other) => _value == other._value;
        public override bool Equals(object? obj) => obj is RomanNumber other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        // ============== ПРЕОБРАЗОВАНИЕ ==============

        /// <summary>
        /// Возвращает римское число в виде строки (например, "XIV")
        /// </summary>
        public override string ToString() => ConvertToRoman(_value);

        /// <summary>
        /// Возвращает целочисленное значение римского числа
        /// </summary>
        public int ToInt() => _value;

        /// <summary>
        /// Неявное преобразование из int в RomanNumber (удобно для тестов)
        /// </summary>
        public static implicit operator RomanNumber(int value) => new RomanNumber(value);

        /// <summary>
        /// Явное преобразование из RomanNumber в int
        /// </summary>
        public static explicit operator int(RomanNumber roman) => roman._value;
    }
}