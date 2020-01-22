using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Populate_HubPage
{
    public class UpdateFields : HubPage
    {
        ModuleRegressionPage modulePage = new ModuleRegressionPage();
        LoopItTest loopIt = new LoopItTest();
        ReadExcelData data = new ReadExcelData();

        [Test]
        public void UpdateModuleRegressionFields()
        {
            var hubPage = CreatePageInstance();
            hubPage.SignOnToApplication();
            modulePage.EnterEditMode();
            modulePage.UpdateResults("Search", "UI Miscellaneous");
            modulePage.UpdateResults("Related Information", "UI Content");
            modulePage.UpdateResults("Related Information", "UI Mobile");
            modulePage.ClickCancelButton();
            //modulePage.UpdateResults("UI Miscellaneous");
        }

        [Test]
        public void ReadData()
        {
            data.getExcelFile();
        }

        [Test]
        public void LoopIt()
        {
            loopIt.LoopIt();
            loopIt.LoginToApplication();
            loopIt.HomePage();
        }
    }
}