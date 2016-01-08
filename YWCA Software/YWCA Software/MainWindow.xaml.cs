using System.Windows;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes Main Window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Open ADVP database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonADVP_Click(object sender, RoutedEventArgs e)
        {
            ParticipantSelect ps = new ParticipantSelect();
            ps.Show();
            Close();
        }
    }
}
