using System.CommandLine;

namespace cli
{
    public interface ICommand
    {
        Command Create();
    }
}