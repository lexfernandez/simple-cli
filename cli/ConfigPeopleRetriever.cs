using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace cli
{
    public class ConfigPeopleRetriever:IRetriever<Person>
    {
        private Dictionary<string, Person> _people;

        public ConfigPeopleRetriever(IConfigurationRoot config)
        {
            var people = new List<Person>();
            config.GetSection("people").Bind(people);

            _people = people.ToDictionary(person => person.Name, person => person);
        }
        public IEnumerable<Person> List()
        {
            return _people.Values.ToList();
        }

        public Person Get(string name)
        {
            return _people[name];
        }
    }
}