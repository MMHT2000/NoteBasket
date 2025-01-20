namespace NoteBasket
{
    partial class EditNoteDetails
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.returntodashboard_btn = new System.Windows.Forms.Button();
            this.changepassword_btn = new System.Windows.Forms.Button();
            this.description_textbox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editprofile_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.name_textbox = new System.Windows.Forms.TextBox();
            this.dob_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.name_label = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.email_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Snow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(677, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Free",
            "Silver",
            "Gold"});
            this.comboBox2.Location = new System.Drawing.Point(491, 188);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(264, 21);
            this.comboBox2.TabIndex = 21;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "CSE",
            "EEE",
            "LAW",
            "IPE",
            "BBA",
            "Economics",
            "MMC",
            "ENGLISH",
            "PHARMACY",
            "ARCHITECTURE",
            "CNCS",
            "DS"});
            this.comboBox1.Location = new System.Drawing.Point(493, 145);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(262, 21);
            this.comboBox1.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(277, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Category:";
            // 
            // returntodashboard_btn
            // 
            this.returntodashboard_btn.BackColor = System.Drawing.SystemColors.Highlight;
            this.returntodashboard_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returntodashboard_btn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returntodashboard_btn.ForeColor = System.Drawing.Color.Transparent;
            this.returntodashboard_btn.Location = new System.Drawing.Point(440, 328);
            this.returntodashboard_btn.Name = "returntodashboard_btn";
            this.returntodashboard_btn.Size = new System.Drawing.Size(157, 32);
            this.returntodashboard_btn.TabIndex = 17;
            this.returntodashboard_btn.Text = "Return to Dashboard";
            this.returntodashboard_btn.UseVisualStyleBackColor = false;
            this.returntodashboard_btn.Click += new System.EventHandler(this.returntodashboard_btn_Click);
            // 
            // changepassword_btn
            // 
            this.changepassword_btn.BackColor = System.Drawing.SystemColors.Highlight;
            this.changepassword_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changepassword_btn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changepassword_btn.ForeColor = System.Drawing.Color.Transparent;
            this.changepassword_btn.Location = new System.Drawing.Point(358, 290);
            this.changepassword_btn.Name = "changepassword_btn";
            this.changepassword_btn.Size = new System.Drawing.Size(157, 32);
            this.changepassword_btn.TabIndex = 16;
            this.changepassword_btn.Text = "Update";
            this.changepassword_btn.UseVisualStyleBackColor = false;
            this.changepassword_btn.Click += new System.EventHandler(this.changepassword_btn_Click);
            // 
            // description_textbox
            // 
            this.description_textbox.Location = new System.Drawing.Point(493, 54);
            this.description_textbox.Multiline = true;
            this.description_textbox.Name = "description_textbox";
            this.description_textbox.Size = new System.Drawing.Size(262, 68);
            this.description_textbox.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.editprofile_label);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 140);
            this.panel1.TabIndex = 10;
            // 
            // editprofile_label
            // 
            this.editprofile_label.AutoSize = true;
            this.editprofile_label.BackColor = System.Drawing.Color.LightSkyBlue;
            this.editprofile_label.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editprofile_label.Location = new System.Drawing.Point(517, 48);
            this.editprofile_label.Name = "editprofile_label";
            this.editprofile_label.Size = new System.Drawing.Size(224, 32);
            this.editprofile_label.TabIndex = 6;
            this.editprofile_label.Text = "Edit Note Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, -30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 24;
            this.label2.Text = "filepath";
            this.label2.Visible = false;
            // 
            // name_textbox
            // 
            this.name_textbox.Location = new System.Drawing.Point(493, 9);
            this.name_textbox.Name = "name_textbox";
            this.name_textbox.Size = new System.Drawing.Size(262, 20);
            this.name_textbox.TabIndex = 6;
            // 
            // dob_label
            // 
            this.dob_label.AutoSize = true;
            this.dob_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dob_label.Location = new System.Drawing.Point(275, 187);
            this.dob_label.Name = "dob_label";
            this.dob_label.Size = new System.Drawing.Size(172, 23);
            this.dob_label.TabIndex = 3;
            this.dob_label.Text = "Subscription Level:";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.Location = new System.Drawing.Point(275, 229);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(84, 23);
            this.username_label.TabIndex = 1;
            this.username_label.Text = "Filepath:";
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_label.Location = new System.Drawing.Point(277, 6);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(54, 23);
            this.name_label.TabIndex = 0;
            this.name_label.Text = "Title:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(492, 229);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 23);
            this.panel3.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 25;
            this.label3.Text = "filepath";
            this.label3.Visible = false;
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email_label.Location = new System.Drawing.Point(277, 50);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(113, 23);
            this.email_label.TabIndex = 2;
            this.email_label.Text = "Description:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.returntodashboard_btn);
            this.panel2.Controls.Add(this.changepassword_btn);
            this.panel2.Controls.Add(this.description_textbox);
            this.panel2.Controls.Add(this.name_textbox);
            this.panel2.Controls.Add(this.dob_label);
            this.panel2.Controls.Add(this.email_label);
            this.panel2.Controls.Add(this.username_label);
            this.panel2.Controls.Add(this.name_label);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(116, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 409);
            this.panel2.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Highlight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(521, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 32);
            this.button2.TabIndex = 24;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EditNoteDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "EditNoteDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditNoteDetails";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button returntodashboard_btn;
        private System.Windows.Forms.Button changepassword_btn;
        private System.Windows.Forms.TextBox description_textbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label editprofile_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name_textbox;
        private System.Windows.Forms.Label dob_label;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label email_label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
    }
}