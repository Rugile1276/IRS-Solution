using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.IO;
using IRS_tests.Additional;

namespace IRS_tests
{

    class IRSPage : IrsBaseUI
    {
        #region Instances
        IWebDriver driver;
        Helper helper = new Helper();

        public IWebElement titleElement;
        public string titleText;
        public string titleExpectedText;

        public IWebElement categoryElement;
        public String categoryText;
        public String categoryExpectedText;


        public IWebElement categoriesListElement;
        public List<String> categoriesListText = new List<string>();
        public List<String> categoriesListExpectedText = new List<string>();

        public IWebElement actionsButtonCLosedElement;
        public IWebElement actionsButtonExpendedElement;
        public IWebElement formsListButtonElement;
        public IWebElement meetingMinutesFormElement;
        public IWebElement createButtonElement;

        public IWebElement checkBoxElement;
        public IWebElement assignUserOptionInActionsButton;
        public IWebElement usersListFieldElement;
        public String assignedPerson = "BSI TEST";
        public String assignFieldValueInGrid;
        public IWebElement updateFormsButtonElement;

        public IWebElement exportToExcelOptionElement;
        public IWebElement setToClosedOptionElement;
        public IWebElement setToOpenOptionElement;
        public IWebElement exportToPDFOptionElement;
        int i = 0;

        public IWebElement openPillElement;
        public int openPillValue;
        public IWebElement closePillElement;
        public int closePillValue;
        public IWebElement allPillElement;
        public int allPillValue;
        public IWebElement myOpenFormsPillElement;
        public int myOpenFormsPillPillValue;

        public int pillValueBeforeTest;
        public int pillValueAfterTest;
        #endregion

        public IRSPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        #region Click In Base FDM page
        public override IrsBaseUI FindBodyElements()
        {
            base.FindBodyElements();

            //Find Titlte
            helper.WaitForLoadFinish(20000);
            titleElement = driver.FindElement(By.XPath("//span[@class='spa-title ng-binding']"));
            titleText = titleElement.Text;
            titleExpectedText = "All Form Data";

            //Find Category text elements
            categoryElement = driver.FindElement(By.XPath("//span[contains(., 'Category:')]"));
            categoryText = categoryElement.Text;
            categoryExpectedText = "Category:";

            //Find Categories List element

            helper.WaitForLoadFinish(6000);
            categoriesListElement = driver.FindElement(By.CssSelector("span.k-dropdown-wrap.k-state-default"));
            return this;
        }

        public IRSPage CloseTipForUser()
        {
            //Find Getting Started Notification and close it 
            helper.WaitForLoadFinish(6000);
            try
            {
                actionsButtonCLosedElement = driver.FindElement(By.CssSelector("[class='hopscotch-bubble-close hopscotch-close']"));
                actionsButtonCLosedElement.Click();
            }
            catch
            {
                return this;
            }
            return this;
        }

        public IRSPage ExpandActionsButton()
        {
            try
            {
                i++;
                //Find Actions Button
                actionsButtonCLosedElement = driver.FindElement(By.CssSelector("[class='ng-binding ng-scope']"));
                //Click to expand Actions button
                actionsButtonCLosedElement.Click();
            }
            catch (Exception ex)
            {
                if (i < 5)
                {
                    helper.WaitForLoadFinish(6000);
                    ExpandActionsButton();
                }
                else
                    Assert.Fail(string.Format("Unable expand Actions button: {0}", ex));
            }
            i = 0;

            //Find Actions button list
            actionsButtonExpendedElement = driver.FindElement(By.CssSelector("[class='btn-regular dropdown-toggle masterButton btn hovered']"));

            return this;
        }

        public IRSPage CheckFormInGrid()
        {
            //Select a form in grid by clicking on textbox
            //The problem is random error - element is not clicable at the point..
            //So it do until no error occurs
            try
            {
                i++;
                checkBoxElement = driver.FindElements(By.CssSelector("[class='gridcheckbox']"))[0];
                checkBoxElement.Click();
            }
            catch (Exception ex)
            {
                if (i < 5)
                {
                    helper.WaitForLoadFinish(6000);
                    CheckFormInGrid();
                }
                else
                    Assert.Fail(string.Format("Unable to check a form: {0}", ex));
            }
            i = 0;
            return this;
        }

        //Random error appears, try catch block is as workarround
        public IRSPage FindAssignedFieldValueInGrid()
        {
            try
            {
                i++;
                assignFieldValueInGrid = driver.FindElement(By.XPath("//*[@id='DefaultForm_0']/div[2]/div[1]/table/tbody/tr[1]/td[4]/span")).Text;
                if (assignFieldValueInGrid == "")
                    throw new Exception();
            }
            catch (Exception ex)
            {
                if (i < 5)
                {
                    helper.WaitForLoadFinish(6000);
                    FindAssignedFieldValueInGrid();
                }
                else
                {
                    i = 0;
                    Assert.Fail(string.Format("Unable to find Assigned To field value element: {0}", ex));
                }
            }
            i = 0;
            return this;

        }
        #endregion

        #region Clicks in Actions button

        public IRSPage ClickToExportFormsToExcellInActionsButton()
        {
            //Find Export to Excel option in Action and click
            exportToExcelOptionElement = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[3];
            //Selenium is not able to test downloads
            //exportToExcelOptionElement.Click();
            return this;
        }

        public IRSPage ClickToAssignUserInActionsButton()
        {
            //Find Assign To option in Action and click
            assignUserOptionInActionsButton = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[4];
            assignUserOptionInActionsButton.Click();
            return this;
        }

        public IRSPage ClickToExportToPDFInActionsButton()
        {
            //Find Assign To option in Action and click
            exportToPDFOptionElement = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[5];

            //Selenium is not able to test downloads
            //exportToPDFOptionElement.Click();
            return this;
        }

        public IRSPage ClickToOpenFormInActionsButton()
        {
            //Find Set to Open option in Action and click
            setToOpenOptionElement = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[6];

            setToOpenOptionElement.Click();
            return this;
        }

        public IRSPage ClickToCloseFormInActionsButton()
        {
            //Find Set to Close option in Action and click
            setToClosedOptionElement = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[7];
            setToClosedOptionElement.Click();
            return this;
        }

        public IRSPage ClickToCreateFormInActionsButton()
        {
            //Find Create option in Action and click
            meetingMinutesFormElement = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[8];
            meetingMinutesFormElement.Click();
            return this;
        }
        #endregion

        #region Clicks in Pills
        
        public int GetOpenPillCurrentValue()
        {
            openPillElement = driver.FindElement(By.XPath("//*[@id='countToggle']/div[1]/div[1]"));
            openPillValue = Int32.Parse(openPillElement.Text);
            return openPillValue;
        }

        public int GetClosedPillCurrentValue()
        {
            closePillElement = driver.FindElement(By.XPath("//*[@id='countToggle']/div[2]/div[1]"));
            closePillValue = Int32.Parse(closePillElement.Text);
            return closePillValue;
        }

        public int GetAllPillCurrentValue()
        {
            allPillElement = driver.FindElement(By.XPath("//*[@id='countToggle']/div[3]/div[1]"));
            allPillValue = Int32.Parse(allPillElement.Text);
            return allPillValue;
        }

        public int GetMyOpenFormPillCurrentValue()
        {
            myOpenFormsPillElement = driver.FindElement(By.XPath("//*[@id='countToggle']/div[3]/div[1]"));
            myOpenFormsPillPillValue = Int32.Parse(myOpenFormsPillElement.Text);
            return myOpenFormsPillPillValue;
        }

        public void ClickOnOpenPill()
        {
            driver.FindElement(By.XPath("//*[@id='countToggle']/div[2]/div[1]")).Click();
        }

        public void ClickOnClosePill()
        {
            driver.FindElement(By.XPath("//*[@id='countToggle']/div[2]/div[1]")).Click();
        }

        public void ClickOnAllPill()
        {
            driver.FindElement(By.XPath("//*[@id='countToggle']/div[3]/div[1]")).Click();
        }

        public void ClickOnAllMyFormsPill()
        {
            driver.FindElement(By.XPath("//*[@id='countToggle']/div[3]/div[1]")).Click();
        }

        #endregion

        #region Clicks in Create New Form dialog
        public MeetingisMinutesFormPage SelectMeetingMinutesForm()
        {
            helper.WaitForLoadFinish(6000);
            //Expand categories List
            formsListButtonElement = driver.FindElements(By.CssSelector("[class='ng-pristine ng-untouched ng-valid']"))[0];
            formsListButtonElement.Click();

            //Select Meeting Minutes form from the list
            meetingMinutesFormElement = driver.FindElements(By.CssSelector("[class*='ng-binding ng-scope']"))[9];
            meetingMinutesFormElement.Click();

            //Click to create
            createButtonElement = driver.FindElement(By.CssSelector("[class='btn btn-primary confirm ng-binding ng-scope']"));
            createButtonElement.Click();
            return new MeetingisMinutesFormPage(driver);
        }
        #endregion

        #region Assign User Dialog
        //Random error appears, try-catch is as workarround
        public IRSPage SelectUserToAssign()
        {
            try
            {
                i++;
                usersListFieldElement = driver.FindElement(By.XPath("//*[@id='AssignmentModal']/div[2]/span/span/input"));
                usersListFieldElement.SendKeys("BSI TEST");
            }
            catch(Exception ex)
            {
                if (i < 5)
                    {
                    helper.WaitForLoadFinish(6000);
                    SelectUserToAssign();
                    }
                else
                    Assert.Fail(string.Format("Unable to find users list field element: {0}", ex));
            }
            i = 0;
            return this;
        }

        public IRSPage ClickUpdateFormsInAssigUserDialog()
        {
            updateFormsButtonElement = driver.FindElement(By.XPath("//*[@id='modalId_0']/form/div[3]/button[1]"));
            updateFormsButtonElement.Click();
            return this;
        }
        #endregion

        #region Formed full functions

        public IRSPage ExportToExcellThroughActionsButton()
        {
            CloseTipForUser();
            ExpandActionsButton();
            ClickToExportFormsToExcellInActionsButton();
            return this;
        }

        public IRSPage AssignUserToFormThroughActionsButton()
        {
            CloseTipForUser();
            CheckFormInGrid();
            ExpandActionsButton();
            ClickToAssignUserInActionsButton();
            SelectUserToAssign();
            ClickUpdateFormsInAssigUserDialog();
            FindAssignedFieldValueInGrid();
            return this;
        }

        public IRSPage ExportToPDFThroughActionsButton()
        {
            CloseTipForUser();
            CheckFormInGrid();
            ExpandActionsButton();
            ClickToExportToPDFInActionsButton();
            return this;
        }

        public IRSPage SetToOpenThroughActionsButton()
        {

            CloseTipForUser();

            //In case there are no closed form, close one
            CheckFormInGrid();
            ExpandActionsButton();
            ClickToCloseFormInActionsButton();

            helper.WaitForLoadFinish(6000);
            pillValueBeforeTest = GetOpenPillCurrentValue();
            ClickOnClosePill();
            ExpandActionsButton();
            ClickToOpenFormInActionsButton();
            helper.WaitForLoadFinish(6000);
            pillValueAfterTest = GetOpenPillCurrentValue();
            return this;
        }

        public IRSPage SetToCloseThroughActionsButton()
        {
            CloseTipForUser();
            pillValueBeforeTest = GetClosedPillCurrentValue();
            CheckFormInGrid();
            ExpandActionsButton();
            ClickToCloseFormInActionsButton();
            helper.WaitForLoadFinish(6000);
            pillValueAfterTest = GetClosedPillCurrentValue();
            return this;
        }

        public MeetingisMinutesFormPage OpenCreateNewMeetingMinuteFormThroughActionsButton()
        {
            CloseTipForUser();
            ExpandActionsButton();
            ClickToCreateFormInActionsButton();
            SelectMeetingMinutesForm();
            return new MeetingisMinutesFormPage(driver);
        }
        #endregion
    }
}
