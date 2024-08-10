namespace Helper.Constant
{
    /// <summary>
    /// Class to define common constants
    /// </summary>
    public static class ExchangeRateConstant
    {
        /// <summary>
        /// Key name to read the service url from config file
        /// </summary>
        public const string PathKey = "Api_Url";

        /// <summary>
        /// Pattern to set base currency
        /// </summary>
        public const string BaseCurrency = "?base=";

        /// <summary>
        /// Pattern to set base currency
        /// </summary>
        public const string BaseCurrencyForPeriod = "&base=";

        /// <summary>
        /// Pattern to set target currency
        /// </summary>
        public const string TargetCurrency = "&symbols=";

        /// <summary>
        /// Pattern to read rate
        /// </summary>
        public const string Rate = "rates";

        /// <summary>
        /// Required date fromat
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Required content type
        /// </summary>
        public const string ContentType = "application/json";

        /// <summary>
        /// Required content type
        /// </summary>
        public const string APIKey = "apikey";

        /// <summary>
        /// Pattern to set start date
        /// </summary>
        public const string StartAt = "timeseries?start_date=";

        /// <summary>
        /// Pattern to set end date
        /// </summary>
        public const string EndAt = "&end_date=";

        /// <summary>
        /// Invalid input error message
        /// </summary>
        public const string InvalidInut = "Please provide a valid input object.";

        /// <summary>
        /// Invalid base currency error message
        /// </summary>
        public const string InvalidBaseCurency = "Please provide a valid base currency.";

        /// <summary>
        /// Invalid input date error message
        /// </summary>
        public const string InvalidDates = "Please provide a valid set of dates.";

        /// <summary>
        /// Invalid target currency error message
        /// </summary>
        public const string InvalidTargetCurrency = "Please provide a valid target currency.";

        /// <summary>
        /// Set of char to remove from string
        /// </summary>
        public static char[] charToRemove = { '}', '"' };
    }
}
