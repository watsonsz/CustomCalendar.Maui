using CustomCalendar.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.DataAccess.Repositories
{
    public class DayRepository
    {
        private string connectionString;
        


        public DayRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<DaysEntity> GetDaysForMonth(Guid monthId)
        {
            List<DaysEntity> daysList = new List<DaysEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "SELECT * FROM dbo.Days WHERE MonthId = @MonthId",

                        Connection = connection
                    };
                    command.Parameters.Add(new SqlParameter("@MonthId", SqlDbType.UniqueIdentifier) { Value = monthId});

                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DaysEntity day = new DaysEntity
                            {
                                DayDatetime = (DateTime)reader["DayDateTime"],
                                MonthId = (Guid)reader["MonthId"]
                            };
                            daysList.Add(day);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return daysList;
        }

        public void SaveDays(List<DaysEntity> daysList)
        {
            //save day entities
            foreach (var day in daysList) {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand
                        {
                            CommandText = "INSERT INTO dbo.Days Values(@DayDate, @Month)",

                            Connection = connection
                        };
                        command.Parameters.AddWithValue("@DayDate", day.DayDatetime);
                        command.Parameters.AddWithValue("@Month", day.MonthId);

                        connection.Open();
                        command.ExecuteNonQuery();
                                            

                    }
                }
                catch (Exception ex)
                {
                        Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
