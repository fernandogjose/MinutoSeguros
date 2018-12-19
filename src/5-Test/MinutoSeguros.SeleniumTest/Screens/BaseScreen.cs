using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MinutoSeguros.SeleniumTest.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MinutoSeguros.SeleniumTest.Screens {
    public class BaseScreen {
        protected readonly IConfiguration _configuration;

        protected readonly Browser _browser;

        protected IWebDriver _driver;

        public BaseScreen (IConfiguration configuration, Browser browser) {
            _configuration = configuration;
            _browser = browser;

            string driver = Directory.GetCurrentDirectory ();
            _driver = WebDriverFactory.CreateWebDriver (browser, driver, false); // headless = false = abrir browser ---- headless = true = não abrir browser
        }

        public void CloseScreen () {
            _driver.Quit ();
            _driver = null;
        }

        public void TakeScreenshot (string filePath, string fileName) {
            if (!Directory.Exists (filePath)) {
                Directory.CreateDirectory (filePath);
            }

            ITakesScreenshot takesScreenshot = _driver as ITakesScreenshot;
            Screenshot screenshot = takesScreenshot.GetScreenshot ();
            screenshot.SaveAsFile (filePath + fileName, ScreenshotImageFormat.Png);
        }

        public void WaitByHtml (TimeSpan seconds, By by) {
            WebDriverWait wait = new WebDriverWait (_driver, seconds);
            wait.Until (x => !string.IsNullOrEmpty (x.FindElement (by).GetAttribute ("innerHTML")));
        }
    }
}