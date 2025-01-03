namespace NoteBasket
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.password_label = new System.Windows.Forms.Label();
            this.user_textbox = new System.Windows.Forms.TextBox();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.signin_btn = new System.Windows.Forms.Button();
            this.forgotpass_label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(476, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome To NoteBasket";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.Location = new System.Drawing.Point(305, 70);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(107, 24);
            this.username_label.TabIndex = 1;
            this.username_label.Text = "Username:";
            this.username_label.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 140);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.forgotpass_label);
            this.panel2.Controls.Add(this.signin_btn);
            this.panel2.Controls.Add(this.password_textbox);
            this.panel2.Controls.Add(this.user_textbox);
            this.panel2.Controls.Add(this.password_label);
            this.panel2.Controls.Add(this.username_label);
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(115, 208);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1016, 409);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_label.Location = new System.Drawing.Point(305, 126);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(103, 24);
            this.password_label.TabIndex = 2;
            this.password_label.Text = "Password:";
            // 
            // user_textbox
            // 
            this.user_textbox.BackColor = System.Drawing.Color.Azure;
            this.user_textbox.Location = new System.Drawing.Point(461, 72);
            this.user_textbox.Name = "user_textbox";
            this.user_textbox.Size = new System.Drawing.Size(207, 20);
            this.user_textbox.TabIndex = 3;
            // 
            // password_textbox
            // 
            this.password_textbox.BackColor = System.Drawing.Color.Azure;
            this.password_textbox.Location = new System.Drawing.Point(461, 130);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(207, 20);
            this.password_textbox.TabIndex = 4;
            // 
            // signin_btn
            // 
            this.signin_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.signin_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signin_btn.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signin_btn.Location = new System.Drawing.Point(461, 173);
            this.signin_btn.Name = "signin_btn";
            this.signin_btn.Size = new System.Drawing.Size(91, 34);
            this.signin_btn.TabIndex = 5;
            this.signin_btn.Text = "Sign In";
            this.signin_btn.UseVisualStyleBackColor = false;
            this.signin_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // forgotpass_label
            // 
            this.forgotpass_label.AutoSize = true;
            this.forgotpass_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgotpass_label.Location = new System.Drawing.Point(429, 223);
            this.forgotpass_label.Name = "forgotpass_label";
            this.forgotpass_label.Size = new System.Drawing.Size(153, 16);
            this.forgotpass_label.TabIndex = 6;
            this.forgotpass_label.Text = "Forgotten Password?";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(432, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Create New Account";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "NoteBasket";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox password_textbox;
        private System.Windows.Forms.TextBox user_textbox;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Button signin_btn;
        private System.Windows.Forms.Label forgotpass_label;
        private System.Windows.Forms.Button button1;
    }
}

