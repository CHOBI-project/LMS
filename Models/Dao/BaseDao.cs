namespace LMS.Models.Dao;

public abstract class BaseDao<T>
{
    protected readonly string ConnectionString;
    protected readonly ILogger<T> Logger;

    protected BaseDao(IConfiguration configuration, ILogger<T> logger)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("接続文字列 'DefaultConnection' が見つかりません。");
        Logger = logger;
    }
}