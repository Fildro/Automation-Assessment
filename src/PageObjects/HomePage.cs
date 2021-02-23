using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Codetest
{
    class HomePage
    {
        string url = "http://automationpractice.com/index.php";

        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            	this.driver = driver;
	            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
	            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName , Using = "login" )]
        [CacheLookup]
        private IWebElement btnSignIn;
        
        public void goToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        public LoginPage login(){
            btnSignIn.Click();
            return new LoginPage(driver);
        }

    }
}