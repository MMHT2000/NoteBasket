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
    public partial class UserRegistration_Form : Form
    {
        public UserRegistration_Form()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void signin_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f1 = new Login_Form();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = name_textbox.Text.Trim();
                string username = username_textbox.Text.Trim();
                string email = email_textbox.Text.Trim();
                string dob = dob_picker.Value.ToString("yyyy-MM-dd");
                string password = password_textbox.Text.Trim();
                string confirmPassword = confirmpassword_textbox.Text.Trim();

                string gender = male_Btn.Checked ? "Male" : female_Btn.Checked ? "Female" : null;

                if (string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(confirmPassword) ||
                    string.IsNullOrEmpty(gender))
                {
                    MessageBox.Show("Please fill in all the fields.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (password != confirmPassword)
                {
                    MessageBox.Show("Password and Confirm Password do not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string role = "Free";

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "INSERT INTO Users (Username, PasswordHash, Name, Email, DOB, Gender, Role) " +
                                 "VALUES (@Username, @PasswordHash, @Name, @Email, @DOB, @Gender, @Role)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Role", role);  
                        con.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Details inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        Login_Form f1 = new Login_Form();
                        f1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void male_Btn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void female_Btn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void confirmpassword_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void beanotemaster_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            NoteMasterRegistration_Form form6 = new NoteMasterRegistration_Form();
            form6.Show();
        }
    }
}
