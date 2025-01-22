# Polygon.IO API Tests Automation Project

## Overview
This project is focused on validating the functionality and accuracy of the Polygon.IO API by performing cross-comparisons with equivalent data retrieved from the AlphaVantage API.
The test suite verifies response schemas, data consistency, and handles various edge cases.




---

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Test Scenarios](#test-scenarios)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Reporting](#reporting)


---

## Features
- Framework: NUnit for test execution.
- Reporting: Allure.NET for detailed test reporting.
- HTTP Client: RestSharp with HttpTracer for request/response logging.
- Configuration: JSON-based configuration file for API keys and base URLs.
- Data-driven testing using a custom DataProvider.

---
## Technologies Used

- Languages: C#

- Frameworks: NUnit

- Libraries: RestSharp, HttpTracer, Allure.NET

- Tools: Visual Studio, Allure Report Generator

- APIs: Polygon.IO, AlphaVantage
---



## Test Scenarios

### API Calls Status Tests
- **PolygonGetTickerInfoAsync**: Verify status code for PolygonGetTickerInfoAsync is 200.
- **PolygonGetOpenCloseData**: Verify status code for PolygonGetOpenCloseData is 200".
- **PolygonGetTickerDetails**: Verify status code for PolygonGetTickerDetails is 200.
- **AlphaGetOpenCloseData**: Verify status code for AlphaGetOpenCloseData is 200.
- **AlphaGetLastDayOpenClose**: Verify status code for AlphaGetLastDayOpenClose is 200.
- **AlphaGetComapnyOverview**: Verify status code for AlphaGetComapnyOverview is 200.

### Compare stock metrics from Polygon.IO API to AlphaVentage API
- **GetOpenRate**: Get Stock Open Rate for previous business day compare to AlphaVantage quote.
- **GetClosingRate**: Get Stock Closing Rate for previous business day compare to AlphaVantage quote.
- **GetDayHighRate**: Get Stock High Rate for previous business day compare to AlphaVantage quote.
- **GetDayLowRate**: Get Stock Low Rate for previous business day compare to AlphaVantage quote.
- **GetTradeVolume**: Get Stock trade volume for previous business day compare to AlphaVantage quote.

### Polygom.IO API Boundary and Error Handling
- **XRateLimitErrorTest**: Verify rate limit error when exceeding max API calls per minute.
- **ValidateOpenCloseResponseSchemaTest**: Validating the response schema for a stock ticker query.
- **TestTickerSpecialChars**: send request with ticker name with special characters.
- **TestTickerMaxCharsAllowedInput**: send request with ticker name with the Max chars allowed in nasdaq stocks:  5 chars.
- **TestTickerLongerTheMaxCharsAllowedInput**: send request with ticker longer name then the Max chars allowed in nasdaq stocks:  5 chars, this also tests non existing stock.
- **TestTickerWhiteSpaces**: send request with ticker name 4 white spaces.

---



## Setup and Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/inbal-itzhak/PolygonIOAPITests.git
   ```

2. Install dependencies using NuGet:
   - RestSharp
   - HttpTracer
   - Microsoft.Extensions.Configuration.Json
   - Microsoft.Extensions.Configuration.Abstractions
   - Allure.Net.Commons
   - Allure.NUnit

3. Configure the project:
   - Update `appsettings.json` with the Polygon.IO api key
   - Update `appsettings.json` with the AlphaVentage api key

4. Ensure you have .NET installed:
   ```bash
   dotnet --version
   ```

---

## Usage

1. Open the solution in Visual Studio.
2. Build the project to ensure all dependencies are installed.
3. Run the tests using the Test Explorer or CLI:
   ```bash
   dotnet test
   ```
4. Generate Allure reports:
   ```bash
   allure serve allure-results
   ```

---


## Reporting
This project uses Allure.Net.Commons for detailed reporting. Reports include:
- Test execution results
- Categories and labels for better organization

To view reports, run:
```bash
allure serve allure-results
```
