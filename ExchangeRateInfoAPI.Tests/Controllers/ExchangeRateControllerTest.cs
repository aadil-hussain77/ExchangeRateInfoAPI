namespace ExchangeRateInfoAPI.Tests.Controllers
{
    using ExchangeRateBL.BL;
    using ExchangeRateContract.Request;
    using ExchangeRateModel.Input;
    using ExchangeRateModel.Output;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Unit test class for Exchange Rate Controller Test
    /// </summary>
    [TestClass]
    public class ExchangeRateControllerTest
    {
        /// <summary>
        /// Unit test case of GetExchangeRateInformation method
        /// </summary>
        [TestMethod]
        public void GetExchangeRateInformationTest()
        {
            var exchangeRateManagerMock = new Mock<IExchangeRateManager>();
            var exchangeRateController = new ExchangeRateInfoAPI.Controllers.ExchangeRateController(exchangeRateManagerMock.Object);
            string[] dates = { "2018-02-01", "2018-02-15", "2018-03-01" };
            // Arrange
            var exchangeRateRequest = new ExchangeRateRequest
            {
                BaseCurrency = "SEK",
                TargetCurrency = "NOK",
                Dates = dates
            };

            exchangeRateManagerMock.Setup(service => service.GetExchangeRateInformation(It.IsAny<ExchangeRateInput>()))
            .Returns(new ExchangeRateOutput
            {
                MinRate = "A min rate of 0.9546869595 on 2018-03-01",
                MaxRate = "A max rate of 0.9815486993 on 2018-02-15",
                AverageRate = "An average rate of 0.970839476467"
            });

            // Act
            var exchangeRateOutput = exchangeRateController.GetExchangeRateInformation(exchangeRateRequest);

            // Assert
            Assert.IsNotNull(exchangeRateOutput);
        }
    }
}
