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
    /// Interaction logic for CreateNewParticipant.xaml
    /// </summary>
    /// 
    public partial class CreateNewParticipant : Window
    {
        private string _department;
        public CreateNewParticipant()
        {
            InitializeComponent();
        }

        public CreateNewParticipant(string department)
        {
            _department = department;
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

             if(sender == Back_Button_Participant)
            {
                ParticipantSelect ps = new ParticipantSelect();
                ps.Show();
            }
            Close();
        }

        private void buttonNewParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (_department == @"ADVP")
            {
                if (!DbConnector.CheckForExistingPid(textBoxNewParticipantID.Text))
                {
                    DbConnector.RunQuery(@"INSERT INTO tbl_Consumer_List_Entry (Consumer_Id) VALUES ('" +
                                         textBoxNewParticipantID.Text + @"');");
                }
                IntakeForm intakeForm = new IntakeForm(textBoxNewParticipantID.Text);
                intakeForm.Show();
            }
            else if (_department == @"ECAP")
            {
                if (!DbConnector.CheckForExistingPid(textBoxNewParticipantID.Text))
                {
                    DbConnector.RunQuery(@"INSERT INTO ECAP (Ecap_Id) VALUES ('" +
                                         textBoxNewParticipantID.Text + @"');");
                }
                Ecap ecap = new Ecap(textBoxNewParticipantID.Text);
                ecap.Show();
            }
            Close();
        }

        private void textBoxNewParticipantID_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxNewParticipantID.Text = @"";
        }

        private void textBoxNewParticipantID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonNewParticipant_Click(sender, e);
            };
        }
    }
}
