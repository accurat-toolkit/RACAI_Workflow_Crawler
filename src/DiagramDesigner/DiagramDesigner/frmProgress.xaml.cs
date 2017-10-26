using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiagramDesigner {
    /// <summary>
    /// Interaction logic for frmProgress.xaml
    /// </summary>
    public partial class frmProgress : Window {
        public frmProgress() {
            InitializeComponent();
        }
        public void SetTitle(string title) {
            this.Title = title;
        }
        public void SetStatus(string status) {
            this.lblStatus.Content = status;
        }
        public ProgressBar MyProgressBar { get { return this.pgbMain; } }
        public void AddToLog(string log) {
            this.edtLog.Text = log + "\r\n" + this.edtLog.Text;
        }
    }
}
