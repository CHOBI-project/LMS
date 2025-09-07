using System.Diagnostics;
using LMS.Models;
using LMS.Models.Dao.Interface;
using LMS.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers;

public class LmsController : Controller
{
    private readonly ILogger<LmsController> _logger;
    private readonly IBookDao _bookDao;

    public LmsController(ILogger<LmsController> logger, IBookDao bookDao)
    {
        _logger = logger;
        _bookDao = bookDao;
    }

    public IActionResult Index()
    {
        List<BookEntity> result = _bookDao.FindAll();
        ViewData["Message"] = TempData["Message"];
        return View(result);
    }

    [HttpGet]
    public IActionResult Search(string searchType, string keyword)
    {
        List<BookEntity> result = new List<BookEntity>();
        BookEntity book;
        
        switch (searchType)
        {
            case "isbn":
                book = new BookEntity() { Isbn = keyword };
                var isbnBook = _bookDao.FindByIsbn(book);
                if (isbnBook != null)
                {
                    result.Add(isbnBook);
                }
                break;
            case "title":
                book = new BookEntity() { Title = keyword };
                var titleBook = _bookDao.FindByTitle(book);
                if (titleBook != null)
                {
                    result.Add(titleBook);
                }
                break;
            case "author":
                book = new BookEntity() { Author = keyword };
                result = _bookDao.FindByAuthor(book);
                break;
        }
        
        if (result.Count == 0 )
        {
            ViewData["Message"] = "該当する書籍が見つかりませんでした。";
        }
        
        return View("Index", result);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string isbn, string title, string author)
    {
        if (string.IsNullOrEmpty(isbn) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
        {
            ViewData["Message"] = "すべてのフィールドを入力してください。";
            return View();
        }
        
        BookEntity newBook = new BookEntity
        {
            Isbn = isbn,
            Title = title,
            Author = author
        };
        
        _bookDao.Add(newBook);
        TempData["Message"] = "書籍が正常に登録されました。";
        
        return RedirectToAction("Index");
    }

    public IActionResult Edit(string isbn, string title, string author)
    {
        if (string.IsNullOrEmpty(isbn) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
        {
            ViewData["Message"] = "詳細情報が取得できませんでした。";
            return View("Index");
        }

        BookEntity book = new BookEntity()
        {
            Isbn = isbn,
            Title = title,
            Author = author
        };
        
        return View(book);
    }

    [HttpPost]
    public IActionResult Update(string isbn, string title, string author)
    {
        if (string.IsNullOrEmpty(isbn) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
        {
            ViewData["Message"] = "更新情報が取得できませんでした。";
            return View("Edit");
        }
        
        BookEntity book = new BookEntity()
        {
            Isbn = isbn,
            Title = title,
            Author = author
        };
        
        _bookDao.Update(book);
        TempData["Message"] = "書籍が正常に更新されました。";
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(String isbn)
    {
        if (string.IsNullOrEmpty(isbn))
        {
            ViewData["Message"] = "削除情報が取得できませんでした。";
            return View("Edit");
        }
        BookEntity book = new BookEntity()
        {
            Isbn = isbn
        };
        
        _bookDao.Delete(book);
        TempData["Message"] = "書籍が正常に削除されました。";
        
        return RedirectToAction("Index");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}