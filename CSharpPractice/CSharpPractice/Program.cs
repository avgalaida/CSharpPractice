namespace CSharpPractice
{
    static class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var result = StringProcessor.Process(input);
            Console.WriteLine(result);
        }
    }
}