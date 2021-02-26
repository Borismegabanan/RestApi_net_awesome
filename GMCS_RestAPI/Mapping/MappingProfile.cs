using System;
using AutoMapper;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestAPI.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorModel>().ReverseMap();

            CreateMap<ReadModelBook, BookModel>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(BookStates), c.BookStateId))).ReverseMap();

            CreateMap<Book, BookModel>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(BookStates), c.BookStateId))).ReverseMap();

            CreateMap<AuthorRequest, CreateAuthorCommand>().ForMember(a => a.FullName, opt => opt.MapFrom(b => $"{b.Surname} {b.Name} {b.MiddleName}" )).ReverseMap();

            CreateMap<Author, CreateAuthorCommand>().ReverseMap();
        }
    }
}
