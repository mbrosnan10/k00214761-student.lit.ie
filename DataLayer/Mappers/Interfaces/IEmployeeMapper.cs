using BusinessEntities;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IEmployeeMapper : IMapper<IEmployee, int>
    {
        IEmployee GetByUsername(string username);

        bool ChangePassword(int id, string newPassword);

        bool SetLoggedInStatus(int id, bool value);

        bool DeleteByUsername(string username);
        List<IEmployee> GetLogged();
    }
}