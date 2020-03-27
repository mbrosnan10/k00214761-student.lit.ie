using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    interface IEmployeeTypeMapper
    {
        bool Insert(IEmployeeType employeeType);
        List<IEmployeeType> GetAll();
        IEmployeeType Get(string employeeType);
        bool Update(IEmployeeType employeeType);
        bool Delete(string employeeType);
    }
}
