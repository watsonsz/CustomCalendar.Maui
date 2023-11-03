using CustomCalendar.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CustomCalendar.DataAccess.Repositories
{
    public class MonthRepository
    {
        public MonthRepository()
        {
            this.connectionString = "Data Source=localhost;Initial Catalog=CustomCalendar;Integrated Security=True";
        }


        private string connectionString;

        public MonthEntity GetMonth(string Month, string Year)
        {
            MonthEntity newMonth = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "SELECT * FROM dbo.Months WHERE [Month] = @Month AND [year] = @Year",
                                        
                        Connection = connection
                    };
                    command.Parameters.Add(new SqlParameter("@Month", SqlDbType.NVarChar) { Value = Month });
                    command.Parameters.Add(new SqlParameter("@Year", SqlDbType.NVarChar) { Value = Year });

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Data found in the database, create a MonthEntity
                        newMonth = new MonthEntity
                        {
                            MonthID = (Guid)reader["MonthID"],
                            Year = (string)reader["Year"],
                            MonthName = (string)reader["Month"],
                        };
                    }
                    else
                    {
                        // No data found, create a new MonthEntity
                        newMonth = new MonthEntity
                        {
                            MonthID = Guid.NewGuid(),
                            Year = Year,
                            MonthName = Month
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newMonth;
        }

        public void SaveMonth(MonthEntity Month)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = @"INSERT INTO dbo.Months
                                        Values(@Id, @Month, @Year)",
                        Connection = connection
                    };
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Month.MonthID;
                    command.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Month.MonthName;
                    command.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Month.Year;

                    connection.Open();
                    int rowsAffected= command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
