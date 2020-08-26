using System;
using System.Collections.Generic;
using System.Data;
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

                db_find_startRecord();
            }
        }

        private void btnUpdateDB_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dgTermine.Items.GetItemAt(dgTermine.SelectedIndex);
            // TODO: Popup-Window mit vorausgefülltem Formular welches bearbeitet werden kann
            EditWindow edit = new EditWindow(row["ID"].ToString(), row["Datum_von"].ToString(), row["Datum_bis"].ToString(), row["TerminName"].ToString(), row["TerminDescription"].ToString(),
                row["Uhrzeit_von"].ToString(), row["Uhrzeit_bis"].ToString(), row["Tag"].ToString());
            edit.ShowDialog();

            //MessageBox.Show("Termin wurde bearbeitet.", "Hinweis");

            db_find_startRecord();

        }
    }
}
