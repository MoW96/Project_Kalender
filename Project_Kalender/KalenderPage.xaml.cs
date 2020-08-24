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
using System.Data.SqlClient;
using System.Data;

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

            //Daten mit BubbleSort nach Größe sortieren
            Datum = SortDate(Datum);

            Date_von.Text = Datum[0].ToString("dddd, dd.MM.yyyy");
            Date_bis.Text = Datum[(Datum.Length - 1)].ToString("dddd, dd.MM.yyyy");
            Date_vonTermin.Text = Datum[0].ToString("dddd, dd.MM.yyyy"); ;
            Date_bisTermin.Text = Datum[(Datum.Length - 1)].ToString("dddd, dd.MM.yyyy"); ;

            Date_von.Foreground = Brushes.DarkBlue;
            Date_bis.Foreground = Brushes.DarkBlue;
            Date_vonTermin.Foreground = Brushes.DarkBlue;
            Date_bisTermin.Foreground = Brushes.DarkBlue;

            formularDateVon.Text = Datum[0].ToString("dd.MM.yyyy");
            formularDateBis.Text = Datum[(Datum.Length - 1)].ToString("dd.MM.yyyy");
            CheckformularDate();

            db_find_Record(formularDateVon.Text, formularDateBis.Text);
        }

        private void CheckformularDate()
        {
            if (formularDateVon.SelectedDate != null)
            {
                isDateVonFilled = true;
            }
            else
            {
                isDateVonFilled = false;
            }

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

        private void checkboxGanztägig_Checked(object sender, RoutedEventArgs e)
        {
            if (checkboxGanztägig.IsChecked == true)
            {
                formularUhrzeitVonStunde.IsEnabled = false;
                formularUhrzeitVonMinute.IsEnabled = false;
                formularUhrzeitBisStunde.IsEnabled = false;
                formularUhrzeitBisMinute.IsEnabled = false;

                formularUhrzeitVonStunde.Text = "00";
                formularUhrzeitVonMinute.Text = "00";
                formularUhrzeitBisStunde.Text = "23";
                formularUhrzeitBisMinute.Text = "59";
            }
            else
            {
                formularUhrzeitVonStunde.IsEnabled = true;
                formularUhrzeitVonMinute.IsEnabled = true;
                formularUhrzeitBisStunde.IsEnabled = true;
                formularUhrzeitBisMinute.IsEnabled = true;

                formularUhrzeitVonStunde.SelectedIndex = -1;
                formularUhrzeitVonMinute.SelectedIndex = -1;
                formularUhrzeitBisStunde.SelectedIndex = -1;
                formularUhrzeitBisMinute.SelectedIndex = -1;
            }
        }

        private void btn_Enable()
        {
            if (formularDateVon.SelectedDate != null && formularDateBis.SelectedDate != null && formularUhrzeitVonStunde.SelectedIndex != -1 && formularUhrzeitVonMinute.SelectedIndex != -1 &&
                formularUhrzeitBisStunde.SelectedIndex != -1 && formularUhrzeitBisMinute.SelectedIndex != -1 && isDateVonFilled == true && isDatebisFilled == true && isTerminNameFilled == true && isTerminDescriptionFilled == true && isUhrzeitHVonFilled == true &&
                    isUhrzeitMinVonFilled == true && isUhrzeitHBisFilled == true && isUhrzeitMinBisFilled == true)
            {
                string strDateVon = formularDateVon.Text.Substring(0, 10);
                string strDateBis = formularDateBis.Text.Substring(0, 10);
                DateTime DateVon = DateTime.Parse(strDateVon + " " + formularUhrzeitVonStunde.SelectedIndex.ToString() + ":" + formularUhrzeitVonMinute.SelectedIndex.ToString() + ":" + "00");
                DateTime DateBis = DateTime.Parse(strDateBis + " " + formularUhrzeitBisStunde.SelectedIndex.ToString() + ":" + formularUhrzeitBisMinute.SelectedIndex.ToString() + ":" + "00");

   
                    if (DateTime.Compare(DateVon, DateBis) < 0)
                    {
                        btn_TerminErstellen.IsEnabled = true;
                    }
                    else
                    {
                        btn_TerminErstellen.IsEnabled = false;
                    }
            }
            else
            {
                    btn_TerminErstellen.IsEnabled = false;
            }
        }

        private DateTime[] SortDate(DateTime[] input)
        {
            DateTime Temp;
            for (int i = 1; i < input.Length; i++)
            {
                for (int k = 0; k < (input.Length - i); k++)
                {
                    if (DateTime.Compare(input[k], input[k+1]) > 0)
                    {
                        Temp = input[k];
                        input[k] = input[k + 1];
                        input[k + 1] = Temp;
                    }
                }
            }
            return input;
        }

    private void db_Add_Record(string DateVon, string DateBis, string TerminName, string TerminDescription, string UhrzeitVon, string UhrzeitBis, string Tag)
        {

            //sURL = sURL.Replace("'", "''");
            //sTitle = sTitle.Replace("'", "''");

            // add
            string sql_Add = "INSERT INTO tbl_Termin ([Datum_von],[Datum_bis],[TerminName],[TerminDescription],[Uhrzeit_von],[Uhrzeit_bis],[Tag]) VALUES('" + DateVon + "','" + DateBis + "','" + TerminName + "','" +
                TerminDescription + "', '" + UhrzeitVon + "','" + UhrzeitBis + "','" + Tag + "')";
                clsDB.Execute_SQL(sql_Add);

            MessageBox.Show("Termin: " + "'" + TerminName + "'" + " wurde angelegt.", "Hinweis");
            //    //update
            //    string ID = tbl.Rows[0]["Id"].ToString();
            //    string sql_Update = "UPDATE tbl_Details SET [dtScan] = SYSDATETIME() WHERE Id = " + ID;
            //    clsDB.Execute_SQL(sql_Update);
        }

        private void db_find_Record(string dateVon, string dateBis)
        {

            string sSQL = "SELECT * FROM tbl_Termin WHERE [Datum_von] BETWEEN '" + dateVon + "' AND '" + dateBis + "'";

            dgTermine.ItemsSource = clsDB.Get_DataTable(sSQL).DefaultView;
        }

        private void btn_TerminErstellen_Click(object sender, RoutedEventArgs e)
        {
            // Date-Format = JJJJ-MM-TT
            var parsedDate = DateTime.Parse(formularDateVon.Text);
            string DateVon = parsedDate.ToString("dd.MM.yyyy");

            parsedDate = DateTime.Parse(formularDateBis.Text);
            string DateBis = parsedDate.ToString("dd.MM.yyyy");

            string TerminName = formularDateName.Text;
            string TerminDescription = formularDateDescription.Text;

            string Tag;
            
            if (formularTag.SelectedIndex != -1)
            {
                Tag = formularTag.Text;
            }
            else
            {
                Tag = "";
            }
            

            // Time-Format = hh:mm:ss
            string UhrzeitVon = formularUhrzeitVonStunde.Text + ":" + formularUhrzeitVonMinute.Text + ":" + "00";

            string UhrzeitBis = formularUhrzeitBisStunde.Text + ":" + formularUhrzeitBisMinute.Text + ":" + "00";

            db_Add_Record(DateVon, DateBis, TerminName, TerminDescription, UhrzeitVon, UhrzeitBis, Tag);

            db_find_Record(formularDateVon.Text, formularDateBis.Text);
        }

        // Termin löschen - durch Klick auf ein Icon welches immer am ende der Zeile plaziert werden soll 
        private void db_delete_Termin(string Id_Data)
        {
            string SQL_Del = "DELETE FROM tbl_Termin WHERE [Id] = '" + Id_Data + "'";

            clsDB.Execute_SQL(SQL_Del);
        }

        private void btnDeleteFromDB_Click(object sender, RoutedEventArgs e)
        {
            if ((int)dgTermine.SelectedIndex != -1)
            {

                DataRowView row = (DataRowView)dgTermine.Items.GetItemAt(dgTermine.SelectedIndex);

                if (MessageBox.Show("Wollen Sie den Termin " + "'" + row["Terminname"].ToString() + "'" + " wirklich löschen?", "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    
                    db_delete_Termin(row["ID"].ToString());

                    MessageBox.Show("Termin: " + "'" + row["Terminname"].ToString() + "'" + " wurde gelöscht!");

                }
                else
                {
                    MessageBox.Show("Termin: " + "'" + row["Terminname"].ToString() + "'" + " wurde nicht gelöscht!");
                }

                db_find_Record(formularDateVon.Text, formularDateBis.Text);
            }
        }
    }
}
