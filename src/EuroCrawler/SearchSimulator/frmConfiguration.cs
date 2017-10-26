using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Agora.Text.Web.Extraction;
using System.Threading;
using System.IO;

namespace SearchSimulator {
    public partial class frmConfiguration : Form {
        Agora.Builder.System.BaseApplication MyApplication;
        public frmConfiguration(Agora.Builder.System.BaseApplication MyApplication) {
            this.MyApplication = MyApplication;
            InitializeComponent();
            getTopics();
            MyApplication.Memory[0] = "C:\\temp\\arch";
        }
        string response;
        private void getTopics() {
            searcher.wb = new TextWebBrowser();
            response = searcher.wb.Navigate("http://www.europarl.europa.eu/news/archive/search.do?language=EN");
            Topic[] topics = searcher.ExtractTopics(response);
            for (int i = 0; i < topics.Length; i++) {
                this.checkedListBox1.Items.Add(topics[i], true);
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) {

        }
        Searcher searcher = new Searcher();
        public string[] ArticleList;
        private void button1_Click(object sender, EventArgs e) {
            if (!Directory.Exists(this.edtPath.Text)) {
                MessageBox.Show("Destination folder must be set before you can continue");
                return;
            } else {
                if (Directory.GetFiles(this.edtPath.Text).Length != 0) {
                    MessageBox.Show("Destination folder must be empty!");
                    return;
                }
            }
            RefreshArticles();
            this.Close();
        }
        private void RefreshArticles() {
            this.listBox1.Items.Clear();
            //facem lista de topicuri de cautare

            //Topic[] tmp = (Topic[])this.checkedListBox1;
            //string[] topics=new string[tmp.Length];
            List<string> topics = new List<string>();
            for (int i = 0; i < this.checkedListBox1.CheckedItems.Count; i++) {
                topics.Add(((Topic)this.checkedListBox1.CheckedItems[i]).id);
            }
            string searchUrl = searcher.ExtractSearchUrl(response);
            ////activam search-ul
            string[] urls = searcher.ExtractSearchResults(topics.ToArray(), searchUrl);
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(urls);
            this.ArticleList = urls;
        }
        private void button1_Click_1(object sender, EventArgs e) {
            RefreshArticles();
            this.lblItemCount.Text = "Found: " + this.listBox1.Items.Count + " articles";
        }

        private void button3_Click(object sender, EventArgs e) {
            for (int i = 0; i < this.checkedListBox1.Items.Count;i++ ) {
                this.checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++) {
                this.checkedListBox1.SetItemChecked(i, true);
            }

        }

        private void button5_Click(object sender, EventArgs e) {
            ArticleList = new String[0];
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            MyApplication.Memory[0] = this.edtPath.Text;            
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
