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
    public partial class Form2 : Form
    {
        public Form2()
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
            Form f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data from input fields
                string name = name_textbox.Text.Trim();
                string username = username_textbox.Text.Trim();
                string email = email_textbox.Text.Trim();
                string dob = dob_picker.Value.ToString("yyyy-MM-dd");
                string password = password_textbox.Text.Trim();
                string confirmPassword = confirmpassword_textbox.Text.Trim();

                // Determine gender based on radio button selection
                string gender = male_Btn.Checked ? "Male" : female_Btn.Checked ? "Female" : null;

                // Check if all fields are filled
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

                // Validate if password and confirm password match
                if (password != confirmPassword)
                {
                    MessageBox.Show("Password and Confirm Password do not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Check if username already exists in the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string checkUsernameQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(checkUsernameQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        con.Open();
                        int userCount = (int)cmd.ExecuteScalar();
                        con.Close();

                        // If the user count is greater than 0, the username already exists
                        if (userCount > 0)
                        {
                            MessageBox.Show("A user/NoteMaster with this username already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // Hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // Define default role as "Free"
                string role = "Free";

                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Use a parameterized query to prevent SQL injection
                    string sql = "INSERT INTO Users (Username, PasswordHash, Name, Email, DOB, Gender, Role) " +
                                 "VALUES (@Username, @PasswordHash, @Name, @Email, @DOB, @Gender, @Role)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters with their values
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Role", role);  // Insert "Free" as the role

                        // Open the connection
                        con.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();

                        // Inform the user of success
                        MessageBox.Show("Details inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
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
            Form6 form6 = new Form6();
            form6.Show();
        }
    }
}
