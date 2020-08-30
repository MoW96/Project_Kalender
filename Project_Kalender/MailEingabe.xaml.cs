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

namespace Project_Kalender
{
    /// <summary>
    /// Interaktionslogik für MailEingabe.xaml
    /// </summary>
    public partial class MailEingabe : Window
    {

        public string Mail;

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public MailEingabe()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
           Mail= tbMailAdress.Text;

            this.Close();
        }

        private void tbMailAdress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsValidEmail(tbMailAdress.Text))
            {
                btnSave.IsEnabled = true;
            }
            else
            {
                btnSave.IsEnabled = false;
            }
        }
    }
}
