namespace ClientApp
{
    partial class Serial_port
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.com_chose_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(41, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(124, 180);
            this.listBox1.TabIndex = 0;
            // 
            // com_chose_btn
            // 
            this.com_chose_btn.Location = new System.Drawing.Point(217, 76);
            this.com_chose_btn.Name = "com_chose_btn";
            this.com_chose_btn.Size = new System.Drawing.Size(133, 64);
            this.com_chose_btn.TabIndex = 1;
            this.com_chose_btn.Text = "Ligar";
            this.com_chose_btn.UseVisualStyleBackColor = true;
            this.com_chose_btn.Click += new System.EventHandler(this.com_chose_btn_Click);
            // 
            // Serial_port
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 254);
            this.Controls.Add(this.com_chose_btn);
            this.Controls.Add(this.listBox1);
            this.Name = "Serial_port";
            this.Text = "Serial_port";
            this.Load += new System.EventHandler(this.Serial_port_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button com_chose_btn;
    }
}