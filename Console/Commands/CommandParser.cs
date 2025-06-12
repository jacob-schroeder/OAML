namespace Console.Commands;

public class CommandParser
{
    public static Command Parse(string input)
    {
        return new Command()
        {
            action = "send",
            node = "john",
            crypt = "aes",
            message = "Hey man!"
        };
    }
}

//example command: send message john aes "Hey man!"
/*
 * var command = input.parse();
 * var nodeName = command.name; //john
 * var cryptName = command.crypt; //aes
 * var msg = command.message; //"Hey man!"
 */
public class Command
{
    public string action { get; set; }
    public string node { get; set; }
    public string crypt { get; set; }
    public string message { get; set; }
}