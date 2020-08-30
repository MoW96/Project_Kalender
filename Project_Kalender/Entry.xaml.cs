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

            circleThread = new Thread(new ThreadStart(movingCircle));
            circleThread.IsBackground = true;
            circleThread.SetApartmentState(ApartmentState.STA);
            circleThread.Start();

            // PageGeladen();
        }

        public bool AllowsBack => false;

        public bool AllowsHome => false;


        public Interface_Kalender Previous { get; set; }
        public event NavigationRequestEventHandler NavigationRequest;

        private void PageGeladen()
        {
            if (EMailAdresse == null)
            {
                // TODO: prüfen ob username schon in DB steht (extra tabelle) wenn nein, dann username reinschreiben und aufforderung starten eine Mail-Adresse einzugeben
                // bei jedem start des Programms das abprüfen, am besten bevor das Programm überhaupt das MainWindow lädt

                MailEingabe mailEingabe = new MailEingabe();
                mailEingabe.Show();
                EMailAdresse = mailEingabe.Mail;
            }

            MessageBox.Show(EMailAdresse);
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
                        Canvas.SetTop(ellipse1, 175 - i);
                    });
                    Thread.Sleep(5);
                }

                for (int i = 0; i <= 500; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(ellipse, i);
                        Canvas.SetLeft(ellipse1, 500 - i);
                    });
                    Thread.Sleep(5);
                }

                for (int i = 175; i >= 0; i--)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetTop(ellipse, i);
                        Canvas.SetTop(ellipse1, 175 - i);
                    });
                    Thread.Sleep(5);
                }

                for (int i = 500; i >= 0; i--)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(ellipse, i);
                        Canvas.SetLeft(ellipse1, 500 - i);
                    });
                    Thread.Sleep(5);
                }
            }
        }
    }
}
