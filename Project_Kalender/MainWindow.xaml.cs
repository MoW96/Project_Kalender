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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Interface_Kalender currentPage = null;
        public MainWindow()
        {
            InitializeComponent();

            Username.Text = "User: " + Environment.UserName;

            //if (get_UserFromDB().Equals("") || get_UserFromDB() == null)
            //{
            //    string sql_Add = "INSERT INTO tblUser ([Username],[MailAdresse]) VALUES('" + Environment.UserName + "','" + "" + "')";
            //    clsDB.Execute_SQL(sql_Add);
            //}

            NavigateToPage(new Entry());
        }

        private void NavigateToPage(Interface_Kalender page, bool back = false)
        {
            if (!back)
            {
                page.Previous = currentPage;
            }

            //Event-Handler registrieren: hier dazu genutzt, dass Pages dem Fenster mitteilen können welche page angezeigt werden soll
            page.NavigationRequest += Page_NavigationRequest;
            if (currentPage != null)
            {
                //Event-Handler von der vorherigen Page entfernen: 
                currentPage.NavigationRequest -= Page_NavigationRequest;
            }
            currentPage = page;
            headerText.Text = (page as Page).Title;
            cmdBack.IsEnabled = page.AllowsBack;
            cmdHome.IsEnabled = page.AllowsHome;
            frame.Content = page as Page;
        }

        private string get_UserFromDB()
        {
            string sSQL = "SELECT Username FROM tblUser WHERE [Username] = '" + Environment.UserName + "'";

            return clsDB.Get_String(sSQL, "User");
        }

        private void Page_NavigationRequest(object sender, Interface_Kalender page)
        {
            NavigateToPage(page);
        }

        //Button in Pages extra einfügen -> wenn nicht: Loop
        private void CmdReturn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage.AllowsBack)
            {
                NavigateToPage(currentPage.Previous, false);
            }
        }

        private void CmdBack_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage.AllowsBack)
            {
                NavigateToPage(currentPage.Previous, true);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            //Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            //e.Handled = true;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new Entry());
        }

        private void Cmd_ImpressumClick(object sender, RoutedEventArgs e)
        {
           // NavigateToPage(impressumPage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
