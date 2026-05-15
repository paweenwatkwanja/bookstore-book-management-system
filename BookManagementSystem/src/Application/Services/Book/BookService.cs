using Data;
using Models;
using Repositories;
using MongoDB.Driver;
using Global.Exceptions;

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
        BookEntity? bookEntity = await _bookRepository.GetBookByIDAsync(objectId);
        if (bookEntity == null)
        {
            throw new NotFoundException($"Book with ID '{objectId}' not found.");
        }

        BookResponse bookResponse = new BookResponse()
        {
            ObjectId = bookEntity.ObjectId,
            Title = bookEntity.Title,
            Author = bookEntity.Author,
            ISBN = bookEntity.ISBN,
            Publisher = bookEntity.Publisher,
            PublicationDate = bookEntity.PublicationDate
        };
        return bookResponse;
    }

    public async Task<string?> CreateBookAsync(BookRequest bookRequest)
    {
        if (bookRequest == null)
        {
            throw new BadRequestException("Book request cannot be null.");
        }
        BookEntity bookEntity = new BookEntity()
        {
            Title = bookRequest.Title,
            Author = bookRequest.Author,
            ISBN = bookRequest.ISBN,
            Publisher = bookRequest.Publisher,
            PublicationDate = bookRequest.PublicationDate
        };
        return await _bookRepository.CreateBookAsync(bookEntity);
    }

    public async Task<BookResponse> UpdateBookAsync(string objectId, BookRequest bookRequest)
    {
        if (bookRequest == null)
        {
            throw new BadRequestException("Book request cannot be null.");
        }

        BookEntity bookEntity = new BookEntity()
        {
            Title = bookRequest.Title,
            Author = bookRequest.Author,
            ISBN = bookRequest.ISBN,
            Publisher = bookRequest.Publisher,
            PublicationDate = bookRequest.PublicationDate
        };

        BookEntity? updatedBook = await _bookRepository.UpdateBookAsync(objectId, bookEntity);
        if (updatedBook == null)
        {
            throw new NotFoundException($"Book with ID '{objectId}' not found.");
        }

        BookResponse bookResponse = new BookResponse()
        {
            ObjectId = updatedBook.ObjectId,
            Title = updatedBook.Title,
            Author = updatedBook.Author,
            ISBN = updatedBook.ISBN,
            Publisher = updatedBook.Publisher,
            PublicationDate = updatedBook.PublicationDate
        };
        return bookResponse;
    }

    public async Task DeleteBookAsync(string objectId)
    {
        DeleteResult deleteResult = await _bookRepository.DeleteBookAsync(objectId);
        if (deleteResult.DeletedCount == 0)
        {
            throw new NotFoundException($"Book with ID '{objectId}' not found.");
        }
    }
}