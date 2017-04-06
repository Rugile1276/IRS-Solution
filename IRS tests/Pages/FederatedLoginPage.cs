using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace IRS_tests
{

    class FederatedLoginPage
    {
        private IWebDriver driver;
        IWebElement federatedEmailBox;
        IWebElement federatedPasswordBox;
        IWebElement submitBtn;

        
        public FederatedLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            federatedEmailBox = driver.FindElement(By.Id("userNameInput"));
            federatedPasswordBox = driver.FindElement(By.Id("passwordInput"));
            submitBtn = driver.FindElement(By.Id("submitButton"));
        }

        //Log with federated login and return IRS page

        public IRSPage federatedLogin(String name, String password)
        {
            enterUserNameFederated(name);
            enterUserPasswordFederated(password);
            return submitLoginCredentials();
        }
        
        public FederatedLoginPage enterUserNameFederated(String name)
        {
            federatedEmailBox.Click();
            federatedEmailBox.SendKeys(name);
            return this;
        }
        
        public FederatedLoginPage enterUserPasswordFederated(String password)
        {
            federatedPasswordBox.Click();
            federatedPasswordBox.SendKeys(password);
            return this;
        }
        
        public IRSPage submitLoginCredentials()
        {
            submitBtn.Click();
            return new IRSPage(driver);
        }
    }
}
