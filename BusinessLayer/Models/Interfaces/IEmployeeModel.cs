using BusinessEntities;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IEmployeeModel : IModel<IEmployee, int>
    {
        IEmployee CurrentUser { get; set; }
        List<IEmployee> GetLogged();
  
        bool Login(string username, string password);
        bool Logout();
        bool DeleteByUsername(string username);
        bool ChangeAccountPassword(string username, string oldPassword, string newPassword);
        IEmployee GetByUsername(string username);
    }
}