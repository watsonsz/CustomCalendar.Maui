using CustomCalendar.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.DataAccess.Repositories
{
    public class EmployeeRepository
    {
        private string connectionString;
        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public EmployeeRepository()
        {
            connectionString = "Data Source=localhost;Initial Catalog=CustomCalendar;Integrated Security=True";
        }

        public List<EmployeeEntity> GetEmployeesFromList(List<EmployeeShiftEntity> employeeList)
        {
            List<EmployeeEntity> newEmployeeList = new List<EmployeeEntity>();
            foreach(EmployeeShiftEntity shiftEntry in employeeList)
            {
                newEmployeeList.Add(GetEmployeeById(shiftEntry.EmployeeId));
            }
            return newEmployeeList;
        }

        public EmployeeEntity GetEmployeeById(Guid employeeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "SELECT * FROM dbo.Employees WHERE [Id] = @Id",

                        Connection = connection
                    };
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = employeeId });
                    

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        EmployeeEntity employeeEntity = new EmployeeEntity((string)reader["FullName"], (Guid)reader["Id"]);
                        employeeEntity.PhoneNumber = (string)reader["PhoneNumber"];
                        employeeEntity.Address = (string)reader["Address"];
                        employeeEntity.HasPTO = false;
                        employeeEntity.WantsMoreHours = (bool)reader["MoreHours"];
                        employeeEntity.IsActive = (bool)reader["IsActive"];
                        return employeeEntity;
                    }
                    else
                    {
                        EmployeeEntity entity = null;
                        return entity;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void SaveEmployee(EmployeeEntity employeeEntity)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = @"INSERT INTO dbo.Employees
                                        Values(@Id, @Name, @Phone,@Address,@Hours,@Active)",
                        Connection = connection
                    };
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = employeeEntity.Id;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = employeeEntity.FullName;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = employeeEntity.PhoneNumber;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = employeeEntity.Address;
                    command.Parameters.Add("@Hours", SqlDbType.Binary).Value = BitConverter.GetBytes(employeeEntity.WantsMoreHours);
                    command.Parameters.Add("@Active", SqlDbType.Binary).Value = BitConverter.GetBytes(employeeEntity.IsActive);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<EmployeeEntity> GetAllEmployees()
        {
            List<EmployeeEntity> employees = new List<EmployeeEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "SELECT * FROM dbo.Employees",

                        Connection = connection
                    };
                    


                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    
                    while (reader.Read())
                    {
                        EmployeeEntity employeeEntity = new EmployeeEntity((string)reader["FullName"], (Guid)reader["Id"]);
                        employeeEntity.PhoneNumber = (string)reader["PhoneNumber"];
                        employeeEntity.Address = (string)reader["Address"];
                        employeeEntity.HasPTO = false;
                        employeeEntity.WantsMoreHours = (bool)reader["MoreHours"];
                        employeeEntity.IsActive = (bool)reader["IsActive"];

                        employees.Add(employeeEntity);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employees;
        }
    }
}
