using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinutoSeguros.Application.Interfaces.AppServices;
using MinutoSeguros.Application.ViewModels;

namespace MinutoSeguros.Mvc.Controllers {

    public class HomeController : Controller {

        private readonly IBlogAppService _blogAppService;

        public HomeController (IBlogAppService blogAppService) {
            _blogAppService = blogAppService;
        }

        public IActionResult Index () {

            // O ideal aqui é ter um middleware para tratamento de erros e não precisar deste try catch feio aqui
            // tem um exemplo disso no projeto Track (github.com/fernandogjose)
            // Estou fazendo com parâmetro so para ter uma regra de negócio no domain, o ideal é que esta parâmetro venha de um config
            try {
                BlogViewModel blog = _blogAppService.ListTop10 ("https://www.minutoseguros.com.br/blog/feed/");
                return View (blog);
            } catch {
                return View (null);
            }

        }
    }
}