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

            var processedString = ProcessString(input);
            var charCounts = GetCharCounts(processedString);
            var longestVowelSubstring = GetLongestVowelSubstring(processedString);

            var result = new StringBuilder();
            result.AppendLine(processedString);
            foreach (var pair in charCounts)
            {
                result.AppendLine($"{pair.Key}: {pair.Value}");
            }
            result.AppendLine($"Подстрока: {longestVowelSubstring}");
            return result.ToString();
        }

        private static string ProcessString(string input)
        {
            var length = input.Length;
            var sb = new StringBuilder(length * 2);

            if (length % 2 == 0)
            {
                var mid = length / 2;
                sb.Append(Reverse(input.AsSpan(0, mid)));
                sb.Append(Reverse(input.AsSpan(mid)));
            }
            else
            {
                sb.Append(Reverse(input.AsSpan()));
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

        private static Dictionary<char, int> GetCharCounts(string input)
        {
            var charCounts = new Dictionary<char, int>(26); // 26 букв в алфавите.
            foreach (var c in input)
            {
                if (charCounts.TryGetValue(c, out int count))
                {
                    charCounts[c] = count + 1;
                }
                else
                {
                    charCounts[c] = 1;
                }
            }
            return charCounts;
        }
        
        private static string GetLongestVowelSubstring(string input)
        {
            const string vowels = "aeiouy";
            var maxLength = 0;
            var startIndex = -1;

            var left = 0;
            var right = input.Length - 1;
            
            while (left < input.Length && !vowels.Contains(input[left]))
            {
                left++;
            }
            
            while (right >= 0 && !vowels.Contains(input[right]))
            {
                right--;
            }
            
            if (left < right)
            {
                maxLength = right - left + 1;
                startIndex = left;
            }

            return startIndex == -1 ? "" : input.Substring(startIndex, maxLength);
        }
    }
}