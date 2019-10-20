namespace ExchangeRateInfoAPI.Controllers
{
    using ExchangeRateBL.BL;
    using ExchangeRateContract.Request;
    using ExchangeRateContract.Response;
    using ExchangeRateModel.Input;
    using Helper.Constant;
    using System;
    using System.Net;
    using System.Web.Http;

    /// <summary>
    /// Exchange rate api controller handles input requests
    /// </summary>
    [System.Web.Http.RoutePrefix("ExchangeRate")]
    public class ExchangeRateController : ApiController
    {
        /// <summary>
        /// Exchange rate manager interface
        /// </summary>
        private IExchangeRateManager _exchangeRateManager;

        /// <summary>
        /// Paramaterized constructor of controller to inject required instancea
        /// </summary>
        /// <param name="exchangeRateManager">Instance of ExchangeRateManager class</param>        
        public ExchangeRateController(IExchangeRateManager exchangeRateManager)
        {
            _exchangeRateManager = exchangeRateManager;
        }

        /// <summary>
        /// Operation to fetch exchange rate information based on input
        /// </summary>
        /// <param name="exchangeRateRequest"></param>
        /// <returns>Exchange rate response contains max, min and average rate</returns>
        [System.Web.Http.Route("GetExchangeRateInformation")]
        [System.Web.Http.HttpGet]
        public ExchangeRateResponse GetExchangeRateInformation(ExchangeRateRequest exchangeRateRequest)
        {
            var exchangeRateResponse = new ExchangeRateResponse();
            try
            {
                exchangeRateResponse = ValidateInput(exchangeRateRequest, exchangeRateResponse);
                if (exchangeRateResponse.Error == null)
                {
                    var exchangerateinput = new ExchangeRateInput
                    {
                        BaseCurrency = exchangeRateRequest.BaseCurrency,
                        TargetCurrency = exchangeRateRequest.TargetCurrency,
                        Dates = exchangeRateRequest.Dates
                    };
                    var result = _exchangeRateManager.GetExchangeRateInformation(exchangerateinput);
                    if (result.Error == null)
                    {
                        exchangeRateResponse.MinRate = result.MinRate;
                        exchangeRateResponse.MaxRate = result.MaxRate;
                        exchangeRateResponse.AverageRate = result.AverageRate;
                    }
                    else
                    {
                        exchangeRateResponse.Error = new ErrorResponse
                        {
                            ErrorCode = result.Error.ErrorCode,
                            ErrorMessage = result.Error.ErrorMessage
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                exchangeRateResponse.Error = new ErrorResponse
                {
                    ErrorCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
            }
            return exchangeRateResponse;
        }

        /// <summary>
        /// Method to valid input data
        /// </summary>
        /// <param name="exchangeRateRequest"></param>
        /// <param name="exchangeRateResponse"></param>
        /// <returns></returns>
        private ExchangeRateResponse ValidateInput(ExchangeRateRequest exchangeRateRequest, ExchangeRateResponse exchangeRateResponse)
        {
            try
            {
                if (exchangeRateRequest == null)
                {
                    exchangeRateResponse.Error = new ErrorResponse
                    {
                        ErrorCode = HttpStatusCode.NoContent,
                        ErrorMessage = ExchangeRateConstant.InvalidInut
                    };
                }
                else if (exchangeRateRequest.BaseCurrency == null || string.IsNullOrEmpty(exchangeRateRequest.BaseCurrency.Trim()))
                {
                    exchangeRateResponse.Error = new ErrorResponse
                    {
                        ErrorCode = HttpStatusCode.PartialContent,
                        ErrorMessage = ExchangeRateConstant.InvalidBaseCurency
                    };
                }
                else if (exchangeRateRequest.TargetCurrency == null || string.IsNullOrEmpty(exchangeRateRequest.TargetCurrency.Trim()))
                {
                    exchangeRateResponse.Error = new ErrorResponse
                    {
                        ErrorCode = HttpStatusCode.PartialContent,
                        ErrorMessage = ExchangeRateConstant.InvalidTargetCurrency
                    };
                }
                else if (exchangeRateRequest.Dates == null || exchangeRateRequest.Dates.Length == 0)
                {
                    exchangeRateResponse.Error = new ErrorResponse
                    {
                        ErrorCode = HttpStatusCode.PartialContent,
                        ErrorMessage = ExchangeRateConstant.InvalidDates
                    };
                }
                return exchangeRateResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
