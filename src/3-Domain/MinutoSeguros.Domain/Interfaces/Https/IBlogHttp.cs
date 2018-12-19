using System.Threading.Tasks;
using System.Xml.Linq;

namespace MinutoSeguros.Domain.Interfaces.Https
{
    public interface IBlogHttp
    {
         XElement LoadXml(string xml);
    }
}