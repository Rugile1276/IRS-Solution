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
    class RunActionsButtonTests
    {
        IWebDriver driver;
        FederatedLoginPage federatedLoginPage;
        IRSPage irsPage;
        MeetingisMinutesFormPage meetingMinutesFormPage;
        Helper helper = new Helper();
        int i = 0;
        

        [SetUp]
        public void TestInit()
        {
            try
            {
                i++;
                driver = new ChromeDriver();
                LoginPage loginPage = new LoginPage(driver);
                helper.WaitForLoadFinish(10000);
                federatedLoginPage = loginPage.login("Rugile.Petrukauskaite@bentley.com");
                irsPage = federatedLoginPage.federatedLogin("Rugile.Petrukauskaite@bentley.com", "Rugilyte2017");
                helper.WaitForLoadFinish(20000);
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


        [Description("Actions -> Export forms to Excel: Click Actions > Export forms to Excel. Verify if button is displayed")]
        [Test]
        public void ExportToExcelThroughActionsButtonTest()
        {
            irsPage.ExportToExcellThroughActionsButton();
            
            Assert.IsTrue(irsPage.exportToExcelOptionElement.Displayed, "Export to Excel option in Actions button was not found");
        }

        [Description("Actions -> Assign a User to Form: Select a form, click Actions > Assign User. In Assign User dialog select a user and click Update. Verify field value is correct")]
        [Test]
        public void UserAssigmentThroughActionsButtonTest()
        {
            irsPage.AssignUserToFormThroughActionsButton();
            // Verify if form Assigned To field value is correct in grid
            Assert.AreEqual(irsPage.assignedPerson, irsPage.assignFieldValueInGrid, "Wrong Assigned To field value");
        }

        [Description("Actions -> Export to PDF: Check a form in grid , click Actions > Export forms to PDF. Verify if option is displayed")]
        [Test]
        public void ExportToPDFThroughActionsButtonTest()
        {
            irsPage.ExportToPDFThroughActionsButton();

            Assert.IsTrue(irsPage.exportToPDFOptionElement.Displayed, "Export to PDF option in Actions button was not found");
        }

        [Description("Actions -> Set Form to Open: Click Close pill, check first form, click Actions->Set to Open and if pill number changes correctly ")]
        [Test]
        public void SetFormToOpenThroughActionsButtonTest()
        {
            irsPage.SetToOpenThroughActionsButton();
            Assert.AreEqual(irsPage.pillValueBeforeTest + 1, irsPage.pillValueAfterTest, "Open pill value is incorrect");
        }

        [Description("Actions -> Set Form to Close: Click Close pill, check first form, click Actions->Set to Open and if pill number changes correctly ")]
        [Test]
        public void SetFormToCloseThroughActionsButtonTest()
        {
            irsPage.SetToCloseThroughActionsButton();
            Assert.AreEqual(irsPage.pillValueBeforeTest + 1, irsPage.pillValueAfterTest, "Close pill value is incorrect");
        }

        [Description("Actions -> Create New Form: Click on Actions Button -> Create Form, select Meetings Minutes form, click Create button.")]
        [Test]
        public void MeetingMinutesFormCreationThroughActionsButtonTest()
        {
            meetingMinutesFormPage = irsPage.OpenCreateNewMeetingMinuteFormThroughActionsButton();

            //Check if 'Actions' button is presented
            Assert.IsTrue(irsPage.actionsButtonCLosedElement.Displayed, "Action button was not found");
            Assert.IsTrue(irsPage.actionsButtonExpendedElement.Displayed, "Action button did not expand");
            helper.WaitForLoadFinish(6000);

            //Fill and save Meeting Minutes form
            meetingMinutesFormPage.FillMeetingMinutesFormFields();
            meetingMinutesFormPage = meetingMinutesFormPage.ClickApplyButton();

            //Checks if correct page (Form Detail) is opened
            Assert.AreEqual(meetingMinutesFormPage.titleExpectedText, meetingMinutesFormPage.titleText, "Title 'Form Details' was not found");

            //Checks if entered values are correct
            meetingMinutesFormPage.FindFormsFieldsValues();
            Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy"), meetingMinutesFormPage.datePickerFieldEnteredValue, "DatePicker field value in Meeting Minutes form is incorrect");
            Assert.AreEqual(meetingMinutesFormPage.meetingNumberField.ToString(), meetingMinutesFormPage.numberFieldEnteredValue, "Number field value in Meeting Minutes form is incorrect");
            Assert.AreEqual(meetingMinutesFormPage.subjectField.ToString(), meetingMinutesFormPage.subjectFieldEnteredValue, "Subject field value in Meeting Minutes form is incorrect");
            Assert.AreEqual(meetingMinutesFormPage.locationField.ToString(), meetingMinutesFormPage.locationFieldEnteredValue, "Location field value in Meeting Minutes form is incorrect");
            Assert.AreEqual(meetingMinutesFormPage.assignedPerson, meetingMinutesFormPage.assignedPerson, "Assigned To field value in Meeting Minutes form is incorrect");

            //Close form and navigates back to Base FDM page 
            meetingMinutesFormPage.ClickCloseButton();
        }
    }
}
