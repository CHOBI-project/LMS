using LMS.Models.Entities;

namespace LMS.Models.Dao.Interface;

public interface IBookDao
{
    BookEntity FindByIsbn(BookEntity entity);
    BookEntity FindByTitle(BookEntity entity);
    List<BookEntity> FindByAuthor(BookEntity entity);
}