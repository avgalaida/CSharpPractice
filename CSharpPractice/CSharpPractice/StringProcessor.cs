using System.Text;

namespace CSharpPractice
{
    public static class StringProcessor
    {
        public static string Process(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "Ошибка: пустая строка.";
            }
            
            var invalidChars = GetInvalidChars(input);
            
            if (invalidChars.Length > 0)
            {
                return $"Ошибка: найдены неподходящие символы - {invalidChars}.";
            }

            var length = input.Length;
            var sb = new StringBuilder();

            if (length % 2 == 0)
            {
                var mid = length / 2;
                sb.Append(Reverse(input.AsSpan(0, mid)));
                sb.Append(Reverse(input.AsSpan(mid)));
            }
            else
            {
                var reversed = Reverse(input.AsSpan());
                sb.Append(reversed);
                sb.Append(input);
            }

            return sb.ToString();
        }

        private static string Reverse(ReadOnlySpan<char> span)
        {
            var array = new char[span.Length];
            span.CopyTo(array);
            Array.Reverse(array);
            return new string(array);
        }
        
        private static string GetInvalidChars(string input)
        {
            var invalidChars = new StringBuilder();
            foreach (char c in input)
            {
                if (c < 'a' || c > 'z')
                {
                    invalidChars.Append(c);
                }
            }
            return invalidChars.ToString();
        }
    }
}