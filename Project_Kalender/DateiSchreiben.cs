using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Kalender
{
    class DateiSchreiben
    {
        private string dateipfad = @"C:\Users\mwegn\OneDrive\Dokumente\MyKalender\";
        private ArrayList lines = new ArrayList();

        public DateiSchreiben(string Dateiname, string[] zeilen)
        {
            dateipfad = dateipfad + Dateiname + ".txt";

            for (int i = 0; i < zeilen.Length; i++)
            {
                lines.Add(zeilen[i]);
            }

            WriteToFile();
        }

        //TODO: Ablageordener evtl. auswählen können

        private void WriteToFile()
        {
            TextWriter tw = new StreamWriter(dateipfad, false);
            foreach (string text in lines)
            {
                tw.WriteLine(text);
            }
            tw.Close();
        }
    }
}
