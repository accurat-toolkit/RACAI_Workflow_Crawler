using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agora.Text.UI.Decision {
    public partial class DecisionContainer : UserControl {
        Timer animation;
        public DecisionContainer() {
            InitializeComponent();
            animation = new Timer();
            animation.Tick += new EventHandler(animation_Tick);
            animation.Interval = 30;
            this.cmbDestination.SelectedIndex = 0;
        }

        void animation_Tick(object sender, EventArgs e) {
            //throw new NotImplementedException();
            bool gasit = false;
            foreach (DecisionLevel dl in decisionLevels) {
                if (dl.newTop != dl.Top) {
                    if (dl.newTop < dl.Top) {
                        dl.Top -= 45;
                        if (dl.Top < dl.newTop)
                            dl.Top = dl.newTop;
                    } else {
                        dl.Top += 45;
                        if (dl.Top > dl.newTop)
                            dl.Top = dl.newTop;
                    }
                    gasit = true;
                }
            }
            if (!gasit) {
                animation.Enabled = false;
            }
        }
        List<DecisionLevel> decisionLevels = new List<DecisionLevel>();

        public List<DecisionLevel> DecisionLevels {
            get { return decisionLevels; }
            set { decisionLevels = value; }
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e) {
            DecisionLevel dl = new DecisionLevel();
            dl.Width = this.pnlMain.Width - 26;
            dl.Left = 3;
            dl.Top = 3 + decisionLevels.Count * 156;
            dl.Height = 150;
            dl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dl.DecisionContainer = this;
            dl.newTop = dl.Top;
            this.pnlMain.Controls.Add(dl);
            this.decisionLevels.Add(dl);
        }

        private void pnlMain_Resize(object sender, EventArgs e) {
            this.pnlMain.AutoScrollMinSize = this.pnlMain.Size;
        }

        internal void RemoveLevel(DecisionLevel decisionLevel) {
            //throw new NotImplementedException();
            this.pnlMain.Controls.Remove(decisionLevel);
            this.decisionLevels.Remove(decisionLevel);
            int i=0;
            foreach (DecisionLevel dl in this.decisionLevels) {
                dl.newTop = 3 + i * 156;
                i++;
            }
            animation.Enabled = true;
        }

        internal void MoveDecisionUp(DecisionLevel decisionLevel) {
//            throw new NotImplementedException();
            //cautam pozitia curenta a decizie in lista
            int i=0;
            for (i = 0; i < this.DecisionLevels.Count; i++) {
                if (DecisionLevels[i] == decisionLevel)
                    break;
            }
            if (i == 0)
                return;
            //interschimbam in lista si apoi animam
            DecisionLevel tmp = decisionLevels[i - 1];
            decisionLevels[i - 1] = decisionLevel;
            decisionLevels[i] = tmp;
            decisionLevel.newTop = 3 + (i-1) * 156;
            tmp.newTop = 3 + (i) * 156;
            animation.Enabled = true;

            decisionLevel.BringToFront();
        }

        internal void MoveDecisionDown(DecisionLevel decisionLevel) {
            //cautam pozitia curenta a decizie in lista
            int i = 0;
            for (i = 0; i < this.DecisionLevels.Count; i++) {
                if (DecisionLevels[i] == decisionLevel)
                    break;
            }
            if (i == decisionLevels.Count-1)
                return;
            //interschimbam in lista si apoi animam
            DecisionLevel tmp = decisionLevels[i + 1];
            decisionLevels[i + 1] = decisionLevel;
            decisionLevels[i] = tmp;
            decisionLevel.newTop = 3 + (i + 1) * 156;
            tmp.newTop = 3 + (i) * 156;
            animation.Enabled = true;

            decisionLevel.BringToFront();
        }
    }
}
