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
using System.Collections;

namespace Project_Kalender
{
    /// <summary>
    /// Interaktionslogik für KalenderPage.xaml
    /// </summary>
    public partial class KalenderPage : Page, Interface_Kalender
    {

        private DateTime[] Datum { get; set; }
        public KalenderPage()
        {
            InitializeComponent();
        }

        public bool AllowsBack => true;

        public bool AllowsHome => true;

        public Interface_Kalender Previous { get; set; }
        public event NavigationRequestEventHandler NavigationRequest;

        private void Kalender_SlectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar myCal = (Calendar)sender;
            Datum = myCal.SelectedDates.ToArray();

            Date_von.Text = Datum[0].ToString("dddd, dd.MM.yyyy");
            Date_bis.Text = Datum[(Datum.Length - 1)].ToString("dddd, dd.MM.yyyy");
        }
    }
}
