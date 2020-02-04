using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HubPage_Automation
{
    public class UpdateFields : HubPage
    {
        ModuleRegressionPage modulePage = new ModuleRegressionPage();

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
    }
}
