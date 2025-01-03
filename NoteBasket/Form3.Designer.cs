namespace NoteBasket
{
    partial class Form3
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
            this.userdashboard_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userdashboard_label
            // 
            this.userdashboard_label.AutoSize = true;
            this.userdashboard_label.BackColor = System.Drawing.Color.LightSkyBlue;
            this.userdashboard_label.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userdashboard_label.Location = new System.Drawing.Point(493, 44);
            this.userdashboard_label.Name = "userdashboard_label";
            this.userdashboard_label.Size = new System.Drawing.Size(211, 32);
            this.userdashboard_label.TabIndex = 2;
            this.userdashboard_label.Text = "User Dashboard";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.userdashboard_label);
            this.Name = "Form3";
            this.Text = "User Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userdashboard_label;
    }
}