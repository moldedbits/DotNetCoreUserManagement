namespace UserAppService.Utility.Extensions
{
    public static class IntExtensions
    {

        public static int SetValueIfZero(this int i, int testValue)
        {
            if (i==0) i = testValue;
            return i;
        }


        public static string ToOrdinal(this int number)
        {
            string result = number.ToString();
            int firstDigit = number < 10 ? number : number % 10; //first from the right side!
            int secondDigit = number < 10 ? 0 : (number % 100) / 10;
            switch (secondDigit)
            {
                case 1:
                    result += "th";
                    break;
                default:
                    switch (firstDigit)
                    {
                        case 1: result += "st"; break;
                        case 2: result += "nd"; break;
                        case 3: result += "rd"; break;
                        default: result += "th"; break;
                    }
                    break;
            }
            return result;
        }
    }
}
