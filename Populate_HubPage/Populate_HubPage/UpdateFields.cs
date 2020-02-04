using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Populate_HubPage
{
    public class UpdateFields : HubPage
    {
        UpdateHubPage updateHubPage = new UpdateHubPage();
        ReadData readData = new ReadData();
        LoopItTest loopIt = new LoopItTest();

        [Test]
        public void UpdateModuleRegressionFields()
        {
            string url = "https://thehub.thomsonreuters.com/docs/DOC-892790";
            string suiteType = "module";
            var hubPage = CreatePageInstance(url);
            hubPage.SignOnToApplication();
            updateHubPage.EnterEditMode();
            updateHubPage.UpdateHubPagePage(readData,suiteType);
            updateHubPage.ClickCancelButton();
        }

        [Test]
        public void UpdateFeatureRegressionFields()
        {
            string url = "https://thehub.thomsonreuters.com/docs/DOC-892793";
            string suiteType = "feature";
            var hubPage = CreatePageInstance(url);
            hubPage.SignOnToApplication();
            updateHubPage.EnterEditMode();
            updateHubPage.UpdateHubPagePage(readData,suiteType);
            updateHubPage.ClickCancelButton();
        }

        //[Test]
        //public void ReadData()
        //{
        //    Console.WriteLine("Enter 1 or 2");
        //    int choice = int.Parse(Console.ReadLine());
        //    if(choice==1)
        //        driver.Navigate().GoToUrl("https://www.google.com");
        //    driver.Navigate().GoToUrl("https://www.facebook.com");
        //}

        [Test]
        public void LoopIt()
        {
            loopIt.LoopIt();
            loopIt.LoginToApplication();
            loopIt.HomePage();
        }
    }
}