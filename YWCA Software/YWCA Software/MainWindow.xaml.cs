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
            DbConnector dc = new DbConnector();
        }
        /// <summary>
        /// Open Advp database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ADVP_Button)
            {
                ParticipantSelect ps = new ParticipantSelect(@"ADVP");
                ps.Show();
            }
            else if(sender == ECAP_Button)
            {
                EcapMenu em = new EcapMenu();
                em.Show();
            }
            else if(sender == WOC_Button)
            {
                WocMenu wm = new WocMenu();
                wm.Show();
            }
            
 
            Close();
        }

        private void WOCClass_Click(object sender, RoutedEventArgs e)
        {
            DbConnector dc = new DbConnector();
            dc.WocClass("select", "aaatest1", "10/13/1993");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DbConnector dc = new DbConnector();
            dc.WocAppt("select", "aaatest1", "10/13/1993");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DbConnector dc = new DbConnector();
            dc.WocCompLog("select", "aaatest1");
        }
    }
}
