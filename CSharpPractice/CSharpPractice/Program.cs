namespace CSharpPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            var input = Console.ReadLine();

            var validationMessage = StringProcessor.ValidateString(input);
            if (validationMessage != null)
            {
                Console.WriteLine(validationMessage);
                return;
            }

            var (processedString, charCounts, longestVowelSubstring) = StringProcessor.Process(input);

            var algorithm = GetSortAlgorithmFromUser();

            var sortedString = StringProcessor.SortString(processedString, algorithm);

            var result = new System.Text.StringBuilder();
            result.AppendLine($"Обработанная строка: {processedString}");
            result.AppendLine("Информация о количестве повторений каждого символа:");
            foreach (var pair in charCounts)
            {
                result.AppendLine($"{pair.Key}: {pair.Value}");
            }
            result.AppendLine($"Самая длинная подстрока, начинающаяся и заканчивающаяся на гласную: {longestVowelSubstring}");
            result.AppendLine($"Отсортированная обработанная строка: {sortedString}");

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