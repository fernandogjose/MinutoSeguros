using System.Threading.Tasks;
using System.Xml.Linq;

namespace MinutoSeguros.Domain.Interfaces.DomainServices {

    public interface IBlogService {

        XElement LoadXml (string xmlRequest);
    }
}