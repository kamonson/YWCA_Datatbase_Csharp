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
    /// Interaction logic for EcapMenu.xaml
    /// </summary>
    public partial class EcapMenu : Window
    {
        public EcapMenu()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ecapintake_Button)
            {
                ParticipantSelect ps = new ParticipantSelect(@"ECAP");
                ps.Show();
            }
            else if (sender == ecapvolunteer_Button)
            {
                EcapMenu em = new EcapMenu();
                em.Show();
            }

            Close();
        }
    }
}
