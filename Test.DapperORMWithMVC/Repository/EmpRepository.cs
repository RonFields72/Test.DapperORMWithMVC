using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Test.DapperORMWithMVC.Models;

namespace Test.DapperORMWithMVC.Repository
{
    public class EmpRepository
    {
        public SqlConnection con;
        // connection activities      
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);

        }

        // Add Employee
        public void AddEmployee(EmpModel objEmp)
        {

            //Additing the employess      
            try
            {
                connection();
                con.Open();
                con.Execute("AddNewEmpDetails", objEmp, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // get employees
        public List<EmpModel> GetAllEmployees()
        {
            try
            {
                connection();
                con.Open();
                IList<EmpModel> EmpList = SqlMapper.Query<EmpModel>(
                                  con, "GetEmployees").ToList();
                con.Close();
                return EmpList.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // update Employee
        public void UpdateEmployee(EmpModel objUpdate)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("UpdateEmpDetails", objUpdate, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        // delete Employee
        public bool DeleteEmployee(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpId", Id);
                connection();
                con.Open();
                con.Execute("DeleteEmpById", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                // log errors as needed       
                throw ex;
            }
        }
    }
}