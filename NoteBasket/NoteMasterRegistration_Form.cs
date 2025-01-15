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
    public partial class NoteMasterRegistration_Form : Form
    {
        public NoteMasterRegistration_Form()
        {
            InitializeComponent();
        }

        private void createaccount_btn_Click(object sender, EventArgs e)
        {
            try
            {
                                string name = name_textbox.Text.Trim();
                string username = username_textbox.Text.Trim();
                string password = password_textbox.Text.Trim();
                string confirmPassword = confirmpassword_textbox.Text.Trim();

                                if (string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                                if (password != confirmPassword)
                {
                    MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string checkUsernameQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(checkUsernameQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        con.Open();
                        int userCount = (int)cmd.ExecuteScalar();
                        con.Close();

                                                if (userCount > 0)
                        {
                            MessageBox.Show("A user/NoteMaster with this username already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                                string role = "Notemaster";

                                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                                        string sql = "INSERT INTO Users (Username, PasswordHash, Name, Role) " +
                                 "VALUES (@Username, @PasswordHash, @Name, @Role)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                                                cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Role", role);  
                                                con.Open();

                                                cmd.ExecuteNonQuery();

                                                MessageBox.Show("Notemaster registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void beanotemaster_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserRegistration_Form form2 = new UserRegistration_Form();
            form2.Show();
        }

        private void signin_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form form1 = new Login_Form ();
            form1.Show();
        }
    }
}
