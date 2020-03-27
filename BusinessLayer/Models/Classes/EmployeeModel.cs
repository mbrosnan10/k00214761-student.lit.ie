using BusinessEntities;
using DataLayer;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer
{
    public partial class EmployeeModel : IEmployeeModel
    {
        private static IEmployeeModel instance;
        private static readonly object padlock = new object();

        private IEmployeeMapper employeeMapper;
        private IDatabase db;

        public IEmployee CurrentUser { get; set; }
        public List<IEmployee> EntityList { get;}

        #region Singleton stuff

        private EmployeeModel(IDatabase db, IEmployeeMapper employeeMapper)
        {
            this.CurrentUser = null;
            this.db = db;
            this.employeeMapper = employeeMapper;
            EntityList = employeeMapper.GetAll();
        }

        public static IEmployeeModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IEmployeeMapper employeeMapper = EmployeeMapper.GetInstance();
                    instance = new EmployeeModel(db, employeeMapper);
                }
                return instance;
            }
        }

        public static IEmployeeModel GetInstance(IDatabase db, IEmployeeMapper employeeMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new EmployeeModel(db, employeeMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Login(string username, string password)
        {
            // Get an employee based on the username given.
            IEmployee employee = employeeMapper.GetByUsername(username);

            // Check if the passwords match
            if (employee.Password == Hash(password))
            {
                // Set the employee logged in flag to true and update that on the databsae.
                employee.IsLoggedIn = true;
                employeeMapper.SetLoggedInStatus(employee.EmployeeId, true);

                // Set the current user.
                this.CurrentUser = employee;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Logout()
        {
            // Logging out wont be needed if the CurrentUser is null.
            if (CurrentUser != null)
            {
                // Set the LoggedIn field in the database to false (ie. user is not logged in)
                CurrentUser.IsLoggedIn = false;

                if (employeeMapper.SetLoggedInStatus(CurrentUser.EmployeeId, false))
                {
                    CurrentUser = null;
                    db.CloseConnection();
                    return true;
                }
                else
                {
                    CurrentUser.IsLoggedIn = true;
                }
            }
            return false;
        }

        public bool ChangeAccountPassword(string username, string oldPassword, string newPassword)
        {
            IEmployee employee = employeeMapper.GetByUsername(username);

            //if the entered old password matches the new one
            if (employee.Password == Hash(oldPassword))
            {
                //set the new password
                employeeMapper.ChangePassword(employee.EmployeeId, Hash(newPassword));
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(IEmployee employee)
        {
            employee.Password = employee.DateOfBirth.ToString("ddmmyyy");
            employee.Password = Hash(employee.Password);

            return employeeMapper.Insert(employee);
        }

        public bool DeleteByUsername(string username)
        {
            // Don't let the user delete themselves.
            if (CurrentUser.Username == username)
            {
                return false;
            }

            return employeeMapper.DeleteByUsername(username);
        }

        public bool DeleteByKey(int employeeId)
        {
            // Don't let the user delete themselves.
            if (CurrentUser.EmployeeId == employeeId)
            {
                return false;
            }

            return employeeMapper.DeleteByKey(employeeId);
        }

        public bool Update(IEmployee employee)
        {
            return employeeMapper.Update(employee);
        }

        public List<IEmployee> GetAll()
        {
            return employeeMapper.GetAll();
        }

        public IEmployee GetByKey(int employeeId)
        {
            return employeeMapper.GetByKey(employeeId);
        }
        public IEmployee GetByUsername(string username)
        {
            return employeeMapper.GetByUsername(username);
        }

        #endregion

        private static string Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public List<IEmployee> GetLogged()
        {
            throw new System.NotImplementedException();
        }
    }
}