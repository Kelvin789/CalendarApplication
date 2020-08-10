using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class RecurringAppointments : Appointment, ICalendarEntry
    {
        DateTime _Start;
        int _Length;
        string _DisplayText;
        string _SavedData;
        public string[] arr;

        RecurringFrequency _RecurringData;
        int _FrequencyAmount;

        public RecurringAppointments(string saved)
        {
            _SavedData = saved;

            /*splitting the inputs*/
            arr = saved.Split(new char[] { '\t' });

            _Start = DateTime.Parse(arr[0]);
            _DisplayText = arr[1];
            _Length = int.Parse(arr[2]);
            _FrequencyAmount = int.Parse(arr[4]);

            Start = _Start;
            DisplayText = _DisplayText;
            Length = _Length;
            RecurringData = _RecurringData;
            FrequencyAmount = _FrequencyAmount;

            switch (arr[3])
            {
                case "Daily":
                    _RecurringData = RecurringFrequency.Daily;
                    break;
                case "Weekly":
                    _RecurringData = RecurringFrequency.Weekly;
                    break;
                case "Fortnightly":
                    _RecurringData = RecurringFrequency.Fortnightly;
                    break;
                case "Monthly":
                    _RecurringData = RecurringFrequency.Monthly;
                    break;
                case "Yearly":
                    _RecurringData = RecurringFrequency.Yearly;
                    break;
                default:
                    break;
            }
        }

        public RecurringAppointments()
        {
        }

        public RecurringFrequency RecurringData
        {
            get
            {
                return _RecurringData;
            }
            set
            {
                _RecurringData = value;
            }
        }

        public int FrequencyAmount
        {
            get
            {
                return _FrequencyAmount;
            }
            set
            {
                _FrequencyAmount = value;
            }
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
            DateTime day;
            List<DateTime> _DateStorage = new List<DateTime>(); //List for dates
            int x = 0;

            switch (_RecurringData) //Switch for frequency
            {
                case RecurringFrequency.Daily: //Implementing the day differences
                    for (x = 0; x <= _FrequencyAmount; x++)
                    {
                        day = _Start.AddDays(x);
                        _DateStorage.Add(day);
                    }
                    break;
                case RecurringFrequency.Weekly:
                    for (x = 0; x <= _FrequencyAmount; x++)
                    {
                        day = _Start.AddDays(x * 7);
                        _DateStorage.Add(day);
                    }
                    break;
                case RecurringFrequency.Fortnightly:
                    for (x = 0; x <= _FrequencyAmount; x++)
                    {
                        day = _Start.AddDays(x * 14);
                        _DateStorage.Add(day);
                    }
                    break;
                case RecurringFrequency.Monthly:
                    for (x = 0; x <= _FrequencyAmount; x++)
                    {
                        day = _Start.AddMonths(x);
                        _DateStorage.Add(day);
                    }
                    break;
                case RecurringFrequency.Yearly:
                    for (x = 0; x <= _FrequencyAmount; x++)
                    {
                        day = _Start.AddYears(x);
                        _DateStorage.Add(day);
                    }
                    break;
                default:
                    break;
            }

            foreach (DateTime d in _DateStorage) //Comparing dates
            {
                if (d.Date == date.Date)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
