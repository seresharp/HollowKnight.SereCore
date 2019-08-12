using System.Collections.Generic;
using JetBrains.Annotations;

namespace SeanprCore
{
    [PublicAPI]
    public static class DictionaryExtensions
    {
        public static IEnumerable<(TKey, TVal)> AsTuples<TKey, TVal>(this IDictionary<TKey, TVal> self)
        {
            foreach (KeyValuePair<TKey, TVal> pair in self)
            {
                yield return (pair.Key, pair.Value);
            }
        }
    }
}
