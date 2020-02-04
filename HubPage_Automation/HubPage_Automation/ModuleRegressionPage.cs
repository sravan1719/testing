using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HubPage_Automation
{
    public class ModuleRegressionPage : HubPage
    {
        private static readonly By editLocator = By.XPath("//li[@id='jive-link-edit']");

        private const string SuiteNameLctMask =
            "//td/span[text()='{0}']/following::*//a[text()='{1}']/ancestor::td";

        private static readonly By CancelLocator = By.Id("composeButtonCancel");

        private static readonly By ColumnPropertiesLocator = By.XPath("//div[@aria-label='Cell properties']/button");

        private const string DailySuiteValueLctMask = "({0}/ancestor::tr)[1]";

        private const string WeeklySuiteValueLctMask = "({0}/ancestor::tr/following-sibling::tr)[1]";

        private const string UpdateResultsValueLctMask = "{0}/td/span[contains(text(),'%')]";

        private const string UpdateDateValueLctMask = "{0}//*[contains(text(),'-')]";

        UpdateValues updateValues = new UpdateValues();

        public void EnterEditMode()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(editLocator));
            driver.FindElement(editLocator).Click();

        }

        public void UpdateResults(string module, string suiteName)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Frame("wysiwygtext_ifr");
            suiteName = string.Format(SuiteNameLctMask, module, suiteName);
            var suitePath = By.XPath(suiteName);
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementIsVisible(suitePath));
            string dailySuitePath = string.Format(DailySuiteValueLctMask, suiteName);
            var x = driver.FindElements(By.XPath(string.Format(UpdateResultsValueLctMask, dailySuitePath)));
            var date = driver.FindElements(By.XPath(string.Format(UpdateDateValueLctMask, dailySuitePath)));
            FillResults(x);
            FillDate(date);
            int count = TotalCountOfSuite(driver.FindElement(suitePath));
            if (count == 2)
            {
                string weeklySuitePath = string.Format(WeeklySuiteValueLctMask, suiteName);
                date = driver.FindElements(By.XPath(string.Format(UpdateDateValueLctMask, weeklySuitePath)));
                x = driver.FindElements(By.XPath(string.Format(UpdateResultsValueLctMask, weeklySuitePath)));
                FillResults(x);
                FillDate(date);
            }
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
        }

        public int TotalCountOfSuite(IWebElement suite)
        {
            int count = 1;
            try
            {
                string value = suite.GetAttribute("rowspan");
                if (value != null)
                    count = 2;
            }
            catch (Exception) { }
            return count;
        }

        public void FillResults(IList<IWebElement> column)
        {
            foreach (var Result in column)
            {
                ReplaceValue(Result, "20%");
                driver.SwitchTo().DefaultContent();
                driver.FindElement(ColumnPropertiesLocator).Click();
                updateValues.UpdateBackgroundColor(20);
                driver.SwitchTo().Frame("wysiwygtext_ifr");
            }
        }

        public void FillDate(IList<IWebElement> column)
        {
            foreach (var Date in column)
            {
                ReplaceValue(Date, "Jan-08");
            }
        }

        public void ReplaceValue(IWebElement element, string value)
        {
            element.Click();
            element.SendKeys(Keys.End);
            Actions actions = new Actions(driver);
            actions.KeyDown(Keys.Shift).SendKeys(Keys.Home).KeyUp(Keys.Shift).SendKeys(value).Build().Perform();
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
