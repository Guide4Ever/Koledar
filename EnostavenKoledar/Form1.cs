using EnostavenKoledar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnostavenKoledar
{
    public partial class App : Form
    {   
        private List<HolidayDto> dates = new List<HolidayDto>();
        private DateTime globalDate = new DateTime();

        public App()
        {
            InitializeComponent();
        }

        //First method to be ran on execution
        private void App_Load(object sender, EventArgs e)
        {
            readFile();
            displayDays();
        }

        private void displayDays() 
        {
            DateTime now = DateTime.Now;
            globalDate = now;
            addDays(now);           
        }

        private void addDays(DateTime now)
        {
            //Create "<month>, <year>" header
            MonthYearText.Text = now.ToString("MMMM", new CultureInfo("sl"))
                                + ", "
                                + now.Year.ToString();

            DateTime firstDayMonth = new DateTime(now.Year, now.Month, 1);

            int days = DateTime.DaysInMonth(now.Year, now.Month);

            //Blank space buffer count (in front)
            int bufferBlank = Convert.ToInt32(firstDayMonth.DayOfWeek.ToString("d"));

            //Create blank spaces (in front) in MonthBox panel
            for (int i = 1; i <= bufferBlank; i++)
            {
                UserControlBlank ucbla = new UserControlBlank();
                MonthBox.Controls.Add(ucbla);
            }

            //Create days in MonthBox panel
            for (int i = 1; i <= days; i++)
            {
                if (i > 1)
                {
                    firstDayMonth = firstDayMonth.AddDays(1);
                }
                UserControlDays ucday = new UserControlDays(firstDayMonth);

                //Assign day integer + colorize Sundays
                ucday.days(i);

                //Colorize holidays
                ucday.holidays(dates);

                MonthBox.Controls.Add(ucday);
            }
        }

        private void readFile() 
        {
            //Holds rows of strings located in prazniki.txt
            List<string> file = new List<string>();

            //Holds a date and an indicator for repetition
            string[] line = new string[2];

            string path = @"..\prazniki.txt";

            try
            {   
                //File exists
                file = File.ReadAllLines(path).ToList();
            }
            catch (Exception ex) 
            {
                //File does not exist
                return;
            }

            //File is empty
            if (!file.Any()) {
                return;
            }

            //Create a list of HolidayDtos
            file.ForEach(i => 
            {
                line = i.Split(',');

                HolidayDto holiday = new HolidayDto { 
                    HolidayDate = DateTime.Parse(line[0]),
                    IsRepeated = line[1].Equals("P") ? true : false
                };

                dates.Add(holiday);
            }); 

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Set a new global date and update (clear + add) calendar
                DateTime currentDate = DateTime.Parse(textbox1.Text);
                globalDate = currentDate;
                
                MonthBox.Controls.Clear();
                addDays(currentDate);

                FormatWarning.Text = "Format ustreza.";
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                FormatWarning.Text = "Format ne ustreza.";
            }
        }

        private void textLeto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Set a new global date and update (clear + add) calendar
                int year = Convert.ToInt32(textLeto.Text);
                int month = globalDate.Month;
                int day = globalDate.Day;
                globalDate = new DateTime(year, month, day);

                MonthBox.Controls.Clear();
                addDays(globalDate);
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine(ex.Message);
            }
        }

        private void ComboBoxMesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {   
                //Set a new global date and update (clear + add) calendar
                string monthText = ComboBoxMesec.Text;
                int month = DateTime.ParseExact(monthText, "MMMM", new CultureInfo("sl")).Month;
                int day = globalDate.Day;
                int year = globalDate.Year;
                globalDate = new DateTime(year, month, day);

                MonthBox.Controls.Clear();
                addDays(globalDate);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
