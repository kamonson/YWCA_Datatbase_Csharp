using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for ParticipantSelect.xaml
    /// </summary>
    public partial class ParticipantSelect
    {
        DbConnector AdvbDb = new DbConnector(); //ViewModel for data binding
        /// <summary>
        /// Initialize window and Update dataContext
        /// </summary>
        public ParticipantSelect()
        {
            InitializeComponent();
            DataContext = AdvbDb;
        }
        /// <summary>
        /// Clear text on focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text == @"First Name")
            {
                textBoxFirstName.Text = "";
            }
        }
        /// <summary>
        /// Clear text on focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLastName.Text == @"Last Name")
            {
                textBoxLastName.Text = "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxParticipantID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxParticipantID.Text == @"Participant ID")
            {
                textBoxParticipantID.Text = "";
            }
        }
        /// <summary>
        /// Fetch PID list from first name AND last name OR PID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            AdvbDb.RunQueryFindClient();
        }
        /// <summary>
        /// Create intake form with selected PID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectParticipant_Click(object sender, RoutedEventArgs e)
        {
            IntakeForm intakeForm = new IntakeForm(textBoxParticipantID.Text);
            intakeForm.Show();
            Close();
        }
        /// <summary>
        /// Update PID, First name, Last name eachtime different PID is changed in list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string pid = listBoxPID.SelectedItem?.ToString();
            textBoxParticipantID.Text = pid;
            AdvbDb.SetPid(pid);
            AdvbDb.RunQueryFNameFromPid(@"select");
            AdvbDb.RunQueryLNameFromPid(@"select");
        }
        /// <summary>
        /// Run search with enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listBoxPID.Focus();
                ButtonSearch_Click(sender, e);
            }
        }
        /// <summary>
        /// Run search with enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxParticipantID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listBoxPID.Focus();
                ButtonSearch_Click(sender, e);
            }
        }
        /// <summary>
        /// Start Intake with enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxPID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonSelectParticipant_Click(sender, e);
            }
        }
    }
}
