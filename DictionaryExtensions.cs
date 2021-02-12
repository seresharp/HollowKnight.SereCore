using System.Collections.Generic;
using JetBrains.Annotations;

namespace SereCore
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

        public static void Deconstruct<TKey, TVal>(this KeyValuePair<TKey, TVal> self, out TKey key, out TVal val)
        {
            key = self.Key;
            val = self.Value;
        }
    }
}
