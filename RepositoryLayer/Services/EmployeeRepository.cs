using Microsoft.Data.SqlClient;
using ModelLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        string connectionString = "Data Source=DESKTOP-2GMEK14\\SQLEXPRESS;Initial Catalog=EmployeePayRoll;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        //To View all employees details 
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    employee.Name = reader["Name"].ToString();
                    employee.ProfileImage = reader["ProfileImage"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.Department = reader["Department"].ToString();
                    employee.Salary = Convert.ToInt32(reader["Salary"]);
                    employee.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    employee.Notes = reader["Notes"].ToString();
                    employees.Add(employee);

                }
                conn.Close();
                return employees;
            }

        }
        //To Add new employee record 
        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spAddEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure; // Set command type for stored procedure
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);


                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return employee;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;

            }

        }
        // to update employe details
        public EmployeeModel EmployeeUpdate(EmployeeModel employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure;// Set command type for stored procedure
                    cmd.Parameters.AddWithValue("@EmployeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                    cmd.ExecuteNonQuery();
                    return employee;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }

        }
        // to delete the employee data 
        public bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteEmployee", conn);
                    cmd.CommandType = CommandType.StoredProcedure; // Set command type for stored procedure

                    cmd.Parameters.AddWithValue("@EmpId", id);


                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }

        //to get employee Details by id
        public EmployeeModel GetEmployeeById(int id)
        {
            EmployeeModel employee = new EmployeeModel();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("getEmpById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpID", id);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {

                        employee.EmployeeId = Convert.ToInt32(dataReader["EmployeeId"]);
                        employee.Name = dataReader["Name"].ToString();
                        employee.ProfileImage = dataReader["ProfileImage"].ToString();
                        employee.Gender = dataReader["Gender"].ToString();
                        employee.Department = dataReader["Department"].ToString();
                        employee.Salary = Convert.ToInt64(dataReader["Salary"]);
                        employee.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                        employee.Notes = dataReader["Notes"].ToString();

                    }
                    return employee;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;

            }
        }
        public EmployeeModel Login(LoginModel loginModel)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spLoginModel", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", loginModel.EmployeeId);
                cmd.Parameters.AddWithValue("@name", loginModel.Name);
                conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    EmployeeModel model = new EmployeeModel();
                    model.EmployeeId = Convert.ToInt32(dataReader["EmployeeId"]);
                    model.Name = dataReader["Name"].ToString();
                    return model;
                }
                return null; ;


            }
        }
        //public EmployeeModel GetEmployeeByName(string name)
        //{
        //    EmployeeModel employee = new EmployeeModel();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("spCustomerModel", conn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@name", name);
        //            conn.Open();
        //            SqlDataReader dataReader = cmd.ExecuteReader();

        //            while (dataReader.Read())
        //            {

        //                employee.EmployeeId = Convert.ToInt32(dataReader["Id"]);
        //                employee.Name = dataReader["Name"].ToString();
        //                employee.ProfileImage = dataReader["ProfileImage"].ToString();
        //                employee.Gender = dataReader["Gender"].ToString();
        //                employee.Department = dataReader["Department"].ToString();
        //                employee.Salary = Convert.ToInt64(dataReader["Salary"]);
        //                employee.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
        //                employee.Notes = dataReader["Notes"].ToString();

        //            }
        //            return employee;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //        return null;

        //    }
        //}
        public IEnumerable<EmployeeModel> GetEmployeesByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    List<EmployeeModel> list = new List<EmployeeModel>();
                    SqlCommand cmd = new SqlCommand("spCustomerModel", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        EmployeeModel employee = new EmployeeModel();
                        employee.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                        employee.Name = reader["Name"].ToString();
                        employee.ProfileImage = reader["ProfileImage"].ToString();
                        employee.Gender = reader["Gender"].ToString();
                        employee.Department = reader["Department"].ToString();
                        employee.Salary = Convert.ToInt32(reader["Salary"]);
                        employee.StartDate = Convert.ToDateTime(reader["StartDate"]);
                        employee.Notes = reader["Notes"].ToString();
                        list.Add(employee);

                    }
                    return list;
                }
                catch(Exception ex)
                {
                    return null;
                }
                
                finally
                {
                    conn.Close();
                }
                
            }
        }



    }

}  

