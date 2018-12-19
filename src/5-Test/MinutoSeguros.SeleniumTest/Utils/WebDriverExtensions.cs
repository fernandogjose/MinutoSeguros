using System;
using OpenQA.Selenium;

namespace MinutoSeguros.SeleniumTest.Utils
{
    public static class WebDriverExtensions
    {
        public static void LoadPage(this IWebDriver webDriver, TimeSpan timeToWait, string url)
        {
            webDriver.Manage().Timeouts().PageLoad = timeToWait;
            webDriver.Navigate().GoToUrl(url);
        }

        public static string GetHtml(this IWebDriver webDriver, By by)
        {
            IWebElement webElement = webDriver.FindElement(by);
            return webElement.GetAttribute("innerHTML");
        }
    }
}
