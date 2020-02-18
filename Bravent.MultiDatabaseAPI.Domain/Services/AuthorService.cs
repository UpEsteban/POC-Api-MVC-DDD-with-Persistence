using AutoMapper;
using Bravent.MultiDatabaseAPI.Domain.Domains;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.ServiceContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.Domain.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorDomain _domain;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorDomain domain)
        {
            _domain = domain;
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<AuthorDTO, Author>();
                config.CreateMap<Author, AuthorDTO>();
            }).CreateMapper();
        }

        public async Task<AuthorDTO> Insert(AuthorDTO item)
        {
            return _mapper.Map<Author, AuthorDTO>(await _domain.Insert(_mapper.Map<AuthorDTO, Author>(item)));
        }
    }
}
