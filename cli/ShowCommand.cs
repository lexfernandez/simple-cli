using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace cli
{
    public class ShowCommand : ICommand
    {
        private IRetriever<Emoticon> _emoticons;
        private IRetriever<Person> _people;

        public ShowCommand(IRetriever<Emoticon> emoticons, IRetriever<Person> people)
        {
            _emoticons = emoticons;
            _people = people;
        }

        public Command Create()
        {
            var command = new Command("show", "can show an emoticon or a person");
            command.AddCommand(new ShowEmoticonCommand(_emoticons).Create());
            command.AddCommand(new ShowPersonCommand(_people).Create());

            return command;
        }
    }

    public class ShowPersonCommand : ICommand
    {
        private IRetriever<Person> _people;

        public ShowPersonCommand(IRetriever<Person> people)
        {
            _people = people;
        }

        public Command Create()
        {
            var command = new Command("person", "can show a person");

            command.AddArgument(new Argument<string>("name"));
            command.AddOption(new Option(new string[] { "--verbose", "-v" }, "if provided, it will also print the name of the emoticon")
            {
                Argument = new Argument<bool>(() => false)
            });

            command.Handler = CommandHandler.Create<string,bool>((name,verbose) =>
            {
                if (verbose)
                {
                    var person = _people.Get(name);
                    Console.WriteLine($"{person.Name,20} {person.Age}");
                }
                else
                    Console.WriteLine(_people.Get(name).Age);
            });

            return command;
        }
    }

    public class ShowEmoticonCommand : ICommand
    {
        private IRetriever<Emoticon> _emoticons;

        public ShowEmoticonCommand(IRetriever<Emoticon> emoticons)
        {
            _emoticons = emoticons;
        }

        public Command Create()
        {
            var command = new Command("emoticon","can show an emoticon");

            command.AddArgument(new Argument<string>("name"));
            command.AddOption(new Option(new string[] { "--verbose", "-v" }, "if provided, it will also print the name of the emoticon")
            {
                Argument = new Argument<bool>(() => false)
            });

            command.Handler = CommandHandler.Create<string, bool>(((name, verbose) =>
            {
                if (verbose)
                {
                    var emoji = _emoticons.Get(name);
                    Console.WriteLine($"{emoji.Name,20} {emoji.Emoji}");
                }
                else
                    Console.WriteLine(_emoticons.Get(name));
            }));

            return command;
        }
    }
}