
using BookStore.Core.Model;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext context;

        public BookRepository(BookStoreDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Book>> Get()
        {
            var bookEntities = await context.Books
                .AsNoTracking()
                .ToListAsync();


            var books = bookEntities
                .Select(x => Book.Create(x.Id, x.Title, x.Description, x.Price).Book)
                .ToList();

            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
            };

            await context.Books.AddAsync(bookEntity);
            await context.SaveChangesAsync();


            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await context.Books
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Title, b => title)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Price, b => price));


            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await context.Books
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
