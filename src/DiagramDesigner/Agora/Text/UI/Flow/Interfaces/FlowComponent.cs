using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Agora.Text.UI.Flow.Interfaces {
    public interface FlowComponent {
        void SetComponentContainer(FlowDiagram fd);
        FlowDiagram GetComponentContainer();
        void SlideControl(int deltaX, int deltaY);
        FlowComponent GetDefaultNextControl();
        void SetDefaultNextControl(Control c);
        Control GetPinInput();
        object GetDataBinding();
        void SetDataBinding(object o);
        void SetSelected(bool value);

        void SetName(string value);
    }
}
