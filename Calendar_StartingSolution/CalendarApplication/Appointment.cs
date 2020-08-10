using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using static System.String;

namespace Calendar
{
    public partial class Appointment : Form
    {
        public SingleAppointments _SingleAppointments = null;
        const int MAX = 50;

        public Appointment()
        {
            InitializeComponent();
        }

        public SingleAppointments SingleAppointments
        {
            get
            {
                return _SingleAppointments;
            }
            set
            {
                _SingleAppointments = value;
            }
        }

        public void LoadForEdit(ICalendarEntry calendarEntry)
        {
            if (calendarEntry.DisplayText != null)
            {
                string[] arr;
                string[] subArr;
                string[] locArr;

                arr = calendarEntry.DisplayText.Split(new char[] { ',' });
                subArr = arr[0].Split(new char[] { ':' });
                locArr = arr[1].Split(new char[] { ':' });

                txtSubject.Text = subArr[1].TrimStart();
                txtLocation.Text = locArr[1].TrimStart();

                cboLength.Text = calendarEntry.Length.ToString();
                cboStartTime.Text = calendarEntry.Start.ToString("HH:mm:ss");
            }
        }

        public void Appointment_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;

            //Limitations to text boxes
            txtSubject.MaxLength = MAX;
            txtLocation.MaxLength = MAX;

            BoxCharacteristics();

            //Displays chosen date on label
            if (lblDisplayedDate.Text == null)
            {
                lblDisplayedDate.Text = now.ToString();
            }
            else
            {
                lblDisplayedDate.Text = MainForm.passDate;
            }
        }

        private void BoxCharacteristics()
        {
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog; //prevent resizing
            CenterToScreen(); //box to centre screen
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        DateTime start;
        int leng;
        string displayText;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSubject.Text == "" || txtLocation.Text == "" || cboLength.SelectedItem == null || cboStartTime.SelectedItem == null)
            {
                MessageBox.Show("Error, inputs need to be entered.");
            }
            else
            {
                /*Getting user input*/
                start = new DateTime();

                DateTime chosenDate = DateTime.Parse(lblDisplayedDate.Text);
                TimeSpan startTime = TimeSpan.Parse(cboStartTime.Text);
                start = chosenDate + startTime;

                int apptLength = cboLength.SelectedIndex + 1;
                leng = apptLength * 30;
                string sub = txtSubject.Text;
                string loc = txtLocation.Text;
                string savedData = start + "\t" + displayText + "\t" + leng;

                displayText = "Subject: " + sub + ", Location: " + loc;

                _SingleAppointments = new SingleAppointments(savedData);
                /*Passing user input*/
                _SingleAppointments.SavedData = start + "\t" + displayText + "\t" + leng;
                _SingleAppointments.Start = start;
                _SingleAppointments.Length = leng;
                _SingleAppointments.DisplayText = displayText;

                DialogResult = DialogResult.OK;
            }          
        }

        private void lblDisplayedDate_Click(object sender, EventArgs e)
        {
        }
    }
}
