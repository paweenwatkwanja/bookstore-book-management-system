using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Global.Exceptions;

namespace BookManagementSystem.Test;

[TestClass]
public sealed class BookServiceTest : MongoDbTestBase
{
    private static BookRequest CreateDefaultBookRequest(string? title = null, string? author = null) =>
        new BookRequest
        {
            Title = title ?? "BookTitle",
            Author = author ?? "BookAuthor",
            ISBN = "BookISBN",
            Publisher = "BookPublisher",
            PublicationDate = new DateOnly(2026, 1, 1)
        };

    private static BookRequest CreateUpdateBookRequest() =>
        new BookRequest()
        {
            Title = "BookTitle1",
            Author = "BookAuthor1",
            ISBN = "BookISBN1",
            Publisher = "BookPublisher1",
            PublicationDate = new DateOnly(2026, 1, 12)
        };

    [TestMethod]
    public async Task TestCreateBookAsyncCaseCreated()
    {
        string? objectId = await Controller.CreateBookAsync(CreateDefaultBookRequest());

        Assert.IsFalse(string.IsNullOrEmpty(objectId));
    }

    [TestMethod]
    public async Task TestCreateBookAsyncCaseCreationFailed()
    {
        BookRequest bookRequest = null!;
        BadRequestException exception = null!;

        try
        {
            await Controller.CreateBookAsync(bookRequest);
        }
        catch (BadRequestException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(400, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestGetBookByIdAsyncCaseFound()
    {
        string? objectId = await Controller.CreateBookAsync(CreateDefaultBookRequest());
        Assert.IsFalse(string.IsNullOrEmpty(objectId));

        BookResponse actual = await Controller.GetBookByIdAsync(objectId!);

        Assert.AreEqual("BookTitle", actual.Title);
        Assert.AreEqual("BookAuthor", actual.Author);
        Assert.AreEqual("BookISBN", actual.ISBN);
        Assert.AreEqual("BookPublisher", actual.Publisher);
        Assert.AreEqual(new DateOnly(2026, 1, 1), actual.PublicationDate);
    }

    [TestMethod]
    public async Task TestGetBookByIdAsyncCaseNotFound()
    {
        NotFoundException exception = null!;

        try
        {
            await Controller.GetBookByIdAsync("56fc40f9d735c28df206d078");
        }
        catch (NotFoundException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(404, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestGetBookByIdAsyncCaseBadRequestIncorrectObjectId()
    {
        BadRequestException exception = null!;

        try
        {
            await Controller.GetBookByIdAsync("aaaaaa28df206daaa");
        }
        catch (BadRequestException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(400, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestGetBooksAsyncCaseFound()
    {
        List<BookRequest> bookRequests = new List<BookRequest>()
        {
            CreateDefaultBookRequest("BookTitle1", "BookAuthor1"),
            CreateDefaultBookRequest("BookTitle2", "BookAuthor2")
        };

        foreach (BookRequest bookRequest in bookRequests)
        {
            string? objectId = await Controller.CreateBookAsync(bookRequest);
            Assert.IsFalse(string.IsNullOrEmpty(objectId));
        }

        List<BookResponse> bookResponses = await Controller.GetBooksAsync();

        Assert.HasCount(2, bookResponses);
    }

    [TestMethod]
    public async Task TestGetBooksAsyncCaseNotFound()
    {
        List<BookResponse> bookResponses = await Controller.GetBooksAsync();

        Assert.IsEmpty(bookResponses);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseOK()
    {
        string? objectId = await Controller.CreateBookAsync(CreateDefaultBookRequest());
        Assert.IsFalse(string.IsNullOrEmpty(objectId));

        BookResponse bookResponse = await Controller.UpdateBookAsync(objectId!, CreateUpdateBookRequest());

        Assert.AreEqual("BookTitle1", bookResponse.Title);
        Assert.AreEqual("BookAuthor1", bookResponse.Author);
        Assert.AreEqual("BookISBN1", bookResponse.ISBN);
        Assert.AreEqual("BookPublisher1", bookResponse.Publisher);
        Assert.AreEqual(new DateOnly(2026, 1, 12), bookResponse.PublicationDate);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseNotFound()
    {
        NotFoundException exception = null!;

        try
        {
            await Controller.UpdateBookAsync("56fc40f9d735c28df206d078", CreateUpdateBookRequest());
        }
        catch (NotFoundException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(404, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseBadRequestRequestNull()
    {
        BookRequest bookUpdateRequest = null!;
        BadRequestException exception = null!;

        try
        {
            await Controller.UpdateBookAsync("56fc40f9d735c28df206d078", bookUpdateRequest);
        }
        catch (BadRequestException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(400, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseBadRequestIncorrectObjectId()
    {
        BadRequestException exception = null!;

        try
        {
            await Controller.UpdateBookAsync("aaaaaa28df206daaa", CreateUpdateBookRequest());
        }
        catch (BadRequestException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(400, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestDeleteBookAsyncCaseOk()
    {
        string? objectId = await Controller.CreateBookAsync(CreateDefaultBookRequest());
        Assert.IsFalse(string.IsNullOrEmpty(objectId));

        await Controller.DeleteBookAsync(objectId!);

        NotFoundException exception = null!;

        try
        {
            await Controller.GetBookByIdAsync(objectId);
        }
        catch (NotFoundException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(404, exception.StatusCode);
    }

    [TestMethod]
    public async Task TestDeleteBookAsyncCaseBadRequestIncorrectObjectId()
    {
        BadRequestException exception = null!;

        try
        {
            await Controller.DeleteBookAsync("aaaaaa28df206daaa");
        }
        catch (BadRequestException ex)
        {
            exception = ex;
        }

        Assert.IsNotNull(exception);
        Assert.AreEqual(400, exception.StatusCode);
    }
}
