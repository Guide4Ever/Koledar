using EnostavenKoledar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnostavenKoledar
{
    public partial class UserControlDays : UserControl
    {
        private DateTime date;
        public UserControlDays(DateTime date)
        {
            InitializeComponent();
            this.date = date; 
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {

        }
        public void days(int day) 
        {
            //Assign day integer
            DayInBox.Text = day.ToString();

            //Background: WHITE = !Sunday, ORANGE = Sunday
            this.BackColor = (DayOfWeek.Sunday == date.DayOfWeek)
                                ? Color.FromArgb(252, 225, 186)
                                : Color.White;
        }
        public void holidays(List<HolidayDto> dates)
        {
            //Background: BLUE = Holidays (overrides Sundays)
            dates.ForEach(d => 
            {
                if (d.HolidayDate.Month == date.Month &&
                    d.HolidayDate.Day == date.Day) 
                {
                    if (d.IsRepeated)
                    {
                        this.BackColor = Color.FromArgb(204, 229, 255);
                    }
                    else if (d.HolidayDate.Year == date.Year) 
                    {
                        this.BackColor = Color.FromArgb(204, 229, 255);
                    }
                }
            });
                       
        }
    }
}
