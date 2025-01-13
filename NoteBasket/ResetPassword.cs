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
    public partial class ResetPassword : Form
    {
        public ResetPassword()
        {
            InitializeComponent();
        }

        private void backtosignin_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form form1 = new Login_Form ();
            form1.Show();
        }

        private void resetpassword_btn_Click(object sender, EventArgs e)
        {
            // Retrieve data from input fields
            string username = user_textbox.Text.Trim();
            string email = email_textbox.Text.Trim();
            string password = password_textbox.Text.Trim();
            string confirmPassword = confirmpassword_textbox.Text.Trim();

            // Check if all fields are filled
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate if password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Query to check if the username and email match
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Email = @Email";

                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, con))
                    {
                        // Add parameters with their values
                        cmdCheck.Parameters.AddWithValue("@Username", username);
                        cmdCheck.Parameters.AddWithValue("@Email", email);

                        // Open the connection
                        con.Open();

                        // Execute the query
                        int matchCount = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (matchCount == 0)
                        {
                            // Username and email do not match
                            MessageBox.Show("Email and Username do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // If username and email match, update the password
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                    string updateQuery = "UPDATE Users SET PasswordHash = @PasswordHash WHERE Username = @Username AND Email = @Email";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters with their values
                        cmdUpdate.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmdUpdate.Parameters.AddWithValue("@Username", username);
                        cmdUpdate.Parameters.AddWithValue("@Email", email);

                        // Execute the update query
                        cmdUpdate.ExecuteNonQuery();

                        // Inform the user of success
                        MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        Login_Form form1 = new Login_Form();
                        form1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
