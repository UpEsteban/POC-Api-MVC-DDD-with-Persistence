using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bravent.MultiDatabaseAPI.Domain.Shared.Models;
using Bravent.MultiDatabaseAPI.Domain.Shared.Repositories;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;
using System.Linq.Expressions;
using Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Implementation;

namespace Bravent.MultiDatabaseAPI.Infrastructure.Persistence.SQLServer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly Persistence<DAO.Book, Guid?> _persistence;
        private readonly Query<DAO.Book, Guid?> _query;
        private readonly IMapper _mapper;

        public BookRepository(IPersistence<DAO.Book, Guid?> persistence, IQuery<DAO.Book, Guid?> query, IMapper mapper)
        {
            _persistence = (Persistence<DAO.Book, Guid?>)persistence;
            _query = (Query<DAO.Book, Guid?>)query;
            _mapper = mapper;
        }

        private void InsertDAOValidations(DAO.Book item)
        {

        }

        private void UpdateDAOValidations(DAO.Book item)
        {

        }

        private void DeleteDAOValidations()
        {

        }

        public async Task<Book> FindById(string id)
        {
            return _mapper.Map<DAO.Book, Book>(await _query.GetAsync(new Guid(id)));
        }

        public async Task<IEnumerable<Book>> FindByName(string name)
        {
            Expression<Func<DAO.Book, bool>> expr = a => a.Name.Contains(name);

            return _mapper.Map<IEnumerable<DAO.Book>, IEnumerable<Book>>(await _query.GetDbSet().Where(expr).Include(book => book.Author).ToListAsync());
        }

        public async Task<Book> FindByISBN(string isbn)
        {
            Expression<Func<DAO.Book, bool>> expr = a => a.ISBN == isbn;

            return _mapper.Map<DAO.Book, Book>(await _query.GetAsync(expr));
        }

        public async Task<Book> Insert(Book item)
        {
            DAO.Book dao = _mapper.Map<Book, DAO.Book>(item);
            dao.Author = null;
            dao.SetNewId();

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
            DAO.Book item = await _query.GetAsync(new Guid(id));

            DeleteDAOValidations();

            await _persistence.BeginTransactionAsync();
            await _persistence.DeleteAsync(item);
            await _persistence.CommitAsync();
        }
    }
}
