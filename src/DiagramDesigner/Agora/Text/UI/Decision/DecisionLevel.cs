using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agora.Text.UI.Decision {
    public partial class DecisionLevel : UserControl {
        Timer animation;
        public DecisionLevel() {
            InitializeComponent();
            animation = new Timer();
            animation.Interval = 30;
            animation.Enabled = false;
            animation.Tick += new EventHandler(animation_Tick);
        }

        void animation_Tick(object sender, EventArgs e) {
            bool gasit = false;
            foreach (SingleDecision sd in singleDecisions) {
                if (sd.newLeft != sd.Left) {
                    if (sd.newLeft < sd.Left) {
                        sd.Left -= 45;
                        if (sd.Left < sd.newLeft)
                            sd.Left = sd.newLeft;
                    } else {
                        sd.Left += 45;
                        if (sd.Left > sd.newLeft)
                            sd.Left = sd.newLeft;
                    }
                    gasit = true;
                }
            }
            if (!gasit) {
                animation.Enabled = false;
            }

        }

        List<SingleDecision> singleDecisions = new List<SingleDecision>();

        bool selected;
        public bool Selected {
            get {
                return selected;
            }
            set {
                selected = value;
                ChangeUI();
            }
        }

        DecisionContainer decisionContainer;

        public DecisionContainer DecisionContainer {
            get { return decisionContainer; }
            set { decisionContainer = value; }
        }

        private void ChangeUI() {
            if (selected) {
                this.panel1.BackColor = Color.FromArgb(240, 240, 240);
                this.pnlMain.BackColor = Color.FromArgb(240, 240, 240);
                //le deselectam pe celalalte
                //daca ne aflam pe un container corespunzator
                if (decisionContainer != null) {
                    DecisionContainer dc = decisionContainer;
                    foreach (DecisionLevel dl in dc.DecisionLevels) {
                        if (dl != this)
                            dl.Selected = false;
                    }
                }
            } else {
                this.panel1.BackColor = Color.White;
                this.pnlMain.BackColor = Color.White;
            }
        }

        private void ControlClick(object sender, EventArgs e) {
            this.Selected = true;
        }

        private void addValidatorToolStripMenuItem_Click(object sender, EventArgs e) {
            SingleDecision sd = new SingleDecision();
            sd.DecisionName = "Unknown";
            sd.Left = this.singleDecisions.Count * 137 + 15;
            sd.newLeft = sd.Left;
            sd.Height = 57;
            sd.Width = 132;
            sd.Top = (this.pnlMain.Height - 57) / 2-16;
            sd.DecisionLevel = this;
            sd.DoubleClick += new EventHandler(sd_DoubleClick);
            this.pnlMain.Controls.Add(sd);
            this.singleDecisions.Add(sd);
        }

        void sd_DoubleClick(object sender, EventArgs e) {
            //throw new NotImplementedException();
            (sender as SingleDecision).ShowEditorWindow();
        }

        SingleDecision selectedSingleDecision;

        private void RebuildUI() {
            int i = 0;
            foreach (SingleDecision sd in singleDecisions) {
                sd.newLeft = i * 137 + 15;
                i++;
            }
            animation.Enabled = true;
        }

        private void mnuDecision_Opening(object sender, CancelEventArgs e) {
            selectedSingleDecision = (sender as SingleDecision);
        }

        private void button3_Click(object sender, EventArgs e) {
            if (this.decisionContainer != null) {
                this.decisionContainer.RemoveLevel(this);
            }
        }
        #region animation
        public int newTop;
        #endregion

        private void button2_Click(object sender, EventArgs e) {
            DecisionContainer.MoveDecisionUp(this);
        }

        private void button1_Click(object sender, EventArgs e) {
            DecisionContainer.MoveDecisionDown(this);
        }


        internal void RemoveSingleDecision(SingleDecision singleDecision) {
            //throw new NotImplementedException();
            this.pnlMain.Controls.Remove(singleDecision);
            this.singleDecisions.Remove(singleDecision);
            RebuildUI();
        }
    }
}
