# ExchangeRateInfoAPI
Instruction to run the service • Url to clone the repository https://github.com/aadil-hussain77/ExchangeRateInfoAPI.git • Steps to run the service:

Open ExchangeRateInfoAPI.sln solution file in Visual studio
Check ExchangeRateInfoAPI is the startup project
Otherwise, please set it as startup project
Build and run the solution
Use any rest client like postman, to consume the service Input to consume the service- • URL - http://localhost:<port_number>/ExchangeRate/GetExchangeRateInformation • Set Header - Content-Type = application/json • Input -
{ "Dates":["2018-02-01", "2018-02-15", "2018-03-01"], "BaseCurrency":"SEK", "TargetCurrency":"NOK" }
• Output- { "MinRate": "A min rate of 0.9546869595 on 2018-03-01", "MaxRate": "A max rate of 0.9815486993 on 2018-02-15", "AverageRate": "An average rate of 0.970839476466667", "Error": null }
