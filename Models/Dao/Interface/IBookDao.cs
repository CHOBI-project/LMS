using LMS.Models.Entities;

namespace LMS.Models.Dao.Interface;

public interface IBookDao
{
    List<BookEntity> FindAll();
    BookEntity FindByIsbn(BookEntity entity);
    BookEntity FindByTitle(BookEntity entity);
    List<BookEntity> FindByAuthor(BookEntity entity);
    void Add(BookEntity entity);
    void Update(BookEntity entiry);
    void Delete(BookEntity entity);
}