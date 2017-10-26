using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agora.Text.UI.Decision {
    public partial class frmSingleDecisionEditor : Form {
        public frmSingleDecisionEditor(object PropertyBinding) {
            InitializeComponent();
            this.propertyGrid1.SelectedObject = PropertyBinding;
        }

    }
}
