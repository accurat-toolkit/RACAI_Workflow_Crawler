using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agora.Builder.Interfaces;
using System.IO;
using System.Text.RegularExpressions;
using Agora.Text.Web.Extraction;
using System.Windows.Forms;
using System.Web;
using System.Threading;

namespace ResultProcessor {
    public class MainClass : ProcessingBlock {
        #region other processing
        public string[] ExtractDataUsingRegex(Regex r, string text) {
            Match m = r.Match(text);
            List<string> tmp = new List<string>();
            while (m.Success) {
                tmp.Add(m.Value);
                m = m.NextMatch();
            }
            return tmp.ToArray();
        }
        private string RemoveHTML(string response) {
            Regex r = new Regex("<div id=\"content\">(.*?)<div class=\"blocref\">", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //HttpUtility.
            string rasp = HtmlUtility.GetPlainText(response);
            //string[] text = ExtractDataUsingRegex(r, response);
            //string rasp = server.HtmlDecode(response); ;
            //foreach (string tmp in text) {
            //    //postprocesare pentru eliminarea de taguri html
            //    string tmpProc = tmp;

            //    Regex filtrare1 = new Regex(@"<(.*?)>", RegexOptions.Singleline);
            //    tmpProc = filtrare1.Replace(tmpProc, "");
            //    //inlocuire taguri html
            //    tmpProc = tmpProc.Replace("&lt;", "<");
            //    tmpProc = tmpProc.Replace("&quot;", "\"");
            //    tmpProc = tmpProc.Replace("&nbsp;", " ");
            //    tmpProc = tmpProc.Replace("&iexcl;", "!");
            //    tmpProc = tmpProc.Replace("\r", "");
            //    tmpProc = tmpProc.Replace("\n", "");
            //    //TODO: de completat ce se mai poate reprezenta
            //    //ce ne scapa distrugem
            //    Regex filtrare2 = new Regex(@"&(.*?);");
            //    tmpProc = filtrare2.Replace(tmpProc, "");
            //    tmpProc = tmpProc.Replace("&amp;", "&");

            //    rasp = rasp + tmpProc + "\r\n";
            //}
            if ((rasp.Contains("Document not found")) || (rasp.Contains("PDF-1.4"))) return "";
            return rasp;
        }
        #endregion
        #region ProcessingBlock Members
        private static string[] Limbi = new string[] { "RO", "EN", "DE", "BG", "ES", "CS",
            "DA","ET","EL", "FR", "IT", "LV","LT","HU","MT","NL",
            "PL", "PT","SK", "SL", "FI", "SV"
        };
        List<string> lstFisiere = new List<string>();
        ManualResetEvent ev = new ManualResetEvent(false);
        delegate void SetTextDelegate(string text);
        public void SetText(string text) {
            if (fp.lblArticle.InvokeRequired) {
                SetTextDelegate d = new SetTextDelegate(SetText);
                fp.lblArticle.Invoke(d, new object[] { text });
            } else {
                fp.lblArticle.Text = text;
            }
        }
        delegate void SetValueDelegate(int i);
        private void SetValue(int i) {
            if (fp.progressBar1.InvokeRequired) {
                SetValueDelegate d = new SetValueDelegate(SetValue);
                fp.progressBar1.Invoke(d, new object[] { i });
            } else {
                fp.progressBar1.Value = i;
            }
        }
        private void Crawl(object o) {
            Agora.Builder.System.BaseApplication MyApplication = (Agora.Builder.System.BaseApplication)o;
            TextWebBrowser wb = new TextWebBrowser();
            String BasePath = MyApplication.Memory[0].ToString();
            if (BasePath[BasePath.Length - 1] != '\\') {
                BasePath += "\\";
            }
            TextWriter twMaster = new StreamWriter(BasePath + "descriptor.txt");
            string[] urls = (string[])data;
            int i = 0;
            foreach (string url in urls) {
                if (fp.Cancel)
                    break;
                SetText(url);
                i++;
                SetValue(i);
                //Log("Procesez : " + i + "/" + urls.Length);
                foreach (string limba in Limbi) {
                    try {
                        string response = wb.Navigate(url + limba);
                        string text = RemoveHTML(response);
                        TextWriter tw = new StreamWriter(BasePath + "" + i + "_" + limba + ".txt");
                        tw.Write(text);
                        tw.Close();
                        tw.Dispose();
                        twMaster.WriteLine(BasePath + "" + i + "_" + limba + ".txt" + " " + url + " " + text.Split(new char[] { ' ', ',', '.', ';' }, StringSplitOptions.RemoveEmptyEntries).Length);
                        lstFisiere.Add(BasePath + "" + i + "_" + limba + ".txt");
                    } catch (Exception e) {
                        MessageBox.Show(e.ToString());
                    }
                }
            }
            twMaster.Close();
            twMaster.Dispose();
            done = true;
        }
        frmProgress fp = new frmProgress();
        object data;
        bool done = false;
        object ProcessingBlock.ProcessData(object data, Agora.Builder.System.BaseApplication MyApplication) {
            this.data = data;
            string[] urls = (string[])data;
            fp.progressBar1.Maximum = urls.Length;
            fp.progressBar1.Value = 0;
            fp.Show();
            Application.DoEvents();
            Thread t = new Thread(new ParameterizedThreadStart(Crawl));
            t.Start(MyApplication);
            while (!done) {
                Thread.Sleep(10);
                Application.DoEvents();
                if (fp.Cancel) {
                    t.Abort();
                    break;
                }
            }
            fp.Close();
            fp.Dispose();
            //MessageBox.Show(lstFisiere.Count.ToString());

            return lstFisiere.ToArray();
        }

        #endregion
    }

}
