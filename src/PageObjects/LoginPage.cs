using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Codetest
{
    class LoginPage{
        private IWebDriver driver;
        Int32 timeout = 10000; // in milliseconds

        public LoginPage(IWebDriver driver){
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.Id , Using = "email_create")]
        [CacheLookup]
        private IWebElement txtEmail;
        
        [FindsBy(How = How.Id , Using = "SubmitCreate")]
        [CacheLookup]
        private IWebElement btnCreateAcc;

        public RegisterPage enterEmail(string email){  
            txtEmail.SendKeys(email);
            btnCreateAcc.Click();
            return new RegisterPage(driver);
        } 
    
    }
}