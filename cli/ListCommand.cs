using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace cli
{
    public class ListCommand:ICommand
    {
        public Command Create()
        {
            var command = new Command("list", "can list emoticons or people");

            command.AddCommand(new EmoticonsCommand().Create());
            command.AddCommand(new PeopleCommand().Create());
            return command;
        }
    }

    public class EmoticonsCommand : ICommand
    {
        public Command Create()
        {
            var command = new Command("emoticons", "List all emoticons available on this project")
            {
                Handler = CommandHandler.Create(() => { Console.WriteLine("listing emoticons"); })
            };


            return command;
        }
    }

    public class PeopleCommand : ICommand
    {
        public Command Create()
        {
            var command = new Command("people", "List all emoticons available on this project")
            {
                Handler = CommandHandler.Create(() => { Console.WriteLine("listing emoticons"); })
            };


            return command;
        }
    }
}