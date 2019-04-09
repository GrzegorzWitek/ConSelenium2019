using System.Threading;
using GAExample.Pages;
using GAExample.SeleniumUtils;
using TechTalk.SpecFlow;

namespace GAExample.Steps
{
    [Binding]
    internal class NavigationSteps
    {
        [Given(@"I am on Main Page")]
        public void GivenIAmOnMainPage()
        {
            Driver.OpenMainPage();
            Thread.Sleep(1000);
        }

        [When(@"I enter birth date ""(.*)""")]
        public void WhenIEnterBirthDate(string date)
        {
            date = date.Replace("-", "");
            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);

            WelcomePage page = new WelcomePage();
            page.EnterDay(day)
                .EnterMonth(month)
                .EnterYear(year);
        }

        [When(@"I click Enter button")]
        public void WhenIClickEnterButton()
        {
            new WelcomePage().ClickEnterButton();
        }

        [When(@"I scroll to the end of MainPage page")]
        public void ScrollToTheEndOfPage()
        {
            MainPage page = new MainPage();
            page.ScrollDown();
        }
    }
}
