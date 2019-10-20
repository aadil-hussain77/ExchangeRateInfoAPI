namespace ExchangeRateBL.BL
{
    using ExchangeRateModel.Input;
    using ExchangeRateModel.Output;

    /// <summary>
    /// Interface for ExchangeRateManager class
    /// </summary>
    public interface IExchangeRateManager
    {
        /// <summary>
        /// Method to fetch required exchange rate information based on input data
        /// </summary>
        /// <param name="exchangeRateInput">Exchange rate input model</param>
        /// <returns>Exchange rate information based on input data</returns>
        ExchangeRateOutput GetExchangeRateInformation(ExchangeRateInput exchangeRateInput);
    }
}
