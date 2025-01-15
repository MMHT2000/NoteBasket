using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace NoteBasket
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
            panel2.Paint += panel2_Paint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = user_textbox.Text.Trim();
            string password = password_textbox.Text.Trim();

                        if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username/email and password.");
                return;
            }

            try
            {
                                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                                        string sql = "SELECT UserID, Username, PasswordHash, Role " +
                                 "FROM Users WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                                                cmd.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);

                                                con.Open();

                                                using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                                                        if (reader.Read())
                            {
                                                                int userId = Convert.ToInt32(reader["UserID"]);
                                string role = reader["Role"].ToString();

                                                                string storedPasswordHash = reader["PasswordHash"].ToString();
                                if (BCrypt.Net.BCrypt.Verify(password, storedPasswordHash))
                                {
                                                                        switch (role)
                                    {
                                        case "Free":
                                        case "Silver":
                                        case "Gold":
                                                                                        User_Dashboard form3 = new User_Dashboard(userId);
                                            form3.Show();
                                            break;

                                        case "Notemaster":
                                                                                        NoteMaster_Dashboard form4 = new NoteMaster_Dashboard(userId);
                                            form4.Show();
                                            break;

                                        case "Admin":
                                                                                        Admin_Dashboard form5 = new Admin_Dashboard(userId);
                                            form5.Show();
                                            break;

                                        default:
                                                                                        MessageBox.Show("Unknown role. Please contact support.");
                                            break;
                                    }

                                                                        this.Hide();
                                }
                                else
                                {
                                                                        MessageBox.Show("Incorrect password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                                }
                            }
                            else
                            {
                                                                MessageBox.Show("User not found. Please check your username/email and try again.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            UserRegistration_Form f2 = new UserRegistration_Form();
            f2.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ResetPassword f7 = new ResetPassword();
            f7.Show();
        }
private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    Application.Exit();
}

    }
}
