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
        private string _department;
        DbConnector dbc = new DbConnector(); //ViewModel for data binding
        /// <summary>
        /// Initialize window and Update dataContext
        /// </summary>
        public ParticipantSelect()
        {
            InitializeComponent();
            DataContext = dbc;
        }

        public ParticipantSelect(string department)
        {
            InitializeComponent();
            DataContext = dbc;
            _department = department;
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
            dbc.RunQueryFindClient(_department);
        }
        /// <summary>
        /// Create intake form with selected PID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (_department == @"ADVP")
            {
                IntakeForm intakeForm = new IntakeForm(textBoxParticipantID.Text);
                intakeForm.Show();
            }
            else if(_department == @"ECAP")
            {
                Ecap ecap = new Ecap(textBoxParticipantID.Text);
                ecap.Show();
            }
            else if(_department == @"ECAP_VHOURS")
            {

            }
            else if(_department == @"WOC_EPT")
            {

            }
            else if(_department == @"WOC_CLASS")
            {

            }
            else if(_department == @"WOC_COMPLOG")
            {

            }
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
            dbc.SetPid(pid);
            dbc.RunQueryFNameFromPid(@"select");
            dbc.RunQueryLNameFromPid(@"select");
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

        //private void buttonNewParticipant_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (!DbConnector.CheckForExistingPid(textBoxNewParticipantID.Text))
        //    //{
        //    //    DbConnector.RunQuery(@"INSERT INTO tbl_Consumer_List_Entry (Consumer_Id) VALUES ('" +
        //    //                         textBoxNewParticipantID.Text + @"');");
        //    //}
        //    //IntakeForm intakeForm = new IntakeForm(textBoxNewParticipantID.Text);
        //    //intakeForm.Show();
        //    //Close();
        //}

        private void buttonNewParticipantWindow_Click(object sender, RoutedEventArgs e)
        {
            CreateNewParticipant create = new CreateNewParticipant(_department);
            create.Show();
            Close();
        }
    }
}
