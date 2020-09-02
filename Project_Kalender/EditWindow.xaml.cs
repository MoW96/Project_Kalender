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
using System.Net.Mail;

namespace Project_Kalender
{
    /// <summary>
    /// Interaktionslogik für EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private bool isDateVonFilled, isDatebisFilled, isTerminNameFilled, isTerminDescriptionFilled, isUhrzeitHVonFilled, isUhrzeitMinVonFilled, isUhrzeitHBisFilled, isUhrzeitMinBisFilled;
        private string Id;
        private DateTime[] Datum { get; set; }
        public EditWindow(string ID, string DateVon, string DateBis, string TerminName, string TerminDescription, string UhrzeitVon, string UhrzeitBis, string Tag)
        {
            InitializeComponent();

            this.Id = ID;
            formularDateVon.SelectedDate = DateTime.Parse(DateVon);
            formularDateBis.SelectedDate = DateTime.Parse(DateBis);
            formularDateName.Text = TerminName;
            formularDateDescription.Text = TerminDescription;

            int index;

            if (UhrzeitVon.Equals("00:00:00") && UhrzeitBis.Equals("23:59:00"))
            {
                checkboxGanztägig.IsChecked = true;
            } 
            else
            {
                var comboBoxItem = formularUhrzeitVonStunde.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == UhrzeitVon.Substring(0, 2).ToString());
                index = formularUhrzeitVonStunde.SelectedIndex = formularUhrzeitVonStunde.Items.IndexOf(comboBoxItem);
                formularUhrzeitVonStunde.SelectedIndex = index;

                comboBoxItem = formularUhrzeitVonMinute.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == UhrzeitVon.Substring(3, 2).ToString());
                index = formularUhrzeitVonMinute.SelectedIndex = formularUhrzeitVonMinute.Items.IndexOf(comboBoxItem);
                formularUhrzeitVonMinute.SelectedIndex = index;

                comboBoxItem = formularUhrzeitBisStunde.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == UhrzeitBis.Substring(0, 2).ToString());
                index = formularUhrzeitBisStunde.SelectedIndex = formularUhrzeitBisStunde.Items.IndexOf(comboBoxItem);
                formularUhrzeitBisStunde.SelectedIndex = index;

                comboBoxItem = formularUhrzeitBisMinute.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == UhrzeitBis.Substring(3, 2).ToString());
                index = formularUhrzeitBisMinute.SelectedIndex = formularUhrzeitBisMinute.Items.IndexOf(comboBoxItem);
                formularUhrzeitBisMinute.SelectedIndex = index;
            }

            var comboBoxItemNeu = formularTag.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == Tag.ToString());
            index = formularTag.SelectedIndex = formularTag.Items.IndexOf(comboBoxItemNeu);
            formularTag.SelectedIndex = index;
        }

        private void Kalender_SlectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar myCal = (Calendar)sender;
            Datum = myCal.SelectedDates.ToArray();

            //Daten mit BubbleSort nach Größe sortieren
            Datum = SortDate(Datum);

            formularDateVon.Text = Datum[0].ToString("dd.MM.yyyy");
            formularDateBis.Text = Datum[(Datum.Length - 1)].ToString("dd.MM.yyyy");
            CheckformularDate();
        }

        private DateTime[] SortDate(DateTime[] input)
        {
            DateTime Temp;
            for (int i = 1; i < input.Length; i++)
            {
                for (int k = 0; k < (input.Length - i); k++)
                {
                    if (DateTime.Compare(input[k], input[k + 1]) > 0)
                    {
                        Temp = input[k];
                        input[k] = input[k + 1];
                        input[k + 1] = Temp;
                    }
                }
            }
            return input;
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
            if (formularDateName.Text != "")
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
            if (formularUhrzeitVonStunde.SelectedIndex != -1)
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
                    btn_TerminUpdate.IsEnabled = true;
                }
                else
                {
                    btn_TerminUpdate.IsEnabled = false;
                }
            }
            else
            {
                btn_TerminUpdate.IsEnabled = false;
            }
        }
        private void btn_TerminUpdate_Click(object sender, RoutedEventArgs e)
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

            db_update_DB(Id, DateVon, DateBis, TerminName, TerminDescription, UhrzeitVon, UhrzeitBis, Tag);

            if (get_SendenFromDB().Equals("Ja"))
            {
                string from = "termin@kalender.de";
                string to = get_MailFromDB();

                SmtpClient SmtpMail = new SmtpClient("smtp.gmail.com");
                SmtpMail.Port = 587;
                SmtpMail.Credentials = new System.Net.NetworkCredential("sendmail.wegner", "MaiL.Wegner.121");
                SmtpMail.EnableSsl = true;

                MailMessage myMail = new MailMessage(from, to);
                myMail.Subject = "Termin: " + formularDateName.Text + " updated";
                myMail.Priority = MailPriority.Low;
                myMail.IsBodyHtml = true;
                myMail.Body = "<html>" +
                    "<body style='text-align:center, margin:auto'>" +
                    "<h2 style='color:#cc9900'><u>" + formularDateName.Text + "</u></h2>" +
                    "<p>" +
                    formularDateDescription.Text + "<br>" +
                    "<u>Von:</u> " + formularDateVon.Text + "&nbsp;&nbsp;&nbsp; <u>Bis:</u> " + formularDateBis.Text + "<br>" +
                    "<u>Uhrzeit:</u> " + formularUhrzeitVonStunde.Text + ":" + formularUhrzeitVonMinute.Text + "&nbsp; - &nbsp;" + formularUhrzeitBisStunde.Text + ":" + formularUhrzeitBisMinute.Text + "<br>" +
                    "<u>Art:</u> " + formularTag.Text + "<br>" +
                    "</p>" +
                    "</body>" +
                    "</html>";

                SmtpMail.Send(myMail);
            }

            this.Close();
        }

        private void db_update_DB(string ID, string DateVon, string DateBis, string TerminName, string TerminDescription, string UhrzeitVon, string UhrzeitBis, string Tag)
        {
            string sql_update = "UPDATE tbl_Termin SET Datum_von = '" + DateVon + "', " +
                "Datum_bis = '" + DateBis + "', " +
                "TerminName = '" + TerminName + "', " +
                "TerminDescription = '" + TerminDescription + "', " +
                "Uhrzeit_von = '" + UhrzeitVon + "', " +
                "Uhrzeit_bis = '" + UhrzeitBis + "', " +
                "Tag = '" + Tag + "'" +
                 "WHERE Id = '" + ID + "'";
            clsDB.Execute_SQL(sql_update);
        }

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
    }
}
