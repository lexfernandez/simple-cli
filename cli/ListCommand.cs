using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;

namespace cli
{
    public class ListCommand : ICommand
    {
        private IRetriever<Emoticon> _emoticons;
        private IRetriever<Person> _people;

        public ListCommand(IRetriever<Emoticon> emoticons, IRetriever<Person> people)
        {
            _emoticons = emoticons;
            _people = people;
        }

        public Command Create()
        {
            var command = new Command("list", "can list emoticons or people");

            command.AddCommand(new EmoticonsCommand(_emoticons).Create());
            command.AddCommand(new PeopleCommand(_people).Create());
            return command;
        }
    }

    public class EmoticonsCommand : ICommand
    {
        private IRetriever<Emoticon> _emoticons;

        public EmoticonsCommand(IRetriever<Emoticon> emoticons)
        {
            _emoticons = emoticons;

        }
        public Command Create()
        {
            var command = new Command("emoticons", "List all emoticons available on this project");

            command.AddOption(new Option(new string[] { "--verbose", "-v" }, "if provided, it will also print the name of the emoticon")
            {
                Argument = new Argument<bool>(() => false)
            });

            command.Handler = CommandHandler.Create<bool>((bool verbose) =>
            {
                foreach (Emoticon emoticon in _emoticons.List())
                {
                    Console.WriteLine(verbose ? $"{emoticon.Name,-20} {emoticon.Emoji}" : emoticon.Emoji);
                }
            });

            return command;
        }
    }

    public class PeopleCommand : ICommand
    {
        private IRetriever<Person> _people;

        public PeopleCommand(IRetriever<Person> people)
        {
            _people = people;
        }

        public Command Create()
        {
            var command = new Command("people", "List people available on this project");

            command.AddOption(new Option(new string[] { "--verbose", "-v" }, "if provided, it will also print the age of the people")
            {
                Argument = new Argument<bool>(() => false)
            });

            command.Handler = CommandHandler.Create<bool>(verbose =>
            {
                foreach (Person person in _people.List())
                {
                    Console.WriteLine(verbose ? $"{person.Name,-20} {person.Age.ToString()}" : person.Name.ToString());
                }
            });

            return command;
        }
    }
}