using System.Threading.Tasks;
using System.Xml.Linq;
using MinutoSeguros.Application.Interfaces.AppServices;
using MinutoSeguros.Application.Mappers;
using MinutoSeguros.Application.ViewModels;
using MinutoSeguros.Domain.Interfaces.DomainServices;

namespace MinutoSeguros.Application.AppServices {

    public class BlogAppService : IBlogAppService {

        private readonly IBlogService _blogService;
        
        private readonly BlogMapper _blogMapper;

        public BlogAppService (IBlogService blogService, BlogMapper blogMapper) {
            _blogService = blogService;
            _blogMapper=blogMapper;
        }

        public BlogViewModel ListTop10 (string xmlRequest) {
            XElement xmlResponse = _blogService.LoadXml (xmlRequest);
            BlogViewModel blog = _blogMapper.XmlElementToViewModel(xmlResponse);
            return blog;
        }
    }
}