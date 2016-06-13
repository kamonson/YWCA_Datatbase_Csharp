using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for EcapVolunteer.xaml
    /// </summary>
    public partial class EcapVolunteer : Window
    {
        /// <summary>
        /// No arg constructor for blank form
        /// </summary>
        private readonly DbConnector _ecapDB = new DbConnector();
        private readonly Sql _sql = new Sql();

        public EcapVolunteer()
        {
            InitializeComponent();
            DataContext = _ecapDB;
            _ecapDB.EcapVHours("select");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            EcapMenu ecapWindow = new EcapMenu();
            ecapWindow.Show();
            Close();
        }

        private void ecapupdate_button_Click(object sender, RoutedEventArgs e)
        {
            _ecapDB.EcapVHours("update");
            EcapMenu ecapWindow = new EcapMenu();
            ecapWindow.Show();
            Close();
        }
    }
}
