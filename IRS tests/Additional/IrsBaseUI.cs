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
    class IrsBaseUI
    {
        IWebDriver driver;
        Helper helper = new Helper();

        public IWebElement bentleyIcon;
        public IWebElement orgName;
        public IWebElement projectNumberName;
        public IWebElement currentProject;
        public IWebElement currentService;
        public IWebElement notificationButton;
        public IWebElement helpButton;
        public IWebElement torcoButton;

        public IWebElement bentleyIncorporatedElement;
        public String bentleyIncorporatedText;
        public String bentleyIncorporatedExpectedText;

        public IWebElement eulaElement;
        public String eulaText;
        public String eulaExpectedText;
        public String eulaHref;
        public String eulaExpectedHref;

        public IWebElement privacyPoliceElement;
        public String privacyPoliceText;
        public String privacyPoliceExpectedText;
        public String privacyPoliceHref;
        public String privacyPoliceExpectedHref;

        public IWebElement termOfUseElement;
        public String termOfUseText;
        public String termOfUseExpectedText;
        public String termOfUseHref;
        public String termOfUseExpectedHref;

        public IWebElement cookyElement;
        public String cookyText;
        public String cookyExpectedText;
        public String cookyHref;
        public String cookyExpectedHref;

        public IWebElement legalNoticesElement;
        public String legalNoticesText;
        public String legalNoticesExpectedText;
        public String legalNoticesHref;
        public String legalNoticesExpectedHref;

        public IWebElement flagIconElement;
        public IWebElement techPreviewElement;
        public String techPreviewText;
        public String techPreviewExpectedText;
        public IWebElement techPreviewIconElement;
        public IWebElement subTitleElement;
        public String subTitleText;
        public String subTitleExpectedText;

        public IrsBaseUI(IWebDriver driver)
        {
            this.driver = driver;

        }

        public IrsBaseUI FindHeaderElemets()
        {
            #region Header
            helper.WaitForLoadFinish(6000);
            bentleyIcon = driver.FindElement(By.Id("HFBentleyConnect"));
            orgName = driver.FindElement(By.Id("HFOrganizationName"));
            projectNumberName = driver.FindElement(By.XPath("//*[@id='HFCurrentProject']/div/div[1]/div"));
            currentProject = driver.FindElement(By.XPath("//li[@id='HFCurrentProject']//button[@class='btn btn-secondary dropdown-toggle atpHeaderProjectDropdown']"));
            currentService = driver.FindElement(By.XPath("//a[@id='HFCurrentServiceUrl' and @href]"));
            notificationButton = driver.FindElement(By.Id("HFNotificationsButton"));
            helpButton = driver.FindElement(By.Id("HFHelpTooltip"));
            torcoButton = driver.FindElement(By.XPath("//span[@class='glyphicon glyphicon-user']"));
            #endregion
            return this;
        }

        public IrsBaseUI FindFooterElements()
        {
            #region Footer
            bentleyIncorporatedElement = driver.FindElement(By.Id("HFBentleySystemsIncorporated"));
            bentleyIncorporatedText = bentleyIncorporatedElement.Text;
            bentleyIncorporatedExpectedText = "© " + DateTime.Now.Year + " Bentley Systems, Incorporated";

            eulaElement = driver.FindElement(By.Id("HFEulaReadOnly"));
            eulaText = eulaElement.Text;
            eulaExpectedText = "Terms of Service";
            eulaHref = eulaElement.GetAttribute("href");
            eulaExpectedHref = "https://qa-agreementportal-eus.cloudapp.net/AgreementApp/Home/Eula/View/ReadOnly/BentleyConnect";

            privacyPoliceElement = driver.FindElement(By.XPath("//span[@id='HFPrivacyPolicy']/.."));
            privacyPoliceText = privacyPoliceElement.Text;
            privacyPoliceExpectedText = "Privacy";
            privacyPoliceHref = privacyPoliceElement.GetAttribute("href");
            privacyPoliceExpectedHref = "https://www.bentley.com/en/privacy-policy";

            termOfUseElement = driver.FindElement(By.XPath("//span[@id='HFTermsOfUse']/.."));
            termOfUseText = termOfUseElement.Text;
            termOfUseExpectedText = "Terms of Use";
            termOfUseHref = termOfUseElement.GetAttribute("href");
            termOfUseExpectedHref = "https://www.bentley.com/en/terms-of-use-and-select-online-agreement";

            cookyElement = driver.FindElement(By.XPath("//span[@id='HFCookiePolicy']/.."));
            cookyText = cookyElement.Text;
            cookyExpectedText = "Cookies";
            cookyHref = cookyElement.GetAttribute("href");
            cookyExpectedHref = "https://www.bentley.com/en/cookie-policy";

            legalNoticesElement = driver.FindElement(By.XPath("//span[@id='HFLegalNotices']/.."));
            legalNoticesText = legalNoticesElement.Text;
            legalNoticesExpectedText = "Legal Notices";
            legalNoticesHref = legalNoticesElement.GetAttribute("href");
            legalNoticesExpectedHref = "https://qa-webportal-eus.cloudapp.net/Legal";
            #endregion
            return this;
        }

        public virtual IrsBaseUI FindBodyElements()
        {
            #region Body
            helper.WaitForLoadFinish(20000);
            flagIconElement = driver.FindElement(By.XPath("//*[@id='title']/div[1]/div/div[1]/img"));

            techPreviewElement = driver.FindElement(By.XPath("//*[@id='title']/div[1]/div/div[2]/div/div[1]/div[1]"));
            techPreviewText = techPreviewElement.Text;
            techPreviewExpectedText = "[Technology Preview]";

            techPreviewIconElement = driver.FindElement(By.XPath("//*[@id='title']/div[1]/div/div[2]/div/div[2]/img"));

            subTitleElement = driver.FindElement(By.XPath("//*[@id='title']/div[1]/div/div[2]/div/div[1]/div[3]"));
            subTitleText = subTitleElement.Text;
            subTitleExpectedText = "ProjectWise Field Data Management";
            #endregion
            return this;
        }
    }
}
