using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System;
using Bogus;

namespace Codetest{
   
    public class TestCustomer
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        [Category("Customer")] 
        public void TestCustomerLogin()
        {            
            //Bogus to fake data
            var clientFaker = new Faker<ClientModel>()
            .RuleFor(x => x.Id, Guid.NewGuid)
            .RuleFor(x => x.FirstName, x => x.Person.FirstName)
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.Email, x => x.Person.Email)
            .RuleFor(x => x.Company, x => x.Person.Company.Name)
            .RuleFor(x => x.Information, x => x.Lorem.Sentence(10))
            .RuleFor(x => x.MobilePhone, x => x.Person.Phone)
            .RuleFor(x => x.HomePhone, x => x.Person.Phone);
            var clientGenerator = clientFaker.Generate();

            HomePage home_page = new HomePage(driver);
            home_page.goToPage();
            home_page.login();

            LoginPage login_page = new LoginPage(driver);
            login_page.enterEmail(clientGenerator.Email);
            
            RegisterPage register_page = new RegisterPage(driver);
            register_page.registerUser(clientGenerator);

            //Validations
            //My account page(?controller=my-account) is opened
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("account")));
            driver.PageSource.Contains("my-account");

            // Proper username is shown in the header
            string fullname = clientGenerator.FirstName + " " + clientGenerator.LastName; //Bogus
            UserPage user_page = new UserPage(driver);   
            string uiFullName = user_page.getUserName();; //UI
            //Change to AreNotEqual if you want to see the validation that is working good.
            Assert.AreEqual(fullname, uiFullName);
            
            // Log out action is available.
            IWebElement btnSignout = driver.FindElement(By.ClassName("logout"));
            string singOutLabel = btnSignout.GetAttribute("innerText"); //UI
            Assert.AreEqual(singOutLabel,"Sign out");
        }
            
        [OneTimeTearDown]
        public void Close(){ 
            driver.Close();
        }

    }
}