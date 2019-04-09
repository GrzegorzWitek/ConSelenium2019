using System.Threading;
using GAExample.SeleniumUtils;
using OpenQA.Selenium;

namespace GAExample.Pages
{
    internal class MainPage : BasePage
    {
        private readonly By _tasteLinkLocator = By.CssSelector("img[alt='Taste']");
        private readonly By _menuButtonLocator = By.CssSelector("body");

        public void ClickTasteLink()
        {
            Driver.ClickOn(_tasteLinkLocator);
        }

        public MainPage ScrollDown()
        {
            Driver.InsertTextTo(_menuButtonLocator, Keys.End);
            Thread.Sleep(1000);
            return new MainPage();
        }
    }
}
