using Microsoft.Extensions.Configuration;
using MinutoSeguros.SeleniumTest.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MinutoSeguros.SeleniumTest.Screens {

    public class BlogScreen : BaseScreen {

        public BlogScreen (IConfiguration configuration, Browser browser) : base (configuration, browser) { }

        public void LoadScreen () {
            _driver.LoadPage (TimeSpan.FromSeconds (15), _configuration.GetSection ("Selenium:Urls:Blog").Value);
            _driver.Manage ().Window.Maximize ();
        }

        public string GetSuccess () {
            WaitByHtml (TimeSpan.FromSeconds (10), By.Id ("title"));
            string title = _driver.GetHtml(By.Id("title"));
            return title;
        }
    }
}