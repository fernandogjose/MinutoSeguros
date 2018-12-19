using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using MinutoSeguros.Domain.Interfaces.Https;

namespace MinutoSeguros.Infra.Http.Https {

    public class BlogHttp : IBlogHttp {

        public XElement LoadXml (string xml) {

            try {
                XElement response = XElement.Load (xml);
                return response;
            } catch {
                // TODO:: Gravar o erro real no mongo

                // TODO:: Criar exceptions próprias para tratar o erro

                // Volta null, depois que tiver os tratamentos de erro não volta mais null
                return null;
            }

        }
    }
}