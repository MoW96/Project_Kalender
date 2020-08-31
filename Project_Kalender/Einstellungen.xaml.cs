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

namespace Project_Kalender
{
    /// <summary>
    /// Interaktionslogik für Einstellungen.xaml
    /// </summary>
    public partial class Einstellungen : Page, Interface_Kalender
    {
        public Einstellungen()
        {
            InitializeComponent();

            tbMail.Text = get_MailFromDB();

            if (get_SendenFromDB().Equals("Ja"))
            {
                radioMailJa.IsChecked = true;
                radioMailNein.IsChecked = false;
            }
            else
            {
                radioMailJa.IsChecked = false;
                radioMailNein.IsChecked = true;
            }
        }

        public bool AllowsBack => true;

        public bool AllowsHome => true;

        public Interface_Kalender Previous { get; set; }

        public event NavigationRequestEventHandler NavigationRequest;

        private string get_MailFromDB()
        {
            string sSQL = "SELECT MailAdresse FROM tblUser WHERE [Username] = '" + Environment.UserName + "'";

            return clsDB.Get_String(sSQL, "Mail");
        }

        private string get_SendenFromDB()
        {
            string sSQL = "SELECT PerMailSenden FROM tblUser WHERE [Username] = '" + Environment.UserName + "'";

            return clsDB.Get_String(sSQL, "PerMailSenden");
        }

        private void btnMailÄndern_Click(object sender, RoutedEventArgs e)
        {
            MailEingabe mailEingabe = new MailEingabe();
            mailEingabe.ShowDialog();
            string sql_Update = "UPDATE tblUser SET MailAdresse = '" + mailEingabe.Mail + "' WHERE Username = '" + Environment.UserName + "'";
            clsDB.Execute_SQL(sql_Update);

            tbMail.Text = get_MailFromDB();
        }

        private void radioMailJa_Checked(object sender, RoutedEventArgs e)
        {
            string sql_Update = "UPDATE tblUser SET PerMailSenden = '" + "Ja" + "' WHERE Username = '" + Environment.UserName + "'";
            clsDB.Execute_SQL(sql_Update);

            radioMailJa.IsChecked = true;
            radioMailNein.IsChecked = false;
        }

        private void radioMailNein_Checked(object sender, RoutedEventArgs e)
        {
            string sql_Update = "UPDATE tblUser SET PerMailSenden = '" + "Nein" + "' WHERE Username = '" + Environment.UserName + "'";
            clsDB.Execute_SQL(sql_Update);

            radioMailJa.IsChecked = false;
            radioMailNein.IsChecked = true;
        }
    }
}
