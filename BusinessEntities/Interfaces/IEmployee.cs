using System;

namespace BusinessEntities
{
    public interface IEmployee
    {
        int EmployeeId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string EmployeeType { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfBirth { get; set; }
        string Email { get; set; }
        string ContactNumber { get; set; }
        string Address { get; set; }
        string NextOfKinName { get; set; }
        string NextOfKinNumber { get; set; }
        string Ppsn { get; set; }
        string Iban { get; set; }
        string Bic { get; set; }
        bool IsTemporary { get; set; }
        bool IsLoggedIn { get; set; }
        bool PasswordReset { get; set; }
    }
}