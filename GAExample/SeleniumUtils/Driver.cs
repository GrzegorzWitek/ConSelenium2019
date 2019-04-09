using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GAExample.SeleniumUtils
{
    internal static class Driver
    {
        public static IWebDriver driver;
        public static LocalProxy proxy;

        private static readonly string baseUrl = "https://www.makersmark.com";
        private static readonly int proxyPort = 8081;

        public static void CreateDriver()
        {
            proxy = new LocalProxy(proxyPort);
            proxy.StartProxy();

            ChromeOptions option = new ChromeOptions();
            option.AddArguments(new List<string> { "no-sandbox",
                "disable-gpu"});

            option.Proxy = proxy.SeleniumProxy;
            driver = new ChromeDriver(".", option);
            driver.Manage().Window.Maximize();
        }

        public static void OpenMainPage()
        {
            driver.Navigate().GoToUrl(baseUrl);
        }

        public static void Dispose()
        {
            driver.Quit();
            proxy.StopProxy();
        }

        public static void ClickOn(By locator)
        {
            driver.FindElement(locator).Click();
        }

        public static void InsertTextTo(By locator, string text)
        {
            driver.FindElement(locator).SendKeys(text);
        }

        public static void SelectFromDropDown(By locator, string text)
        {
            new SelectElement(driver.FindElement(locator)).SelectByText(text);
        }

    }
}
