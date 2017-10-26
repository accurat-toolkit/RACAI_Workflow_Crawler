using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agora.Text.UI.Flow.Interfaces;
using Agora.Text.UI.Flow.Execution;

namespace Agora.Text.UI.Flow {
    public partial class FlowStop : UserControl,FlowComponent {
        public FlowStop() {
            InitializeComponent();
            this.button3.Left = (this.Width - this.button3.Width) / 2;
            ExecutionUnit = new ExecutionUnit(this, this.label1.Text);
            label1.Click+=new EventHandler(lblDecisionName_Click);
            this.Click += new EventHandler(lblDecisionName_Click);
        }
        public event ClickEvent ControlClick;

        void lblDecisionName_Click(object sender, EventArgs e) {
            if (ControlClick != null)
                ControlClick(this, e);
        }


        #region FlowComponent Members
        FlowDiagram parentDiagram;
        public void SetDataBinding(object o) {
            this.ExecutionUnit.DataBinding = (ExecutionUnitDataBinding)o;
        }
        void FlowComponent.SetComponentContainer(FlowDiagram fd) {
            //throw new NotImplementedException();
            this.parentDiagram = fd;
        }


        public FlowDiagram GetComponentContainer() {
            return parentDiagram;
        }
        Control NextControl;
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
            return this.button3;
        }
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
