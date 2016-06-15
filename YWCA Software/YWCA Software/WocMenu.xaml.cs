using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for WocMenu.xaml
    /// </summary>
    public partial class WocMenu : Window
    {
        private string aptDate;
        private string classDate;
        private readonly DbConnector _wocDb = new DbConnector();
        private readonly Sql _sql = new Sql();
        public WocMenu()
        {
            InitializeComponent();
        }

        public WocMenu(string pid)
        {
            InitializeComponent();
            DataContext = _wocDb;
            _wocDb.SetPid(pid);
            _wocDb.RunQueryFindDateWocApt();
            _wocDb.RunQueryFindDateWocClass();
            aptDate = _wocDb.WocDateApt;
            classDate = _wocDb.WocDateClass;
            _wocDb.WocAppt("select", pid, aptDate);
            _wocDb.WocClass("select", pid, classDate);
            textBlockPid.Text = pid;
        }

        /// <summary>
        /// Runs update intake form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender == buttonUpdate)
            {
                _wocDb.WocAppt("update", textBlockPid.Text, aptDate);
            }
            if (sender == buttonUpdateADVP)
            {
                _wocDb.WocClass("update", textBlockPid.Text, classDate);
            }
        }

        /// <summary>
        /// Highlight information if focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = (System.Windows.Controls.TextBox)sender; /// explicitly says which class Textbox is from
            s.SelectAll();
        }

        /// <summary>
        /// Go back to participant select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var psWindow = new ParticipantSelect(@"WOCAPPT");
            psWindow.Show();
            Close();
        }

        /// <summary>
        /// Update PID, First name, Last name eachtime different PID is changed in list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == listBoxAptDate)
            {
                DataContext = _wocDb;
                string pid = textBlockPid.Text;
                aptDate = listBoxAptDate.SelectedItem?.ToString();
                _wocDb.SetIntakeDate(aptDate);

                _wocDb.WocAppt(@"select", pid, aptDate);
            }
            if (sender == listBoxAptDate_Copy)
            {
                DataContext = _wocDb;
                aptDate = listBoxAptDate.SelectedItem?.ToString();
                string pid = textBlockPid.Text;
                classDate = listBoxAptDate_Copy.SelectedItem?.ToString();
                _wocDb.WocClass("select", pid, classDate);
            }
        }

        private void buttonAddNewDate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender == buttonAddNewDate)
            {
                _wocDb.RunQueryAddWocAptDate(textBlockPid.Text, textBoxNewDate.Text);
                _wocDb.RunQueryFindDateWocApt(); //needs to be re worked some
            }
           else if (sender == buttonAddNewDate_Copy)
            {
                _wocDb.RunQueryAddWocClassDate(textBlockPid.Text, textBoxNewDate_Copy.Text);
                _wocDb.RunQueryFindDateWocClass(); //needs to be re worked some
            }
            else
            {
                string message = "A date to store this information on is not selected. \n";
                string caption = "Please choose a date and try again.";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            }

        }

        /// <summary>
        /// update button for lower half of window
        /// includes a message box to help reduce crashes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateADVP_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /// Message box to make sure date (from bottom half) is selected
            /// or that correct update button is pushed
            if (listBoxAptDate.SelectedItem != null)
            {
                string date = listBoxAptDate.SelectedItem?.ToString();
                string pid = textBlockPid.Text;

                _wocDb.Advp(@"update", pid, date);
            }
            else
            {
                string message = "A date to store this information on is not selected. \n";
                string caption = "Please choose a date and try again.";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            }
        }

        private void checkForEnter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == (System.Windows.Input.Key)Keys.Enter)
            {
                if (richTextBox_Copy.IsFocused)
                {
                    richTextBox_Copy.AppendText("\n");
                }
                if (richTextBox_Copy1.IsFocused)
                {
                    richTextBox_Copy1.AppendText("\n");
                }
                if (richTextBox_Copy2.IsFocused)
                {
                    richTextBox_Copy2.AppendText("\n");
                }
            }
        }
    }
}
