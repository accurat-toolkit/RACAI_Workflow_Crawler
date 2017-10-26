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
    public partial class FlowStart : UserControl, FlowComponent {
        public FlowStart() {
            InitializeComponent();
            this.button1.Left = (this.Width - this.button1.Width) / 2;
            ExecutionUnit = new ExecutionUnit(this, this.label1.Text);
            this.label1.Click += new EventHandler(lblDecisionName_Click);
            this.Click += new EventHandler(lblDecisionName_Click);
        }
        public event ClickEvent ControlClick;

        void lblDecisionName_Click(object sender, EventArgs e) {
            if (ControlClick != null)
                ControlClick(this, e);
        }

        #region FlowComponent Members
        public void SetDataBinding(object o) {
            this.ExecutionUnit.DataBinding = (ExecutionUnitDataBinding)o;
        }
        FlowDiagram parentDiagram;
        public void SetComponentContainer(FlowDiagram fd) {
            //throw new NotImplementedException();
            parentDiagram = fd;
        }

        #endregion
        #region adaugare componente
        Control NextControl = null;
        FlowConnector fc;
        private void button1_Click(object sender, EventArgs e) {

            frmChooseNextComponent frmNC = new frmChooseNextComponent(this);
            frmNC.Left = Cursor.Position.X + 5;
            frmNC.Top = Cursor.Position.Y + 5;
            frmNC.ShowDialog();
            if (frmNC.CreatedObject != null) {
                fc = new FlowConnector(this.button1, (frmNC.CreatedObject as FlowComponent).GetPinInput(), parentDiagram);
                parentDiagram.AddFlowConnector(fc);
                Control c = frmNC.CreatedObject;
                SetDefaultNextControl(c);
            }
        }

        #endregion
        public FlowDiagram GetComponentContainer() {
            return parentDiagram;
        }


        public void SlideControl(int deltaX, int deltaY) {
            //throw new NotImplementedException();
            this.Left += deltaX;
            this.Top += deltaY;
            if (NextControl != null)
                (NextControl as FlowComponent).SlideControl(deltaX, deltaY);
        }

        public FlowComponent GetDefaultNextControl() {
            return NextControl as FlowComponent;
        }

        public void SetDefaultNextControl(Control c) {
            //throw new NotImplementedException();
            //facem pe draq in patru aici si adaugam controlul pe forma
            parentDiagram.SuspendLayout();
            parentDiagram.AddFlowComponent(c);

            if (fc == null) {
                fc = new FlowConnector(this.button1, (c as FlowComponent).GetPinInput(), parentDiagram);
                parentDiagram.AddFlowConnector(fc);
            }

            //se va modifica (acum e varza codul)
            if (fc != null) {
                fc.SetEndControl((c as FlowComponent).GetPinInput());
            }
            c.Left = this.Left - (c.Width - this.Width) / 2;
            c.Top = this.Bottom + 20;
            if (NextControl != null) {
                (NextControl as FlowComponent).SlideControl(0, c.Height + 20);
                (c as FlowComponent).SetDefaultNextControl(NextControl);
            }

            this.NextControl = c;
            parentDiagram.ResumeLayout();
        }
        public Control GetPinInput() {
            return null;
        }



        #region FlowComponent Members
        public ExecutionUnit ExecutionUnit { get; set; }
        public object GetDataBinding() {
            return ExecutionUnit.DataBinding;
        }
        public void SetSelected(bool value) {
        }
        public void SetName(string name) {

        }
        #endregion
    }
}
