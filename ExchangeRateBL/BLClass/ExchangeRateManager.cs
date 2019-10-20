namespace ExchangeRateBL.BL
{
    using ExchangeRateModel.Input;
    using ExchangeRateModel.Output;
    using Helper.Constant;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Exchange rate manager class contains the business logic for exchange rate
    /// </summary>
    public class ExchangeRateManager : IExchangeRateManager
    {
        /// <summary>
        /// API Url
        /// </summary>
        readonly string baseUrl = ConfigurationManager.AppSettings[ExchangeRateConstant.PathKey];

        /// <summary>
        /// Client object to consume exchange rate api
        /// </summary>
        HttpClient _httpClient;

        /// <summary>
        /// Exchnage rate constructor
        /// </summary>
        /// <param name="client"></param>
        public ExchangeRateManager(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// Method to fetch required exchange rate information based on input data
        /// </summary>
        /// <param name="exchangeRateInput">Exchange rate input model</param>
        /// <returns>Exchange rate information based on input data</returns>        
        public ExchangeRateOutput GetExchangeRateInformation(ExchangeRateInput exchangeRateInput)
        {
            ExchangeRateOutput exchangeRateOutput = new ExchangeRateOutput();
            try
            {
                if (_httpClient.BaseAddress == null)
                {
                    ClientInitialSetup();
                }

                List<DateTime> dates = ProcessDate(exchangeRateInput.Dates);
                string requestURL = PrepareRequestUrl(exchangeRateInput, dates);
                var response = _httpClient.GetAsync(requestURL);
                var dataString = response.Result.Content.ReadAsStringAsync();
                response.Wait();
                if (response.IsCompleted && response.Result.IsSuccessStatusCode)
                {
                    List<Tuple<double, DateTime>> outputList = ProcessResponse(exchangeRateInput, dates, dataString);
                    exchangeRateOutput = PrepareOutput(outputList);
                }
                else
                {
                    exchangeRateOutput.Error = new ErrorModel
                    {
                        ErrorCode = response.Result.StatusCode,
                        ErrorMessage = dataString.Result.Substring(10).TrimEnd(ExchangeRateConstant.charToRemove)
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return exchangeRateOutput;
        }

        /// <summary>
        /// Reads response and prepare a list of exchange rate
        /// </summary>
        /// <param name="exchangeRateInput">Exchange rate input object</param>
        /// <param name="dates">set of input dates in required format</param>
        /// <param name="dataString">Reponse of the service call</param>
        /// <returns>List of date along with exchange rate</returns>
        private List<Tuple<double, DateTime>> ProcessResponse(ExchangeRateInput exchangeRateInput, List<DateTime> dates, Task<string> dataString)
        {
            try
            {
                var result = (JObject)JsonConvert.DeserializeObject(dataString?.Result.ToString());
                var rateList = result.SelectToken(ExchangeRateConstant.Rate).ToList();
                List<Tuple<double, DateTime>> outputList = new List<Tuple<double, DateTime>>();
                if (rateList.Count > 1)
                {
                    foreach (var item in rateList)
                    {
                        outputList.Add(new Tuple<double, DateTime>(Convert.ToDouble(item.ToString().Split(' ')[4].TrimEnd(ExchangeRateConstant.charToRemove).Trim()), (DateTime.Parse(item.ToString().Substring(1, 10).Trim(), CultureInfo.InvariantCulture)).Date));
                    }
                }
                else
                {
                    outputList.Add(new Tuple<double, DateTime>(Convert.ToDouble(rateList[0].ToString().Split()[1].Trim()), (DateTime.Parse(exchangeRateInput.Dates[0].Trim(), CultureInfo.InvariantCulture)).Date));
                }
                outputList = outputList.Where(x => dates.Any(z => x.Item2 == z)).ToList();
                return outputList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Process input dates
        /// </summary>
        /// <param name="inputDates">Input dates</param>
        /// <returns>List of input dates in correct format</returns>
        private List<DateTime> ProcessDate(string[] inputDates)
        {
            try
            {
                List<DateTime> dates = new List<DateTime>();
                foreach (var date in inputDates)
                {
                    var tempDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
                    if (tempDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        tempDate = tempDate.AddDays(-2);
                    }
                    else if (tempDate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        tempDate = tempDate.AddDays(-1);
                    }
                    dates.Add(tempDate);
                }
                return dates;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Prepare request url as per input
        /// </summary>
        /// <param name="exchangeRateInput">Exchange rate input object</param>
        /// <param name="dates">Set of input date</param>
        /// <returns>Request Url</returns>
        private string PrepareRequestUrl(ExchangeRateInput exchangeRateInput, List<DateTime> dates)
        {
            try
            {
                var minDate = dates.Min().ToString(ExchangeRateConstant.DateFormat, CultureInfo.InvariantCulture);
                var maxDate = dates.Count > 1 ? dates.Max().ToString(ExchangeRateConstant.DateFormat, CultureInfo.InvariantCulture) : string.Empty;
                var requestURL = string.IsNullOrEmpty(maxDate)
                    ? minDate + ExchangeRateConstant.BaseCurrency + exchangeRateInput.BaseCurrency + ExchangeRateConstant.TargetCurrency + exchangeRateInput.TargetCurrency
                    : ExchangeRateConstant.StartAt + minDate + ExchangeRateConstant.EndAt + maxDate + ExchangeRateConstant.BaseCurrencyForPeriod + exchangeRateInput.BaseCurrency + ExchangeRateConstant.TargetCurrency + exchangeRateInput.TargetCurrency;
                return requestURL;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to fill output object
        /// </summary>
        /// <param name="exchangeRateInput"></param>
        /// <param name="ExchangeRates"></param>
        /// <param name="sum"></param>
        /// <returns>Required output object</returns>
        private ExchangeRateOutput PrepareOutput(List<Tuple<double, DateTime>> ExchangeRates)
        {
            try
            {
                var maxRate = ExchangeRates.Max();
                var minRate = ExchangeRates.Min();
                var avgRate = ExchangeRates.Sum(x => x.Item1) / ExchangeRates.Count;
                return new ExchangeRateOutput
                {
                    MinRate = $"A min rate of {minRate.Item1.ToString()} on {minRate.Item2.ToString(ExchangeRateConstant.DateFormat, CultureInfo.InvariantCulture)}",
                    MaxRate = $"A max rate of {maxRate.Item1.ToString()} on {maxRate.Item2.ToString(ExchangeRateConstant.DateFormat, CultureInfo.InvariantCulture)}",
                    AverageRate = $"An average rate of {avgRate.ToString()}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method for initial setup of http client 
        /// </summary>
        private void ClientInitialSetup()
        {
            try
            {
                _httpClient.BaseAddress = new Uri(baseUrl);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ExchangeRateConstant.ContentType));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
