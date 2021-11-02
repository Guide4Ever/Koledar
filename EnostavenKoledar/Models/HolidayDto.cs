using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnostavenKoledar.Models
{
    public class HolidayDto
    {
        public DateTime HolidayDate {  get; set; }

        public bool IsRepeated { get; set; }
    }
}
