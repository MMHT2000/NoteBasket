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
            this.panel1 = new System.Windows.Forms.Panel();
            this.logout_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.name_label = new System.Windows.Forms.Label();
            this.profilepicture_box = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.editprofile_btn = new System.Windows.Forms.Button();
            this.accountcreationdynamic_label = new System.Windows.Forms.Label();
            this.loyaltydynamic_label = new System.Windows.Forms.Label();
            this.subscriptionsdynamic_label = new System.Windows.Forms.Label();
            this.roledynamic_label = new System.Windows.Forms.Label();
            this.genderdynamiclabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dobdynamic_label = new System.Windows.Forms.Label();
            this.emaildynamic_label = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture_box)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // userdashboard_label
            // 
            this.userdashboard_label.AutoSize = true;
            this.userdashboard_label.BackColor = System.Drawing.Color.LightSkyBlue;
            this.userdashboard_label.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userdashboard_label.Location = new System.Drawing.Point(495, 38);
            this.userdashboard_label.Name = "userdashboard_label";
            this.userdashboard_label.Size = new System.Drawing.Size(211, 32);
            this.userdashboard_label.TabIndex = 2;
            this.userdashboard_label.Text = "User Dashboard";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.logout_btn);
            this.panel1.Controls.Add(this.userdashboard_label);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 108);
            this.panel1.TabIndex = 3;
            // 
            // logout_btn
            // 
            this.logout_btn.BackColor = System.Drawing.SystemColors.Highlight;
            this.logout_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_btn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout_btn.ForeColor = System.Drawing.Color.Transparent;
            this.logout_btn.Location = new System.Drawing.Point(1047, 38);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(157, 32);
            this.logout_btn.TabIndex = 17;
            this.logout_btn.Text = "Log Out";
            this.logout_btn.UseVisualStyleBackColor = false;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel2.Controls.Add(this.name_label);
            this.panel2.Controls.Add(this.profilepicture_box);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(930, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 558);
            this.panel2.TabIndex = 4;
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.BackColor = System.Drawing.Color.LightBlue;
            this.name_label.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_label.Location = new System.Drawing.Point(72, 160);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(45, 19);
            this.name_label.TabIndex = 1;
            this.name_label.Text = "name";
            this.name_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // profilepicture_box
            // 
            this.profilepicture_box.BackColor = System.Drawing.Color.SteelBlue;
            this.profilepicture_box.Location = new System.Drawing.Point(100, 26);
            this.profilepicture_box.Name = "profilepicture_box";
            this.profilepicture_box.Size = new System.Drawing.Size(128, 128);
            this.profilepicture_box.TabIndex = 0;
            this.profilepicture_box.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.Controls.Add(this.editprofile_btn);
            this.panel3.Controls.Add(this.accountcreationdynamic_label);
            this.panel3.Controls.Add(this.loyaltydynamic_label);
            this.panel3.Controls.Add(this.subscriptionsdynamic_label);
            this.panel3.Controls.Add(this.roledynamic_label);
            this.panel3.Controls.Add(this.genderdynamiclabel);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dobdynamic_label);
            this.panel3.Controls.Add(this.emaildynamic_label);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.username_label);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(13, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(317, 393);
            this.panel3.TabIndex = 3;
            // 
            // editprofile_btn
            // 
            this.editprofile_btn.BackColor = System.Drawing.Color.Black;
            this.editprofile_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editprofile_btn.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editprofile_btn.ForeColor = System.Drawing.Color.Transparent;
            this.editprofile_btn.Location = new System.Drawing.Point(233, 11);
            this.editprofile_btn.Name = "editprofile_btn";
            this.editprofile_btn.Size = new System.Drawing.Size(76, 32);
            this.editprofile_btn.TabIndex = 18;
            this.editprofile_btn.Text = "Edit Profile";
            this.editprofile_btn.UseVisualStyleBackColor = false;
            this.editprofile_btn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // accountcreationdynamic_label
            // 
            this.accountcreationdynamic_label.AutoSize = true;
            this.accountcreationdynamic_label.BackColor = System.Drawing.Color.LightBlue;
            this.accountcreationdynamic_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountcreationdynamic_label.Location = new System.Drawing.Point(151, 359);
            this.accountcreationdynamic_label.Name = "accountcreationdynamic_label";
            this.accountcreationdynamic_label.Size = new System.Drawing.Size(58, 17);
            this.accountcreationdynamic_label.TabIndex = 18;
            this.accountcreationdynamic_label.Text = "Account";
            // 
            // loyaltydynamic_label
            // 
            this.loyaltydynamic_label.AutoSize = true;
            this.loyaltydynamic_label.BackColor = System.Drawing.Color.LightBlue;
            this.loyaltydynamic_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loyaltydynamic_label.Location = new System.Drawing.Point(120, 333);
            this.loyaltydynamic_label.Name = "loyaltydynamic_label";
            this.loyaltydynamic_label.Size = new System.Drawing.Size(92, 17);
            this.loyaltydynamic_label.TabIndex = 17;
            this.loyaltydynamic_label.Text = "Loyalty Points";
            // 
            // subscriptionsdynamic_label
            // 
            this.subscriptionsdynamic_label.AutoSize = true;
            this.subscriptionsdynamic_label.BackColor = System.Drawing.Color.LightBlue;
            this.subscriptionsdynamic_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subscriptionsdynamic_label.Location = new System.Drawing.Point(156, 306);
            this.subscriptionsdynamic_label.Name = "subscriptionsdynamic_label";
            this.subscriptionsdynamic_label.Size = new System.Drawing.Size(79, 17);
            this.subscriptionsdynamic_label.TabIndex = 16;
            this.subscriptionsdynamic_label.Text = "Subscription";
            // 
            // roledynamic_label
            // 
            this.roledynamic_label.AutoSize = true;
            this.roledynamic_label.BackColor = System.Drawing.Color.LightBlue;
            this.roledynamic_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roledynamic_label.Location = new System.Drawing.Point(72, 278);
            this.roledynamic_label.Name = "roledynamic_label";
            this.roledynamic_label.Size = new System.Drawing.Size(30, 17);
            this.roledynamic_label.TabIndex = 15;
            this.roledynamic_label.Text = "role";
            // 
            // genderdynamiclabel
            // 
            this.genderdynamiclabel.AutoSize = true;
            this.genderdynamiclabel.BackColor = System.Drawing.Color.LightBlue;
            this.genderdynamiclabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderdynamiclabel.Location = new System.Drawing.Point(81, 250);
            this.genderdynamiclabel.Name = "genderdynamiclabel";
            this.genderdynamiclabel.Size = new System.Drawing.Size(48, 17);
            this.genderdynamiclabel.TabIndex = 14;
            this.genderdynamiclabel.Text = "gender";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightBlue;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(75, 250);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 17);
            this.label10.TabIndex = 13;
            // 
            // dobdynamic_label
            // 
            this.dobdynamic_label.AutoSize = true;
            this.dobdynamic_label.BackColor = System.Drawing.Color.LightBlue;
            this.dobdynamic_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dobdynamic_label.Location = new System.Drawing.Point(112, 224);
            this.dobdynamic_label.Name = "dobdynamic_label";
            this.dobdynamic_label.Size = new System.Drawing.Size(29, 17);
            this.dobdynamic_label.TabIndex = 12;
            this.dobdynamic_label.Text = "dob";
            // 
            // emaildynamic_label
            // 
            this.emaildynamic_label.AutoSize = true;
            this.emaildynamic_label.BackColor = System.Drawing.Color.LightBlue;
            this.emaildynamic_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emaildynamic_label.Location = new System.Drawing.Point(69, 202);
            this.emaildynamic_label.Name = "emaildynamic_label";
            this.emaildynamic_label.Size = new System.Drawing.Size(44, 17);
            this.emaildynamic_label.TabIndex = 11;
            this.emaildynamic_label.Text = "Email:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.LightBlue;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Account Created At:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.LightBlue;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 333);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Loyalty Points:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightBlue;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Subscription Ends At:";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.BackColor = System.Drawing.Color.LightBlue;
            this.username_label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.Location = new System.Drawing.Point(109, 168);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(79, 17);
            this.username_label.TabIndex = 2;
            this.username_label.Text = "@username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LightBlue;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Gender:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightBlue;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Date of Birth:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightBlue;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Role:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightBlue;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Email:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilepicture_box)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label userdashboard_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.PictureBox profilepicture_box;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label accountcreationdynamic_label;
        private System.Windows.Forms.Label loyaltydynamic_label;
        private System.Windows.Forms.Label subscriptionsdynamic_label;
        private System.Windows.Forms.Label roledynamic_label;
        private System.Windows.Forms.Label genderdynamiclabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label dobdynamic_label;
        private System.Windows.Forms.Label emaildynamic_label;
        private System.Windows.Forms.Button editprofile_btn;
    }
}