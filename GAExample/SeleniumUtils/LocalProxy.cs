using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace GAExample.SeleniumUtils
{
    internal class LocalProxy
    {
        private ProxyServer _proxyServer;
        private readonly ExplicitProxyEndPoint _explicitEndPoint;
        private readonly List<string> _requestUrlBlocked;
        private readonly List<string> _requestUrl;
        private readonly List<string> _responseUrl;

        public Proxy SeleniumProxy { get; }

        public DataContext RequestContext { get; }

        public DataContext ResponseContext { get; }

        public DataContext RequestBlockedContext { get; }

        public LocalProxy(int portNumber)
        {
            RequestContext = new DataContext();
            ResponseContext = new DataContext();
            RequestBlockedContext = new DataContext();
            _requestUrlBlocked = new List<string>(new string[] { "www.google-analytics.com/r/collect","www.google-analytics.com" });
            _requestUrl = new List<string>(new string[] { "locatorapi.beamsuntory.com" });
            _responseUrl = new List<string>(new string[] { "www.makersmark.com" });

            _explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, portNumber);
            SeleniumProxy = new Proxy
            {
                HttpProxy = "http://localhost:" + portNumber,
                SslProxy = "http://localhost:" + portNumber,
                FtpProxy = "http://localhost:" + portNumber
            };
        }

        public void StartProxy()
        {
            _proxyServer = new ProxyServer();
            _proxyServer.AddEndPoint(_explicitEndPoint);
            _proxyServer.Start();

            _proxyServer.CertificateManager.TrustRootCertificate(true);
            _proxyServer.BeforeRequest += OnRequest;
            _proxyServer.BeforeRequest += OnRequestBlock;
            _proxyServer.BeforeResponse += OnResponse;
        }

        public void StopProxy()
        {
            _proxyServer.BeforeRequest -= OnRequest;
            _proxyServer.BeforeRequest -= OnRequestBlock;
            _proxyServer.BeforeResponse -= OnResponse;

            _proxyServer.Dispose();
        }

        private async Task OnRequest(object sender, SessionEventArgs e)
        {
            foreach (string page in _requestUrl)
            {
                if (e.WebSession.Request.Url.Contains(page))
                {
                    string guid = Guid.NewGuid().ToString();
                    RequestContext.Current.Add(guid, e.WebSession.Request);
                    RequestContext.Current.Add(guid + "_body", await e.GetRequestBodyAsString());
                }
            }
        }

        private async Task OnRequestBlock(object sender, SessionEventArgs e)
        {
            foreach (string page in _requestUrlBlocked)
            {
                if (e.WebSession.Request.Url.Contains(page))
                {
                    string guid = Guid.NewGuid().ToString();
                    RequestBlockedContext.Current.Add(guid, e.WebSession.Request);
                    RequestBlockedContext.Current.Add(guid + "_body", await e.GetRequestBodyAsString());
                    e.Ok("<!DOCTYPE html>" +
                         "<html><head><title>BLOCKED</title></head>" +
                         "<body><h1>Website Blocked</h1>" +
                         "<p>Blocked by proxy.</p>" +
                         "</body></html>");
                    
                }
            }
        }

        private async Task OnResponse(object sender, SessionEventArgs e)
        {
            foreach (string page in _responseUrl)
            {
                if (e.WebSession.Request.Url.Contains(page) && e.WebSession.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    string guid = Guid.NewGuid().ToString();
                    ResponseContext.Current.Add(guid, e.WebSession.Response);
                    ResponseContext.Current.Add(guid + "_body", await e.GetResponseBody());
                }
            }
        }
    }
}
