using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TransportForLondon.ApplicationPages
{
    public class JourneyResultsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public JourneyResultsPage(IWebDriver _driver, WebDriverWait _wait)
        {
            driver = _driver;
            wait = _wait;
        }
        By RecommendedResults = By.XPath("(//div[@class='disambiguation-box']/div/span)[1]");
        By JourneyResultTitle = By.XPath("//span[@class='jp-results-headline']");
        By CyclingAndOtherOptionsTitle = By.XPath("//div[@class='r']/div[contains(@class,'extra-journey-options')]/h2");
        By LeastWalkingTabTitle = By.XPath("//a[@data-tracking='accordion_expansion_details' and @title='Least walking']");
        By ResultsErrorScreen = By.XPath("//ul[@class='field-validation-errors']/li");
        By EditJourneyButton = By.XPath("//div[@id='plan-a-journey']/div[1]/div[3]/a/span");
        By EditJourneyFromLocation = By.XPath("//input[@id='InputFrom']");
        By EditJourneyToLocation = By.XPath("//input[@id='InputTo']");
        By UpdateJourneyButton = By.XPath("//input[@id='plan-journey-button']");
        By PlanAJourneyButton = By.XPath("//ol[@aria-labelledby='breadcrumb-label']/li[2]/a");
        By FromSuggestionsFlag = By.XPath("//input[@id='InputFrom' and @size='5']");
        By ToSuggestionsFlag = By.XPath("//input[@id='InputTo' and @size='5']");
        By SuggestionExactValue = By.XPath("//span[@id='places-extra-search-suggestion-0']");

        /// <summary>
        /// Journey results page verification for valid input locations
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public JourneyResultsPage ValidResultsVerification(string From, string To)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(JourneyResultTitle));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(CyclingAndOtherOptionsTitle));
                wait.Until(ExpectedConditions.ElementIsVisible(LeastWalkingTabTitle));
            }
            catch (Exception)
            {
                Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(RecommendedResults)).Text.Contains(To) || wait.Until(ExpectedConditions.ElementIsVisible(RecommendedResults)).Text.Contains(From));
            }
            return this;
        }
        /// <summary>
        /// Journey results page verification for invalid input locations
        /// </summary>
        /// <returns></returns>
        public JourneyResultsPage InvalidResultsVerification()
        {
            Assert.AreEqual(wait.Until(ExpectedConditions.ElementIsVisible(ResultsErrorScreen)).Text, "Sorry, we can't find a journey matching your criteria", "Invalid journey results");
            return this;
        }
        /// <summary>
        /// Click action on Edit Journey text
        /// </summary>
        /// <returns></returns>
        public JourneyResultsPage ClickEditJourney()
        {
            driver.FindElement(EditJourneyButton).Click();
            return this;
        }
        /// <summary>
        /// Click action on Update Journey button
        /// </summary>
        /// <returns></returns>
        public JourneyResultsPage ClickUpdateJourney()
        {
            driver.FindElement(UpdateJourneyButton).Click();
            return this;
        }
        /// <summary>
        /// Verifying Edit Journey screen
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public JourneyResultsPage VerifyEditJourneyScreen(string From, string To)
        {
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(EditJourneyFromLocation)).Displayed.Equals(true), "The From field is absent");
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(EditJourneyFromLocation)).GetAttribute("value").Contains(From), "Invalid journey results");
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(EditJourneyToLocation)).Displayed.Equals(true), "The To field is absent");
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(EditJourneyToLocation)).GetAttribute("value").Contains(To), "Invalid journey results");
            return this;
        }
        /// <summary>
        /// Entering new locations on Edit screen
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public JourneyResultsPage EnterNewLocationsOnEditScreen(string From, string To)
        {
            driver.FindElement(EditJourneyFromLocation).Click();
            driver.FindElement(EditJourneyFromLocation).SendKeys(Keys.Control + "a");
            driver.FindElement(EditJourneyFromLocation).SendKeys(From);
            wait.Until(ExpectedConditions.ElementExists(FromSuggestionsFlag));
            wait.Until(ExpectedConditions.ElementIsVisible(SuggestionExactValue)).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(EditJourneyToLocation).Click();
            driver.FindElement(EditJourneyToLocation).SendKeys(Keys.Control + "a");
            driver.FindElement(EditJourneyToLocation).SendKeys(To);
            wait.Until(ExpectedConditions.ElementExists(ToSuggestionsFlag));
            wait.Until(ExpectedConditions.ElementIsVisible(SuggestionExactValue)).Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            return this;
        }
        /// <summary>
        /// Returning to Plan A Journey Page
        /// </summary>
        /// <returns></returns>
        public PlanAJourneyPage ReturnToPlanAJourneyPage()
        {
            driver.FindElement(PlanAJourneyButton).Click();
            return new PlanAJourneyPage(driver, wait);
        }
    }
}
