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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YWCA_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbConnector db = new DbConnector();

        public MainWindow()
        {
            InitializeComponent();
            textBoxFName.DataContext = db;
            textBoxLName.DataContext = db;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            db.RunQueryFNameFromPID(textBoxPID.Text);
            db.RunQueryLNameFromPID(textBoxPID.Text);
        }
    }
}
