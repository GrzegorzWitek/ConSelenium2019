using GAExample.SeleniumUtils;
using OpenQA.Selenium;

namespace GAExample.Pages
{
    internal class WelcomePage : BasePage
    {
        private readonly By _countryDropDownLocator = By.Id("age-gate-locale");
        private readonly By _monthInputLocator = By.CssSelector("[name='month']");
        private readonly By _dayInputLocator = By.CssSelector("[name='day']");
        private readonly By _yearInputLocator = By.CssSelector("[name='year']");
        private readonly By _enterButtonLocator = By.CssSelector(".wax-button>[type='submit']");
        public WelcomePage EnterDay(string day)
        {
            Driver.InsertTextTo(_dayInputLocator, day);
            return new WelcomePage();
        }

        public WelcomePage EnterMonth(string month)
        {
            Driver.InsertTextTo(_monthInputLocator, month);
            return new WelcomePage();
        }

        public WelcomePage EnterYear(string year)
        {
            Driver.InsertTextTo(_yearInputLocator, year);
            return new WelcomePage();
        }

        public MainPage ClickEnterButton()
        {
            Driver.ClickOn(_enterButtonLocator);
            return new MainPage();
        }

        public WelcomePage SelectCountry(string countryName)
        {
            Driver.SelectFromDropDown(_countryDropDownLocator, countryName);
            return new WelcomePage();
        }
    }
}
