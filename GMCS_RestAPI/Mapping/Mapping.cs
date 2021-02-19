using System;
using AutoMapper;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestApi.Domain.Classes;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestAPI.Mapping
{
    public class Mapping :Profile
    {
        public Mapping()
        {
            CreateMap<Author, AuthorResponse>().ReverseMap();

            CreateMap<CBook, BookResponse>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(EBookState), c.BookStateId))).ReverseMap();

            CreateMap<Book, BookResponse>().ForMember(e => e.BookState, opt => opt.MapFrom(c => Enum.GetName(typeof(EBookState), c.BookStateId))).ReverseMap();

        }
    }
}
