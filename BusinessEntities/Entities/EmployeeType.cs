using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class EmployeeType : IEmployeeType
    {
        public string EmployeeTypeName { get; set; }

        public EmployeeType(string EmployeeTypeName)
        {
            this.EmployeeTypeName = EmployeeTypeName;
        }
    }
}
