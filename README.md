# Automation-Assessment
Automation Assessment test

Things we have to install before run:

1. Download visual studio code.
2. In Vs Code: Download NuGet Package Manager
3. Download NetCore 5.0: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.100-windows-x64-installer
4. In Vs Code: Download C# 1.23.6

How to Run:

1. Enter into visual studio code.
2. Open the Project
3. Open a Terminal (In the Project Path)


Task 1 - UI Automated test using Selenium WebDriver (Test/TestCustomer.cs)
 dotnet test --filter TestCategory=Customer

Task 2 - API Automated test using (Test/TestAPI.cs)
1) // 1 - Get each country (US, DE and GB) individually and validate the response
 dotnet test --filter TestCategory=APIlist

2) // 2 -  Try to get information for inexistent countries and validate the response
 dotnet test --filter TestCategory=APIlistNonExistingCountries
 
3) // 3 - This API has not a POST method at the moment, but it is being developed
dotnet test --filter TestCategory=APIPostExample
