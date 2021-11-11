using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Splits the provided list into multiple chunks of specified size
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="source">Source list to split</param>
        /// <param name="size">Number of items in chuncked lists</param>
        /// <returns></returns>
        public static IEnumerable<List<T>> SplitList<T>(this List<T> source, int size)
        {
            for (int i = 0; i < source.Count; i += size)
            {
                yield return source.GetRange(i, Math.Min(size, source.Count - i));
            }
        }
    }
}
