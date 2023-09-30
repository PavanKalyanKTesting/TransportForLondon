using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TransportForLondon.ApplicationPages
{
    public class PlanAJourneyPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public PlanAJourneyPage(IWebDriver _driver, WebDriverWait _wait)
        {
            driver = _driver;
            wait = _wait;
        }

        By StatusUpdatesMenuItem = By.XPath("//*[@id='mainnav']/div[2]/div/div[2]/ul/li[2]/a");
        By PlanAJourneyPageTitle = By.XPath("//*[@id='jp-hero']/div[2]/div/h1/span");
        By FromLocation = By.XPath("//input[@id='InputFrom']");
        By ToLocation = By.XPath("//input[@id='InputTo']");
        By FromSuggestionsFlag = By.XPath("//input[@id='InputFrom' and @size='6']");
        By ToSuggestionsFlag = By.XPath("//input[@id='InputTo' and @size='5']");
        By SuggestionExactValue = By.XPath("//span[@id='places-extra-search-suggestion-1']");
        By PlanAJourneyButton = By.XPath("//input[@id='plan-journey-button']");
        By FromError = By.XPath("//span[@id='InputFrom-error']");
        By ToError = By.XPath("//span[@id='InputTo-error']");
        By RecentSearches = By.XPath("//div[@id='jp-recent-content-jp-']/a");

        /// <summary>
        /// Verifying Plan A Journey page
        /// </summary>
        /// <returns></returns>
        public PlanAJourneyPage VerifyPlanAJourneyPage()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(StatusUpdatesMenuItem)); //menu tool bar on PlanAJourney is verified
            }
            catch (Exception)
            {
                driver.Navigate().Refresh();
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
                wait.Until(ExpectedConditions.ElementIsVisible(StatusUpdatesMenuItem)); //menu tool bar on PlanAJourney is verified
            }
            wait.Until(ExpectedConditions.ElementIsVisible(PlanAJourneyPageTitle)); //PlanAJourneyTitle
            return this;
        }
        /// <summary>
        /// Entering valid locations on Plan a Journey page
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public PlanAJourneyPage EnterValidLocations(string From, string To)
        {
            driver.FindElement(FromLocation).SendKeys(From);
            wait.Until(ExpectedConditions.ElementExists(FromSuggestionsFlag));
            wait.Until(ExpectedConditions.ElementIsVisible(SuggestionExactValue)).Click();
            driver.FindElement(ToLocation).SendKeys(To);
            wait.Until(ExpectedConditions.ElementExists(ToSuggestionsFlag));
            wait.Until(ExpectedConditions.ElementIsVisible(SuggestionExactValue)).Click();
            return this;
        }
        /// <summary>
        /// Entering invalid locations on Plan a Journey page
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public PlanAJourneyPage EnterInvalidLocations(string From, string To)
        {
            driver.FindElement(FromLocation).SendKeys(From);
            driver.FindElement(ToLocation).SendKeys(To);
            return this;
        }
        /// <summary>
        /// Click action on Plan A Journey button
        /// </summary>
        /// <returns></returns>
        public JourneyResultsPage ClickPlanAJourneyButton()
        {
            driver.FindElement(PlanAJourneyButton).Click();
            return new JourneyResultsPage(driver, wait);
        }
        /// <summary>
        /// Click action on Plan A Journey for Empty location fields
        /// </summary>
        /// <returns></returns>
        public PlanAJourneyPage ClickPlanAJourneyButtonForEmptyFields()
        {
            driver.FindElement(PlanAJourneyButton).Click();
            return this;
        }
        /// <summary>
        /// Validation of error messages when empty locations are given
        /// </summary>
        /// <returns></returns>
        public PlanAJourneyPage EmptyFieldsValidations()
        {
            Assert.AreEqual(wait.Until(ExpectedConditions.ElementIsVisible(FromError)).Text, "The From field is required.", "Invalid From error message");
            Assert.AreEqual(wait.Until(ExpectedConditions.ElementIsVisible(ToError)).Text, "The To field is required.", "Invalid To error message");
            return this;
        }
        /// <summary>
        /// Verifying recent search feature
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public PlanAJourneyPage VerifyRecentSearches(string From, string To)
        {
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(RecentSearches)).Text.Contains(To) || wait.Until(ExpectedConditions.ElementIsVisible(RecentSearches)).Text.Contains(From));
            return this;
        }
    }
}
