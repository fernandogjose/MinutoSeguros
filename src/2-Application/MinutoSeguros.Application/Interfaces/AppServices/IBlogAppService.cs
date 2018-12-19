using MinutoSeguros.Application.ViewModels;
using System.Threading.Tasks;

namespace MinutoSeguros.Application.Interfaces.AppServices
{
    public interface IBlogAppService
    {
        BlogViewModel ListTop10(string xml);
    }
}
