using System;
using System.Linq;
using System.Windows.Controls;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for IntakeForm.xaml
    /// </summary>
    public partial class IntakeForm
    {
        /// <summary>
        /// No arg constructor for blank form
        /// </summary>
        private readonly DbConnector _advbDb = new DbConnector();
        private readonly Sql _sql = new Sql();
        public IntakeForm()
        {
            InitializeComponent();
            DataContext = _advbDb;
        }
        /// <summary>
        /// Use PID to Generate form with data
        /// </summary>
        /// <param name="pid"></param>
        public IntakeForm(string pid)
        {
            InitializeComponent();
            DataContext = _advbDb;
            _advbDb.SetPid(pid);
            _advbDb.Demographics("select", pid);
            _advbDb.RunQueryFindDate();
        }
        /// <summary>
        /// Runs update intake form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] table = { "tbl_Intake", "tbl_Forms_Flow_Table" };
            var pid = textBlockPid.Text;
            var end = @" Where Consumer_ID = " + pid + @";";
            foreach (var i in table.Where(i => !DbConnector.QueryTest(_sql.Select(@"Consumer_ID") + _sql.From(i) + end)))
            {
                DbConnector.RunQuery(_sql.InsertInto(i) + @"(Consumer_ID) " + _sql.Values(pid) + @";");
                Console.WriteLine(@"One item added to the database");
            }
            _advbDb.Demographics("update", textBlockPid.Text);
        }
        /// <summary>
        /// Highlight information if focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = (TextBox)sender;
            s.SelectAll();
        }
        /// <summary>
        /// Go back to participant select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var psWindow = new ParticipantSelect();
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
            string date = listBoxIntakeDate.SelectedItem?.ToString();
            string pid = textBlockPid.Text;

            _advbDb.Advp(@"select", pid, date);
        }

        private void buttonAddNewDate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _advbDb.RunQueryAddDate(textBlockPid.Text, textBoxNewDate.Text);
            _advbDb.RunQueryFindDate();
        }

        private void buttonUpdateADVP_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string date = listBoxIntakeDate.SelectedItem?.ToString();
            string pid = textBlockPid.Text;

            _advbDb.Advp(@"update", pid, date);
        }
    }
}
