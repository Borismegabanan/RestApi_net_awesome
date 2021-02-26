using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;
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
        
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(string name = null)
        {
            if (name == null)
            {
                return await _applicationContext.Authors.ToListAsync();
            }

            return await _applicationContext.Authors.Where(x => x.FullName.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<Author> GetTheAuthorAsync(int id)
        {
            return await _applicationContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsAuthorExistAsync(AuthorQuery query)
        {
            return await _applicationContext.Authors.Where(x => x.Id == query.Id).AnyAsync();
        }
    }
}
