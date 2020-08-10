using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class Recurring_Appointment : Form
    {
        public RecurringAppointments _RecurringAppointments = null;

        const int MIN = 1;
        const int MAX = 50;
        const int MAXCHAR = 3;

        public Recurring_Appointment()
        {
            InitializeComponent();
        }

        public RecurringAppointments RecurringAppointments
        {
            get
            {
                return _RecurringAppointments;
            }
            set
            {
                _RecurringAppointments = value;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Recurring_Appointment_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;

            //Displays chosen date on label
            if (lblDisplayedDate.Text == null)
            {
                lblDisplayedDate.Text = now.ToString();
            }
            else
            {
                lblDisplayedDate.Text = MainForm.passDate;
            }

            //Limitations to text boxes
            txtSubject.MaxLength = MAX;
            txtLocation.MaxLength = MAX;
            txtOccurences.MaxLength = MAXCHAR;

            BoxCharacteristics();

            //Adding frequency attributes to the recurring combo box
            foreach (var obj in Enum.GetValues(typeof(RecurringFrequency)))
            {
                cboFrequency.Items.Add(obj);
            }

            /*Setting default values*/
            //cboStartTime.SelectedIndex = 18;
            //cboLength.SelectedIndex = 0;
            cboFrequency.SelectedIndex = 0;
            txtOccurences.Text = "1";
        }

        private void BoxCharacteristics()
        {
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen(); 
        }

        DateTime start;
        int leng;
        string displayText;
        RecurringFrequency freq;
        int freqAmount = 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOccurences.Text, out freqAmount) || int.Parse(txtOccurences.Text) < MIN)
            {
                MessageBox.Show("Error, enter number between 1 and 999.");
            }
            else if (txtSubject.Text == "" || txtLocation.Text == "" || cboLength.SelectedItem == null || cboStartTime.SelectedItem == null)
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

                Enum.TryParse(cboFrequency.Text, out freq);

                string savedData = start + "\t" + displayText + "\t" + leng + "\t" + freq + "\t" + freqAmount;

                displayText = "Subject: " + sub + ", Location: " + loc + ", Frequency: " + freq + ", Amount: " + freqAmount;

                _RecurringAppointments = new RecurringAppointments(savedData);

                /*Passing user input*/
                _RecurringAppointments.SavedData = start + "\t" + displayText + "\t" + leng + "\t" + freq + "\t" + freqAmount;
                _RecurringAppointments.Start = start;
                _RecurringAppointments.Length = leng;
                _RecurringAppointments.DisplayText = displayText;
                _RecurringAppointments.RecurringData = freq;
                _RecurringAppointments.FrequencyAmount = freqAmount;

                DialogResult = DialogResult.OK;
            }
        }

        private void txtOccurences_TextChanged(object sender, EventArgs e)
        {     
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}
