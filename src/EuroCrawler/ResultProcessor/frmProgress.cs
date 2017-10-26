using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResultProcessor {
    public partial class frmProgress : Form {
        public frmProgress() {
            InitializeComponent();
        }
        public bool Cancel { get; set; }
        private void button1_Click(object sender, EventArgs e) {
            Cancel = true;
        }
    }
}
