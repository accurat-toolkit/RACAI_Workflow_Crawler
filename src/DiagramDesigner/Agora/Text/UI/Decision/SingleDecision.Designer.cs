namespace Agora.Text.UI.Decision {
    partial class SingleDecision {
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
            this.lblDecisionName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDecisionName
            // 
            this.lblDecisionName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDecisionName.BackColor = System.Drawing.Color.Transparent;
            this.lblDecisionName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecisionName.Location = new System.Drawing.Point(13, 17);
            this.lblDecisionName.Name = "lblDecisionName";
            this.lblDecisionName.Size = new System.Drawing.Size(109, 32);
            this.lblDecisionName.TabIndex = 0;
            this.lblDecisionName.Text = "label1";
            this.lblDecisionName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDecisionName.DoubleClick += new System.EventHandler(this.lblDecisionName_DoubleClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // SingleDecision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::Agora.Properties.Resources.DecisionBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDecisionName);
            this.Name = "SingleDecision";
            this.Size = new System.Drawing.Size(132, 57);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDecisionName;
        private System.Windows.Forms.Label label1;
    }
}
