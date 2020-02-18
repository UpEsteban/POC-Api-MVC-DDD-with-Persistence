using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.Domain.Shared.Repositories;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Helpers;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Implementation;
using System.Linq.Expressions;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.MongoDB.Repositories
{
    public class BookRepository : BaseMongoRepository<DAO.Book, string>, IBookRepository
    {
        private readonly Persistence<DAO.Book, string> _persistence;
        private readonly Query<DAO.Book, string> _query;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public BookRepository(IPersistence<DAO.Book, string> persistence, IQuery<DAO.Book, string> query, IMapper mapper, IAuthorRepository authorRepository) : base(DbCollectionCatalog.Book)
        {
            _persistence = (Persistence<DAO.Book, string>)persistence;
            _query = (Query<DAO.Book, string>)query;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        private void InsertDAOValidations(DAO.Book book)
        {

        }

        private void UpdateDAOValidations(DAO.Book book)
        {

        }

        private void DeleteDAOValidations()
        {

        }

        public async Task<IEnumerable<Book>> FindByName(string name)
        {
            IEnumerable<Book> items = _mapper.Map<IEnumerable<DAO.Book>, IEnumerable<Book>>(await _query.FindAsync(item => item.TableName == GetTableName() && item.Name.Contains(name)));

            await items.ParallelForEachAsync(async item => {
                item.Author = await _authorRepository.Get(item.AuthorId);
            });

            return items;
        }

        public async Task<Book> FindById(string id)
        {
            Book item = _mapper.Map<DAO.Book, Book>(await _query.GetAsync(id));
            item.Author = await _authorRepository.Get(item.AuthorId);
            return item;
        }

        public async Task<Book> FindByISBN(string isbn)
        {
            Book item = _mapper.Map<DAO.Book, Book>(await _query.GetAsync(GetBaseFilter().Append(GetFilterBuilder().Eq("ISBN", isbn))));
            item.Author = await _authorRepository.Get(item.AuthorId);
            return item;
        }

        public async Task<Book> Insert(Book item)
        {
            DAO.Book dao = _mapper.Map<Book, DAO.Book>(item);
            dao.SetNewId();
            dao.Author = null;

            InsertDAOValidations(dao);

            await _persistence.BeginTransactionAsync();
            await _persistence.InsertAsync(dao);
            await _persistence.CommitAsync();

            return _mapper.Map<DAO.Book, Book>(dao);
        }

        public async Task<Book> Update(Book item)
        {
            DAO.Book dao = _mapper.Map<Book, DAO.Book>(item);

            UpdateDAOValidations(dao);

            await _persistence.BeginTransactionAsync();
            await _persistence.UpdateAsync(dao);
            await _persistence.CommitAsync();

            return _mapper.Map<DAO.Book, Book>(dao);
        }

        public async Task Delete(string id)
        {
            DAO.Book item = await _query.GetAsync(id);

            DeleteDAOValidations();

            await _persistence.BeginTransactionAsync();
            await _persistence.DeleteAsync(item);
            await _persistence.CommitAsync();
        }
    }
}
