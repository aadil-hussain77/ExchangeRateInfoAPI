namespace ExchangeRateContract.Response
{
    /// <summary>
    /// Exchange rate response class
    /// </summary>
    public class ExchangeRateResponse
    {
        /// <summary>
        /// Minimum exchange rate during the period
        /// </summary>
        public string MinRate { get; set; }

        /// <summary>
        /// Maximum exchange rate during the period
        /// </summary>
        public string MaxRate { get; set; }

        /// <summary>
        /// Average exchange rate during the period
        /// </summary>
        public string AverageRate { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public ErrorResponse Error { get; set; }
    }
}
