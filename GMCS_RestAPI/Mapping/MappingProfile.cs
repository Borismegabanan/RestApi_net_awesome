using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestAPI.Contracts.Response;
using ServiceReference;
using System;
using Book = GMCS_RestApi.Domain.Models.Book;
using BookDisplayModel = GMCS_RestAPI.Contracts.Response.BookDisplayModel;
using CreateBookRequest = GMCS_RestAPI.Contracts.Request.CreateBookRequest;

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
            CreateMap<CreateBookCommand, CreateBookRequest>().ReverseMap();
            CreateMap<CreateBookCommand, Book>().BeforeMap((command, book) =>
                {
                    book.InitDate = DateTime.Now;
                    book.WhoChanged = Environment.UserName;
                    book.BookStateId = (int)BookStates.InStock;
                })
                .ReverseMap();

            CreateMap<CreateBookRequest, ServiceReference.CreateBookRequest>().ReverseMap();
            CreateMap<CreateBookRequest, CreateBookRequest1>().ForMember(e => e.newBook, opt => opt.MapFrom(c => c)).ReverseMap();
            CreateMap<BookDisplayModel, ServiceReference.BookDisplayModel>().ReverseMap();
            CreateMap<BookDisplayModel, CreateBookResponse>()
                .ForMember(e => e.CreateBookResult, opt => opt.MapFrom(c => c)).ReverseMap();

            CreateMap<BookQuery, ServiceReference.RemoveBookRequest>().ReverseMap();
            CreateMap<BookDisplayModel, ServiceReference.RemoveBookResponse>()
                .ForMember(e => e.RemoveBookResult, opt => opt.MapFrom(c => c)).ReverseMap();
            CreateMap<Book, ServiceReference.Book>().ReverseMap();
            CreateMap<ServiceReference.Book, BookDisplayModel>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(BookStates), c.BookStateId))).ReverseMap();
        }
    }
}
