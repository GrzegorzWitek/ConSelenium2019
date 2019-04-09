using System.Collections.Generic;

namespace GAExample.SeleniumUtils
{
    internal class DataContext
    {
        public Dictionary<string, object> Current { get; }

        public DataContext()
        {
            Current = new Dictionary<string, object>();
        }
    }
}
