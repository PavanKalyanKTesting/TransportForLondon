using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TransportForLondon.Context
{
    public class WebDriverContext
    {
        public ChromeDriver driver;
        public WebDriverWait wait;

        /// <summary>
        /// Constructor to initiate chrome browser and WebDriverWait
        /// </summary>
        public WebDriverContext()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }
    }
}
