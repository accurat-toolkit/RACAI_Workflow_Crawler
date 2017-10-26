using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Agora.Text.UI.Decision {
    public partial class SingleDecision : UserControl {
        public SingleDecision() {
            InitializeComponent();
            properties = new SingleDecisionPropertyBinding(this);
            properties.DecisionName = "Unknown";
            properties.Type = SingleDecisionType.Contains;

        }
        public string DecisionName {
            get {
                return this.lblDecisionName.Text;
            }
            set {
                this.lblDecisionName.Text = value;
            }
        }
        SingleDecisionPropertyBinding properties;


        public void ShowEditorWindow() {
            frmSingleDecisionEditor fsde = new frmSingleDecisionEditor(properties);
            fsde.ShowDialog();
        }

        DecisionLevel decisionLevel;

        public DecisionLevel DecisionLevel {
            get { return decisionLevel; }
            set { decisionLevel = value; }
        }

        private void label1_Click(object sender, EventArgs e) {
            //stergem regula
            DecisionLevel.RemoveSingleDecision(this);
        }

        #region animare
        public int newLeft = 0;
        #endregion

        private void lblDecisionName_DoubleClick(object sender, EventArgs e) {
            ShowEditorWindow();
        }
    }
    public class SingleDecisionPropertyBinding {
        private SingleDecision mySingleDecision;
        public SingleDecisionPropertyBinding(SingleDecision mySingleDecision) {
            this.mySingleDecision = mySingleDecision;
        }

        string decisionName;
        /// <summary>
        /// Friendly name that will be displayer on the control
        /// </summary>
        public string DecisionName {
            get { return decisionName; }
            set { decisionName = value; mySingleDecision.DecisionName = value; }
        }
        string regex;
        /// <summary>
        /// Regular expression to be applied to the input string
        /// </summary>
        public string Regex {
            get { return regex; }
            set { regex = value; }
        }
        /// <summary>
        /// This flag represents how the result will be interpreted after regexp filtering
        /// </summary>
        SingleDecisionType type;

        public SingleDecisionType Type {
            get { return type; }
            set { type = value; }
        }
        RegexOptions options;
        /// <summary>
        /// .NET style Regx options. For more information, consult MSDN
        /// </summary>
        public RegexOptions Options {
            get { return options; }
            set { options = value; }
        }
    }
    public enum SingleDecisionType {
        Contains,
        DoesNotContain
    }
}
