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
    [TestFixture]
    class RunUITests
    {
        IWebDriver driver;
        FederatedLoginPage federatedLoginPage;
        IRSPage irsPage;
        IrsBaseUI irsBaseUI;
        LoginPage loginPage;
        MeetingisMinutesFormPage mmFormPage;


        [TestFixtureSetUp]
        public void TestInit()
        {
            try
            {
                driver = new ChromeDriver();
                loginPage = new LoginPage(driver);
                irsBaseUI = new IrsBaseUI(driver);
                mmFormPage = new MeetingisMinutesFormPage(driver);
                System.Threading.Thread.Sleep(20000);
                federatedLoginPage = loginPage.login("Rugile.Petrukauskaite@bentley.com");
                irsPage = federatedLoginPage.federatedLogin("Rugile.Petrukauskaite@bentley.com", "Rugilyte2017");
                System.Threading.Thread.Sleep(20000);
                Assert.IsTrue(driver.FindElement(By.CssSelector("[class='spa-title ng-binding']")).Displayed, "'All Form Data' title was not found");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Unable to initialize class: {0}", ex));
            }
        }

        [TestFixtureTearDown]
        public void cleanup()
        {
            driver.Close();
        }

        public void CommonTest()
        {
            irsBaseUI.FindBodyElements();

            #region Body
            Assert.IsTrue(irsBaseUI.flagIconElement.Displayed, "Flag icon was not found");

            Assert.IsTrue(irsBaseUI.techPreviewElement.Displayed, "Tech Preview text was not found");
            Assert.AreEqual(irsBaseUI.techPreviewExpectedText, irsBaseUI.techPreviewText, "Tech Preview text is wrong");

            Assert.IsTrue(irsBaseUI.techPreviewIconElement.Displayed, "Tech Preview icon was not found");

            Assert.IsTrue(irsBaseUI.subTitleElement.Displayed, "Subtitle text was not found");
            Assert.AreEqual(irsBaseUI.subTitleExpectedText, irsBaseUI.subTitleText, "Subtitle text is wrong");

            #endregion
        }

        [Description("Checks if all header elements are displayed")]
        [Test]
        public void IRSHeaderTest()
        {
            irsPage.FindHeaderElemets();

            #region Header
            Assert.IsTrue(irsPage.bentleyIcon.Displayed, "Bentley icon wasn't found");
            Assert.IsTrue(irsPage.orgName.Displayed, "Organization name was not found");
            Assert.IsTrue(irsPage.projectNumberName.Displayed, "Project number/name was not found");
            Assert.IsTrue(irsPage.projectNumberName.Displayed, "Project number/name dropdown button was not found");
            Assert.IsTrue(irsPage.currentService.Displayed, "Service name was not found");
            Assert.IsTrue(irsPage.notificationButton.Displayed, "Bell icon was not found");
            Assert.IsTrue(irsPage.helpButton.Displayed, "Help icon was not found");
            Assert.IsTrue(irsPage.torcoButton.Displayed, "Grey torco icon was not found");
            #endregion
        }

        [Description ("Checks if all footer elements are displayed and opens correct pagges after clicking")]
        [Test]
        public void IRSFooterTest()
        {
            irsPage.FindFooterElements();
            #region Footer

            Assert.IsTrue(irsPage.bentleyIncorporatedElement.Displayed, "Bentley Connect copyright was not found");
            Assert.AreEqual(irsPage.bentleyIncorporatedExpectedText, irsPage.bentleyIncorporatedText, "Copyright text is wrong.");

            Assert.IsTrue(irsPage.eulaElement.Displayed, "Eula link was not found");
            Assert.AreEqual(irsPage.eulaExpectedText, irsPage.eulaText, "Eula text is wrong");
            Assert.AreEqual(irsPage.eulaExpectedHref, irsPage.eulaHref, "Eula url does not match");

            Assert.IsTrue(irsPage.privacyPoliceElement.Displayed, "Privacy police link was not found");
            Assert.AreEqual(irsPage.privacyPoliceExpectedText, irsPage.privacyPoliceText, "Privacy police text is wrong");
            Assert.AreEqual(irsPage.privacyPoliceExpectedHref, irsPage.privacyPoliceHref, "Privacy police url does not match");

            Assert.IsTrue(irsPage.termOfUseElement.Displayed, "Term of Use link was not found");
            Assert.AreEqual(irsPage.termOfUseExpectedText, irsPage.termOfUseText, "Term of Use text is wrong");
            Assert.AreEqual(irsPage.termOfUseExpectedHref, irsPage.termOfUseHref, "Term of Use url does not match");

            Assert.IsTrue(irsPage.cookyElement.Displayed, "Cookies link was not found");
            Assert.AreEqual(irsPage.cookyExpectedText, irsPage.cookyText, "Cookies text is wrong");
            Assert.AreEqual(irsPage.cookyExpectedHref, irsPage.cookyHref, "Cookies url does not match");

            Assert.IsTrue(irsPage.legalNoticesElement.Displayed, "Legal Notices link was not found");
            Assert.AreEqual(irsPage.legalNoticesExpectedText, irsPage.legalNoticesText, "Legal Notices text is wrong");
            Assert.AreEqual(irsPage.legalNoticesExpectedHref, irsPage.legalNoticesHref, "Legal Notices url does not match");
            #endregion
        }
        [Description ("Checks if Title, subtitle, techPreview and its icon, flag icon are displayed in base Field Data Management Page")]
        [Test]
        public void IRSBodyTest()
        {
            #region Body

            CommonTest();

            irsPage.FindBodyElements();
            Assert.IsTrue(irsPage.titleElement.Displayed, "Title text was not found");
            Assert.AreEqual(irsPage.titleExpectedText, irsPage.titleText, "Title text is wrong");

            Assert.IsTrue(irsPage.categoryElement.Displayed, "Category Text was not found");
            Assert.AreEqual(irsPage.categoryExpectedText, irsPage.categoryText, "Category text is wrong");

            Assert.IsTrue(irsPage.categoriesListElement.Displayed, "Categories List was not found");

            #endregion
        }
        [Description ("Checks if Title, subtitle, techPreview and its icon, flag icon are displayed in Create New Form Page ")]
        [Test]
        public void CreateNewMeetingMinutesFormpageBodyTest()
        {
            //Open Create New Form page
            irsPage.OpenCreateNewMeetingMinuteFormThroughActionsButton();

            CommonTest();

            mmFormPage.FindBodyElements();
            Assert.IsTrue(mmFormPage.subTitleElement.Displayed, "Title text was not found");
            Assert.AreEqual(mmFormPage.titleExpectedText, mmFormPage.titleText, "Title text is wrong");
            mmFormPage.ClickCancelButton();
        }
    }
}
