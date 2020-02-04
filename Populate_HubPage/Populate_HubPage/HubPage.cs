using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Populate_HubPage
{
    public class HubPage
    {
        public static IWebDriver driver;

        private static readonly By UserID = By.XPath("//input[@id='USER']");

        private static readonly By Password = By.XPath("//input[@id='PASSWORD']");

        private static readonly By LoginButtonLocator = By.Id("safeLoginbtn");

        public HubPage CreatePageInstance(string url)
        {
            driver = new ChromeDriver("D://DotNet//Populate_HubPage");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            return new HubPage();
        }
        public void SignOnToApplication()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(1000)).Until(ExpectedConditions.ElementExists(UserID));
            driver.FindElement(UserID).SendKeys("C261008");
            driver.FindElement(Password).SendKeys("K@nn@1708");
            driver.FindElement(LoginButtonLocator).Click();
        }
    }
}
