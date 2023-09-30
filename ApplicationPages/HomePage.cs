using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TransportForLondon.ApplicationPages
{
    public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public HomePage(IWebDriver _driver, WebDriverWait _wait)
        {
            driver = _driver;
            wait = _wait;
        }

        By CookiesAcceptButton = By.XPath("//*[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']");
        By PlanAJourneyMenuItem = By.XPath("(//li[@class='plan-journey']/a)[1]");

        /// <summary>
        /// Method to accept cookies
        /// </summary>
        /// <returns></returns>
        public HomePage AcceptCookies()
        {
            try
            {
                Assert.IsTrue(wait.Until(d => d.FindElements(CookiesAcceptButton).Count > 0));
                IList<IWebElement> CookiesButton = driver.FindElements(CookiesAcceptButton);
                if (CookiesButton.Count > 0)
                {
                    foreach (IWebElement Acceptcookie in CookiesButton)
                    {
                        Acceptcookie.Click();
                        break;
                    }
                }
            }
            catch (Exception) { };
            return this;
        }
        /// <summary>
        /// Click action on Plan A Journey Menu item
        /// </summary>
        /// <returns></returns>
        public PlanAJourneyPage PlanAJourneyMenuClick()
        {
            driver.FindElement(PlanAJourneyMenuItem).Click();
            return new PlanAJourneyPage(driver, wait);
        }
    }
}
