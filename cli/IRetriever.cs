using System.Collections;
using System.Collections.Generic;

namespace cli
{
    public interface IRetriever<T>
    {
        IEnumerable<T> List();
        T Get(string name);
    }
}