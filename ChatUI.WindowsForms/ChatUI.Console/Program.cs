namespace ChatUI.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Welcome to ChatUI based on OAML");
        System.Console.WriteLine("===============================");

        while (true)
        {
            var input = System.Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                input = "exit";

            if (input.Contains("exit", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }
            
            System.Console.WriteLine($"Command: {input}");
        }
    }
}