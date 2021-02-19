
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Services
{
    public interface IAuthorsService
    {
        void Post(Author author);

        void Delete(Author author);

    }
}
