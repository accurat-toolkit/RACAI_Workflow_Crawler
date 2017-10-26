using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Agora.Text.Web.Processing.Sites.Wikitravel;
using System.IO;
using System.Threading;

namespace Common {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        WikitravelCrawler wc = null;
        private void button1_Click(object sender, EventArgs e) {
            //WikitravelDataExtraction wte = new WikitravelDataExtraction(this.textBox1.Text);
            //this.lstLinkuri.Items.Clear();
            //this.lstLinkuri.Items.AddRange(wte.FollowLinks);
            //this.textBox2.Text = wte.TextContents;
            if (this.btnProcesare.Text == "Start") {
                if (!Directory.Exists(this.edtPath.Text)) {
                    MessageBox.Show("Destination folder must be set before you can continue");
                    return;
                } else {
                    if (Directory.GetFiles(this.edtPath.Text).Length != 0) {
                        MessageBox.Show("Destination folder must be empty!");
                        return;
                    }
                }
                this.btnProcesare.Text = "Stop";
                wc = new WikitravelCrawler(this.edtBaseURL.Text, this.edtServerLocation.Text, (int)this.edtPagesNeeded.Value);
                this.treeResults.Nodes[0].Nodes.Clear();
                wc.DataCollected += new WikitravelCrawler.DataCollectedEvent(wc_DataCollected);
                wc.NoMoreData += new WikitravelCrawler.DataCollectedEnd(wc_NoMoreData);
                wc.StartCrawling();
                this.progressBar1.Value = 0;
                this.progressBar1.Maximum = (int)this.edtPagesNeeded.Value;
                ro_en = 0;
                ro_de = 0;
            } else {
                this.btnProcesare.Text = "Start";
                wc.StopCrawling();
                this.progressBar1.Value = 0;
                this.progressBar1.Maximum = (int)this.edtPagesNeeded.Value;

            }
        }

        void wc_NoMoreData() {
            try {
                if (InvokeRequired) {
                    AccessUI del = delegate { wc_NoMoreData(); };
                    Invoke(del);
                    return;
                }
                MessageBox.Show("Done!");
                button1_Click(null, null);
            } catch { }
        }

        int ro_en = 0;
        int ro_de = 0;

        delegate void AccessUI();
        [STAThread]
        void wc_DataCollected(DataCollectedEventArgs e) {
            try {
                if (InvokeRequired) {
                    AccessUI del = delegate { wc_DataCollected(e); };
                    Invoke(del);
                    return;
                }
                if (!chkSilent.Checked) {
                    //aici restul de cod
                    TreeNode root = this.treeResults.Nodes[0];
                    //verificam daca avem nod adaugat. Daca nu cream unul nou pentru pozitia curenta
                    TreeNode tn;
                    if (root.Nodes.Count <= e.CrawlerID) {
                        //generam unul nou
                        tn = new TreeNode();
                        root.Nodes.Add(tn);
                        tn.ImageIndex = 1;
                        tn.SelectedImageIndex = 1;
                        tn.StateImageIndex = 1;
                        tn.Text = e.URL;
                        tn.Tag = "Crawler ID = " + e.CrawlerID;
                    } else {
                        tn = root.Nodes[e.CrawlerID];
                    }



                    //avem nodul pe care trebuie sa punem datele
                    TreeNode newChild = new TreeNode();
                    tn.Nodes.Add(newChild);
                    newChild.Text = "[" + e.Language.ToString() + "] " + e.URL;
                    newChild.ImageIndex = 2;
                    newChild.SelectedImageIndex = 2;
                    newChild.StateImageIndex = 2;
                    newChild.Tag = e.Text;
                }
                //} else {
                //    if (chkWordCounter.Checked) {
                //        //TextWriter tw = new StreamWriter(this.edtPath.Text + e.CrawlerID + "_" + e.Language.ToString() + ".txt", false, Encoding.UTF8);
                //        //tw.Write(e.Text);
                //        //tw.Flush();
                //        //tw.Close();
                //        //tw.Dispose();
                //        int word_count = e.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                //        if (e.Language == LanguageType.RO) {
                //            ro_en += word_count;
                //            ro_de += word_count;
                //        }
                //        if (e.Language == LanguageType.EN) {
                //            ro_en += word_count;
                //        }
                //        if (e.Language == LanguageType.DE) {
                //            ro_de += word_count;
                //        }
                //        this.lblResults.Text = "Results: " + ro_en + "/" + ro_de;
                //        //TextWriter twDesc = new StreamWriter(this.edtPath.Text + "descriptor.txt", true);
                //        //twDesc.WriteLine(this.edtPath.Text + e.CrawlerID + "_" + e.Language.ToString() + ".txt " + word_count.ToString() + " " + e.URL);
                //        //twDesc.Flush();
                //        //twDesc.Close();
                //        //twDesc.Dispose();
                //        if ((ro_en > this.edtPagesNeeded.Value) && (ro_de > this.edtPagesNeeded.Value)) {
                //            this.button1_Click(null, new EventArgs());
                //        }
                //    }

                //}
                this.progressBar1.Value = e.CrawlerID + 1;
                this.lblResults.Text = "Results: " + this.progressBar1.Value + "/" + this.edtPagesNeeded.Value;
                int wcount = e.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                string d = "";
                if (this.edtPath.Text[this.edtPath.Text.Length - 1] != '\\')
                    d = "\\";
                TextWriter tw = new StreamWriter(this.edtPath.Text + d + e.CrawlerID + "_" + e.Language.ToString() + ".txt", false, Encoding.UTF8);
                tw.Write(e.Text);
                tw.Flush();
                tw.Close();
                tw.Dispose();
                TextWriter twDesc = new StreamWriter(this.edtPath.Text + d + "descriptor.txt", true);
                twDesc.WriteLine(this.edtPath.Text + d + e.CrawlerID + "_" + e.Language.ToString() + ".txt " + wcount.ToString() + " " + e.URL);
                twDesc.Flush();
                twDesc.Close();
                twDesc.Dispose();
            } catch (Exception ex) { MessageBox.Show("There was an exception inside the WIKICRAWLER plugin. Please report the following :" + ex.ToString()); this.button1_Click(null, EventArgs.Empty); }
        }

        private void treeResults_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            TreeNode tn = e.Node;
            if (tn.Tag != null) {
                this.edtResults.Text = (string)tn.Tag;
            } else {
                this.edtResults.Text = "Root node - nothing to see here";
            }
        }
        delegate void SetPathCallback(string text);
        private void SetPath(string text) {
            if (this.edtPath.InvokeRequired) {
                SetPathCallback d = new SetPathCallback(SetPath);
                this.Invoke(d, new object[] { text });
            } else {
                this.edtPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
        [STAThread]
        private void DialogThing(object o) {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                SetPath(this.folderBrowserDialog1.SelectedPath);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e) {
            Thread t = new Thread(new ParameterizedThreadStart(DialogThing));
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
        }
    }
}
