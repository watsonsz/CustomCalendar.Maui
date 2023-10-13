using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCalendar.ViewModels
{
    public class DayViewModel
    {
        public DayViewModel(DateTime date)
        {
            this.Date = date;
            //allEmployees = employeeBL.GetAll();
            DAYOFWEEKNUMBER = (int)Date.DayOfWeek;
            AllEmployees = new List<string>();
            AllEmployees.Add("Zack");
            AllEmployees.Add("Talaine");
            
        }
        public string DayLabel { get => GetLabel();}

        public DateTime Date { get; set; }
        public static string FIRST_SHIFT_START;
        public static string FIRST_SHIFT_END;
        public static string LAST_SHIFT_START;
        public static string LAST_SHIFT_END;



        public int DAYOFWEEKNUMBER { get; set; }
        public List<string> AllEmployees { get; set; }

        //These need to be done with employees instead of strings
        public List<string> FirstShiftEmployees { get; set; } = new List<string>();
        public List<string> LastShiftEmployees { get; set; } = new List<string>();

        public string GetLabel()
        {
            var day = Date.DayOfWeek.ToString();
            var shortMonthName = Date.ToString("MMM");
            var label = $"{shortMonthName} {Date.Day}";
            return label;
        }

        

    }
}
