namespace ExchangeRateContract.Response
{
    using System.Net;

    /// <summary>
    /// Class to manage error data
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Get or set Error Code
        /// </summary>
        public HttpStatusCode ErrorCode { get; set; }

        /// <summary>
        /// Get or Set Error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
