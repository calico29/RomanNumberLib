using System;
using System.Collections.Generic;
using System.Text;
using RomanNumberLib;
namespace RomanNumberLib
{
    
    public readonly struct RomanNumber : IComparable<RomanNumber>, IEquatable<RomanNumber>
    {
        
        
        private readonly int _value;

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

        public RomanNumber(string roman)
        {
            if (string.IsNullOrWhiteSpace(roman))
                throw new ArgumentException("Римское число не может быть пустым", nameof(roman));

            _value = ParseRoman(roman);
        }

        private RomanNumber(int value)
        {
            if (value < 1 || value > 3999)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Римские числа поддерживаются только в диапазоне 1-3999");

            _value = value;
        }

        private static int ParseRoman(string roman)
        {
            int total = 0;
            int previousValue = 0;

            for (int i = roman.Length - 1; i >= 0; i--)
            {
                char c = roman[i];
                int currentValue = RomanCharToInt(c);

                if (currentValue == 0)
                    throw new ArgumentException($"Недопустимый символ в римском числе: {c}");

                if (currentValue < previousValue)
                    total -= currentValue;
                else
                    total += currentValue;

                previousValue = currentValue;
            }

            string validation = ConvertToRoman(total);
            if (validation != roman)
                throw new ArgumentException($"Неверный формат римского числа: {roman}. Правильная запись: {validation}");

            return total;
        }

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

       
        public static RomanNumber operator +(RomanNumber a, RomanNumber b)
        {
            return new RomanNumber(a._value + b._value);
        }

        public static RomanNumber operator -(RomanNumber a, RomanNumber b)
        {
            return new RomanNumber(a._value - b._value);
        }

        public static RomanNumber operator *(RomanNumber a, RomanNumber b)
        {
            return new RomanNumber(a._value * b._value);
        }

        public static RomanNumber operator /(RomanNumber a, RomanNumber b)
        {
            if (b._value == 0)
                throw new DivideByZeroException("Деление на ноль");

            return new RomanNumber(a._value / b._value);
        }


        public static bool operator >(RomanNumber a, RomanNumber b) => a._value > b._value;
        public static bool operator <(RomanNumber a, RomanNumber b) => a._value < b._value;
        public static bool operator >=(RomanNumber a, RomanNumber b) => a._value >= b._value;
        public static bool operator <=(RomanNumber a, RomanNumber b) => a._value <= b._value;
        public static bool operator ==(RomanNumber a, RomanNumber b) => a._value == b._value;
        public static bool operator !=(RomanNumber a, RomanNumber b) => a._value != b._value;


        public int CompareTo(RomanNumber other) => _value.CompareTo(other._value);
        public bool Equals(RomanNumber other) => _value == other._value;
        public override bool Equals(object? obj) => obj is RomanNumber other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();

        public override string ToString() => ConvertToRoman(_value);

        public int ToInt() => _value;

        public static implicit operator RomanNumber(int value) => new RomanNumber(value);

        public static explicit operator int(RomanNumber roman) => roman._value;
    }
}