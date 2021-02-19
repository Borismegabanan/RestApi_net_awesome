using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestApi.Domain.Providers
{
    public class AuthorsProvider : IAuthorsProvider
    {
        private readonly ApplicationContext _applicationContext;

        public AuthorsProvider(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        
        public async Task<IEnumerable<Author>> GetAllAuthors(string name = null)
        {
            if (name == null)
            {
                return await _applicationContext.Authors.ToListAsync();
            }

            return await _applicationContext.Authors.Where(x => x.FullName.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
