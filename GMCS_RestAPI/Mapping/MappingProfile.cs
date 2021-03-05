using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestAPI.Contracts.Response;
using System;

namespace GMCS_RestAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDisplayModel>().ReverseMap();
            CreateMap<CreateAuthorRequest, CreateAuthorCommand>().ForMember(a => a.FullName, opt => opt.MapFrom(b => $"{b.Surname} {b.Name} {b.MiddleName}")).ReverseMap();
            CreateMap<Author, CreateAuthorCommand>().ReverseMap();
            CreateMap<AuthorDisplayModel, CreateAuthorRequest>().ReverseMap();

            CreateMap<ReadModelBook, BookDisplayModel>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(BookStates), c.BookStateId))).ReverseMap();
            CreateMap<Book, BookDisplayModel>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(BookStates), c.BookStateId))).ReverseMap();
            CreateMap<CreateBookCommand, Book>().BeforeMap((command, book) =>
                {
                    book.InitDate = DateTime.Now;
                    book.WhoChanged = Environment.UserName;
                    book.BookStateId = (int)BookStates.InStock;
                })
                .ReverseMap();

        }
    }
}
