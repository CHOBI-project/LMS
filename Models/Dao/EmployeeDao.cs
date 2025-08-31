using Google.Protobuf.WellKnownTypes;
using LMS.Models.Dao.Interface;
using LMS.Models.Entities;
using MySql.Data.MySqlClient;

namespace LMS.Models.Dao;

public class EmployeeDao : BaseDao<EmployeeEntity>,IEmployeeDao
{
    public EmployeeDao(IConfiguration configuration, ILogger<EmployeeEntity> logger) : base(configuration, logger) {}
    public EmployeeEntity Find(EmployeeEntity entity) 
    {
        if (string.IsNullOrEmpty(entity.Id) || string.IsNullOrEmpty(entity.Pass)) 
            throw new ArgumentException("Id または Pass が空です");

        using var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        
        string sql = "SELECT * FROM Employees WHERE Id = @id AND Pass = @pass";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.Add("@id", MySqlDbType.VarChar, 50).Value = entity.Id;
        cmd.Parameters.Add("@pass", MySqlDbType.VarChar, 50).Value = entity.Pass;
        
        using var result = cmd.ExecuteReader();

        if (result.Read())
        {
            return new EmployeeEntity
            {
                Id = result.GetString("Id"),
                Name = result.GetString("Name"),
                Pass = result.GetString("Pass"),
            };
        }

        return null;
    }
}