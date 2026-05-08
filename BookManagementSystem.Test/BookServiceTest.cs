using Microsoft.AspNetCore.Mvc;
using Models;

namespace BookManagementSystem.Test;

[TestClass]
public sealed class BookServiceTest : MongoDbTestBase
{
    [TestMethod]
    public async Task TestCreateBookAsyncCaseCreated()
    {
        BookRequest bookRequest = new BookRequest
        {
            Title = "BookTitle",
            Author = "BookAuthor",
            ISBN = "BookISBN",
            Publisher = "BookPublisher",
            PublicationDate = new DateOnly(2026, 1, 1)
        };

        IActionResult actionResult = await Controller.CreateBookAsync(bookRequest);

        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        OkObjectResult okObject = (OkObjectResult)actionResult;
        Assert.AreEqual(200, okObject.StatusCode);

        string objectId = okObject.Value?.ToString() ?? string.Empty;
        Assert.IsFalse(string.IsNullOrEmpty(objectId));
    }

    [TestMethod]
    public async Task TestCreateBookAsyncCaseCreationFailed()
    {
        BookRequest bookRequest = null!;

        IActionResult actionResult = await Controller.CreateBookAsync(bookRequest);

        Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        BadRequestObjectResult badRequestObject = (BadRequestObjectResult)actionResult;
        Assert.AreEqual(400, badRequestObject.StatusCode);
    }

    [TestMethod]
    public async Task TestGetBookByIdAsyncCaseFound()
    {
        BookRequest bookRequest = new BookRequest
        {
            Title = "BookTitle",
            Author = "BookAuthor",
            ISBN = "BookISBN",
            Publisher = "BookPublisher",
            PublicationDate = new DateOnly(2026, 1, 1)
        };

        IActionResult createActionResult = await Controller.CreateBookAsync(bookRequest);
        Assert.IsInstanceOfType(createActionResult, typeof(OkObjectResult));
        OkObjectResult createOkObject = (OkObjectResult)createActionResult;
        string objectId = createOkObject.Value?.ToString() ?? string.Empty;

        IActionResult getActionResult = await Controller.GetBookByIdAsync(objectId);

        Assert.IsInstanceOfType(getActionResult, typeof(OkObjectResult));
        OkObjectResult getOkObject = (OkObjectResult)getActionResult;
        Assert.AreEqual(200, getOkObject.StatusCode);

        Assert.IsInstanceOfType(getOkObject.Value, typeof(BookResponse));
        BookResponse actual = (BookResponse)getOkObject.Value;
        Assert.AreEqual("BookTitle", actual.Title);
        Assert.AreEqual("BookAuthor", actual.Author);
        Assert.AreEqual("BookISBN", actual.ISBN);
        Assert.AreEqual("BookPublisher", actual.Publisher);
        Assert.AreEqual(new DateOnly(2026, 1, 1), actual.PublicationDate);
    }

    [TestMethod]
    public async Task TestGetBookByIdAsyncCaseNotFound()
    {
        IActionResult actionResult = await Controller.GetBookByIdAsync("56fc40f9d735c28df206d078");

        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        NotFoundResult notFoundResult = (NotFoundResult)actionResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }

    [TestMethod]
    public async Task TestGetBooksAsyncCaseFound()
    {
        List<BookRequest> bookRequests = new List<BookRequest>()
        {
            new BookRequest()
            {
                 Title = "BookTitle1",
                Author = "BookAuthor1",
                ISBN = "BookISBN1",
                Publisher = "BookPublisher1",
                PublicationDate = new DateOnly(2026, 1, 1)
            },
            new BookRequest()
            {
                Title = "BookTitle2",
                Author = "BookAuthor2",
                ISBN = "BookISBN2",
                Publisher = "BookPublisher2",
                PublicationDate = new DateOnly(2026, 2, 2)
            }
        };

        foreach (BookRequest bookRequest in bookRequests)
        {
            IActionResult actionResult = await Controller.CreateBookAsync(bookRequest);
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
            OkObjectResult okObject = (OkObjectResult)actionResult;
            Assert.AreEqual(200, okObject.StatusCode);

            string objectId = okObject.Value?.ToString() ?? string.Empty;
            Assert.IsFalse(string.IsNullOrEmpty(objectId));
        }

        IActionResult getActionResult = await Controller.GetBooksAsync();

        Assert.IsInstanceOfType(getActionResult, typeof(OkObjectResult));
        OkObjectResult getOkObject = (OkObjectResult)getActionResult;
        Assert.AreEqual(200, getOkObject.StatusCode);

        List<BookResponse>? bookResponses = (List<BookResponse>?)getOkObject.Value;
        Assert.AreEqual(2, bookResponses?.Count);
    }

    [TestMethod]
    public async Task TestGetBooksAsyncCaseNotFound()
    {
        IActionResult actionResult = await Controller.GetBooksAsync();

        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        OkObjectResult okObject = (OkObjectResult)actionResult;
        Assert.AreEqual(200, okObject.StatusCode);

        List<BookResponse>? bookResponses = (List<BookResponse>?)okObject.Value;
        Assert.AreEqual(0, bookResponses?.Count);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseOK()
    {
        BookRequest bookRequest = new BookRequest()
        {
            Title = "BookTitle",
            Author = "BookAuthor",
            ISBN = "BookISBN",
            Publisher = "BookPublisher",
            PublicationDate = new DateOnly(2026, 1, 1)
        };

        IActionResult actionResult = await Controller.CreateBookAsync(bookRequest);
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        OkObjectResult okObject = (OkObjectResult)actionResult;
        Assert.AreEqual(200, okObject.StatusCode);
        string objectId = okObject.Value?.ToString() ?? string.Empty;
        Assert.IsFalse(string.IsNullOrEmpty(objectId));

        BookRequest bookUpdateRequest = new BookRequest()
        {
            Title = "BookTitle1",
            Author = "BookAuthor1",
            ISBN = "BookISBN1",
            Publisher = "BookPublisher1",
            PublicationDate = new DateOnly(2026, 1, 12)
        };

        IActionResult updateActionResult = await Controller.UpdateBookAsync(objectId, bookUpdateRequest);

        Assert.IsInstanceOfType(updateActionResult, typeof(OkObjectResult));
        OkObjectResult updateOkObject = (OkObjectResult)updateActionResult;
        Assert.AreEqual(200, updateOkObject.StatusCode);
        
        BookResponse? bookResponse = (BookResponse?)updateOkObject.Value;
        Assert.AreEqual("BookTitle1", bookResponse?.Title);
        Assert.AreEqual("BookAuthor1", bookResponse?.Author);
        Assert.AreEqual("BookISBN1", bookResponse?.ISBN);
        Assert.AreEqual("BookPublisher1", bookResponse?.Publisher);
        Assert.AreEqual(new DateOnly(2026, 1, 12), bookResponse?.PublicationDate);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseNotFound()
    {
        BookRequest bookUpdateRequest = new BookRequest()
        {
            Title = "BookTitle1",
            Author = "BookAuthor1",
            ISBN = "BookISBN1",
            Publisher = "BookPublisher1",
            PublicationDate = new DateOnly(2026, 1, 12)
        };

        IActionResult actionResult = await Controller.UpdateBookAsync("56fc40f9d735c28df206d078", bookUpdateRequest);

        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        NotFoundResult notFoundObject = (NotFoundResult)actionResult;
        Assert.AreEqual(404, notFoundObject.StatusCode);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseBadRequestRequestNull()
    {
        BookRequest bookUpdateRequest = null!;

        IActionResult actionResult = await Controller.UpdateBookAsync("56fc40f9d735c28df206d078", bookUpdateRequest);

        Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        BadRequestObjectResult badRequestObject = (BadRequestObjectResult)actionResult;
        Assert.AreEqual(400, badRequestObject.StatusCode);
    }

    [TestMethod]
    public async Task TestUpdateBookAsyncCaseBadRequestIncorrectObjectId()
    {
        BookRequest bookUpdateRequest = new BookRequest()
        {
            Title = "BookTitle1",
            Author = "BookAuthor1",
            ISBN = "BookISBN1",
            Publisher = "BookPublisher1",
            PublicationDate = new DateOnly(2026, 1, 12)
        };

        IActionResult actionResult = await Controller.UpdateBookAsync("aaaaaa28df206daaa", bookUpdateRequest);

        Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        BadRequestObjectResult badRequestObject = (BadRequestObjectResult)actionResult;
        Assert.AreEqual(400, badRequestObject.StatusCode);
    }

    [TestMethod]
    public async Task TestDeleteBookAsyncCaseOk()
    {
        BookRequest bookRequest = new BookRequest()
        {
            Title = "BookTitle",
            Author = "BookAuthor",
            ISBN = "BookISBN",
            Publisher = "BookPublisher",
            PublicationDate = new DateOnly(2026, 1, 1)
        };

        IActionResult actionResult = await Controller.CreateBookAsync(bookRequest);
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        OkObjectResult okObject = (OkObjectResult)actionResult;
        Assert.AreEqual(200, okObject.StatusCode);
        string objectId = okObject.Value?.ToString() ?? string.Empty;
        Assert.IsFalse(string.IsNullOrEmpty(objectId));

        IActionResult deleteActionResult = await Controller.DeleteBookAsync(objectId);

        Assert.IsInstanceOfType(deleteActionResult, typeof(OkResult));
        OkResult deleteOkObject = (OkResult)deleteActionResult;
        Assert.AreEqual(200, deleteOkObject.StatusCode);
    }

    [TestMethod]
    public async Task TestDeleteBookAsyncCaseBadRequestIncorrectObjectId()
    {
        IActionResult actionResult = await Controller.DeleteBookAsync("aaaaaa28df206daaa");

        Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        BadRequestObjectResult badRequestObject = (BadRequestObjectResult)actionResult;
        Assert.AreEqual(400, badRequestObject.StatusCode);
    }
}
