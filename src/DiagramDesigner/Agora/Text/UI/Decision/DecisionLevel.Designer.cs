namespace Agora.Text.UI.Decision {
    partial class DecisionLevel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mnuAddRemove = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addValidatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.orientedLabel1 = new Agora.Text.UI.Decision.OrientedLabel();
            this.panel1.SuspendLayout();
            this.mnuAddRemove.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ContextMenuStrip = this.mnuAddRemove;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.pnlMain);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.orientedLabel1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 150);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.ControlClick);
            // 
            // mnuAddRemove
            // 
            this.mnuAddRemove.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addValidatorToolStripMenuItem});
            this.mnuAddRemove.Name = "mnuAddRemove";
            this.mnuAddRemove.Size = new System.Drawing.Size(150, 26);
            // 
            // addValidatorToolStripMenuItem
            // 
            this.addValidatorToolStripMenuItem.Name = "addValidatorToolStripMenuItem";
            this.addValidatorToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.addValidatorToolStripMenuItem.Text = "Add validator";
            this.addValidatorToolStripMenuItem.Click += new System.EventHandler(this.addValidatorToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackgroundImage = global::Agora.Properties.Resources.Delete_icon;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(517, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 25);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.AutoScroll = true;
            this.pnlMain.ContextMenuStrip = this.mnuAddRemove;
            this.pnlMain.Location = new System.Drawing.Point(17, 30);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(527, 83);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.Click += new System.EventHandler(this.ControlClick);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImage = global::Agora.Properties.Resources.up_icon;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(485, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 25);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = global::Agora.Properties.Resources.down_icon;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(516, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // orientedLabel1
            // 
            this.orientedLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.orientedLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.orientedLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.orientedLabel1.Location = new System.Drawing.Point(0, 0);
            this.orientedLabel1.Name = "orientedLabel1";
            this.orientedLabel1.RotationAngle = -90;
            this.orientedLabel1.Size = new System.Drawing.Size(17, 148);
            this.orientedLabel1.TabIndex = 0;
            this.orientedLabel1.Text = "Decision Level";
            this.orientedLabel1.TextDirection = Agora.Text.UI.Decision.Direction.AntiClockwise;
            this.orientedLabel1.TextOrientation = Agora.Text.UI.Decision.Orientation.Rotate;
            this.orientedLabel1.Click += new System.EventHandler(this.ControlClick);
            // 
            // DecisionLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "DecisionLevel";
            this.Size = new System.Drawing.Size(546, 150);
            this.panel1.ResumeLayout(false);
            this.mnuAddRemove.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private OrientedLabel orientedLabel1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ContextMenuStrip mnuAddRemove;
        private System.Windows.Forms.ToolStripMenuItem addValidatorToolStripMenuItem;
    }
}
