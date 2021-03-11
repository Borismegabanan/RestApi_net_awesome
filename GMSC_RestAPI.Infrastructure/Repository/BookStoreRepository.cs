using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Interfaces;
using ServiceReference;
using System.Threading.Tasks;

namespace GMSC_RestAPI.Infrastructure.Repository
{
    public class BookStoreRepository : IBookStoreRepository
    {
        private readonly IBookStore _bookStore;
        private readonly IMapper _mapper;

        public BookStoreRepository(IMapper mapper, IBookStore bookStore)
        {
            _mapper = mapper;
            _bookStore = bookStore;
        }

        public async Task<BookDisplayServiceResponse> CreateBookAsync(CreateBookServiceRequest createBookRequest)
        {
            var result = await _bookStore.CreateBookAsync(_mapper.Map<CreateBookRequest1>(createBookRequest));
            return _mapper.Map<BookDisplayServiceResponse>(result);
        }

        public async Task<BookDisplayServiceResponse> RemoveBookAsync(RemoveBookServiceRequest removeBookRequest)
        {
            var result = await _bookStore.RemoveBookAsync(_mapper.Map<RemoveBookRequest>(removeBookRequest));
            return _mapper.Map<BookDisplayServiceResponse>(result);
        }

    }
}
