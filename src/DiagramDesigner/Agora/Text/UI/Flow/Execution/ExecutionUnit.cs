using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Drawing.Design;
using System.Windows.Forms;
using Agora.Text.UI.Flow.Interfaces;
using System.Diagnostics;
using System.Reflection;
using Agora.SysUtils.Logging;
using Agora.Builder.Interfaces;
using Agora.Builder.System;

namespace Agora.Text.UI.Flow.Execution {
    [Serializable]
    public class ExecutionUnit {
        //object Plugin;
        public ExecutionUnitDataBinding DataBinding { get; set; }
        public ExecutionUnit() { }
        public ExecutionUnit(FlowComponent parent, string name) {
            DataBinding = new ExecutionUnitDataBinding(parent);
            DataBinding.Name = name;
        }
        public ExecutionUnitOutput Execute(object input) {
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
                    return eo;
                } catch (Exception e) {
                    SysLog.MainInstance.WriteEventException(e);
                    return null;
                }
            }
        }
    }
    [Serializable]
    public class ExecutionUnitOutput {
        public string UnformatedOutput;
        public string RegexpOuput;
        public bool DecisionOutput;
        public object ObjectOutput;
    }

    [Serializable]
    public class ExecutionUnitDataBinding {
        string pluginDll;

        [EditorAttribute(typeof(FilePathEditor), typeof(UITypeEditor))]
        [CategoryAttribute("Execution")]
        [DescriptionAttribute("Dll file for internal plugin. Must implement Decision of Processing Block interfaces")]
        public string PluginDll {
            get { return pluginDll; }
            set { pluginDll = value; }
        }
        [Browsable(false)]
        public string Caption { get; set; }
        FlowComponent myParent;
        string name;
        [CategoryAttribute("Appearance")]
        [DescriptionAttribute("Current node name")]
        public string Name { get { return name; } set { this.Caption = value; this.name = value; if (myParent != null) this.myParent.SetName(value); if (myLabel != null) this.myLabel.Content = value; } }
        [CategoryAttribute("Execution")]
        [EditorAttribute(typeof(FilePathEditor), typeof(UITypeEditor))]
        [DescriptionAttribute("Path to script filename - used to run script")]
        public string ScriptPath { get; set; }
        /// <summary>
        /// Path to executable filename - used to run script
        /// </summary>
        [EditorAttribute(typeof(FilePathEditor), typeof(UITypeEditor))]
        [CategoryAttribute("Execution")]
        [DescriptionAttribute("Path to executable filename - used to run script")]
        public String ExecutablePath { get; set; }
        /// <summary>
        /// Commandline parameters: user $script for script filename and $input for inputstream
        /// </summary>
        [CategoryAttribute("Execution")]
        [DescriptionAttribute("Commandline parameters: user $script for script filename and $input for inputstream")]
        public string Parameters { get; set; }
        /// <summary>
        /// Regular expression to be applied to the input data
        /// </summary>
        [TypeConverterAttribute(typeof(ApplicationRegexExpander))]
        [CategoryAttribute("Regular expressions")]
        [DescriptionAttribute("Regular expression to be applied to the input data")]
        public ApplicationRegexOptions InputRegex { get; set; }
        /// <summary>
        /// Regular expression to be applied to the output data
        /// </summary>
        [TypeConverterAttribute(typeof(ApplicationRegexExpander))]
        [CategoryAttribute("Regular expressions")]
        [DescriptionAttribute("Regular expression to be applied to the output data")]
        public ApplicationRegexOptions OutputRegex { get; set; }
        /// <summary>
        /// Regular expression to be applied in decision making. If the output data contains this sequence we have true value for the condition
        /// </summary>
        [TypeConverterAttribute(typeof(ApplicationRegexExpander))]
        [CategoryAttribute("Regular expressions")]
        [DescriptionAttribute("Regular expression to be applied in decision making. If the output data contains this sequence we have true value for the condition")]
        public ApplicationRegexOptions ConditionRegex { get; set; }
        //public string 
        public void SetMyLabel(System.Windows.Controls.Label label) {
            myLabel = label;
        }
        System.Windows.Controls.Label myLabel;
        public ExecutionUnitDataBinding(System.Windows.Controls.Label myParent) {
            myLabel = myParent;
            //this.myParent = myParent;
            InputRegex = new ApplicationRegexOptions();
            OutputRegex = new ApplicationRegexOptions();
            ConditionRegex = new ApplicationRegexOptions();
        }
        public ExecutionUnitDataBinding(FlowComponent myParent) {
            this.myParent = myParent;
            InputRegex = new ApplicationRegexOptions();
            OutputRegex = new ApplicationRegexOptions();
            ConditionRegex = new ApplicationRegexOptions();
        }
        public ExecutionUnitDataBinding() { }
    }
    [Serializable]
    public class ApplicationRegexOptions {
        string regex = "(.*)";
        /// <summary>
        /// Regular expression to be applied to the input string
        /// </summary>
        [DescriptionAttribute("Regular expression to be applied to the input string")]
        public string Regex {
            get { return regex; }
            set { regex = value; }
        }
        RegexOptions options = RegexOptions.Multiline;
        /// <summary>
        /// .NET style Regx options. For more information, consult MSDN
        /// </summary>
        [DescriptionAttribute(".NET style Regx options. For more information, consult MSDN")]
        public RegexOptions Options {
            get { return options; }
            set { options = value; }
        }
    }
    public class FilePathEditor : UITypeEditor {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            //return base.EditValue(context, provider, value);
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) {
                return value;
            } else {
                value = ofd.FileName;
                return value;
            }
        }
    }
    public class ApplicationRegexExpander : ExpandableObjectConverter {
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                  System.Type destinationType) {
            if (destinationType == typeof(ApplicationRegexOptions))
                return true;

            return base.CanConvertTo(context, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context,
                              System.Type sourceType) {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

    }
}
