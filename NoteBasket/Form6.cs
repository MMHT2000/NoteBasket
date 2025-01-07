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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void createaccount_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data from input fields
                string name = name_textbox.Text.Trim();
                string username = username_textbox.Text.Trim();
                string password = password_textbox.Text.Trim();
                string confirmPassword = confirmpassword_textbox.Text.Trim();

                // Check if all fields are filled
                if (string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }

                // Validate if password and confirm password match
                if (password != confirmPassword)
                {
                    MessageBox.Show("Password and Confirm Password do not match.");
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
                            MessageBox.Show("A user/NoteMaster with this username already exists.");
                            return;
                        }
                    }
                }

                // Hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                // Define role as "Notemaster"
                string role = "Notemaster";

                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Use a parameterized query to prevent SQL injection
                    string sql = "INSERT INTO Users (Username, PasswordHash, Name, Role) " +
                                 "VALUES (@Username, @PasswordHash, @Name, @Role)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters with their values
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Role", role);  // Insert "Notemaster" as the role

                        // Open the connection
                        con.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();

                        // Inform the user of success
                        MessageBox.Show("Notemaster registered successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void beanotemaster_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void signin_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
