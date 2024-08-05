using System.Text;

namespace CSharpPractice
{
    public enum SortAlgorithm
    {
        QuickSort,
        TreeSort
    }

    public static class StringProcessor
    {
        private static readonly HttpClientHandler httpClientHandler = new HttpClientHandler();
        private static readonly HttpClient httpClient = new HttpClient(httpClientHandler);

        public static string? ValidateString(string input)
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

            return null;
        }

        public static string ProcessString(string input)
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

        public static Dictionary<char, int> GetCharCounts(string input)
        {
            var charCounts = new Dictionary<char, int>(26); // 26 букв в алфавите
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

        public static string GetLongestVowelSubstring(string input)
        {
            const string vowels = "aeiouy";
            int firstVowelIndex = -1;
            int lastVowelIndex = -1;
            int maxLength = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (vowels.Contains(input[i]))
                {
                    if (firstVowelIndex == -1)
                    {
                        firstVowelIndex = i;
                    }
                    lastVowelIndex = i;
                }
            }

            if (firstVowelIndex != -1 && lastVowelIndex != -1 && lastVowelIndex > firstVowelIndex)
            {
                maxLength = lastVowelIndex - firstVowelIndex + 1;
            }

            return maxLength == 0 ? "" : input.Substring(firstVowelIndex, maxLength);
        }

        public static string SortString(string input, SortAlgorithm algorithm)
        {
            var charArray = input.ToCharArray();
            switch (algorithm)
            {
                case SortAlgorithm.QuickSort:
                    StringSort.QuickSort(charArray, 0, charArray.Length - 1);
                    break;
                case SortAlgorithm.TreeSort:
                    charArray = StringSort.TreeSort(charArray);
                    break;
            }
            return new string(charArray);
        }

        public static async Task<string> GetReducedString(string input)
        {
            int index;
            try
            {
                var response = await httpClient.GetStringAsync(
                    "https://www.randomnumberapi.com/api/v1.0/random?min=0&max=" + (input.Length - 1));
                index = int.Parse(response.Trim('[', ']'));
            }
            catch
            {
                var random = new Random();
                index = random.Next(0, input.Length);
            }

            return input.Remove(index, 1);
        }
    }
}