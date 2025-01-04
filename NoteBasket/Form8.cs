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
    public partial class Form8 : Form
    {
        private int loggedInUserId;
        private string name;
        private string username;
        private string email;
        private string dob;
        private string gender;

        public Form8(int userId, string name, string username, string email, string dob, string gender)
        {
            InitializeComponent();
            loggedInUserId = userId;
            


            // Load the current values into the textboxes
            name_textbox.Text = name;
            username_textbox.Text = username;
            email_textbox.Text = email;
            this.dob = dob;

            // Parse and set the DOB
            if (!string.IsNullOrEmpty(dob))
            {
                try
                {
                    dob_picker.Value = DateTime.ParseExact(dob, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    MessageBox.Show("The date format for DOB is invalid.");
                    dob_picker.Value = DateTime.Now; // Default to the current date
                }
            }
            else
            {
                dob_picker.Value = DateTime.Now; // Default to the current date
            }

            // Set gender radio buttons
            if (gender == "Male")
            {
                male_Btn.Checked = true;
            }
            else if (gender == "Female")
            {
                female_Btn.Checked = true;
            }
        }

        private void editprofile_label_Click(object sender, EventArgs e)
        {

        }

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            // Retrieve updated values from the input fields
            string name = name_textbox.Text.Trim();
            string username = username_textbox.Text.Trim();
            string email = email_textbox.Text.Trim();
            string dob = dob_picker.Value.ToString("yyyy-MM-dd");
            string gender = male_Btn.Checked ? "Male" : "Female";

            // Check if all fields are filled
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            try
            {
                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=DESKTOP-RS5QGMS\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // SQL query to update user profile
                    string sql = "UPDATE Users SET Name = @Name, Username = @Username, Email = @Email, DOB = @DOB, Gender = @Gender WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters for SQL query
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@UserID", loggedInUserId);  // Use logged-in user ID for updating

                        // Open the database connection
                        con.Open();

                        // Execute the update query
                        cmd.ExecuteNonQuery();

                        // Inform the user that the profile has been updated
                        MessageBox.Show("Profile updated successfully.");

                        // Optionally, close Form8
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
