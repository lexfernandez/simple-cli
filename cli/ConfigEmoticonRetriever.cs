using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace cli
{
    public class ConfigEmoticonRetriever : IRetriever<Emoticon>
    {
        private Dictionary<string, Emoticon> _emoticons;

        public ConfigEmoticonRetriever(IConfigurationRoot config)
        {

            var emoticons = new Dictionary<string,string>();
            config.GetSection("emoticons").Bind(emoticons);

            _emoticons = emoticons.ToDictionary(emoticon => emoticon.Key, emoticon => new Emoticon(emoticon.Key,emoticon.Value));
        }
        public IEnumerable<Emoticon> List()
        {
            return _emoticons.Values.ToList();
        }

        public Emoticon Get(string name)
        {
            return _emoticons[name];
        }
    }
}