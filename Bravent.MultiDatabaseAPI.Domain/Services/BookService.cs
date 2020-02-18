using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bravent.MultiDatabaseAPI.Domain.Domains;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.ServiceContracts.Models;

namespace Bravent.MultiDatabaseAPI.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookDomain _domain;

        public BookService(IBookDomain domain)
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<BookDTO, Book>()
                    .ForMember(destination => destination.PublicationDate, map => map.MapFrom(source => source.PublicationDate.ToDateTime(null)))
                    .ForMember(destination => destination.AuthorId, map => map.MapFrom(source => source.Author.Id));
                config.CreateMap<Book, BookDTO>()
                    .ForMember(destination => destination.PublicationDate, map => map.MapFrom(source => source.PublicationDate.ToUnixTimestamp()));
                config.CreateMap<AuthorDTO, Author>();
                config.CreateMap<Author, AuthorDTO>();
            }).CreateMapper();

            _domain = domain;
        }

        public async Task<BookDTO> FindById(string id)
        {
            return _mapper.Map<Book, BookDTO> (await _domain.FindById(id));
        }

        public async Task<BookDTO> FindByISBN(string isbn)
        {
            return _mapper.Map<Book, BookDTO>(await _domain.FindByISBN(isbn));
        }

        public async Task<IEnumerable<BookDTO>> FindByName(String name)
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(await _domain.FindByName(name));
        }

        public async Task<BookDTO> Insert(BookDTO item)
        {
            return _mapper.Map<Book, BookDTO>(await _domain.InsertBook(_mapper.Map<BookDTO, Book>(item)));
        }
    }
}
