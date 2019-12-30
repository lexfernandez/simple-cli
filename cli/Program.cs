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
        static void Main(string[] args)
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
        }
    }
}
