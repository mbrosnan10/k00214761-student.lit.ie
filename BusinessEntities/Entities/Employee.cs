using System;

namespace BusinessEntities
{
    public class Employee : IEmployee
    {
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmployeeType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinNumber { get; set; }
        public string Ppsn { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool PasswordReset { get; set; }

        public Employee()
        {
        }

        public Employee(int employeeId, string username, string password, string employeeType, string firstName, string lastName, DateTime dateOfBirth, string email, string contactNumber, string address, string nextOfKinName, string nextOfKinNumber, string ppsn, string iban, string bic, bool isTemporary, bool isLoggedIn, bool passwordReset)
        {
            EmployeeId = employeeId;
            Username = username;
            Password = password;
            EmployeeType = employeeType;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            ContactNumber = contactNumber;
            Address = address;
            NextOfKinName = nextOfKinName;
            NextOfKinNumber = nextOfKinNumber;
            Ppsn = ppsn;
            Iban = iban;
            Bic = bic;
            IsTemporary = isTemporary;
            IsLoggedIn = isLoggedIn;
            PasswordReset = passwordReset;
        }
    }
}