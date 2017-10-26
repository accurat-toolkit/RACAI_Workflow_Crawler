using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agora.Builder.Interfaces;
using System.Windows.Forms;

namespace Common {
    public class MainClass : ProcessingBlock {
        #region ProcessingBlock Members
        object ProcessingBlock.ProcessData(object data, Agora.Builder.System.BaseApplication MyApplication) {
            frmMain fc = new frmMain();
            fc.StartPosition = FormStartPosition.CenterScreen;
            fc.ShowDialog();
            return null;
        }
        #endregion
    }
}
