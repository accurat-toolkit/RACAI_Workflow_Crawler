namespace Agora.Text.UI.Flow.Windows {
    partial class frmChooseNextComponent {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnDecision = new System.Windows.Forms.Button();
            this.btnProcessing = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDecision
            // 
            this.btnDecision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecision.Location = new System.Drawing.Point(12, 31);
            this.btnDecision.Name = "btnDecision";
            this.btnDecision.Size = new System.Drawing.Size(194, 23);
            this.btnDecision.TabIndex = 0;
            this.btnDecision.Text = "Decision node";
            this.btnDecision.UseVisualStyleBackColor = true;
            this.btnDecision.Click += new System.EventHandler(this.btnDecision_Click);
            // 
            // btnProcessing
            // 
            this.btnProcessing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessing.Location = new System.Drawing.Point(12, 60);
            this.btnProcessing.Name = "btnProcessing";
            this.btnProcessing.Size = new System.Drawing.Size(194, 23);
            this.btnProcessing.TabIndex = 0;
            this.btnProcessing.Text = "Processing";
            this.btnProcessing.UseVisualStyleBackColor = true;
            this.btnProcessing.Click += new System.EventHandler(this.btnProcessing_Click);
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(12, 89);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(194, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "End node";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(190, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(16, 22);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChooseNextComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 121);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnProcessing);
            this.Controls.Add(this.btnDecision);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChooseNextComponent";
            this.Text = "frmChooseNextComponent";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDecision;
        private System.Windows.Forms.Button btnProcessing;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClose;
    }
}