using BusinessEntities;
using DataLayer;
using System.Collections.Generic;

namespace UnitTests.DataLayer.Stubs
{
    internal class StubEmployeeMapper : IEmployeeMapper
    {
        public List<IEmployee> GetAll_Return { get; set; }
        public IEmployee GetByKey_Return { get; set; }
        public IEmployee GetByUsername_Return { get; set; }
        public bool Insert_Return { get; set; }
        public bool DeleteByKey_Return { get; set; }
        public bool DeleteByUsername_Return { get; set; }
        public bool Update_Return { get; set; }
        public bool ChangePassword_Return { get; set; }
        public bool SetLoggedInStatus_Return { get; set; }


        public StubEmployeeMapper()
        {
            ResetReturns();
        }

        public void ResetReturns()
        {
            GetAll_Return = null;
            GetByKey_Return = null;
            GetByUsername_Return = null;
            Insert_Return = true;
            DeleteByKey_Return = true;
            DeleteByUsername_Return = true;
            Update_Return = true;
            ChangePassword_Return = true;
            SetLoggedInStatus_Return = true;
        }


        public List<IEmployee> GetAll()
        {
            return GetAll_Return;
        }

        public IEmployee GetByKey(int employeeId)
        {
            return GetByKey_Return;
        }

        public IEmployee GetByUsername(string username)
        {
            return GetByUsername_Return;
        }

        public bool Insert(IEmployee employee)
        {
            return Insert_Return;
        }

        public bool DeleteByKey(int employeeId)
        {
            return DeleteByKey_Return;
        }

        public bool DeleteByUsername(string username)
        {
            return DeleteByUsername_Return;
        }

        public bool Update(IEmployee employee)
        {
            return Update_Return;
        }

        public bool ChangePassword(int id, string newPassword)
        {
            return ChangePassword_Return;
        }

        public bool SetLoggedInStatus(int id, bool value)
        {
            return SetLoggedInStatus_Return;
        }

        public List<IEmployee> GetLogged()
        {
            throw new System.NotImplementedException();
        }
    }
}
