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

        private bool isDateVonFilled, isDatebisFilled, isTerminNameFilled, isTerminDescriptionFilled, isUhrzeitHVonFilled, isUhrzeitMinVonFilled, isUhrzeitHBisFilled, isUhrzeitMinBisFilled;
        private DateTime[] Datum { get; set; }
        public KalenderPage()
        {
            InitializeComponent();
            btn_TerminErstellen.IsEnabled = false;
            isDateVonFilled = false;
            isDatebisFilled = false;
            isTerminNameFilled = false;
            isTerminDescriptionFilled = false;
            isUhrzeitHVonFilled = false;
            isUhrzeitMinVonFilled = false;
            isUhrzeitHBisFilled = false;
            isUhrzeitMinBisFilled = false;
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

            Date_von.Foreground = Brushes.DarkBlue;
            Date_bis.Foreground = Brushes.DarkBlue;

            formularDateVon.Text = Datum[0].ToString("dd.MM.yyyy");
            formularDateBis.Text = Datum[(Datum.Length - 1)].ToString("dd.MM.yyyy");
            CheckformularDateVon();
            CheckformularDateBis();
        }

        private void CheckformularDateVon()
        {
            if (formularDateVon.SelectedDate != null)
            {
                isDateVonFilled = true;
            }
            else
            {
                isDateVonFilled = false;
            }

            btn_Enable();
        }

        private void CheckformularDateBis()
        {
            if (formularDateBis.SelectedDate != null)
            {
                isDatebisFilled = true;
            }
            else
            {
                isDatebisFilled = false;
            }

            btn_Enable();
        }

        private void formularDateVon_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (formularDateVon.SelectedDate != null)
            {
                isDateVonFilled = true;
            }
            else
            {
                isDateVonFilled = false;
            }

            btn_Enable();
        }

        private void formularDateBis_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (formularDateBis.SelectedDate != null)
            {
                isDatebisFilled = true;
            }
            else
            {
                isDatebisFilled = false;
            }

            btn_Enable();
        }

        private void formularDateName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(formularDateName.Text != "")
            {
                isTerminNameFilled = true;
            }
            else
            {
                isTerminNameFilled = false;
            }

            btn_Enable();
        }


        private void formularDateDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (formularDateDescription.Text != "")
            {
                isTerminDescriptionFilled = true;
            }
            else
            {
                isTerminDescriptionFilled = false;
            }

            btn_Enable();
        }

        private void formularUhrzeitVonStunde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(formularUhrzeitVonStunde.SelectedIndex != -1)
            {
                isUhrzeitHVonFilled = true;
            }
            else
            {
                isUhrzeitHVonFilled = false;
            }

            btn_Enable();
        }


        private void formularUhrzeitVonMinute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (formularUhrzeitVonMinute.SelectedIndex != -1)
            {
                isUhrzeitMinVonFilled = true;
            }
            else
            {
                isUhrzeitMinVonFilled = false;
            }

            btn_Enable();
        }

        private void formularUhrzeitBisStunde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (formularUhrzeitBisStunde.SelectedIndex != -1)
            {
                isUhrzeitHBisFilled = true;
            }
            else
            {
                isUhrzeitHBisFilled = false;
            }

            btn_Enable();
        }

        private void formularUhrzeitBisMinute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (formularUhrzeitBisMinute.SelectedIndex != -1)
            {
                isUhrzeitMinBisFilled = true;
            }
            else
            {
                isUhrzeitMinBisFilled = false;
            }

            btn_Enable();
        }

        private void btn_Enable()
        {
            if (isDateVonFilled == true && isDatebisFilled == true && isTerminNameFilled == true && isTerminDescriptionFilled == true && isUhrzeitHVonFilled == true &&
                isUhrzeitMinVonFilled == true && isUhrzeitHBisFilled == true && isUhrzeitMinBisFilled == true)
            {
                btn_TerminErstellen.IsEnabled = true;
            }
            else
            {
                btn_TerminErstellen.IsEnabled = false;
            }
        }

        private void btn_TerminErstellen_Click(object sender, RoutedEventArgs e)
        {
            // Date-Format = JJJJ-MM-TT
            // Time-Format = hh:mm:ss


            // Ende: Popup "Termin erstellt"
        }
    }
}
