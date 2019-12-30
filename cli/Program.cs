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

            var serviceCollection = new ServiceCollection();
                serviceCollection.AddTransient<IRetriever<Emoticon>, ConfigEmoticonRetriever>();
                serviceCollection.AddTransient<IRetriever<Person>, ConfigPeopleRetriever>();
                serviceCollection.Add(new ServiceDescriptor(typeof(IConfigurationRoot), config));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var emoticons = serviceProvider.GetService<IRetriever<Emoticon>>();
            var people = serviceProvider.GetService<IRetriever<Person>>();

            var command = new CommandLineBuilder().AddCommand(new ListCommand().Create()).UseDefaults().Build();
            return command.Invoke(args);
        }
    }
}
