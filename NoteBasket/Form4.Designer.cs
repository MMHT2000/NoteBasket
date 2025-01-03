namespace NoteBasket
{
    partial class Form4
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
            this.notemaster_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notemaster_label
            // 
            this.notemaster_label.AutoSize = true;
            this.notemaster_label.BackColor = System.Drawing.Color.LightSkyBlue;
            this.notemaster_label.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notemaster_label.Location = new System.Drawing.Point(496, 49);
            this.notemaster_label.Name = "notemaster_label";
            this.notemaster_label.Size = new System.Drawing.Size(300, 32);
            this.notemaster_label.TabIndex = 3;
            this.notemaster_label.Text = "NoteMaster Dashboard";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.notemaster_label);
            this.Name = "Form4";
            this.Text = "NoteMaster Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label notemaster_label;
    }
}