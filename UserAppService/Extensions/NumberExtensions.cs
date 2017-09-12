using System;

namespace UserAppService.Utility.Extensions
{
    public static class EntityIdExtensions
    {
        /// <summary>
        /// Determine if the provided unique identifier is a transient value, i.e. the entity has not yet been added to the data store, thus its id is zero (0).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsTransient(this int id)
        {
            return id == 0;
        }

        /// <summary>
        /// Return the decimal part of a double value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double DecimalPart(this double value)
        {
            return value - Math.Truncate(value);
        }

        public static double RoundToNearestQuarter(this double value)
        {
            //return Math.Floor(value/nearestOf+fairness)*nearestOf;
            return Math.Round(value * 4, MidpointRounding.ToEven) / 4;
        }
    }
}
