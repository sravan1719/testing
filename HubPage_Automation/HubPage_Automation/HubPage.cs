using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HubPage_Automation
{
    public class HubPage
    {
        public static IWebDriver driver;

        public string url = "https://thehub.thomsonreuters.com/docs/DOC-892790";

        private static readonly By UserID = By.XPath("//input[@id='USER']");

        private static readonly By Password = By.XPath("//input[@id='PASSWORD']");

        private static readonly By LoginButtonLocator = By.Id("safeLoginbtn");

        public HubPage CreatePageInstance()
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
            driver.FindElement(Password).SendKeys("Sravan@17");
            driver.FindElement(LoginButtonLocator).Click();
        }
    }
}
