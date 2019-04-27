

namespace Citizens.Core
{

    using System.Collections.Concurrent;

    public class Counter
    {
        private ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();
        public int Get(string name)
        {
            if (!dictionary.ContainsKey(name) || dictionary[name] > int.MaxValue - 1)
            {
                dictionary[name] = 0;
            }
            dictionary[name] = dictionary[name] + 1;
            return dictionary[name];
        }
    }
}
