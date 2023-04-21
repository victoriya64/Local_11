using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moodtracker.Repositories
{
    internal class Day
    {
        public string username;
        public string date;
        public int mood;
        public string note;

        public Day(string date, int mood, string note, string username)
        {
            this.date = date;
            this.mood = mood;
            this.note = note;
            this.username = username;
        }
    }
}
