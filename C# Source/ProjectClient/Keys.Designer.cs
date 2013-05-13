namespace ProjectClient
{
    partial class Keys
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
            this.btnGen = new System.Windows.Forms.Button();
            this.lblPub = new System.Windows.Forms.Label();
            this.lblPriv = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(256, 414);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(75, 23);
            this.btnGen.TabIndex = 0;
            this.btnGen.Text = "Generate";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // lblPub
            // 
            this.lblPub.Location = new System.Drawing.Point(12, 9);
            this.lblPub.Name = "lblPub";
            this.lblPub.Size = new System.Drawing.Size(273, 402);
            this.lblPub.TabIndex = 1;
            // 
            // lblPriv
            // 
            this.lblPriv.Location = new System.Drawing.Point(291, 9);
            this.lblPriv.Name = "lblPriv";
            this.lblPriv.Size = new System.Drawing.Size(270, 402);
            this.lblPriv.TabIndex = 2;
            // 
            // Keys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 449);
            this.Controls.Add(this.lblPriv);
            this.Controls.Add(this.lblPub);
            this.Controls.Add(this.btnGen);
            this.Name = "Keys";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label lblPub;
        private System.Windows.Forms.Label lblPriv;
    }
}

