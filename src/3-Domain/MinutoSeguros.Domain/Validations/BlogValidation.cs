namespace MinutoSeguros.Domain.Validations {
    
    public class BlogValidation {
    
        public void ValidateXmlRequest (string value) {
            if (string.IsNullOrEmpty (value)) {
                // TODO:: Criar um erro customizado
                throw new System.Exception ("Url do xml é obrigatória!");
            }
        }
    }
}