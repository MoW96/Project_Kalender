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

namespace Project_Kalender
{
    /// <summary>
    /// Interaktionslogik für Entry.xaml
    /// </summary>
    public partial class Entry : Page, Interface_Kalender
    {
        public Entry()
        {
            InitializeComponent();
            btn_Kalender.ToolTip = "Kalender";
            btn_Termin.ToolTip = "Termine";
        }

        public bool AllowsBack => false;

        public bool AllowsHome => false;

        public Interface_Kalender Previous { get; set; }
        public event NavigationRequestEventHandler NavigationRequest;

    }
}
