using CustomCalendar.BusinessEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.DataAccess.Repositories
{
    public class ShiftLinkingRepository
    {
        private string connectionString;

        public ShiftLinkingRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<EmployeeShiftEntity> GetEmployeeShiftEntities(DateTime dayDatetime, int shiftId)
        {
            List<EmployeeShiftEntity> IdList = new List<EmployeeShiftEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "SELECT * FROM dbo.ShiftLinkingTable WHERE [DayDateTime] = @Day AND [ShiftId] = @Shift",

                        Connection = connection
                    };
                    command.Parameters.Add(new SqlParameter("@Day", SqlDbType.DateTime) { Value = dayDatetime });
                    command.Parameters.Add(new SqlParameter("@Shift", SqlDbType.Int) { Value = shiftId });

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeShiftEntity entity = new EmployeeShiftEntity
                            {
                                ShiftType = shiftId,
                                EmployeeId = (Guid)reader["EmployeeId"],
                                DayDatetime = (DateTime)reader["DayDateTime"]
                            };
                            IdList.Add(entity);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return IdList;
        }

        public void SaveList(List<EmployeeShiftEntity> shiftList)
        {
            foreach(EmployeeShiftEntity entity in shiftList)
            {
                try
                {
                    using(SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand
                        {
                            CommandText = "INSERT INTO dbo.ShiftLinkingTable Values(@DayDate, @Id, @Shift)",
                            Connection = connection
                        };
                        command.Parameters.AddWithValue("@DayDate", entity.DayDatetime);
                        command.Parameters.AddWithValue("@Id", entity.EmployeeId);
                        command.Parameters.AddWithValue("@Shift", entity.ShiftType);

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
