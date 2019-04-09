using System.Collections.Generic;
using GAExample.SeleniumUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Titanium.Web.Proxy.Http;
using AventStack.ExtentReports.Utils;

namespace GAExample.Steps
{
    [Binding]
    internal class ProxySteps
    {

        [Then(@"Proxy sent request with text in header ""(.*)""")]
        public void InProxySentRequest(string content)
        {
            List<Request> listOfRequests = new List<Request>();
            foreach (var requestData in Driver.proxy.RequestContext.Current)
            {
                if (!requestData.Key.ToLower().EndsWith("_body"))
                {
                    Request request = (Request)requestData.Value;
                    if (request.HeaderText.Contains(content))
                    {
                        listOfRequests.Add(request);
                    }
                }
            }

            Driver.proxy.RequestContext.Current.Clear();
            Assert.IsFalse(listOfRequests.IsNullOrEmpty());
        }

        [Then(@"Proxy blocked request with text in header ""(.*)""")]
        public void SeeInProxyBlockedRequest(string content)
        {
            List<Request> listOfRequests = new List<Request>();
            foreach (var requestData in Driver.proxy.RequestBlockedContext.Current)
            {
                if (!requestData.Key.ToLower().EndsWith("_body"))
                {
                    Request request = (Request)requestData.Value;
                    if (request.HeaderText.Contains(content))
                    {
                        listOfRequests.Add(request);
                    }
                }
            }

            Driver.proxy.RequestBlockedContext.Current.Clear();
            Assert.IsFalse(listOfRequests.IsNullOrEmpty());
        }

        [Then(@"Proxy received response with type ""(.*)""")]
        public void SeeInProxyReceivedResponse(string responseType)
        {
            List<byte[]> listOfBodies = new List<byte[]>();
            foreach (var responseData in Driver.proxy.ResponseContext.Current)
            {
                if (!responseData.Key.ToLower().EndsWith("_body"))
                {
                    var response = (Response)responseData.Value;
                    if (response.ContentType == responseType)
                    {
                        listOfBodies.Add(
                            (byte[])Driver.proxy.ResponseContext.Current[responseData.Key + "_body"]);
                    }
                }
            }

            Driver.proxy.ResponseContext.Current.Clear();
            Assert.IsFalse(listOfBodies.IsNullOrEmpty(), "Not exist searched response");
        }
    }

}
