namespace ExchangeRateModel.Output
{
    /// <summary>
    /// Exchange rate output class
    /// </summary>
    public class ExchangeRateOutput
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
        public ErrorModel Error { get; set; }
    }
}
