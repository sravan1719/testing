using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Populate_HubPage
{
    public class LoopItTest: HubPage
    {

        const string url = "https://lptqadata002.z13.web.core.windows.net/";

        private static readonly By LoginButtonLocator = By.XPath("//li[@class='button login']");

        private static readonly By UserIdLocator = By.Id("logonIdentifier");

        private static readonly By PasswordLocator = By.Id("password");

        private static readonly By SignInButtonLocator = By.XPath("//button[text()='Sign in']");

        private const string HomePageLctMask = "//h2[text()='{0}']";

        private const string  TableNameLctMask = "//tbody[@role='rowgroup']/tr/td/span[text()='{0}']";

        private const string NavigationButtonLctMask = "//span[contains(text(),'{0}')]";

        private static readonly By LanguagesLocator = By.XPath("//span[text()='Languages']/ancestor::div[@class='mat-form-field-infix']");

        private const string LanguageOptionsLctMask = "//div[contains(@id,'cdk-overlay-')]//span[contains(text(),'{0}')]";

        const string businessName= "apollo1";

        const string locationName = "hill";

        public LoopItTest LoopIt()
        {
            driver = new ChromeDriver("D://DotNet//Populate_HubPage");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            return new LoopItTest();
        }

        public void LoginToApplication()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(LoginButtonLocator));
            driver.FindElement(LoginButtonLocator).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(UserIdLocator));
            driver.FindElement(UserIdLocator).SendKeys("tulasi_kasarapu@epam.com");
            driver.FindElement(PasswordLocator).SendKeys("P@ssword!");
            driver.FindElement(SignInButtonLocator).Click();
        }

        public void HomePage()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(By.XPath(string.Format(HomePageLctMask, "Existing Businesses"))));
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(By.XPath(string.Format(TableNameLctMask, businessName))));
            driver.FindElement(By.XPath(string.Format(TableNameLctMask, businessName))).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(By.XPath(string.Format(NavigationButtonLctMask, "Next"))));
            driver.FindElement(By.XPath(string.Format(NavigationButtonLctMask, "Next"))).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(By.XPath(string.Format(TableNameLctMask, locationName))));
            driver.FindElement(By.XPath(string.Format(TableNameLctMask, locationName))).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(By.XPath(string.Format(NavigationButtonLctMask, "Edit"))));
            driver.FindElement(By.XPath(string.Format(NavigationButtonLctMask, "Edit"))).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(LanguagesLocator));
            driver.FindElement(LanguagesLocator).Click();
            //driver.FindElement(By.XPath(string.Format(LanguageOptionsLctMask, " Arabic, Levantine SM"))).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementExists(By.XPath(string.Format(LanguageOptionsLctMask, "English"))));
            driver.FindElement(By.XPath(string.Format(LanguageOptionsLctMask, "English"))).Click();
            driver.FindElement(By.XPath(string.Format(LanguageOptionsLctMask, "Estonian"))).Click();
            driver.FindElement(By.XPath("//body")).Click();
        }

    }
}
