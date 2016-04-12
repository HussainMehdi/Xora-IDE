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

namespace XORA_IDE
{
    /// <summary>
    /// Interaction logic for EventBuilder.xaml
    /// </summary>
    public partial class EventBuilder : Window
    {

        List<Models.EventsList> EventsList;

        //EventDataGrid DGrid = new EventDataGrid();

        public EventBuilder()
        {
            InitializeComponent();

          //  EventsGrid.ItemsSource = DGrid.BindedDataGrid;
        }

        public EventBuilder(string id)
        {
            EventsList = Facade._ModuleControl.GetModuleOnCanvasEventsByUID(id);
        }
    }
}
