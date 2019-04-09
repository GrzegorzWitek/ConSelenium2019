using System.Collections.Generic;
using GAExample.SeleniumUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace GAExample.Steps
{
    [Binding]
    public class DataLayerSteps
    {
        [Then(@"In DataLayer, in global section is entry ""(.*)"" with value ""(.*)""")]
        public void ThenIGetResultsInGlobal(string entry, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.driver;
            Dictionary<string, object> dataLayer = (Dictionary<string, object>)js.ExecuteScript("return dataLayer[0]");
            Dictionary<string, object> a = (Dictionary<string, object>)dataLayer["global"];
            Assert.AreEqual(value, a[entry]);
        }

        [Then(@"In DataLayer, in user section is entry ""(.*)"" with value ""(.*)""")]
        public void ThenInDataLayerInUserSectionIsEntryWithValue(string entry, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.driver;
            Dictionary<string, object> dataLayer = (Dictionary<string, object>)js.ExecuteScript("return dataLayer[0]");
            Dictionary<string, object> a = (Dictionary<string, object>)dataLayer["user"];
            Assert.AreEqual(value, a[entry]);
        }
    }
}
