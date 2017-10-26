using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Agora.Text.UI.Flow.Execution;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Agora.SysUtils.Logging;
using System.Reflection;
using Agora.Builder.Interfaces;
using Agora.Builder.System;
using System.Windows.Markup;
using System.Windows.Media;

namespace DiagramDesigner.Execution {
    public class Orchestrator {
        ExecutionBlock root;
        List<ExecutionBlock> denyList = new List<ExecutionBlock>();
        public Orchestrator(DesignerItem root, Connection[] connections) {
            //presupunem ca am primit nodul de start si contruim diagram pornind din el
            denyList.Clear();
            this.root = BuildExecutionTree(root, connections);
        }
        frmProgress fp = null;
        public void Execute(frmProgress fp) {
            this.fp = fp;
            ExecutionBlock currentNode = root.NormalChild;
            object lastObject = null;
            while (currentNode != null) {
                AddText("Executing block " + currentNode.DataBinding.Name + "\r\n");

                ExecutionUnitOutput output = currentNode.Execute(lastObject);
                currentNode = currentNode.NormalChild;
                if (output == null) {
                    lastObject = null;
                } else {
                    lastObject = output.ObjectOutput;
                    if (currentNode is DecisionUnit) {
                        if (!output.DecisionOutput) {
                            currentNode = currentNode.NegationChild;
                        }
                    }
                }
            }
            AddText("Execution done!");
        }
        delegate void AddTextDelegate(string p);
        private void AddText(string p) {
            if (fp.Dispatcher.Thread != System.Threading.Thread.CurrentThread) {
                AddTextDelegate d = new AddTextDelegate(AddText);
                fp.Dispatcher.Invoke(d, new object[] { p });
            } else {
                fp.edtLog.Text += p;
            }
        }

        private ExecutionBlock BuildExecutionTree(DesignerItem root, Connection[] connections) {
            foreach (ExecutionBlock ebtmp in denyList) {
                if (ebtmp.MyDesignerItem == root)
                    return ebtmp;
            }
            ExecutionBlock eb = null;
            string s = XamlWriter.Save(root.Content);
            if ((s.Contains("ToolTip=\"Start\"")) || (s.Contains("ToolTip=\"Terminator\"")) || (s.Contains("ToolTip=\"Process\""))) {
                eb = new ProcessingUnit(root.DataBinding);
                eb.MyDesignerItem = root;
                denyList.Add(eb);
                //cautam urmatorul element dupa conector si generam lista
                foreach (Connection c in connections) {
                    if (c.Source.ParentDesignerItem == root) {
                        eb.NormalChild = BuildExecutionTree(c.Sink.ParentDesignerItem, connections);
                        break;
                    }
                }
            } else {
                //avem nod decisional
                eb = new DecisionUnit(root.DataBinding);
                eb.MyDesignerItem = root;
                denyList.Add(eb);
                //aflam conexiunea pentru copilul normal si negat
                int cnt = 0;
                foreach (Connection c in connections) {
                    if (cnt == 2) break;
                    if (c.Source.ParentDesignerItem == root) {
                        if (c.Source.Name == "Right") {
                            cnt++;
                            //copilul normal
                            eb.NormalChild = BuildExecutionTree(c.Sink.ParentDesignerItem, connections);
                        } else {
                            //copilul negat
                            cnt++;
                            eb.NegationChild = BuildExecutionTree(c.Sink.ParentDesignerItem, connections);
                        }
                    }
                }
            }

            return eb;
        }

        private void RecurrentBuild(DesignerItem root, Connection[] connections) {
            throw new NotImplementedException();
        }

    }
    public class ExecutionBlock {
        public DesignerItem MyDesignerItem { get; set; }
        public ExecutionBlock(ExecutionUnitDataBinding db) {
            this.DataBinding = db;
        }
        public ExecutionUnitDataBinding DataBinding { get; set; }
        delegate void Func();
        private void ChangeColor1() {
            MyDesignerItem.Caption.Foreground = Brushes.Red;
        }
        private void ChangeColor2() {
            MyDesignerItem.Caption.Foreground = Brushes.Black;
        }
        [STAThread]
        public ExecutionUnitOutput Execute(object input) {
            MyDesignerItem.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Func(ChangeColor1));
            //MyDesignerItem.Caption.Foreground = Brushes.Red;
            System.Windows.Forms.Application.DoEvents();
            if (DataBinding.PluginDll == "") {
                try {
                    Regex inputReg = new Regex(DataBinding.InputRegex.Regex, DataBinding.InputRegex.Options);
                    Regex outputReg = new Regex(DataBinding.OutputRegex.Regex, DataBinding.OutputRegex.Options);
                    Regex decisionReg = new Regex(DataBinding.ConditionRegex.Regex, DataBinding.ConditionRegex.Options);
                    string inp = "";
                    foreach (Match m in inputReg.Matches(input.ToString())) {
                        inp += m.Value;
                    }

                    Process p = new Process();
                    p.StartInfo.FileName = DataBinding.ExecutablePath;
                    p.StartInfo.Arguments = DataBinding.Parameters.Replace("$script", DataBinding.ScriptPath).Replace("$input", inp);
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();

                    string str = p.StandardOutput.ReadToEnd();
                    ExecutionUnitOutput eo = new ExecutionUnitOutput();
                    eo.UnformatedOutput = str;
                    eo.DecisionOutput = decisionReg.IsMatch(str);
                    eo.RegexpOuput = "";
                    foreach (Match m in outputReg.Matches(str)) {
                        eo.RegexpOuput += m.Value;
                    }
                    eo.ObjectOutput = eo.RegexpOuput;
                    p.Dispose();
                    MyDesignerItem.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Func(ChangeColor2));
                    return eo;
                } catch (Exception e) { SysLog.MainInstance.WriteEventException(e); return null; }
            } else {
                try {
                    //incarcam dll-ul, cautam un assembly compatibil (trebuie sa fie doar unul) si-l instantiem in obiect
                    ExecutionUnitOutput eo = new ExecutionUnitOutput();

                    //if (Plugin == null) {
                    Assembly asm = Assembly.LoadFile(DataBinding.PluginDll);
                    foreach (Type t in asm.GetTypes()) {
                        //trebuie sa instantiem tipurile ca sa verificam daca implementeaza interfata
                        try {
                            Object o = Activator.CreateInstance(t);
                            if (o is DecisionBlock) {
                                eo.DecisionOutput = (o as DecisionBlock).EvaluateCondition(input, BaseApplication.MainInstance);
                                eo.ObjectOutput = (o as DecisionBlock).GetData(input, BaseApplication.MainInstance);
                            }
                            if (o is ProcessingBlock) {
                                eo.ObjectOutput = (o as ProcessingBlock).ProcessData(input, BaseApplication.MainInstance);
                            }
                        } catch (Exception e) {
                            SysLog.MainInstance.WriteEventException(e);
                        }
                    }
                    //}
                    MyDesignerItem.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Func(ChangeColor2));
                    return eo;
                } catch (Exception e) {
                    SysLog.MainInstance.WriteEventException(e);
                    MyDesignerItem.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Func(ChangeColor2));
                    return null;
                }
            }
        }
        public ExecutionBlock NormalChild { get; set; }
        public ExecutionBlock NegationChild { get; set; }
    }
    public class ProcessingUnit : ExecutionBlock {
        ExecutionBlock eu;
        public ProcessingUnit(ExecutionUnitDataBinding db) : base(db) { }
    }
    public class DecisionUnit : ExecutionBlock {
        ExecutionBlock eu;
        public DecisionUnit(ExecutionUnitDataBinding db) : base(db) { }
    }
}
