using System;

namespace UserAppService.Utility.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal GetMonths(this decimal years)
        {
            decimal months = years * 12;
            return months;
        }

        public static decimal GetYears(this decimal months)
        {
            decimal years = months / 12;
            return years;
        }

        public static decimal SetDefaultTestValue(this decimal i, decimal testValue)
        {
            if (i == 0) i = testValue;
            return i;
        }

        //http://stackoverflow.com/questions/4127363/date-difference-in-years-c-sharp
        /// <summary>
        /// Gets the total number of years between two dates, rounded to whole months.
        /// Examples: 
        /// 2011-12-14, 2012-12-15 returns 1.
        /// 2011-12-14, 2012-12-14 returns 1.
        /// 2011-12-14, 2012-12-13 returns 0,9167.
        /// </summary>
        /// <param name="start">
        /// Stardate of time period
        /// </param>
        /// <param name="end">
        /// Enddate of time period
        /// </param>
        /// <returns>
        /// Total Years between the two days
        /// </returns>
        public static decimal DifferenceTotalYears(this DateTime start, DateTime end)
        {
            // Get difference in total months.
            decimal months = ((end.Year - start.Year) * 12) + (end.Month - start.Month);

            // substract 1 month if end month is not completed
            if (end.Day < start.Day)
            {
                months--;
            }

            decimal totalyears = months / 12;
            return totalyears;
        }

        public static double RoundToNearestQuarter(this double value)
        {
            //return Math.Floor(value/nearestOf+fairness)*nearestOf;
            return Math.Round(value * 4, MidpointRounding.ToEven) / 4;
        }
    }
}
