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
    /// Interaction logic for ParticipantSelect.xaml
    /// </summary>
    public partial class ParticipantSelect : Window
    {
        DbConnector AdvbDb = new DbConnector();

        public ParticipantSelect()
        {
            InitializeComponent();
            DataContext = AdvbDb;
        }

        private void textBoxFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text == @"First Name")
            {
                textBoxFirstName.Text = "";
            }
        }

        private void textBoxLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLastName.Text == @"Last Name")
            {
                textBoxLastName.Text = "";
            }
        }

        private void textBoxParticipantID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxParticipantID.Text == @"Participant ID")
            {
                textBoxParticipantID.Text = "";
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            AdvbDb.RunQueryFindClient(textBoxFirstName.Text, textBoxLastName.Text, textBoxParticipantID.Text);
        }

        private void ButtonSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonSearch_Click(sender, e);
            }
        }

        private void buttonSelectParticipant_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
