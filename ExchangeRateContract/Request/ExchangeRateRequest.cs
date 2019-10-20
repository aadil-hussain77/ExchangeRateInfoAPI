namespace ExchangeRateContract.Request
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Exchange rate request class
    /// </summary>
    public class ExchangeRateRequest
    {
        /// <summary>
        /// Base currency for exchange
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        ///Target Currency for exchange
        /// </summary>
        public string TargetCurrency { get; set; }

        /// <summary>
        /// Set of dates for exchange
        /// </summary>
        public string[] Dates { get; set; }
    }
}
