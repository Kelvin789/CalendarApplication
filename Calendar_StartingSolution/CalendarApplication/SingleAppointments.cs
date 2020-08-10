using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class SingleAppointments : ICalendarEntry
    {
        DateTime _Start; 
        int _Length;
        string _DisplayText;
        string _SavedData;
        string[] arr;

        public SingleAppointments(string saved)
        {
            _SavedData = saved;
            /*splitting the inputs*/
            arr = saved.Split(new char[] { '\t' });

            _Start = DateTime.Parse(arr[0]);
            _DisplayText = arr[1];
            _Length = int.Parse(arr[2]);

            Start = _Start;
            DisplayText = _DisplayText;
            Length = _Length;
        }

        public SingleAppointments()
        {
        }

        public DateTime Start
        {
            get
            {
                return _Start;
            }
            set
            {
                _Start = value;
            }
        }

        public int Length
        {
            get
            {
                return _Length;
            }
            set
            {
                _Length = value;
            }
        }

        public string DisplayText
        {
            get
            {
                return _DisplayText;
            }
            set
            {
                _DisplayText = value; 
            }
        }

        public string SavedData
        {
            get
            {
                return _SavedData;
            }
            set
            {
                _SavedData = value;
            }
        }

        public bool OccursOnDate(DateTime date)
        {
            if (date.Date == Start.Date)
            {
                return true;
            }

            return false; //return true;

        }
    }
}
