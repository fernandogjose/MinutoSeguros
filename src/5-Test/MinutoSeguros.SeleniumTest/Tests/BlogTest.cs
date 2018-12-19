using System;
using System.IO;
using MinutoSeguros.SeleniumTest.Screens;
using MinutoSeguros.SeleniumTest.Utils;
using Xunit;

namespace MinutoSeguros.SeleniumTest.Tests {

    // A aplicação precisa estar de pé para funcionar
    // O appsettings.json e o Driver (exe) precisa estar na raiz que o site esta rodando no caso de debug no bin/debug/netcoreapp2.2    

    public class BlogTest : BaseTest {

        [Theory]
        [InlineData (Browser.Chrome)]
        public void DeveCarregarATelaComSucesso (Browser browser) {
            BlogScreen screen = new BlogScreen (_configuration, browser);
            screen.LoadScreen ();
            string title = screen.GetSuccess ();
            string fileName = DateTime.Now.ToString("ddMMyyyyhhmm") + ".png";
            screen.TakeScreenshot($@"{Directory.GetCurrentDirectory()}\Imgs\Tests\", fileName);
            screen.CloseScreen ();

            Assert.True (!string.IsNullOrEmpty (title));
        }
        
        // Incluir outros testes
    }
}