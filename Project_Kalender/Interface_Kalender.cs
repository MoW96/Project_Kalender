using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Kalender
{
    public interface Interface_Kalender
    {

        bool AllowsBack
        {
            get;
        }

        bool AllowsHome
        {
            get;
        }

        //Auskommentiertes siehe Bilanz.xaml.cs
        //bool AllowsNext
        //{
        //    get;
        //}
        //bool AllowsCancel
        //{
        //    get;
        //}
        //bool IsReady
        //{
        //    get;
        //}
        //IBwlPage Next
        //{
        //    get;
        //}
        Interface_Kalender Previous
        {
            get; set;
        }

        //event ReadyChangedEventHandler ReadyChanged;
        event NavigationRequestEventHandler NavigationRequest;

    }

    //public delegate void ReadyChangedEventHandler(object sender, ReadyChangedEventArgs e);
    public delegate void NavigationRequestEventHandler(object sender, Interface_Kalender page);
    public class ReadyChangedEventArgs : EventArgs
    {
        public bool IsReady { get; private set; }
        //auto-property - auch hier: hab ich früher nicht gemacht ist aber schöner

        public ReadyChangedEventArgs(bool isReady)
        {
            IsReady = isReady;
        }

    }
}
