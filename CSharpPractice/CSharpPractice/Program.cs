namespace CSharpPractice
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            var input = Console.ReadLine();

            var validationMessage = StringProcessor.ValidateString(input);
            if (validationMessage != null)
            {
                Console.WriteLine(validationMessage);
                return;
            }
            
            var processedString = StringProcessor.ProcessString(input);
            var charCounts = StringProcessor.GetCharCounts(processedString);
            var longestVowelSubstring = StringProcessor.GetLongestVowelSubstring(processedString);
            
            var algorithm = GetSortAlgorithmFromUser();
            var sortedString = StringProcessor.SortString(processedString, algorithm);

            var reducedString = await StringProcessor.GetReducedString(processedString);

            var result = new System.Text.StringBuilder();
            result.AppendLine($"Обработанная строка: {processedString}");
            result.AppendLine("Информация о количестве повторений каждого символа:");
            foreach (var pair in charCounts)
            {
                result.AppendLine($"{pair.Key}: {pair.Value}");
            }
            result.AppendLine($"Самая длинная подстрока, начинающаяся и заканчивающаяся на гласную: {longestVowelSubstring}");
            result.AppendLine($"Отсортированная обработанная строка: {sortedString}");
            result.AppendLine($"Урезанная обработанная строка: {reducedString}");

            Console.WriteLine(result.ToString());
        }

        private static SortAlgorithm GetSortAlgorithmFromUser()
        {
            Console.WriteLine("Выберите алгоритм сортировки (1 - QuickSort, 2 - TreeSort):");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && (choice == 1 || choice == 2))
            {
                return choice == 1 ? SortAlgorithm.QuickSort : SortAlgorithm.TreeSort;
            }

            Console.WriteLine("Неверный ввод. По умолчанию выбрана QuickSort.");
            return SortAlgorithm.QuickSort;
        }
    }
}