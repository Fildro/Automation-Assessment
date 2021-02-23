using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
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
            driver.Url = "http://automationpractice.com/index.php";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement btnSignIn = driver.FindElement(By.ClassName("login"));
        
            btnSignIn.Click();
            IWebElement txtEmail = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email_create")));
            
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
            var ClientGenerator = clientFaker.Generate();

            txtEmail.SendKeys(ClientGenerator.Email);
            IWebElement btnCreateAcc = driver.FindElement(By.Id("SubmitCreate"));
            btnCreateAcc.Click();

            //Perosnal Information PO
            IWebElement btnGender = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("id_gender1")));
            IWebElement txtFirstName = driver.FindElement(By.Id("customer_firstname"));
            IWebElement txtLastName =  driver.FindElement(By.Id("customer_lastname"));
            IWebElement txtEmailRegister = driver.FindElement(By.Id("email"));
            IWebElement txtPassword = driver.FindElement(By.Id("passwd"));
            IWebElement cbDay = driver.FindElement(By.Id("days"));
            IWebElement cbMonth = driver.FindElement(By.Id("months"));
            IWebElement cbYear = driver.FindElement(By.Id("years"));

            //Address Information PO
            IWebElement txtCompany = driver.FindElement(By.Id("company"));
            IWebElement txtAdress = driver.FindElement(By.Id("address1"));
            IWebElement txtAdress2 = driver.FindElement(By.Id("address2"));
            IWebElement txtCity = driver.FindElement(By.Id("city"));
            IWebElement cbState = driver.FindElement(By.Id("id_state"));
            IWebElement txtPostalCode = driver.FindElement(By.Id("postcode"));
            // I know I used a lot of "By ID", but... is the most simple right now
            IWebElement txtInformation = driver.FindElement(By.Id("other"));
            IWebElement txtHomePhone = driver.FindElement(By.Id("phone"));
            IWebElement txtMobilePhone = driver.FindElement(By.Id("phone_mobile"));
            IWebElement btnRegister = driver.FindElement(By.XPath("//*[@id='submitAccount']"));

            btnGender.Click();
            txtFirstName.SendKeys(ClientGenerator.FirstName);
            txtLastName.SendKeys(ClientGenerator.LastName);
            txtPassword.SendKeys("testautomation");
            cbDay.SendKeys("16");
            cbMonth.SendKeys("November");
            cbYear.SendKeys("1995");
            txtCompany.SendKeys(ClientGenerator.Company);
            txtAdress.SendKeys("201 S 4th ST");
            txtAdress2.SendKeys("201 S 4th ST");
            txtCity.SendKeys("Fort Dodge");
            cbState.SendKeys("Iowa");
            txtPostalCode.SendKeys("50501");
            txtInformation.SendKeys(ClientGenerator.Information);
            //Phone string taking first 9 numbers
            string shortHomePhone = ClientGenerator.HomePhone.Remove(9);
            string shortMobilePhone = ClientGenerator.MobilePhone.Remove(9);
            txtHomePhone.SendKeys(shortHomePhone);
            txtMobilePhone.SendKeys(shortMobilePhone);
            btnRegister.Click();

            //Validations
            //My account page(?controller=my-account) is opened
            IWebElement lblAccountName = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("account")));
            driver.PageSource.Contains("my-account");

            // Proper username is shown in the header
            string fullname = ClientGenerator.FirstName + " " + ClientGenerator.LastName; //Bogus
            string uiFullName = lblAccountName.GetAttribute("innerText"); //UI
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