using System.Windows;
using System.Windows.Forms;

namespace DiagramDesigner
{
    public partial class Window1 : Window
    {
        private static Window1 MyFirstInstance;
        public static Window1 GetFirstInstance(){
            return MyFirstInstance;
        }
        public PropertyGrid pgMain { get; set; }
        public Window1()
        {
            if (MyFirstInstance == null)
                MyFirstInstance = this;
            InitializeComponent();
            pgMain = new PropertyGrid();
            pgMain.Dock = DockStyle.Fill;
            this.wfhPropertyGrid.Child = pgMain;
            this.SnapsToDevicePixels = true;

        }

        private void Expander_Expanded(object sender, RoutedEventArgs e) {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
        }
    }
}
