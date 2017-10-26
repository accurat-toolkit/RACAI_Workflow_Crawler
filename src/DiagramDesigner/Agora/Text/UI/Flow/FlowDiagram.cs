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
using Agora.Builder.Interfaces;
using Agora.Builder.System;
using System.Xml.Serialization;
using System.IO;

namespace Agora.Text.UI.Flow {
    public partial class FlowDiagram : UserControl {
        public bool Execute() {
            if (BaseApplication.MainInstance == null) {
                //ramane apoi sa le numaram sa fie ok 
                BaseApplication.MainInstance = new BaseApplication(1000);
            }
            FlowComponent current = StartNode.GetDefaultNextControl();
            ExecutionUnitOutput eo = null;
            Object dataToken = null;
            while ((current != null) && (!(current is FlowStop))) {
                //trecem la urmatorul nod si executam tot ce se poate:d
                //procesam datele
                if (current is FlowProcessing) {
                    //eo=(current as ProcessingBlock).ProcessData(dataToken, BaseApplication.MainInstance);
                    //dataToken = (current as ProcessingBlock).ProcessData(dataToken, BaseApplication.MainInstance);
                    //if (dataToken == null) return false;
                    eo = (current as FlowProcessing).ExecutionUnit.Execute(dataToken);
                    dataToken = eo.ObjectOutput;
                    if (dataToken == null)
                        return false;

                    current = current.GetDefaultNextControl();
                } else if (current is FlowDecision) {
                    eo = (current as FlowDecision).ExecutionUnit.Execute(dataToken);
                    dataToken = eo.ObjectOutput;
                    bool cond = eo.DecisionOutput;
                    //bool cond = (current as DecisionBlock).EvaluateCondition(dataToken, BaseApplication.MainInstance);
                    //dataToken = (current as DecisionBlock).GetData(dataToken, BaseApplication.MainInstance);
                    if (dataToken == null)
                        return false;
                    if (cond) {
                        current = (current as FlowDecision).GetDefaultNextControl();
                    } else {
                        current = ((current as FlowDecision).GetNegationNextControl() as FlowComponent);
                    }
                }
            }
            return true;
        }
        public FlowComponent StartNode;
        public FlowDiagram() {
            InitializeComponent();
            sg.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            bg.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            sg.Width = 1;
            bg.Width = 1;
            //incarcam o diagrama de start
            FlowStart fs = new FlowStart();
            StartNode = fs;
            this.SuspendLayout();
            this.pnlMain.Controls.Add(fs);
            fs.SetComponentContainer(this);
            fs.Left = (this.pnlMain.Width - fs.Width) / 2;
            fs.Top = 50;
            FlowComponents.Add(fs);
            this.AddFlowComponent(fs);
            this.ResumeLayout();
        }
        #region desenare
        public List<FlowConnector> FlowConnectors = new List<FlowConnector>();
        int smallGranularity = 10;

        public int SmallGranularity {
            get { return smallGranularity; }
            set { smallGranularity = value; }
        }
        int precision = 10;

        public int Precision {
            get { return precision; }
            set { precision = value; }
        }
        Pen sg = new Pen(Brushes.Red);
        Pen bg = new Pen(Brushes.Red);
        int offsetX;
        int offsetY;
        Bitmap bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width + 100, Screen.PrimaryScreen.WorkingArea.Height + 100);
        private void FlowDiagram_Paint(object sender, PaintEventArgs e) {
            //desenam liniile ajutatoare pe fundal
            int deltaX = offsetX % (smallGranularity * precision);
            int deltaY = offsetY % (smallGranularity * precision);
            foreach (FlowConnector fc in FlowConnectors) {
                fc.Paint(e.Graphics);
            }
            //e.Graphics.DrawImageUnscaled(bmp, -deltaX - smallGranularity * precision, -deltaY - smallGranularity * precision, this.pnlMain.Width + smallGranularity * precision, this.pnlMain.Height + smallGranularity * precision);
        }

        private void RebuildGrid() {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(this.pnlMain.BackColor);
            int tmp = 0;

            for (int offset = -precision * smallGranularity; offset < Math.Max(this.bmp.Width, this.bmp.Height) + precision * smallGranularity; offset += smallGranularity) {
                tmp++;
                if (tmp % precision != 0) {
                    g.DrawLine(sg, new Point(offset, 0), new Point(offset, this.bmp.Height));
                    g.DrawLine(sg, new Point(0, offset), new Point(this.bmp.Width, offset));
                } else {
                    g.DrawLine(bg, new Point(offset, 0), new Point(offset, this.bmp.Height));
                    g.DrawLine(bg, new Point(0, offset), new Point(this.bmp.Width, offset));
                }
            }

        }

        private void pnlMain_Resize(object sender, EventArgs e) {
            RebuildGrid();
            //this.pnlMain.AutoScrollMinSize = new Size(this.pnlMain.Width - 1, this.pnlMain.Height - 1);
        }
        bool isMouseDown = false;
        bool isAltPressed = false;
        int lx, ly;
        private void FlowDiagram_MouseDown(object sender, MouseEventArgs e) {
            isMouseDown = true;
            lx = e.X;
            ly = e.Y;

        }

        private void FlowDiagram_KeyDown(object sender, KeyEventArgs e) {
            if (e.Alt) {
                isAltPressed = true;
            } else {
                isAltPressed = false;
            }
        }

        private void FlowDiagram_KeyUp(object sender, KeyEventArgs e) {
            if (!e.Alt) {
                isAltPressed = false;
            } else {
                isAltPressed = true;
            }
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e) {
            isMouseDown = false;
        }

        private void pnlMain_MouseMove(object sender, MouseEventArgs e) {
            int cx = e.X;
            int cy = e.Y;
            isAltPressed = Control.ModifierKeys == Keys.Alt;
            if ((isMouseDown) && (isAltPressed)) {
                int dx = cx - lx;
                int dy = cy - ly;
                this.offsetX -= dx;
                this.offsetY -= dy;
                //this.SuspendLayout();
                foreach (Control c in FlowComponents) {
                    c.Left += dx;
                    c.Top += dy;
                }
                //this.ResumeLayout();
                this.pnlMain.Refresh();
            }
            lx = cx;
            ly = cy;

        }
        #endregion
        #region componente
        public Control GetRenderingPanel() {
            return this.pnlMain;
        }
        public void AddFlowComponent(Control c) {
            if (!this.pnlMain.Controls.Contains(c))
                this.pnlMain.Controls.Add(c);
            if (!this.flowComponents.Contains(c))
                this.flowComponents.Add(c);
            (c as FlowComponent).SetComponentContainer(this);
            //c.DoubleClick += new EventHandler(c_DoubleClick);
            //c.Click += new EventHandler(c_Click);
            if (c is FlowDecision) {
                (c as FlowDecision).ControlClick += new ClickEvent(FlowDiagram_ControlClick);
            } else if (c is FlowStart) {
                (c as FlowStart).ControlClick += new ClickEvent(FlowDiagram_ControlClick);
            } else if (c is FlowProcessing) {
                (c as FlowProcessing).ControlClick += new ClickEvent(FlowDiagram_ControlClick);
            } else if (c is FlowStop) {
                (c as FlowStop).ControlClick += new ClickEvent(FlowDiagram_ControlClick);
            }
        }

        void FlowDiagram_ControlClick(object sender, EventArgs e) {
            this.pgMain.SelectedObject = (sender as FlowComponent).GetDataBinding();
        }

        void c_Click(object sender, EventArgs e) {
            //throw new NotImplementedException();
        }

        List<Control> flowComponents = new List<Control>();

        public List<Control> FlowComponents {
            get { return flowComponents; }
            set { flowComponents = value; }
        }

        #endregion
        internal void RemoveFlowConnector(FlowConnector fc) {
            FlowConnectors.Remove(fc);
        }
        internal void AddFlowConnector(FlowConnector fc) {
            FlowConnectors.Add(fc);
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            this.Execute();
        }

        private void newToolStripButton_Click(object sender, EventArgs e) {
            FlowComponents.Clear();
            this.pnlMain.Controls.Clear();
            this.FlowConnectors.Clear();

            FlowStart fs = new FlowStart();
            StartNode = fs;
            this.SuspendLayout();
            this.pnlMain.Controls.Add(fs);
            fs.SetComponentContainer(this);
            fs.Left = (this.pnlMain.Width - fs.Width) / 2;
            fs.Top = 50;
            FlowComponents.Add(fs);
            this.AddFlowComponent(fs);
            this.ResumeLayout();
        }
        public void LoadFromFile(string FileName) {
            XmlSerializer ser = new XmlSerializer(typeof(FlowDiagramXML));
            Stream sw = new FileStream(FileName, FileMode.Open);
            FlowDiagramXML fdx = (FlowDiagramXML)ser.Deserialize(sw);
            sw.Close();
            sw.Dispose();
            this.flowComponents.Clear();
            this.pnlMain.Controls.Clear();
            this.FlowConnectors.Clear();
            RebuildUI(fdx.FlowTree);
        }

        private object RebuildUI(FlowComponentXML fcx) {
            //throw new NotImplementedException();
            //FlowComponentXML current = fdx.FlowTree;
            //while (
            FlowComponent fc = null;
            if (fcx.type == 1) {
                fc = new FlowStart();
                this.StartNode = fc;
            } else if (fcx.type == 2) {
                fc = new FlowStop();
            } else if (fcx.type == 3) {
                fc = new FlowProcessing();
            } else if (fcx.type == 4) {
                fc = new FlowDecision();
            }
            fc.SetComponentContainer(this);
            this.flowComponents.Add(fc as Control);
            this.pnlMain.Controls.Add(fc as Control);
            fc.SetDataBinding(fcx.data);
            fc.SetName(fcx.data.Name);
            (fc as Control).Left = fcx.left;
            (fc as Control).Top = fcx.top;
            if (fcx.next1 != null) {
                fc.SetDefaultNextControl(RebuildUI(fcx.next1) as Control);
            }
            if (fcx.next2 != null) {
                (fc as FlowDecision).SetNegationNextControl(RebuildUI(fcx.next2) as Control);
            }
            return fc;
        }
        public void SaveToFile(string FileName) {
            FlowDiagramXML fdx = new FlowDiagramXML(this);
            XmlSerializer ser = new XmlSerializer(typeof(FlowDiagramXML));
            Stream sw = new FileStream(FileName, FileMode.Create);
            ser.Serialize(sw, fdx);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e) {
            if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.SaveToFile(this.saveFileDialog1.FileName);
        }

        private void openToolStripButton_Click(object sender, EventArgs e) {
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.LoadFromFile(this.openFileDialog1.FileName);
        }
    }
    [Serializable]
    public class FlowComponentXML {
        public FlowComponentXML() { }
        public int left { get; set; }
        public int top { get; set; }
        public ExecutionUnitDataBinding data { get; set; }
        public FlowComponentXML(FlowComponent fc) {
            this.left = (fc as Control).Left;
            this.top = (fc as Control).Top;
            this.data = (ExecutionUnitDataBinding)fc.GetDataBinding();
            if (fc is FlowStart)
                this.type = 1;
            if (fc is FlowStop)
                this.type = 2;
            if (fc is FlowProcessing)
                this.type = 3;
            if (fc is FlowDecision)
                this.type = 4;
            if (this.type == 4) {
                //avem doi copii
                if (fc.GetDefaultNextControl() != null)
                    next1 = new FlowComponentXML(fc.GetDefaultNextControl());
                if ((fc as FlowDecision).GetNegationNextControl() != null)
                    next2 = new FlowComponentXML((fc as FlowDecision).GetNegationNextControl() as FlowComponent);
            } else {
                //avem un singur copil
                if (fc.GetDefaultNextControl() != null)
                    next1 = new FlowComponentXML(fc.GetDefaultNextControl());
            }
        }
        public FlowComponentXML next1 { get; set; }
        public FlowComponentXML next2 { get; set; }
        public int type { get; set; }
    }
    [Serializable]
    public class FlowDiagramXML {
        public FlowDiagramXML() { }
        public FlowDiagramXML(FlowDiagram fd) {
            FlowTree = new FlowComponentXML(fd.StartNode);
        }
        public FlowComponentXML FlowTree { get; set; }
    }
}
