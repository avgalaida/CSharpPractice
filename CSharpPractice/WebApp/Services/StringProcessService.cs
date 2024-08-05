using System.Text;
using WebApp.Models;
using WebApp.Utils;

namespace WebApp.Services;
public class StringProcessService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public StringProcessService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public string? ValidateString(string input)
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

        var blacklistedWord = CheckBlacklist(input);

        if (!string.IsNullOrEmpty(blacklistedWord))
        {
            return $"Ошибка: найдено слово из черного списка - {blacklistedWord}.";
        }

        return null;
    }

    private string? CheckBlacklist(string input)
    {
        var blacklist = _configuration.GetSection("Settings:BlackList").Get<List<string>>();
        return blacklist.Contains(input) ? input : null;
    }

    public string ProcessString(string input)
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

    public Dictionary<char, int> GetCharCounts(string input)
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

    public string GetLongestVowelSubstring(string input)
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

    public string SortString(string input, SortAlgorithmNames algorithm)
    {
        var charArray = input.ToCharArray();
        switch (algorithm)
        {
            case SortAlgorithmNames.QuickSort:
                StringSort.QuickSort(charArray, 0, charArray.Length - 1);
                break;
            case SortAlgorithmNames.TreeSort:
                charArray = StringSort.TreeSort(charArray);
                break;
        }
        return new string(charArray);
    }

    public async Task<string> GetReducedString(string input)
    {
        int index;
        var apiUrl = _configuration["RandomApi"] + "/random?min=0&max=";
        try
        {
            var response = await _httpClient.GetStringAsync(apiUrl + (input.Length - 1));
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