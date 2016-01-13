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
        readonly DbConnector _advbDb = new DbConnector();//viewModel for data binding
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
            _advbDb.GetIntakeForm(pid);
        }

        private void buttonUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _advbDb.SetPid(textBlockPid.Text);
            _advbDb.RunQueryFNameFromPid(@"Set");
            _advbDb.RunQueryMInitialFromPid(@"Set");
            _advbDb.RunQueryLNameFromPid(@"Set");
            _advbDb.RunQueryDobFromPid(@"Set");
        }

        private new void GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = (TextBox) sender;
            s.SelectAll();
        }

        private void buttonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ParticipantSelect psWindow = new ParticipantSelect();
            psWindow.Show();
            Close();
        }
    }
}
