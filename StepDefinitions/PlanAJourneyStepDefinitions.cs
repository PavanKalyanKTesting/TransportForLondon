using TransportForLondon.ApplicationPages;
using TransportForLondon.Context;

namespace TransportForLondon.StepDefinitions
{
    [Binding]
    public class PlanAJourneyStepDefinitions
    {
        private readonly WebDriverContext webDriverContext;
        public PlanAJourneyStepDefinitions(WebDriverContext _webDriverContext)
        {
            webDriverContext = _webDriverContext;
        }

        DataReaderClass data = new DataReaderClass();
        HomePage home;
        PlanAJourneyPage planAJourney;
        JourneyResultsPage journeyResults;

        [Given(@"Open the URL in chrome browser")]
        public void GivenOpenTheURLInChromeBrowser()
        {
            webDriverContext.driver.Url = data.ReadFileData().URL;
            webDriverContext.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            home = new HomePage(webDriverContext.driver, webDriverContext.wait).AcceptCookies();
        }

        [When(@"Identify Plan a journey widget")]
        public void WhenIdentifyPlanAJourneyWidget()
        {
            home.PlanAJourneyMenuClick(); //PlanAJourneyClick
            planAJourney = new PlanAJourneyPage(webDriverContext.driver, webDriverContext.wait).VerifyPlanAJourneyPage();
        }

        [When(@"From and To fields are entered with valid locations")]
        public void WhenFromAndToFieldsAreEnteredWithValidLocations()
        {
            journeyResults = planAJourney.EnterValidLocations(data.ReadFileData().ValidLocation.From, data.ReadFileData().ValidLocation.To).ClickPlanAJourneyButton();
        }

        [Then(@"valid journey results are loaded")]
        public void ThenValidJourneyResultsAreLoaded()
        {
            journeyResults.ValidResultsVerification(data.ReadFileData().ValidLocation.From, data.ReadFileData().ValidLocation.To);
        }

        [When(@"From and To fields are entered with invalid locations")]
        public void WhenFromAndToFieldsAreEnteredWithInvalidLocations()
        {
            journeyResults = planAJourney.EnterInvalidLocations(data.ReadFileData().InvalidLocation.From, data.ReadFileData().InvalidLocation.To).ClickPlanAJourneyButton();
        }

        [Then(@"invalid journey results are loaded")]
        public void ThenInvalidJourneyResultsAreLoaded()
        {
            journeyResults.InvalidResultsVerification();
        }

        [When(@"From and To fields are entered with empty locations")]
        public void WhenFromAndToFieldsAreEnteredWithEmptyLocations()
        {
            planAJourney.ClickPlanAJourneyButtonForEmptyFields();
        }

        [Then(@"Field Error messages validated")]
        public void ThenFieldErrorMessagesValidated()
        {
            planAJourney.EmptyFieldsValidations();
        }

        [When(@"Edit Journey Results")]
        public void WhenEditJourneyResults()
        {
            journeyResults.ClickEditJourney().VerifyEditJourneyScreen(data.ReadFileData().ValidLocation.From, data.ReadFileData().ValidLocation.To);
        }

        [Then(@"New To Location Added")]
        public void ThenNewToLocationAdded()
        {
            journeyResults.EnterNewLocationsOnEditScreen(data.ReadFileData().EditLocation.From, data.ReadFileData().EditLocation.To).ClickUpdateJourney();
        }

        [Then(@"New valid journey results are loaded")]
        public void ThenNewValidJourneyResultsAreLoaded()
        {
            journeyResults.ValidResultsVerification(data.ReadFileData().EditLocation.From, data.ReadFileData().EditLocation.To);
        }

        [When(@"Return to PlanAJourney Page")]
        public void WhenReturnToPlanAJourneyPage()
        {
            planAJourney=journeyResults.ReturnToPlanAJourneyPage();
            planAJourney.VerifyPlanAJourneyPage();
        }

        [Then(@"Verify Recent searches")]
        public void ThenVerifyRecentSearches()
        {
            planAJourney.VerifyRecentSearches(data.ReadFileData().ValidLocation.From, data.ReadFileData().ValidLocation.To);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (webDriverContext.driver != null)
            {
                webDriverContext.driver.Dispose();
                webDriverContext.driver = null;
            }
        }
    }
}
