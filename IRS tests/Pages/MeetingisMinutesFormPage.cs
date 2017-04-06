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
    class MeetingisMinutesFormPage : IrsBaseUI
    {
        IWebDriver driver;
        IJavaScriptExecutor jsDriver;
        Helper helper = new Helper();

        public IWebElement titleElement;
        public string titleText;
        public string titleExpectedText;

        public IWebElement meetingMinutesDataPickerField;
        public IWebElement meetingNumberField;
        public IWebElement subjectField;
        public IWebElement locationField;
        public IWebElement assignedToField;
        public String assignedPerson = "Rugile Petrukauskaite"; 

        public IWebElement saveChangesButtonElement;
        public IWebElement closeChangesButtonElement;
        public IWebElement cancelChangesButtonElement;

        public String datePickerFieldEnteredValue;
        public String numberFieldEnteredValue;
        public String subjectFieldEnteredValue;
        public String locationFieldEnteredValue;
        public String assignedToFieldEnteredValue;


        public MeetingisMinutesFormPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
             jsDriver = (IJavaScriptExecutor)driver;
        }

        public override IrsBaseUI FindBodyElements()
        {
            base.FindBodyElements();

            //Find Title  for Create New Form
            helper.WaitForLoadFinish(20000);
            titleElement = driver.FindElement(By.XPath("//span[@class='spa-title ng-binding']"));
            titleText = titleElement.Text;

            //Verify which part is opened - Create new Form or Form Details
            try
            {
                bool isElementDisplayed = driver.FindElement(By.ClassName("k-tabstrip-wrapper")).Displayed;
                titleExpectedText = "Form Details";
            }
            catch
            {
                titleExpectedText = "Create New Form";
            }
            return this;
        }

        public MeetingisMinutesFormPage FillMeetingMinutesFormFields()
        {
            meetingMinutesDataPickerField = driver.FindElement(By.Id("Datepicker1Input_1"));
            meetingMinutesDataPickerField.SendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

            meetingNumberField = driver.FindElement(By.Id("TextBox2Input_1"));
            meetingNumberField.SendKeys(meetingNumberField.ToString());

            subjectField = driver.FindElement(By.Id("TextBox1Input_1"));
            subjectField.SendKeys(subjectField.ToString());

            locationField = driver.FindElement(By.Id("TextBox3Input_1"));
            locationField.SendKeys(locationField.ToString());

            assignedToField = driver.FindElement(By.CssSelector("[class='k-dropdown-wrap k-state-default']"));
            assignedToField = driver.FindElement(By.CssSelector("[class='k-input kendo-form-control ng-pristine ng-untouched ng-valid']"));
            assignedToField.SendKeys(assignedPerson);

            return this;
        }
        public MeetingisMinutesFormPage ClickApplyButton()
        {
            helper.WaitForLoadFinish(6000);
            saveChangesButtonElement = driver.FindElement(By.CssSelector("[class='btn-lg btn-primary genObjectFooterButton genObjectFooterButtonLeft ng-binding']"));
            saveChangesButtonElement.Click();
            helper.WaitForLoadFinish(6000);
            FindBodyElements();

            return this;
        }

        public MeetingisMinutesFormPage FindFormsFieldsValues()
        { 
            datePickerFieldEnteredValue = (string)jsDriver.ExecuteScript("return document.getElementsByClassName('k-picker-wrap k-state-default')[0].children[0].value;");
            numberFieldEnteredValue = (string)jsDriver.ExecuteScript("return document.getElementsByClassName('flow-layout ng-pristine ng-valid')[1].children[0].children[0].value;");
            subjectFieldEnteredValue = (string)jsDriver.ExecuteScript("return document.getElementsByClassName('flow-layout ng-pristine ng-valid')[2].children[0].children[0].value;");
            locationFieldEnteredValue = (string)jsDriver.ExecuteScript("return document.getElementsByClassName('flow-layout ng-pristine ng-valid')[3].children[0].children[0].value;");
            assignedToFieldEnteredValue = (string)jsDriver.ExecuteScript("return document.getElementsByClassName('k-input kendo-form-control ng-pristine ng-untouched ng-valid')[0].value;");
            return this;
        }
        public IRSPage ClickCloseButton()
        {
            closeChangesButtonElement = driver.FindElement(By.CssSelector("[class='ng-binding ng-scope btn-lg btn-primary genObjectFooterButton']"));
            closeChangesButtonElement.Click();
            return new IRSPage(driver);
        }

        public IRSPage ClickCancelButton()
        {
            cancelChangesButtonElement = driver.FindElement(By.CssSelector("[class='btn-lg btn-secondary genObjectFooterButton ng-binding']"));
            cancelChangesButtonElement.Click();
            helper.WaitForLoadFinish(6000);
            return new IRSPage(driver);
        }
    }
}
