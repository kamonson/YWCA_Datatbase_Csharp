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
        }
    }
}
