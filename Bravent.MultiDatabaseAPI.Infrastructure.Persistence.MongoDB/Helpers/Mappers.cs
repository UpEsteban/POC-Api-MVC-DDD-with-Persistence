using System;
using AutoMapper;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<DAO.Book, Domain.Shared.Models.Book>()
                .ForMember(destination => destination.PublicationDate, map => map.MapFrom(source => source.PublicationDate.ToDateTime(null)));
            CreateMap<Domain.Shared.Models.Book, DAO.Book>()
                .ForMember(destination => destination.TableName, map => map.MapFrom(source => DbCollectionCatalog.Book))
                .ForMember(destination => destination.PublicationDate, map => map.MapFrom(source => source.PublicationDate.ToUnixTimestamp()));

            CreateMap<DAO.Author, Domain.Shared.Models.Author>();
            CreateMap<Domain.Shared.Models.Author, DAO.Author>()
                .ForMember(destination => destination.TableName, map => map.MapFrom(source => DbCollectionCatalog.Author));
        }

    }
}
