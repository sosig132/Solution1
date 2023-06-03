﻿using Back_End.Models.Books;
using Back_End.Repositories.GenericRepository;

namespace Back_End.Repositories.BookRepository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<Book> GetAllWithInclude();
        Book GetByIdWithJoin(Guid id);
        Book GetByTitle(string title);
    }
}
