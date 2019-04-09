using GAExample.SeleniumUtils;
using TechTalk.SpecFlow;

namespace GAExample.Steps
{
    [Binding]
    class SetupSteps
    {
        [BeforeScenario]
        public void SetupForProxyScenario()
        {
            Driver.CreateDriver();
        }

        [AfterScenario]
        public void TearDown()
        {
            Driver.Dispose();
        }
    }
}
