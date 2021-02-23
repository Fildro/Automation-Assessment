using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Codetest
{
    class UserPage{
        private IWebDriver driver;

        public UserPage(IWebDriver driver){
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        [FindsBy(How = How.ClassName , Using = "account")]
        [CacheLookup]
        private IWebElement lblAccountName;

        public string getUserName(){
            return lblAccountName.GetAttribute("innerText");
        }
    
    }
}