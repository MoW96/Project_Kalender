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
using System.Threading;

namespace Project_Kalender
{
    /// <summary>
    /// Interaktionslogik für Entry.xaml
    /// </summary>
    public partial class Entry : Page, Interface_Kalender
    {
        public string EMailAdresse;
        Thread circleThread;
        public Entry()
        {
            InitializeComponent();
            btn_Kalender.ToolTip = "Kalender";
            btn_Termin.ToolTip = "Termine";
            btn_Einstellungen.ToolTip = "Einstellungen";

            circleThread = new Thread(new ThreadStart(movingCircle));
            circleThread.IsBackground = true;
            circleThread.SetApartmentState(ApartmentState.STA);
            circleThread.Start();

            EingabeMail();
        }

        public bool AllowsBack => false;

        public bool AllowsHome => false;


        public Interface_Kalender Previous { get; set; }
        public event NavigationRequestEventHandler NavigationRequest;

        private void EingabeMail()
        {
            if (get_MailFromDB().Equals("") || get_MailFromDB() == null)
            {
                // Einstellungen erstellen in denen die Mail geändert werden kann

                MailEingabe mailEingabe = new MailEingabe();
                mailEingabe.ShowDialog();
                EMailAdresse = mailEingabe.Mail;
                string sql_Add = "INSERT INTO tblUser ([Username],[MailAdresse],[PerMailSenden],[DateiSchreiben]) VALUES('" + Environment.UserName + "','" + EMailAdresse + "','" + "Ja" + "','" + "Ja" + "')";
                clsDB.Execute_SQL(sql_Add);
            }
        }

        private string get_MailFromDB()
        {
            string sSQL = "SELECT MailAdresse FROM tblUser WHERE [Username] = '" + Environment.UserName + "'";
            
            return clsDB.Get_String(sSQL, "Mail");
        }

        private void btn_Kalender_Click(object sender, RoutedEventArgs e)
        {
            NavigationRequest?.Invoke(this, new KalenderPage());
            //circleThread.Abort();
        }

        private void btn_Termin_Click(object sender, RoutedEventArgs e)
        {
            NavigationRequest?.Invoke(this, new TerminePage());
            //circleThread.Abort();
        }

        private void movingCircle()
        {
            while (true)
            {
                for (int i = 0; i <= 175; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetTop(ellipse, i);
                        Canvas.SetTop(ellipse2, 175 - i);
                        Canvas.SetLeft(ellipse1, i);
                        Canvas.SetLeft(ellipse3, 175 - i);
                        //ellipse4.Height = ellipse4.Height + (i / 150);
                        //ellipse4.Width = ellipse4.Width + (i / 150);

                        changeColorEllipse();
                    });
                    Thread.Sleep(5);         
                }

                for (int i = 0; i <= 175; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(ellipse, i);
                        Canvas.SetLeft(ellipse2, 175 - i);
                        Canvas.SetTop(ellipse1, 175 - i);
                        Canvas.SetTop(ellipse3, i);
                        //ellipse4.Width = ellipse4.Width - (i / 150);
                        //ellipse4.Height = ellipse4.Height - (i / 150);

                        changeColorEllipse();
                    });
                    Thread.Sleep(5);
                }

                for (int i = 175; i >= 0; i--)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetTop(ellipse, i);
                        Canvas.SetTop(ellipse2, 175 - i);
                        Canvas.SetLeft(ellipse1, i);
                        Canvas.SetLeft(ellipse3, 175 - i);
                        //ellipse4.Height = ellipse4.Height + (i / 150);
                        //ellipse4.Width = ellipse4.Width + (i / 150);

                        changeColorEllipse();
                    });
                    Thread.Sleep(5);
                }

                for (int i = 175; i >= 0; i--)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(ellipse, i);
                        Canvas.SetLeft(ellipse2, 175 - i);
                        Canvas.SetTop(ellipse1, 175 - i);
                        Canvas.SetTop(ellipse3, i);
                        //ellipse4.Width = ellipse4.Width - (i / 150);
                        //ellipse4.Height = ellipse4.Height - (i / 150);

                        changeColorEllipse();
                    });
                    Thread.Sleep(5);                    
                }
            }
        }

        private void changeColorEllipse()
        {
            if (ellipse.IsMouseDirectlyOver == true || ellipse2.IsMouseDirectlyOver == true)
            {
                ellipse.Fill = Brushes.Navy;
                ellipse2.Fill = Brushes.Navy;
            }
            else
            {
                ellipse.Fill = Brushes.DarkRed;
                ellipse2.Fill = Brushes.DarkRed;
            }

            if (ellipse1.IsMouseDirectlyOver == true || ellipse3.IsMouseDirectlyOver == true)
            {
                ellipse1.Fill = Brushes.Gold;
                ellipse3.Fill = Brushes.Gold;
            }
            else
            {
                ellipse1.Fill = Brushes.Gray;
                ellipse3.Fill = Brushes.Gray;
            }

            //if (ellipse4.IsMouseDirectlyOver == true)
            //{
            //    ellipse4.Fill = Brushes.Purple;
            //}
            //else
            //{
            //    ellipse4.Fill = Brushes.DarkGreen;
            //}
        }

        private void btn_Einstellungen_Click(object sender, RoutedEventArgs e)
        {
            NavigationRequest?.Invoke(this, new Einstellungen());
        }
    }
}
