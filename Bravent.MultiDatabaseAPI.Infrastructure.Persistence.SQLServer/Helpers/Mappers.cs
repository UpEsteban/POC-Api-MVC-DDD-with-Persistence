using System;
using AutoMapper;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DAO.Book, Domain.Shared.Models.Book>()
                .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id.Value.ToString()));
            CreateMap<Domain.Shared.Models.Book, DAO.Book>()
                .ForMember(destination => destination.Id, map => {
                    map.PreCondition(source => !string.IsNullOrEmpty(source.Id));
                    map.MapFrom(source => new Guid(source.Id));
                }
            );

            CreateMap<DAO.Author, Domain.Shared.Models.Author>()
                .ForMember(destination => destination.Id, map => map.MapFrom(source => source.Id.Value.ToString()));
            CreateMap<Domain.Shared.Models.Author, DAO.Author>()
                .ForMember(destination => destination.Id, map => {
                    map.PreCondition(source => !string.IsNullOrEmpty(source.Id));
                    map.MapFrom(source => new Guid(source.Id));
                }
            );
        }

    }
}
