using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agora.Text.UI.Flow.Interfaces;
using Agora.Text.UI.Flow.Windows;
using Agora.Text.UI.Flow.Execution;

namespace Agora.Text.UI.Flow {
    public partial class FlowDecision : UserControl, FlowComponent {
        public FlowDecision() {
            InitializeComponent();
            ExecutionUnit = new ExecutionUnit(this,this.lblDecisionName.Text);
            lblDecisionName.Click += new EventHandler(lblDecisionName_Click);
            this.Click += new EventHandler(lblDecisionName_Click);
        }
        public event ClickEvent ControlClick;

        void lblDecisionName_Click(object sender, EventArgs e) {
            if (ControlClick != null)
                ControlClick(this, e);
        }

        #region FlowComponent Members
        Control NextControl;
        Control OtherControl;

        FlowDiagram parentDiagram;
        void FlowComponent.SetComponentContainer(FlowDiagram fd) {
            this.parentDiagram = fd;
        }

        public FlowDiagram GetComponentContainer() {
            return parentDiagram;
        }

        public void SlideControl(int deltaX, int deltaY) {
            //throw new NotImplementedException();
            this.Left += deltaX;
            this.Top += deltaY;
            if (NextControl != null)
                (NextControl as FlowComponent).SlideControl(deltaX, deltaY);
            if (OtherControl != null)
                (OtherControl as FlowComponent).SlideControl(deltaX, deltaY);
        }

        public FlowComponent GetDefaultNextControl() {
            return NextControl as FlowComponent;
        }



        public void SetDefaultNextControl(Control c) {
            //throw new NotImplementedException();
            //facem pe draq in patru aici si adaugam controlul pe forma
            parentDiagram.SuspendLayout();
            parentDiagram.AddFlowComponent(c);
            if (fc2 == null) {
                fc2 = new FlowConnector(this.button2, (c as FlowComponent).GetPinInput(), parentDiagram);
                parentDiagram.AddFlowConnector(fc2);
            }

            if (fc2 != null) {
                fc2.SetEndControl((c as FlowComponent).GetPinInput());
            }

            c.Left = this.Left - (c.Width - this.Width) / 2;
            c.Top = this.Bottom + 20;
            (c as FlowComponent).SlideControl(this.Width / 2, 0);
            if (NextControl != null) {
                (NextControl as FlowComponent).SlideControl(this.Width / 2, c.Height + 20);
                (c as FlowComponent).SetDefaultNextControl(NextControl);
            }

            this.NextControl = c;
            parentDiagram.ResumeLayout();
        }

        #endregion

        private void button2_Click(object sender, EventArgs e) {

            frmChooseNextComponent frmNC = new frmChooseNextComponent(this);
            frmNC.Left = Cursor.Position.X + 5;
            frmNC.Top = Cursor.Position.Y + 5;
            frmNC.ShowDialog();
            if (frmNC.CreatedObject != null) {
                fc2 = new FlowConnector(this.button2, (frmNC.CreatedObject as FlowComponent).GetPinInput(), parentDiagram);
                parentDiagram.AddFlowConnector(fc2);

                Control c = frmNC.CreatedObject;
                SetDefaultNextControl(c);
            }
        }
        FlowConnector fc1, fc2;
        private void button1_Click(object sender, EventArgs e) {
            frmChooseNextComponent frmNC = new frmChooseNextComponent(this);
            frmNC.Left = Cursor.Position.X + 5;
            frmNC.Top = Cursor.Position.Y + 5;
            frmNC.ShowDialog();
            if (frmNC.CreatedObject != null) {
                fc1 = new FlowConnector(this.button1, (frmNC.CreatedObject as FlowComponent).GetPinInput(), parentDiagram);
                parentDiagram.AddFlowConnector(fc1);
                Control c = frmNC.CreatedObject;
                SetNegationNextControl(c);
            }
        }
        public Control GetNegationNextControl() {
            return OtherControl;
        }
        public void SetNegationNextControl(Control c) {
            //throw new NotImplementedException();
            //facem pe draq in patru aici si adaugam controlul pe forma
            parentDiagram.SuspendLayout();
            parentDiagram.AddFlowComponent(c);
            if (fc1 == null) {
                fc1 = new FlowConnector(this.button1, (c as FlowComponent).GetPinInput(), parentDiagram);
                parentDiagram.AddFlowConnector(fc1);
            }

            if (fc1 != null) {
                fc1.SetEndControl((c as FlowComponent).GetPinInput());
            }

            c.Left = this.Left - (c.Width - this.Width) / 2;
            c.Top = this.Bottom + 20;
            (c as FlowComponent).SlideControl(-this.Width / 2, 0);
            if (OtherControl != null) {
                (OtherControl as FlowComponent).SlideControl(-this.Width / 2, c.Height + 20);
                (c as FlowComponent).SetDefaultNextControl(OtherControl);
            }

            this.OtherControl = c;
            parentDiagram.ResumeLayout();
        }
        public Control GetPinInput() {
            return this.button3;
        }
        public ExecutionUnit ExecutionUnit { get; set; }
        public void SetDataBinding(object o) {
            this.ExecutionUnit.DataBinding = (ExecutionUnitDataBinding)o;
        }
        public object GetDataBinding() {
            return ExecutionUnit.DataBinding;
        }
        public void SetSelected(bool value) {
        }
        public void SetName(string name) {
            this.lblDecisionName.Text = name;
        }


    }
    public delegate void ClickEvent(object sender, EventArgs e);
}
