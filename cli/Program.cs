using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddYamlFile("appsettings.yaml", true, true)
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var emoticons = config.GetSection("emoticons")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            var people = new List<Person>();
            config.GetSection("people").Bind(people);

            var rootCommand = new RootCommand("My app cli")
            {
                new Option("--int-opt","get an int option")
                {
                    Argument = new Argument<int>(),
                    Required = true
                },
                new Option("--bool-opt","get a bool option")
                {
                    Argument = new Argument<bool>()
                },
                new Option("--file-opt","get a file option")
                {
                    Argument = new Argument<FileInfo>().ExistingOnly()
                },
                
            };
            
            rootCommand.Handler = CommandHandler.Create<int, bool, FileInfo>((intOpt, boolOpt, fileOpt) =>
            {
                Console.WriteLine($"an int: {intOpt}");
                Console.WriteLine($"a bool: {boolOpt}");
                Console.WriteLine($"a file: {fileOpt}");
            });

            return rootCommand.Invoke(args);


        }
    }
}
