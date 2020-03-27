using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public static class EmployeeFactory
    {
        public static IEmployee GetEmployee(int employeeId, string username, string password, string employeeType,
            string firstName, string lastName, DateTime dateOfBirth, string email, string contactNumber,
            string address, string nextOfKinName, string nextOfKinNumber, string ppsn, string iban,
            string bic, bool isTempStaff, bool isLoggedIn, bool passwordReset)
        {
            return new Employee(employeeId, username, password, employeeType, firstName, lastName, dateOfBirth, 
                email, contactNumber, address, nextOfKinName, nextOfKinNumber, ppsn, iban, bic, isTempStaff, isLoggedIn, passwordReset);
        }

        public static IEmployee GetEmployeeWithoutId(string username, string password, string employeeType,
            string firstName, string lastName, DateTime dateOfBirth, string email, string contactNumber,
            string address, string nextOfKinName, string nextOfKinNumber, string ppsn, string iban,
            string bic, bool isTempStaff, bool isLoggedIn, bool passwordReset)
        {
            return new Employee(0, username, password, employeeType, firstName, lastName, dateOfBirth, email, 
                contactNumber, address, nextOfKinName, nextOfKinNumber, ppsn, iban, bic, isTempStaff, isLoggedIn, passwordReset);
        }
    }
}
