using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using IRS_tests.Additional;

namespace IRS_tests
{   
    [TestFixture]
    class LoginPage
    {
        IWebDriver driver;
        IWebElement emailBox;
        IWebElement submitBtn;
        Helper helper = new Helper();

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl("https://qa-projectforms-eus.cloudapp.net/#/6e1e7a39-dc8a-4a23-820f-b1a9584dbe31");
            helper.WaitForLoadFinish(10000);
            emailBox = driver.FindElement(By.Id("EmailAddress"));
            submitBtn = driver.FindElement(By.Id("submitLogon"));
        }

        //Log with simple login and return federated login page
       
        public FederatedLoginPage login (String name)
        {
            enterUserName(name);
            return submitLoginCredentials();
        }
        
        public LoginPage enterUserName(String name)
        {
            emailBox.Click();
            emailBox.SendKeys(name);
            return this;
        }
        
        public FederatedLoginPage submitLoginCredentials()
        {
            submitBtn.Click();
            return new FederatedLoginPage(driver);
        }
    }
}
