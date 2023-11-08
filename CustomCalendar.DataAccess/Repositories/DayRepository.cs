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
        private const int FIRST_SHIFT = 1;
        private const int SECOND_SHIFT = 2;
        private const int LAST_SHIFT = 3;
        private const int HOUSEKEEPING = 4;
        private const int KITCHEN = 5;


        public DayRepository()
        {
            connectionString = "Data Source=localhost;Initial Catalog=CustomCalendar;Integrated Security=True";
        }

        public List<DaysEntity> GetDaysForMonth(Guid monthId)
        {
            List<DaysEntity> daysList = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "SELECT * FROM dbo.Days WHERE [MonthId] = @MonthId",

                        Connection = connection
                    };
                    command.Parameters.Add(new SqlParameter("@Month", SqlDbType.UniqueIdentifier) { Value = monthId});

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
            foreach(var day in daysList)
            {
                day.FirstShift = GetShiftEmployees(day.DayDatetime, FIRST_SHIFT);
                day.SecondShift = GetShiftEmployees(day.DayDatetime, SECOND_SHIFT);
                day.ThirdShift = GetShiftEmployees(day.DayDatetime, LAST_SHIFT);
                day.HouseKeeping = GetShiftEmployees(day.DayDatetime, HOUSEKEEPING);
                day.KitchenStaff = GetShiftEmployees(day.DayDatetime, KITCHEN);
            }
            return daysList;
        }

        private List<EmployeeEntity> GetShiftEmployees(DateTime dayDatetime, int shiftId)
        {
            List<EmployeeEntity> shiftList = new List<EmployeeEntity>();
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
                    command.Parameters.Add(new SqlParameter("@Day", SqlDbType.UniqueIdentifier) { Value = dayDatetime });
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

            EmployeeRepository _employeeRepo = new EmployeeRepository();
            foreach(var entity  in IdList)
            {
                EmployeeEntity newEmployee = _employeeRepo.GetEmployeeById(entity.EmployeeId);
                shiftList.Add(newEmployee);
            }
            return shiftList;
        }

        public void SaveDays(List<DaysEntity> daysList)
        {
            //save day entities
            //generate linking tables
            //save linking
        }
    }
}
