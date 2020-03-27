using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLayer
{
    public class EmployeeTypeModel : IEmployeeTypeModel
    {
        public List<IEmployeeType> EntityList => throw new NotImplementedException();

        public bool Create(IEmployeeType entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string key)
        {
            throw new NotImplementedException();
        }

        public List<IEmployeeType> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEmployeeType GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IEmployeeType entity)
        {
            throw new NotImplementedException();
        }
    }
}
