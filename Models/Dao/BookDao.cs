using System.Diagnostics;
using LMS.Models.Dao.Interface;
using LMS.Models.Entities;
using MySql.Data.MySqlClient;

namespace LMS.Models.Dao;

public class BookDao : BaseDao<BookEntity>, IBookDao
{
    public BookDao(IConfiguration configuration, ILogger<BookEntity> logger) : base(configuration, logger) {}

    public List<BookEntity> FindAll()
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT * FROM Books";
        using var cmd = new MySqlCommand(sql, connection);
        using var result = cmd.ExecuteReader();

        List<BookEntity> books = new List<BookEntity>();
        while (result.Read()) 
        {
            books.Add(new BookEntity()
            {
                Isbn = result.GetString("Isbn"),
                Title = result.GetString("Title"),
                Author = result.GetString("Author"),
            });
        }
        return books;
    }

    public BookEntity FindByIsbn(BookEntity entity)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT * FROM Books WHERE Isbn = @isbn";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.Add("@isbn", MySqlDbType.VarChar, 13).Value = entity.Isbn;

        using var result = cmd.ExecuteReader();

        if (result.Read())
        {
            return new BookEntity()
            {
                Isbn = result.GetString("Isbn"),
                Title = result.GetString("Title"),
                Author = result.GetString("Author"),
            };
        }

        return null;
    }

    public BookEntity FindByTitle(BookEntity entity)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT * FROM Books WHERE Title = @title";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 255).Value = entity.Title;

        using var result = cmd.ExecuteReader();

        if (result.Read())
        {
            return new BookEntity()
            {
                Isbn = result.GetString("Isbn"),
                Title = result.GetString("Title"),
                Author = result.GetString("Author"),
            };
        }

        return null;
    }

    public List<BookEntity> FindByAuthor(BookEntity entity)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT * FROM Books WHERE Author = @author";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.Add("@author", MySqlDbType.VarChar, 100).Value = entity.Author;

        using var result = cmd.ExecuteReader();

        List<BookEntity> books = new List<BookEntity>();
        while (result.Read())
        {
            books.Add(new BookEntity()
            {
                Isbn = result.GetString("Isbn"),
                Title = result.GetString("Title"),
                Author = result.GetString("Author"),
            });
        }
        
        return books;
    }

    public void Add(BookEntity entity)
    {
        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();

        string sql = "INSERT INTO Books VALUES (@isbn, @title, @author)";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.Add("@isbn", MySqlDbType.VarChar, 13).Value = entity.Isbn;
        cmd.Parameters.Add("@title", MySqlDbType.VarChar, 255).Value = entity.Title;
        cmd.Parameters.Add("@author", MySqlDbType.VarChar, 100).Value = entity.Author;
        cmd.ExecuteNonQuery();
    }
}