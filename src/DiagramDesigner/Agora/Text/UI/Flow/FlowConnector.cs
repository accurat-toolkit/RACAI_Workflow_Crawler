using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Agora.Text.UI.Flow {
    public class FlowConnector {
        public FlowConnector(Control start, Control stop, FlowDiagram diagram) {
            this.parentDiagram = diagram;
            this.startControl = start;
            this.stopControl = stop;
        }
        FlowDiagram parentDiagram;
        Control startControl;
        Control stopControl;
        public void SetEndControl(Control c) {
            stopControl = c;
        }
        private static Pen p = new Pen(Brushes.Black);
        public void Paint(Graphics g){
            p.Width = 5;
            Point pos1 = GetControlPosition(startControl);
            pos1.X = pos1.X + startControl.Width / 2;
            Point pos2 = GetControlPosition(stopControl);
            pos2.X = pos2.X + stopControl.Width / 2;
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(p, pos1, pos2);
        }

        private Point GetControlPosition(Control startControl) {
            Control tmp = startControl;
            Point start=new Point(0,0);
            try {
                while (tmp != parentDiagram.GetRenderingPanel()) {
                    start.X += tmp.Location.X;
                    start.Y += tmp.Location.Y;
                    tmp = tmp.Parent;
                }
            } catch { }
            return start;
        }
    }
}
