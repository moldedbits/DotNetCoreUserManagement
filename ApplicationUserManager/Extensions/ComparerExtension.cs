using System.Collections.Generic;
using System.Reflection;

namespace UserAppService.Utility.Extensions
{
    internal static class ComparerExtension
    {
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            var variances = new List<Variance>();
            var properties = val1.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                var v = new Variance();
                v.Prop = pi.Name;
                v.valA = pi.GetValue(val1);
                v.valB = pi.GetValue(val2);
                if (!v.valA.Equals(v.valB))
                    variances.Add(v);
            }
            
            return variances;
        }
    }

    public class Variance
    {
        public string Prop { get; set; }

        public object valA { get; set; }

        public object valB { get; set; }
    }
}