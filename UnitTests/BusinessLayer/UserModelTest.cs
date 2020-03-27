using System;
using NUnit.Framework;
using UnitTests.DataLayer.Stubs;
using BusinessEntities;
using BusinessLayer;

namespace UnitTests
{
    [TestFixture]
    public class UserModelTest
    {
        private IEmployeeModel employeeModel;
        private StubEmployeeMapper stubEmployeeMapper;
        private StubDatabase stubDatabase;

        // This one time setup will maintain a reference to a single StubEmployeeMapper for
        // the employeeModel, which is needed to maintain stubbing the return values of the methods.
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            stubEmployeeMapper = new StubEmployeeMapper();
            stubDatabase = new StubDatabase();
            employeeModel = EmployeeModel.GetInstance(stubDatabase, stubEmployeeMapper);
        }

        // We reset the returns at the start of each test
        //
        // It's important we don't "new" up stubEmployeeMapper at the start of each test
        // because employeeModel needs to hold the same reference to a single stubEmployeeMapper
        // for the whole TestFixture.
        [SetUp]
        public void SetUp()
        {
            stubEmployeeMapper.ResetReturns();
        }


        #region Login
        [Test]
        public void Login_ValidLogin_ReturnsTrue()
        {
            // Arrange
            string username = "mark.bromell";
            string passwordPlain = "Password8";
            string passwordHashed = "2ced6e7160a9e2cb4be29c200852bfc4fe29d7531ff3ffc51fc1407399d8d8b8";
            stubEmployeeMapper.GetByUsername_Return = new Employee() { Username = username, Password = passwordHashed };

            // Act
            bool result = employeeModel.Login(username, passwordPlain);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Login_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            string username = "mark.bromell";
            // Plain password is "Password8";
            string passwordHashed = "2ced6e7160a9e2cb4be29c200852bfc4fe29d7531ff3ffc51fc1407399d8d8b8";
            stubEmployeeMapper.GetByUsername_Return = new Employee() { Username = username, Password = passwordHashed };

            // Act
            bool result = employeeModel.Login(username, "OtherPass77");

            // Assert
            Assert.IsFalse(result);
        }
        #endregion


        #region Logout
        [Test]
        public void Logout_ValidLogout_ReturnsTrue()
        {
            // Arrange
            IEmployee employee = new Employee(0, "mark.bromell", "Password8", "", "user", "name", new DateTime(1998, 4, 23),
                    "user@email.com", "0851234567", "Moylish, Limerick, Ireland", "parent", "0871234567",
                    "H13245654", "IE29AIBK93115212345678", "BOFIIE2D", false, false,false);
            employeeModel.CurrentUser = employee;

            // Act
            bool result = employeeModel.Logout();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Logout_NullCurrentUser_ReturnsFalse()
        {
            // Arrange
            employeeModel.CurrentUser = null;

            // Act
            bool result = employeeModel.Logout();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Logout_EmployeeMapperSetLoggedInStatusFailed_ReturnsFalse()
        {
            // Arrange
            employeeModel.CurrentUser = new Employee();
            stubEmployeeMapper.SetLoggedInStatus_Return = false;

            // Act
            bool result = employeeModel.Logout();

            // Assert
            Assert.IsFalse(result);
        }
        #endregion


        #region CreateEmployee
        [Test]
        public void CreateEmployee_ValidDetails_ReturnsTrue()
        {
            // Arrange
            IEmployee employee = new Employee(0, "mark.bromell", "Password8", "", "user", "name", new DateTime(1998, 4, 23),
                "user@email.com", "0851234567", "Moylish, Limerick, Ireland", "parent", "0871234567",
                "H13245654", "IE29AIBK93115212345678", "BOFIIE2D", false, false,false);

            // Act
            bool result = employeeModel.Create(employee);

            // Assert
            Assert.IsTrue(result);
        }
        #endregion


        #region DeleteEmployeeByUsername
        [Test]
        public void DeleteEmployeeByUsername_ValidDeletion_ReturnsTrue()
        {
            // Arange
            IEmployee employee = new Employee(0, "mark.bromell", "Password8", "", "user", "name", new DateTime(1998, 4, 23),
                "user@email.com", "0851234567", "Moylish, Limerick, Ireland", "parent", "0871234567",
                "H13245654", "IE29AIBK93115212345678", "BOFIIE2D", false, false,false);
            employeeModel.CurrentUser = employee;

            // Act
            bool result = employeeModel.DeleteByUsername("bethany.hanrahan");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteEmployeeByUsername_AttempmtToDeleteSelf_ReturnsFalse()
        {
            // Arange
            IEmployee employee = new Employee(0, "mark.bromell", "Password8", "", "user", "name", new DateTime(1998, 4, 23),
                "user@email.com", "0851234567", "Moylish, Limerick, Ireland", "parent", "0871234567",
                "H13245654", "IE29AIBK93115212345678", "BOFIIE2D", false, false,false);
            employeeModel.CurrentUser = employee;

            // Act
            bool result = employeeModel.DeleteByUsername("mark.bromell");

            // Assert
            Assert.IsFalse(result);
        }
        #endregion


        #region DeleteEmployeeById
        [Test]
        public void DeleteEmployeeById_ValidDeletion_ReturnsTrue()
        {
            // Arange
            IEmployee employee = new Employee(0, "mark.bromell", "Password8", "", "user", "name", new DateTime(1998, 4, 23),
                "user@email.com", "0851234567", "Moylish, Limerick, Ireland", "parent", "0871234567",
                "H13245654", "IE29AIBK93115212345678", "BOFIIE2D", false, false,false);
            employeeModel.CurrentUser = employee;

            // Act
            bool result = employeeModel.DeleteByKey(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteEmployeeById_AttempmtToDeleteSelf_ReturnsFalse()
        {
            // Arange
            IEmployee employee = new Employee(0, "mark.bromell", "Password8", "", "user", "name", new DateTime(1998, 4, 23),
                "user@email.com", "0851234567", "Moylish, Limerick, Ireland", "parent", "0871234567",
                "H13245654", "IE29AIBK93115212345678", "BOFIIE2D", false, false,false);
            employeeModel.CurrentUser = employee;

            // Act
            bool result = employeeModel.DeleteByKey(0);

            // Assert
            Assert.IsFalse(result);
        }
        #endregion
    }
}
