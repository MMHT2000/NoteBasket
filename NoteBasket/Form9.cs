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

namespace NoteBasket
{
    public partial class Form9 : Form
    {
        private int userId;

        public Form9(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {
            // Retrieve input values
            string currentPasswordInput = currentpassword_textbox.Text.Trim();
            string newPassword = password_textbox.Text.Trim();
            string confirmPassword = confirmpassword_textbox.Text.Trim();

            // Validate inputs
            if (string.IsNullOrEmpty(currentPasswordInput) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            try
            {
                // Connect to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Query to retrieve the current password hash
                    string selectQuery = "SELECT PasswordHash FROM Users WHERE UserID = @UserId";
                    con.Open();

                    string storedPasswordHash = null;

                    using (SqlCommand cmdSelect = new SqlCommand(selectQuery, con))
                    {
                        cmdSelect.Parameters.AddWithValue("@UserId", userId);
                        storedPasswordHash = cmdSelect.ExecuteScalar()?.ToString();
                    }

                    // Verify the entered current password using BCrypt
                    if (!BCrypt.Net.BCrypt.Verify(currentPasswordInput, storedPasswordHash))
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    // Hash the new password using BCrypt
                    string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    // Update the password in the database
                    string updateQuery = "UPDATE Users SET PasswordHash = @NewPasswordHash WHERE UserID = @UserId";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                    {
                        cmdUpdate.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
                        cmdUpdate.Parameters.AddWithValue("@UserId", userId);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password changed successfully.");

                    // Navigate to Form8
                    NavigateToForm8();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // Method to navigate to Form8
        private void NavigateToForm8()
        {
            // Close Form9
            this.Hide();

            // Open Form8 and pass the userId
            Form8 editProfileForm = new Form8(userId);
            editProfileForm.Show();

        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8(userId);
            f8.Show();

        }
    }
}
