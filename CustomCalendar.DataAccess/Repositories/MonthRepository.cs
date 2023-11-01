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
    public class MonthRepository
    {
        public MonthRepository()
        {
            this.connectionString = "Data Source=localhost;Initial Catalog=CustomCalendar;Integrated Security=True";
        }


        private string connectionString;

        public void GetMonth(string Month, string Year)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "",
                        Connection = connection
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
