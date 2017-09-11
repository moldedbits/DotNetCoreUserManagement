using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UserAppService.Utility.Extensions
{
    public static class EnumerableExtensions
    {

        public static bool HasValues(this IEnumerable items)
        {
            if(items == null){
                return false;
            }

            if (items.Cast<object>().Any()){
                return true;
            }
            
            return false;
        }

        public static bool HasNoValues(this IEnumerable items)
        {
            return !items.HasValues();
        }

        public static string ToConcatString(this IEnumerable items, string delimiter)
        {
            bool first = true;

            var sb = new StringBuilder();
            foreach (object item in items)
            {
                if (item == null)
                    continue;

                if (!first)
                {
                    sb.Append(delimiter);
                }
                else
                {
                    first = false;
                }
                sb.Append(item);
            }
            return sb.ToString();
        }

        public static T GetEnumFromString<T>(string enumName, T defaultType) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            Enum.TryParse(enumName, true, out defaultType);
            return defaultType;
        }

        //public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        //{
        //    var knownKeys = new HashSet<TKey>(comparer);
        //    foreach (var element in source)
        //    {
        //        if (element != null && knownKeys.Add(keySelector(element)))
        //        {
        //            yield return element;
        //        }
        //    }
        //}

        public static IEnumerable<T> GetLocalized<T>(this IEnumerable<T> source, CultureInfo culture, Func<T, string> languageKey,
            Func<T, string> keySelector)
        {
            return source
                .Where(
                    item => string.Equals(languageKey(item), culture.Name, StringComparison.InvariantCultureIgnoreCase)
                            ||
                            string.Equals(languageKey(item), culture.TwoLetterISOLanguageName,
                                StringComparison.InvariantCultureIgnoreCase))
                .GroupBy(keySelector)
                .Select(group =>
                {
                    var groupItems = group.ToList();
                    if (groupItems.Count == 1)
                        return groupItems[0];

                    var exactMatch =
                        groupItems.FirstOrDefault(
                            item =>
                                string.Equals(languageKey(item), culture.Name,
                                    StringComparison.InvariantCultureIgnoreCase));
                    if (exactMatch != null)
                    {
                        return exactMatch;
                    }

                    return groupItems.First();
                });
        }
    }
}
