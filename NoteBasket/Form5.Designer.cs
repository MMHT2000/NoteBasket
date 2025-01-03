namespace NoteBasket
{
    partial class Form5
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
            this.admindashboard_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // admindashboard_label
            // 
            this.admindashboard_label.AutoSize = true;
            this.admindashboard_label.BackColor = System.Drawing.Color.LightSkyBlue;
            this.admindashboard_label.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.admindashboard_label.Location = new System.Drawing.Point(506, 42);
            this.admindashboard_label.Name = "admindashboard_label";
            this.admindashboard_label.Size = new System.Drawing.Size(239, 32);
            this.admindashboard_label.TabIndex = 3;
            this.admindashboard_label.Text = "Admin Dashboard";
            this.admindashboard_label.Click += new System.EventHandler(this.createanaccount_label_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.admindashboard_label);
            this.Name = "Form5";
            this.Text = "Admin Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label admindashboard_label;
    }
}