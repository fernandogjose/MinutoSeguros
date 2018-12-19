using System.Linq;
using System.Xml.Linq;

namespace MinutoSeguros.Domain.Validations {

    public class BlogValidation {

        public void ValidateXmlRequest (string value) {
            if (string.IsNullOrEmpty (value)) {
                // TODO:: Criar um erro customizado
                throw new System.Exception ("Url do xml é obrigatória!");
            }
        }

        public void ValidateXmlResponse (XElement value) {
            if (value == null || !value.Elements ("channel").Elements ("title").Any ()) {
                // TODO:: Criar um erro customizado para tratar que o retorno esta inválido
                throw new System.Exception ("Xml de retorno inválido!");
            }
        }
    }
}