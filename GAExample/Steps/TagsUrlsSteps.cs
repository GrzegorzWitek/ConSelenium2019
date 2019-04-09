using System;
using System.Collections.Generic;
using AventStack.ExtentReports.Utils;
using GAExample.SeleniumUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Titanium.Web.Proxy.Http;

namespace GAExample.Steps
{
    [Binding]
    public class TagsUrlsSteps
    {
        [Then(@"I see new tag ""(.*)"" with value ""(.*)""")]
        public void ThenISeeNewTagWithValue(string tagName, string tagValue)
        {
            List<Request> listOfRequests = new List<Request>();
            foreach (var requestData in Driver.proxy.RequestBlockedContext.Current)
            {
                if (!requestData.Key.ToLower().EndsWith("_body"))
                {
                    Request request = (Request)requestData.Value;
                    if (request.Url.Contains(Uri.EscapeDataString(tagName) + "=" + Uri.EscapeDataString(tagValue)))
                    {
                        listOfRequests.Add(request);
                    }
                }
            }
            Driver.proxy.RequestBlockedContext.Current.Clear();
            Assert.IsTrue(!listOfRequests.IsNullOrEmpty());
        }
    }

}
