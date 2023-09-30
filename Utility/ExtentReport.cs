using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter.Configuration;

namespace TransportForLondon.Utility
{
    [Binding]
    public class ExtentReport
    {
        public static ExtentReports _extentReport;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;
        private static readonly string base64ImageType = "base64";

        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        public static string testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestResultReports");

        /// <summary>
        /// Initiating Extent Report as HTML
        /// </summary>
        [BeforeTestRun]
        public static void ExtentReportInit()
        {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(testResultPath);
            htmlReporter.Config.ReportName = "Automation status report for Plan a Journey feature";
            htmlReporter.Config.DocumentTitle = "Automation status report";
            htmlReporter.Config.Theme = Theme.Standard;

            _extentReport = new ExtentReports();
            _extentReport.AttachReporter(htmlReporter);
            _extentReport.AddSystemInfo("NAME", "TFL Project");
            _extentReport.AddSystemInfo("Application", "Transport For London");
            _extentReport.AddSystemInfo("Browser", "Chrome");
            _extentReport.AddSystemInfo("OS", "Windows");
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            _extentReport.Flush();
        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }
        [BeforeScenario]
        public void InitializeScenario(ScenarioContext scenarioContext)
        {
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }
        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepInfo = scenarioContext.StepContext.StepInfo.Text;

            string resultOfImplementation = scenarioContext.ScenarioExecutionStatus.ToString();

            if (scenarioContext.TestError == null && resultOfImplementation == "OK")
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(stepInfo);
                else if (stepType == "When")
                    _scenario.CreateNode<When>(stepInfo);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(stepInfo);
            }
            else if (scenarioContext.TestError != null)
            {
                Exception? innerException = scenarioContext.TestError.InnerException;
                string? testError = scenarioContext.TestError.Message;

                if (stepType == "Given")
                    _scenario.CreateNode<Given>(stepInfo).Fail(innerException, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "When")
                    _scenario.CreateNode<When>(stepInfo).Fail(innerException, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
            }
        }
    }
}
