using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaktionslogik für TerminePage.xaml
    /// </summary>
    public partial class TerminePage : Page, Interface_Kalender
    {
        public TerminePage()
        {
            InitializeComponent();
            cbFilterart.SelectedIndex = 0;
            dpDate.Visibility = Visibility.Collapsed;
            tbTermin.Visibility = Visibility.Collapsed;
            cbUhrzeitH.Visibility = Visibility.Collapsed;
            tbUhrzeit.Visibility = Visibility.Collapsed;
            cbUhrzeitMin.Visibility = Visibility.Collapsed;
            cbTag.Visibility = Visibility.Collapsed;
            db_find_startRecord();
        }

        public bool AllowsBack => true;

        public bool AllowsHome => true;

        public Interface_Kalender Previous { get; set; }

        public event NavigationRequestEventHandler NavigationRequest;

        private void db_find_startRecord()
        {

            string sSQL = "SELECT * FROM tbl_Termin";

            dgTermine.ItemsSource = clsDB.Get_DataTable(sSQL).DefaultView;
        }

        private void db_find_Record(string wert, string spalte)
        {

            string sSQL = "SELECT * FROM tbl_Termin WHERE [" + spalte + "] = '" + wert + "'";

            dgTermine.ItemsSource = clsDB.Get_DataTable(sSQL).DefaultView;
        }

        private void db_find_like_Record(string wert, string spalte)
        {

            string sSQL = "SELECT * FROM tbl_Termin WHERE [" + spalte + "] Like '%" + wert + "%'";

            dgTermine.ItemsSource = clsDB.Get_DataTable(sSQL).DefaultView;
        }

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

                    if (File.Exists(@"C:\Users\mwegn\OneDrive\Dokumente\MyKalender\" + row["Dateiname"].ToString() + ".txt"))
                    {
                        File.Delete(@"C:\Users\mwegn\OneDrive\Dokumente\MyKalender\" + row["Dateiname"].ToString() + ".txt");
                    }

                    MessageBox.Show("Termin: " + "'" + row["Terminname"].ToString() + "'" + " wurde gelöscht!", "Löschen", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Termin: " + "'" + row["Terminname"].ToString() + "'" + " wurde nicht gelöscht!", "Löschen", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                searchOptions();
            }
        }

        private void btnUpdateDB_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dgTermine.Items.GetItemAt(dgTermine.SelectedIndex);
            EditWindow edit = new EditWindow(row["ID"].ToString(), row["Datum_von"].ToString(), row["Datum_bis"].ToString(), row["TerminName"].ToString(), row["TerminDescription"].ToString(),
                row["Uhrzeit_von"].ToString(), row["Uhrzeit_bis"].ToString(), row["Tag"].ToString(), row["Dateiname"].ToString());
            edit.ShowDialog();

            searchOptions();

        }

        private void cbAlleTermine_Selected(object sender, RoutedEventArgs e)
        {
            dpDate.Visibility = Visibility.Collapsed;
            tbTermin.Visibility = Visibility.Collapsed;
            radioButtons.Visibility = Visibility.Collapsed;
            cbUhrzeitH.Visibility = Visibility.Collapsed;
            tbUhrzeit.Visibility = Visibility.Collapsed;
            cbUhrzeitMin.Visibility = Visibility.Collapsed;
            cbTag.Visibility = Visibility.Collapsed;
        }

        private void cbDatum_Selected(object sender, RoutedEventArgs e)
        {
            dpDate.Visibility = Visibility.Visible;
            tbTermin.Visibility = Visibility.Collapsed;
            radioButtons.Visibility = Visibility.Collapsed;
            cbUhrzeitH.Visibility = Visibility.Collapsed;
            tbUhrzeit.Visibility = Visibility.Collapsed;
            cbUhrzeitMin.Visibility = Visibility.Collapsed;
            cbTag.Visibility = Visibility.Collapsed;
        }

        private void cbTermin_Selected(object sender, RoutedEventArgs e)
        {
            dpDate.Visibility = Visibility.Collapsed;
            tbTermin.Visibility = Visibility.Visible;
            radioButtons.Visibility = Visibility.Visible;
            cbUhrzeitH.Visibility = Visibility.Collapsed;
            tbUhrzeit.Visibility = Visibility.Collapsed;
            cbUhrzeitMin.Visibility = Visibility.Collapsed;
            cbTag.Visibility = Visibility.Collapsed;

            tbTermin.Tag = "Bitte etwas eingeben";

        }

        private void cbUhrzeit_Selected(object sender, RoutedEventArgs e)
        {
            dpDate.Visibility = Visibility.Collapsed;
            tbTermin.Visibility = Visibility.Collapsed;
            radioButtons.Visibility = Visibility.Collapsed;
            cbUhrzeitH.Visibility = Visibility.Visible;
            tbUhrzeit.Visibility = Visibility.Visible;
            cbUhrzeitMin.Visibility = Visibility.Visible;
            cbTag.Visibility = Visibility.Collapsed;
        }

        private void cbTerminart_Selected(object sender, RoutedEventArgs e)
        {
            dpDate.Visibility = Visibility.Collapsed;
            tbTermin.Visibility = Visibility.Collapsed;
            radioButtons.Visibility = Visibility.Collapsed;
            cbUhrzeitH.Visibility = Visibility.Collapsed;
            tbUhrzeit.Visibility = Visibility.Collapsed;
            cbUhrzeitMin.Visibility = Visibility.Collapsed;
            cbTag.Visibility = Visibility.Visible;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchOptions();
        }

        private void searchOptions()
        {
            switch (cbFilterart.SelectedIndex)
            {
                case 0:
                    db_find_startRecord();
                    break;
                case 1:
                    if (!dpDate.Text.Equals(""))
                    {
                        var parsedDate = DateTime.Parse(dpDate.Text);
                        string DateVon = parsedDate.ToString("dd.MM.yyyy");
                        db_find_Record(DateVon, "Datum_von");
                    }
                    else
                    {
                        MessageBox.Show("Bitte ein Datum auswählen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case 2:
                    if (!dpDate.Text.Equals(""))
                    {
                        var parsedDate = DateTime.Parse(dpDate.Text);
                        string DateVon = parsedDate.ToString("dd.MM.yyyy");
                        db_find_Record(DateVon, "Datum_bis");
                    }
                    else
                    {
                        MessageBox.Show("Bitte ein Datum auswählen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case 3:
                    if (!tbTermin.Text.Equals(""))
                    {
                        if (radioTeil.IsChecked == true)
                        {
                            db_find_like_Record(tbTermin.Text, "TerminName");
                        }

                        if (radioKomplett.IsChecked == true)
                        {
                            db_find_Record(tbTermin.Text, "TerminName");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Bitte etwas eingeben", "Warnung", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case 4:
                    if (!tbTermin.Text.Equals(""))
                    {
                        if (radioTeil.IsChecked == true)
                        {
                            db_find_like_Record(tbTermin.Text, "TerminDescription");
                        }
                        
                        if (radioKomplett.IsChecked == true)
                        {
                            db_find_Record(tbTermin.Text, "TerminDescription");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bitte etwas eingeben");
                    }
                    break;
                case 5:
                    if (!cbUhrzeitH.Text.Equals("") && !cbUhrzeitMin.Text.Equals(""))
                    {
                        string Uhrzeit = cbUhrzeitH.Text + ":" + cbUhrzeitMin.Text + ":00";
                        db_find_Record(Uhrzeit, "Uhrzeit_von");
                    }
                    else
                    {
                        MessageBox.Show("Bitte etwas auswählen");
                    }
                    break;
                case 6:
                    if (!cbUhrzeitH.Text.Equals("") && !cbUhrzeitMin.Text.Equals(""))
                    {
                        string Uhrzeit = cbUhrzeitH.Text + ":" + cbUhrzeitMin.Text + ":00";
                        db_find_Record(Uhrzeit, "Uhrzeit_bis");
                    }
                    else
                    {
                        MessageBox.Show("Bitte etwas auswählen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case 7:
                    if (!cbTag.Text.Equals(""))
                    {
                        db_find_Record(cbTag.Text, "Tag");
                    }
                    else
                    {
                        MessageBox.Show("Bitte eine Art auswählen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
