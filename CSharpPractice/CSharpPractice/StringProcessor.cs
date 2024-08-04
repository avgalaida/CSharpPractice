namespace CSharpPractice
{
    public static class StringProcessor
    {
        public static string Process(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var length = input.Length;

            if (length % 2 == 0)
            {
                var mid = length / 2;
                return Reverse(input.AsSpan(0, mid)) + Reverse(input.AsSpan(mid));
            }
            else
            {
                var reversed = Reverse(input.AsSpan());
                return reversed + input;
            }
        }

        private static string Reverse(ReadOnlySpan<char> span)
        {
            var array = new char[span.Length];
            for (int i = 0, j = span.Length - 1; i < span.Length; i++, j--)
            {
                array[i] = span[j];
            }
            return new string(array);
        }
    }
}