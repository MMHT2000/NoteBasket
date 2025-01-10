namespace NoteBasket
{
    partial class Form8
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.editprofile_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.returntodashboard_btn = new System.Windows.Forms.Button();
            this.changepassword_btn = new System.Windows.Forms.Button();
            this.female_Btn = new System.Windows.Forms.RadioButton();
            this.male_Btn = new System.Windows.Forms.RadioButton();
            this.gender_label = new System.Windows.Forms.Label();
            this.updateprofile_btn = new System.Windows.Forms.Button();
            this.dob_picker = new System.Windows.Forms.DateTimePicker();
            this.email_textbox = new System.Windows.Forms.TextBox();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.name_textbox = new System.Windows.Forms.TextBox();
            this.dob_label = new System.Windows.Forms.Label();
            this.email_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.name_label = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.editprofile_label);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 140);
            this.panel1.TabIndex = 6;
            // 
            // editprofile_label
            // 
            this.editprofile_label.AutoSize = true;
            this.editprofile_label.BackColor = System.Drawing.Color.LightSkyBlue;
            this.editprofile_label.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editprofile_label.Location = new System.Drawing.Point(517, 52);
            this.editprofile_label.Name = "editprofile_label";
            this.editprofile_label.Size = new System.Drawing.Size(221, 32);
            this.editprofile_label.TabIndex = 6;
            this.editprofile_label.Text = "Edit Your Profile";
            this.editprofile_label.Click += new System.EventHandler(this.editprofile_label_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel2.Controls.Add(this.returntodashboard_btn);
            this.panel2.Controls.Add(this.changepassword_btn);
            this.panel2.Controls.Add(this.female_Btn);
            this.panel2.Controls.Add(this.male_Btn);
            this.panel2.Controls.Add(this.gender_label);
            this.panel2.Controls.Add(this.updateprofile_btn);
            this.panel2.Controls.Add(this.dob_picker);
            this.panel2.Controls.Add(this.email_textbox);
            this.panel2.Controls.Add(this.username_textbox);
            this.panel2.Controls.Add(this.name_textbox);
            this.panel2.Controls.Add(this.dob_label);
            this.panel2.Controls.Add(this.email_label);
            this.panel2.Controls.Add(this.username_label);
            this.panel2.Controls.Add(this.name_label);
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(116, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 409);
            this.panel2.TabIndex = 7;
            // 
            // returntodashboard_btn
            // 
            this.returntodashboard_btn.BackColor = System.Drawing.SystemColors.Highlight;
            this.returntodashboard_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returntodashboard_btn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returntodashboard_btn.ForeColor = System.Drawing.Color.Transparent;
            this.returntodashboard_btn.Location = new System.Drawing.Point(518, 324);
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
            this.changepassword_btn.Location = new System.Drawing.Point(346, 324);
            this.changepassword_btn.Name = "changepassword_btn";
            this.changepassword_btn.Size = new System.Drawing.Size(157, 32);
            this.changepassword_btn.TabIndex = 16;
            this.changepassword_btn.Text = "Change Password";
            this.changepassword_btn.UseVisualStyleBackColor = false;
            this.changepassword_btn.Click += new System.EventHandler(this.changepassword_btn_Click);
            // 
            // female_Btn
            // 
            this.female_Btn.AutoSize = true;
            this.female_Btn.Location = new System.Drawing.Point(672, 136);
            this.female_Btn.Name = "female_Btn";
            this.female_Btn.Size = new System.Drawing.Size(59, 17);
            this.female_Btn.TabIndex = 15;
            this.female_Btn.TabStop = true;
            this.female_Btn.Text = "Female";
            this.female_Btn.UseVisualStyleBackColor = true;
            // 
            // male_Btn
            // 
            this.male_Btn.AutoSize = true;
            this.male_Btn.Location = new System.Drawing.Point(606, 137);
            this.male_Btn.Name = "male_Btn";
            this.male_Btn.Size = new System.Drawing.Size(48, 17);
            this.male_Btn.TabIndex = 14;
            this.male_Btn.TabStop = true;
            this.male_Btn.Text = "Male";
            this.male_Btn.UseVisualStyleBackColor = true;
            // 
            // gender_label
            // 
            this.gender_label.AutoSize = true;
            this.gender_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gender_label.Location = new System.Drawing.Point(512, 135);
            this.gender_label.Name = "gender_label";
            this.gender_label.Size = new System.Drawing.Size(76, 23);
            this.gender_label.TabIndex = 13;
            this.gender_label.Text = "Gender:";
            // 
            // updateprofile_btn
            // 
            this.updateprofile_btn.BackColor = System.Drawing.SystemColors.Highlight;
            this.updateprofile_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateprofile_btn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateprofile_btn.ForeColor = System.Drawing.Color.Transparent;
            this.updateprofile_btn.Location = new System.Drawing.Point(434, 194);
            this.updateprofile_btn.Name = "updateprofile_btn";
            this.updateprofile_btn.Size = new System.Drawing.Size(157, 32);
            this.updateprofile_btn.TabIndex = 12;
            this.updateprofile_btn.Text = "Update Profile";
            this.updateprofile_btn.UseVisualStyleBackColor = false;
            this.updateprofile_btn.Click += new System.EventHandler(this.updateprofile_btn_Click);
            // 
            // dob_picker
            // 
            this.dob_picker.Location = new System.Drawing.Point(298, 135);
            this.dob_picker.Name = "dob_picker";
            this.dob_picker.Size = new System.Drawing.Size(200, 20);
            this.dob_picker.TabIndex = 9;
            // 
            // email_textbox
            // 
            this.email_textbox.Location = new System.Drawing.Point(298, 92);
            this.email_textbox.Name = "email_textbox";
            this.email_textbox.Size = new System.Drawing.Size(562, 20);
            this.email_textbox.TabIndex = 8;
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(644, 47);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(216, 20);
            this.username_textbox.TabIndex = 7;
            // 
            // name_textbox
            // 
            this.name_textbox.Location = new System.Drawing.Point(298, 47);
            this.name_textbox.Name = "name_textbox";
            this.name_textbox.Size = new System.Drawing.Size(216, 20);
            this.name_textbox.TabIndex = 6;
            // 
            // dob_label
            // 
            this.dob_label.AutoSize = true;
            this.dob_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dob_label.Location = new System.Drawing.Point(152, 135);
            this.dob_label.Name = "dob_label";
            this.dob_label.Size = new System.Drawing.Size(126, 23);
            this.dob_label.TabIndex = 3;
            this.dob_label.Text = "Date of Birth:";
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email_label.Location = new System.Drawing.Point(152, 88);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(79, 23);
            this.email_label.TabIndex = 2;
            this.email_label.Text = "E-maill:";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.Location = new System.Drawing.Point(539, 47);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(99, 23);
            this.username_label.TabIndex = 1;
            this.username_label.Text = "Username:";
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_label.Location = new System.Drawing.Point(152, 44);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(65, 23);
            this.name_label.TabIndex = 0;
            this.name_label.Text = "Name:";
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Your Profile";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label editprofile_label;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button returntodashboard_btn;
        private System.Windows.Forms.Button changepassword_btn;
        private System.Windows.Forms.RadioButton female_Btn;
        private System.Windows.Forms.RadioButton male_Btn;
        private System.Windows.Forms.Label gender_label;
        private System.Windows.Forms.Button updateprofile_btn;
        private System.Windows.Forms.DateTimePicker dob_picker;
        private System.Windows.Forms.TextBox email_textbox;
        private System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.TextBox name_textbox;
        private System.Windows.Forms.Label dob_label;
        private System.Windows.Forms.Label email_label;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label name_label;
    }
}