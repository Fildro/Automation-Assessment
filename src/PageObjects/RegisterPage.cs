using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Codetest
{
    class RegisterPage{
        private IWebDriver driver;
        public RegisterPage(IWebDriver driver){
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

         //Perosnal Information -------
        [FindsBy(How = How.Id , Using = "id_gender1" )]
        [CacheLookup]
        private IWebElement btnGender;

        [FindsBy(How = How.Id , Using = "customer_firstname" )]
        [CacheLookup]
        private IWebElement txtFirstName;

        [FindsBy(How = How.Id , Using = "customer_lastname" )]
        [CacheLookup]
        private IWebElement txtLastName;

        [FindsBy(How = How.Id , Using = "passwd" )]
        [CacheLookup]
        private IWebElement txtPassword;

        [FindsBy(How = How.Id , Using = "days" )]
        [CacheLookup]
        private IWebElement cbDay;

        [FindsBy(How = How.Id , Using = "months" )]
        [CacheLookup]
        private IWebElement cbMonth;

        [FindsBy(How = How.Id , Using = "years" )]
        [CacheLookup]
        private IWebElement cbYear;

        //Address Information -------

        [FindsBy(How = How.Id , Using = "company" )]
        [CacheLookup]
        private IWebElement txtCompany;

        [FindsBy(How = How.Id , Using = "address1" )]
        [CacheLookup]
        private IWebElement txtAdress;

        [FindsBy(How = How.Id , Using = "address2" )]
        [CacheLookup]
        private IWebElement txtAdress2;

        [FindsBy(How = How.Id , Using = "city" )]
        [CacheLookup]
        private IWebElement txtCity;

        [FindsBy(How = How.Id , Using = "id_state" )]
        [CacheLookup]
        private IWebElement cbState;

        [FindsBy(How = How.Id , Using = "postcode" )]
        [CacheLookup]
        private IWebElement txtPostalCode;

        [FindsBy(How = How.Id , Using = "other" )]
        [CacheLookup]
        private IWebElement txtInformation;

        [FindsBy(How = How.Id , Using = "phone" )]
        [CacheLookup]
        private IWebElement txtHomePhone;

        [FindsBy(How = How.Id , Using = "phone_mobile" )]
        [CacheLookup]
        private IWebElement txtMobilePhone;

        [FindsBy(How = How.XPath , Using = "//*[@id='submitAccount']" )]
        [CacheLookup]
        private IWebElement btnRegister;

        public UserPage registerUser(ClientModel client){
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            var btnGender = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("id_gender1")));
            btnGender.Click();
            txtFirstName.SendKeys(client.FirstName);
            txtLastName.SendKeys(client.LastName);
            txtPassword.SendKeys("testautomation");
            cbDay.SendKeys("16");
            cbMonth.SendKeys("November");
            cbYear.SendKeys("1995");
            txtCompany.SendKeys(client.Company);
            txtAdress.SendKeys("201 S 4th ST");
            txtAdress2.SendKeys("201 S 4th ST");
            txtCity.SendKeys("Fort Dodge");
            cbState.SendKeys("Iowa");
            txtPostalCode.SendKeys("50501");
            txtInformation.SendKeys(client.Information);

            //Phone string taking first 9 numbers
            string shortHomePhone = client.HomePhone.Remove(9);
            string shortMobilePhone = client.MobilePhone.Remove(9);

            txtHomePhone.SendKeys(shortHomePhone);
            txtMobilePhone.SendKeys(shortMobilePhone);
            btnRegister.Click();

            return new UserPage(driver);
        }
    }
}