using System.Windows;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes Main Window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Open Advp database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonADVP_Click(object sender, RoutedEventArgs e)
        {
            ParticipantSelect ps = new ParticipantSelect();
            ps.Show();
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DbConnector dc = new DbConnector();
            dc.Ecap("select","aaatest1");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DbConnector dc = new DbConnector();
            dc.EcapVHours("select", "spokane");
        }
    }
}
