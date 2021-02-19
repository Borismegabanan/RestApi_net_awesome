using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Providers
{
    public interface IAuthorsProvider
    {
        Task<IEnumerable<Author>> GetAllAuthors(string name = null);
    }
}
