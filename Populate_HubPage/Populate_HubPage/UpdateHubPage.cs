using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Populate_HubPage
{
    public class UpdateHubPage : HubPage
    {
        private static readonly By editLocator = By.XPath("//li[@id='jive-link-edit']");

        private const string SuiteNameLctMask =
            "//td//span[text()='{0}']/following::*//a[text()='{1}']/ancestor::td";

        private static readonly By CancelLocator = By.Id("composeButtonCancel");

        private static readonly By ColumnPropertiesLocator = By.XPath("//div[@aria-label='Cell properties']/button");

        private static readonly By EditModeLocator = By.XPath("//span[@class='j-edit-locked']");

        private const string DailySuiteValueLctMask = "({0}/ancestor::tr)[1]";

        private const string WeeklySuiteValueLctMask = "({0}/ancestor::tr/following-sibling::tr)[1]";

        private const string ResultsValueLctMask = "{0}/td//span[contains(text(),'%')]";

        private const string DateValueLctMask = "{0}/td//*[contains(text(),'-')]";

        private const string NotesValueLctMask = "({0}/td)[last()]";

        UpdateValues updateValues = new UpdateValues();

        public void EnterEditMode()
        {
            if(driver.FindElements(EditModeLocator).Count>0)
            {
                Thread.Sleep(3000);
                driver.Close();
                Environment.Exit(0);
            }
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(editLocator));
            driver.FindElement(editLocator).Click();
        }

        public void UpdateHubPagePage(ReadData readData,string suiteType)
        {
            Thread.Sleep(5000);            
            int totalCount = readData.GetTotalCountOfSuites();
            for (int index = 0; index < totalCount; index++)
            {
                if (readData.GetType(index).ToLower().Equals(suiteType))
                {
                    string testName = readData.GetArea(index);
                    string suiteName = readData.GetSuiteName(index);
                    string date = readData.GetDate(index);
                    string additionalNotes = readData.GetNotes(index);
                    var suitResults = readData.GetSuiteResults(index);

                    //driver.FindElement(By.TagName("body")).SendKeys(Keys.Home);
                    driver.SwitchTo().Frame("wysiwygtext_ifr");
                    suiteName = string.Format(SuiteNameLctMask, testName, suiteName);
                    var suitePath = By.XPath(suiteName);
                    new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementIsVisible(suitePath));
                    string dailySuitePath = string.Format(DailySuiteValueLctMask, suiteName);
                    FillResults(dailySuitePath, suitResults);
                    FillDate(dailySuitePath, date);
                    FillNotes(dailySuitePath, additionalNotes);


                    if (TotalCountOfSuite(driver.FindElement(suitePath)) == 2)
                    {
                        index++;
                        suitResults = readData.GetSuiteResults(index);
                        date = readData.GetDate(index);
                        additionalNotes = readData.GetNotes(index);

                        string weeklySuitePath = string.Format(WeeklySuiteValueLctMask, suiteName);
                        FillResults(weeklySuitePath, suitResults);
                        FillDate(weeklySuitePath, date);
                    }

                    driver.SwitchTo().DefaultContent();
                    Thread.Sleep(5000);
                }
            }
        }
        public int TotalCountOfSuite(IWebElement suite)
        {
            int count = 1;
            try
            {
                string value = suite.GetAttribute("rowspan");
                if (value!= null)
                    count=2;
            }
            catch(Exception) { }
            return count;
        }

        public void FillResults(string suitePath, List<string> suiteResults)
        {
            var column = driver.FindElements(By.XPath(string.Format(ResultsValueLctMask, suitePath)));
            for (int index = 0; index < column.Count; index++)
            {
                ReplaceResults(column[index], suiteResults[index]+"%");
                driver.SwitchTo().DefaultContent();
                driver.FindElement(ColumnPropertiesLocator).Click();                
                updateValues.UpdateBackgroundColor(int.Parse(suiteResults[index]));
                driver.SwitchTo().Frame("wysiwygtext_ifr");
            }
        }

        public void FillDate(string suitePath, string date)
        {
            var column = driver.FindElements(By.XPath(string.Format(DateValueLctMask, suitePath)));
            foreach (var Date in column)
            {
                ReplaceDateValue(Date, date);                
            }
        }

        public void FillNotes(string suitePath, string notes)
        {
            var column = driver.FindElement(By.XPath(string.Format(NotesValueLctMask, suitePath)));
            ReplaceNotesValue(column, notes);
        }

        public void ReplaceResults(IWebElement element, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(false)", element);
            element.Click();
            element.SendKeys(Keys.End);
            Actions actions = new Actions(driver);
            actions.KeyDown(Keys.Shift).SendKeys(Keys.Home).KeyUp(Keys.Shift).SendKeys(value).Build().Perform();
        }

        public void ReplaceDateValue(IWebElement element, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(false)", element);
            element.Click();
            element.SendKeys(Keys.End);
            Actions actions = new Actions(driver);
            for (int i = 0;i < 2;i++)
            {
                element.SendKeys(Keys.Backspace);
            }
            actions.KeyDown(Keys.Shift).SendKeys(Keys.Home).KeyUp(Keys.Shift).SendKeys(value).Build().Perform();
        }

        public void ReplaceNotesValue(IWebElement element, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(false)", element);
            element.Click();
            element.SendKeys(Keys.Home);
            Actions actions = new Actions(driver);
            while (element.Text != "")
            {
                var x = element.Text;
                actions.KeyDown(Keys.Shift).SendKeys(Keys.End).KeyUp(Keys.Shift).SendKeys(Keys.Backspace).Build().Perform();
            }
            element.SendKeys(value);
        }
        public void ClickCancelButton()
        {
            var cancel = driver.FindElement(CancelLocator);
            Actions actions = new Actions(driver);
            actions.MoveToElement(cancel);
            actions.Perform();
            cancel.Click();
        }
    }
}
