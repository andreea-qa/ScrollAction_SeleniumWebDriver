using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace ScrollAction_SeleniumWebDriver
{
    public class ECommerceTests
    {
        private static IWebDriver driver;
        private static readonly string gridURL = "@hub.lambdatest.com/wd/hub";
        private static readonly string LT_USERNAME = Environment.GetEnvironmentVariable("LT_USERNAME");
        private static readonly string LT_ACCESS_KEY = Environment.GetEnvironmentVariable("LT_ACCESS_KEY");
        private static readonly string testUrl = "https://ecommerce-playground.lambdatest.io/";

        [SetUp]
        public void Setup()
        {
            ChromeOptions capabilities = new ChromeOptions();
            capabilities.BrowserVersion = "126";
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();
            ltOptions.Add("username", LT_USERNAME);
            ltOptions.Add("accessKey", LT_ACCESS_KEY);
            ltOptions.Add("platformName", "Windows 11");
            ltOptions.Add("project", "Selenium Scroll");
            ltOptions.Add("selenium_version", "4.22.0");
            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-c#");
            capabilities.AddAdditionalOption("LT:Options", ltOptions);
            driver = new RemoteWebDriver(new Uri($"https://{LT_USERNAME}:{LT_ACCESS_KEY}{gridURL}"), capabilities);
        }

        [Test]
        public void ScrollToTheBottomAndToTheTop()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.Navigate().GoToUrl(testUrl);
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            js.ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }

        [Test]
        public void ScrollByAmount()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.Navigate().GoToUrl(testUrl);
            js.ExecuteScript("window.scrollBy(0,500)");
        }

        [Test]
        public void ScrollToElement()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.Navigate().GoToUrl(testUrl);
            var button = driver.FindElement(By.XPath("//a[@class='btn btn-outline-primary btn-lg']"));
            js.ExecuteScript("arguments[0].scrollIntoView();", button);
        }

        [Test]
        public void ScrollHorizontally()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.Navigate().GoToUrl(testUrl);
            js.ExecuteScript("window.scrollBy(50,0)");
        }

        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(5000);
            driver.Quit();
        }
    }
}