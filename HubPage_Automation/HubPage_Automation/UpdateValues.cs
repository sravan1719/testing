using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;


namespace HubPage_Automation
{
    public class UpdateValues : HubPage
    {
        private static readonly By AdvanceFieldLocator = By.XPath("//div[text()='Advanced']");

        private static readonly By SubmitLocator = By.XPath("//button[@role='presentation']/span[text()='Ok']");

        private static readonly By BackgroundColorLocator = By.XPath("//label[text()='Background color']//parent::div/div/input");

        public void UpdateBackgroundColor(int value)
        {
            string color = "#c2d69a";

            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(AdvanceFieldLocator));
            driver.FindElement(AdvanceFieldLocator).Click();

            if (value < 85 && value >= 70)
            {
                color = "#ffffdb";
            }

            else if (value < 70)
            {
                color = "#f49697";
            }

            driver.FindElement(BackgroundColorLocator).Clear();

            driver.FindElement(BackgroundColorLocator).SendKeys(color);

            driver.FindElement(SubmitLocator).Click();
        }
    }
}
