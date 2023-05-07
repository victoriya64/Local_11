using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moodtracker.Model
{
    public class Day
    {
        public DateTime Date { get; set; }
        public int Mood { get; set; }
        public string Note { get; set; }
        public Day(DateTime date, int mood, string note)
        {
            Date = date;
            Mood = mood;
            Note = note;
        }
    }
}
