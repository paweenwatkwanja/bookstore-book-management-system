using Data;
using Models;
using Repositories;

namespace Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
         _bookRepository = bookRepository;
    }

    public async Task<List<BookResponse>> GetBooksAsync()
    {
        List<BookResponse> bookResponses = new List<BookResponse>();
        List<BookEntity>? bookEntities = await _bookRepository.GetBooksAsync();
        if (bookEntities != null)
        {
            foreach (BookEntity bookEntity in bookEntities)
            {
                BookResponse bookResponse = new BookResponse()
                {
                    ObjectId = bookEntity.ObjectId,
                    Title = bookEntity.Title,
                    Author = bookEntity.Author,
                    ISBN = bookEntity.ISBN,
                    Publisher = bookEntity.Publisher,
                    PublicationDate = bookEntity.PublicationDate
                };
                bookResponses.Add(bookResponse);
            }
        }
        return bookResponses;
    }

    public async Task<BookResponse> GetBookByIdAsync(string objectId)
    {
        BookResponse bookResponse = new BookResponse();
        BookEntity? bookEntity = await _bookRepository.GetBookByIDAsync(objectId);
        if (bookEntity != null)
        {
            bookResponse.ObjectId = bookEntity.ObjectId;
            bookResponse.Title = bookEntity.Title;
            bookResponse.Author = bookEntity.Author;
            bookResponse.ISBN = bookEntity.ISBN;
            bookResponse.Publisher = bookEntity.Publisher;
            bookResponse.PublicationDate = bookEntity.PublicationDate;
        };
        return bookResponse;
    }

    public async Task CreateBookAsync(BookRequest bookRequest)
    {
        // validate
        BookEntity bookEntity = new BookEntity()
        {
            Title = bookRequest.Title,
            Author = bookRequest.Author,
            ISBN = bookRequest.ISBN,
            Publisher = bookRequest.Publisher,
            PublicationDate = bookRequest.PublicationDate
        };
        await _bookRepository.CreateBookAsync(bookEntity);
    }

    public async Task UpdateBookAsync(string objectId, BookRequest bookRequest)
    {
        // validate
        // get
        BookEntity bookEntity = new BookEntity()
        {
            ObjectId = objectId,
            Title = bookRequest.Title,
            Author = bookRequest.Author,
            ISBN = bookRequest.ISBN,
            Publisher = bookRequest.Publisher,
            PublicationDate = bookRequest.PublicationDate
        };
        await _bookRepository.UpdateBookAsync(bookEntity);
    }

     public async Task DeleteBookAsync(string objectId)
    {
        // validate
        // get?
        BookEntity bookEntity = new BookEntity()
        {
            ObjectId = objectId
        };
        await _bookRepository.DeletBookAsync(bookEntity);
    }
}