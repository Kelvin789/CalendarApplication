using System;
using System.Collections.Generic;
using System.IO;

namespace Calendar
{
    public class CalendarEntries : List<ICalendarEntry>
     {
        public bool Load(string calendarEntriesFile)
        {
            // TODO.  Add your code to load the data from the file specified in
            //        calendarEntriesFile here.  You can edit the following two 
            //        lines if you wish.          

            StreamReader fileReader = null;
            string lineReadFromFile;
            string[] arr;
            bool status = true;

            try
            {
                // Open the file for reading
                fileReader = new StreamReader(calendarEntriesFile);

                while (!fileReader.EndOfStream)
                {
                    lineReadFromFile = fileReader.ReadLine();
                    arr = lineReadFromFile.Split(new char[] { '\t' });
                    /*Differentiate single appointment and recurrring*/
                    if (arr.Length == 3)
                    {
                        this.Add(new SingleAppointments(lineReadFromFile));
                    }
                    else
                    {
                        this.Add(new RecurringAppointments(lineReadFromFile));
                    }
                }
            }
            catch
            {
                status = false;
            }
            finally
            {
                if (fileReader != null)
                {
                    fileReader.Close();
                }
            }

            return status;
        }

        public bool Save(string calendarEntriesFile)
        {
            // TODO.  Add your code to save the collection to the file specified in
            //        calendarEntriesFile here.  You can edit the following two 
            //        lines if you wish.
            bool status = true;
            StreamWriter textOut = new StreamWriter(calendarEntriesFile, false);

            try
            {
                foreach (ICalendarEntry m in this)
                {
                    textOut.WriteLine(m.SavedData, calendarEntriesFile);
                }
            }
            catch
            {
                status = false;
            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }

            return status;
        }

        // Iterate through the collection, returning the calendar entries that
        // occur on the specified date

        public IEnumerable<ICalendarEntry> GetCalendarEntriesOnDate(DateTime date)
        {
            for (int i = 0; i < this.Count; i++ )
            {
                if (this[i].OccursOnDate(date))
                {
                    yield return this[i];                
                }
            }
        }
    }
}