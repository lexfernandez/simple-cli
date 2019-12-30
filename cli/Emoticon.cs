namespace cli
{
    public class Emoticon
    {

        public Emoticon(string name, string emoji)
        {
            Name = name;
            Emoji=emoji;
        }
        public string Name { get; set; }
        public string Emoji { get; set; }

        public override string ToString()
        {
            return Emoji;
        }
    }
}