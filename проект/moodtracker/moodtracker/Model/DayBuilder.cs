using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day = moodtracker.Model.Day;


namespace moodtracker
{
    public class DayBuilder
    {
        private DateTime _selectedDate;
        private int _selectedMood;
        private string _note;

        public DayBuilder SetSelectedDate(DateTime selectedDate)
        {
            _selectedDate = selectedDate;
            return this;
        }

        public DayBuilder SetSelectedMood(int selectedMood)
        {
            _selectedMood = selectedMood;
            return this;
        }

        public DayBuilder SetNote(string note)
        {
            _note = note;
            return this;
        }

        public Day Build()
        {
            return new Day(_selectedDate, _selectedMood, _note);
        }
    }
}
