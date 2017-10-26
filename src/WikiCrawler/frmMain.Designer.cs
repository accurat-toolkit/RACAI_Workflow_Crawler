namespace Common
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Wikitravel.org");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.chkWordCounter = new System.Windows.Forms.CheckBox();
            this.chkSilent = new System.Windows.Forms.CheckBox();
            this.edtPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.edtPagesNeeded = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProcesare = new System.Windows.Forms.Button();
            this.edtServerLocation = new System.Windows.Forms.TextBox();
            this.edtBaseURL = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeResults = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.edtResults = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtPagesNeeded)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.lblResults);
            this.groupBox1.Controls.Add(this.chkWordCounter);
            this.groupBox1.Controls.Add(this.chkSilent);
            this.groupBox1.Controls.Add(this.edtPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.edtPagesNeeded);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnProcesare);
            this.groupBox1.Controls.Add(this.edtServerLocation);
            this.groupBox1.Controls.Add(this.edtBaseURL);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 127);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings: ";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(253, 78);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(65, 13);
            this.lblResults.TabIndex = 11;
            this.lblResults.Text = "Results: 0/0";
            // 
            // chkWordCounter
            // 
            this.chkWordCounter.AutoSize = true;
            this.chkWordCounter.Location = new System.Drawing.Point(394, 77);
            this.chkWordCounter.Name = "chkWordCounter";
            this.chkWordCounter.Size = new System.Drawing.Size(81, 17);
            this.chkWordCounter.TabIndex = 10;
            this.chkWordCounter.Text = "Word mode";
            this.chkWordCounter.UseVisualStyleBackColor = true;
            this.chkWordCounter.Visible = false;
            // 
            // chkSilent
            // 
            this.chkSilent.AutoSize = true;
            this.chkSilent.Location = new System.Drawing.Point(166, 77);
            this.chkSilent.Name = "chkSilent";
            this.chkSilent.Size = new System.Drawing.Size(81, 17);
            this.chkSilent.TabIndex = 10;
            this.chkSilent.Text = "Silent mode";
            this.chkSilent.UseVisualStyleBackColor = true;
            // 
            // edtPath
            // 
            this.edtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtPath.Location = new System.Drawing.Point(91, 99);
            this.edtPath.Name = "edtPath";
            this.edtPath.ReadOnly = true;
            this.edtPath.Size = new System.Drawing.Size(561, 20);
            this.edtPath.TabIndex = 9;
            this.edtPath.Text = "Not set...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Output path:";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(507, 73);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(99, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // edtPagesNeeded
            // 
            this.edtPagesNeeded.Location = new System.Drawing.Point(91, 76);
            this.edtPagesNeeded.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.edtPagesNeeded.Name = "edtPagesNeeded";
            this.edtPagesNeeded.Size = new System.Drawing.Size(69, 20);
            this.edtPagesNeeded.TabIndex = 6;
            this.edtPagesNeeded.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Results needed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Server location:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Base URL:";
            // 
            // btnProcesare
            // 
            this.btnProcesare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcesare.Location = new System.Drawing.Point(612, 73);
            this.btnProcesare.Name = "btnProcesare";
            this.btnProcesare.Size = new System.Drawing.Size(75, 23);
            this.btnProcesare.TabIndex = 3;
            this.btnProcesare.Text = "Start";
            this.btnProcesare.UseVisualStyleBackColor = true;
            this.btnProcesare.Click += new System.EventHandler(this.button1_Click);
            // 
            // edtServerLocation
            // 
            this.edtServerLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtServerLocation.Location = new System.Drawing.Point(93, 47);
            this.edtServerLocation.Name = "edtServerLocation";
            this.edtServerLocation.Size = new System.Drawing.Size(594, 20);
            this.edtServerLocation.TabIndex = 2;
            this.edtServerLocation.Text = "/ro/Bucureşti";
            // 
            // edtBaseURL
            // 
            this.edtBaseURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtBaseURL.Location = new System.Drawing.Point(93, 21);
            this.edtBaseURL.Name = "edtBaseURL";
            this.edtBaseURL.Size = new System.Drawing.Size(594, 20);
            this.edtBaseURL.TabIndex = 1;
            this.edtBaseURL.Text = "http://wikitravel.org";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(0, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 284);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeResults);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.edtResults);
            this.splitContainer1.Size = new System.Drawing.Size(687, 265);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeResults
            // 
            this.treeResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.treeResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeResults.ImageIndex = 0;
            this.treeResults.ImageList = this.imageList1;
            this.treeResults.Location = new System.Drawing.Point(0, 0);
            this.treeResults.Name = "treeResults";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Wikitravel.org";
            this.treeResults.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeResults.SelectedImageIndex = 0;
            this.treeResults.Size = new System.Drawing.Size(229, 265);
            this.treeResults.TabIndex = 0;
            this.treeResults.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeResults_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "iDisk-icon.png");
            this.imageList1.Images.SetKeyName(1, "Web-icon.png");
            this.imageList1.Images.SetKeyName(2, "Document-icon.png");
            // 
            // edtResults
            // 
            this.edtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.edtResults.Location = new System.Drawing.Point(0, 1);
            this.edtResults.Multiline = true;
            this.edtResults.Name = "edtResults";
            this.edtResults.ReadOnly = true;
            this.edtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtResults.Size = new System.Drawing.Size(454, 261);
            this.edtResults.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(693, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(658, 98);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 442);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "WikiCrawler";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtPagesNeeded)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnProcesare;
        private System.Windows.Forms.TextBox edtServerLocation;
        private System.Windows.Forms.TextBox edtBaseURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown edtPagesNeeded;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox edtPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeResults;
        private System.Windows.Forms.TextBox edtResults;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkSilent;
        private System.Windows.Forms.CheckBox chkWordCounter;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

    }
}

