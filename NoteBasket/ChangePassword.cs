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
    public partial class ChangePassword : Form
    {
        private int userId;

        public ChangePassword(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {
                        string currentPasswordInput = currentpassword_textbox.Text.Trim();
            string newPassword = password_textbox.Text.Trim();
            string confirmPassword = confirmpassword_textbox.Text.Trim();

                        if (string.IsNullOrEmpty(currentPasswordInput) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                                        string selectQuery = "SELECT PasswordHash FROM Users WHERE UserID = @UserId";
                    con.Open();

                    string storedPasswordHash = null;

                    using (SqlCommand cmdSelect = new SqlCommand(selectQuery, con))
                    {
                        cmdSelect.Parameters.AddWithValue("@UserId", userId);
                        storedPasswordHash = cmdSelect.ExecuteScalar()?.ToString();
                    }

                                        if (!BCrypt.Net.BCrypt.Verify(currentPasswordInput, storedPasswordHash))
                    {
                        MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                                        string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                                        string updateQuery = "UPDATE Users SET PasswordHash = @NewPasswordHash WHERE UserID = @UserId";

                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                    {
                        cmdUpdate.Parameters.AddWithValue("@NewPasswordHash", newPasswordHash);
                        cmdUpdate.Parameters.AddWithValue("@UserId", userId);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        NavigateToForm8();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

                private void NavigateToForm8()
        {
                        this.Hide();

                        EditProfile editProfileForm = new EditProfile(userId);
            editProfileForm.Show();

        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditProfile f8 = new EditProfile(userId);
            f8.Show();

        }
    }
}
