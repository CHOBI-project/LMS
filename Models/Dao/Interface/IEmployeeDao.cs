using LMS.Models.Entities;

namespace LMS.Models.Dao.Interface;

public interface IEmployeeDao
{
    EmployeeEntity Find(EmployeeEntity entity);
}