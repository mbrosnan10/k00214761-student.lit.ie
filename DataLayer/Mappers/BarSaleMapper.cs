using BusinessEntities.Entities;
using BusinessEntities.Interfaces;
using DataLayer.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mappers
{
    public partial class BarSaleMapper : IBarSaleMapper
    {
        public List<IBarSale> GetAll()
        {
            string sql = "SELECT BarSaleId,EmployeeId,DateOfSale,AmountPaid,IsPaid FROM BarSale";
            return null;
        //    return db.ExecuteSelectMultiple<IBarSale>(sql, ReaderRowToBarSale);
        }


        public IBarSale GetById(int BarSaleId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(IBarSale BarSale)
        {
            throw new NotImplementedException();
        }

        public bool Update(IBarSale BarSale)
        {
            throw new NotImplementedException();
        }   
        private IBarSale ReaderRowToBarSale(SqlDataReader reader)
        {
            int BarSaleId = (int)reader["BarSaleId"];
            int EmployeeId = (int)reader["EmployeeId"];
            DateTime DateOfSale = (DateTime)reader["DateOfSale"];
            decimal AmountPaid = (decimal)reader["AmountPaid"];
            bool IsPaid = (byte)reader["IsPaid"] == 0 ? false : true;

            return new BarSale(BarSaleId,EmployeeId,DateOfSale,AmountPaid,IsPaid);
        }
    }
}
