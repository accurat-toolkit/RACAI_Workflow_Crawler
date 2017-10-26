using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agora.Text.UI.Flow.Interfaces;

namespace Agora.Text.UI.Flow.Windows {
    public partial class frmChooseNextComponent : Form {
        FlowComponent fc;
        public frmChooseNextComponent(FlowComponent fc) {
            InitializeComponent();
            this.fc = fc;
        }

        private void btnDecision_Click(object sender, EventArgs e) {
            CreatedObject = new FlowDecision();
            this.Close();
        }
        public Control CreatedObject;

        private void btnProcessing_Click(object sender, EventArgs e) {
            CreatedObject = new FlowProcessing();
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            CreatedObject = new FlowStop();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            CreatedObject = null;
            this.Close();
        }
    }
}
