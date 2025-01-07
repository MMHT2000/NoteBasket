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
        private int userId;

        public Form8(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            // Load user data when the form is initialized
            LoadUserData();

        }

        private void LoadUserData()
        {
            try
            {
                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // SQL query to retrieve the user's details
                    string sql = "SELECT Name, Username, Email, DOB, Gender FROM Users WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameter for SQL query
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        // Open the database connection
                        con.Open();

                        // Execute the query and read the data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the form fields with the retrieved data
                                name_textbox.Text = reader["Name"].ToString();
                                username_textbox.Text = reader["Username"].ToString();
                                email_textbox.Text = reader["Email"].ToString();

                                // Parse and set the DOB
                                if (reader["DOB"] != DBNull.Value)
                                {
                                    dob_picker.Value = Convert.ToDateTime(reader["DOB"]);
                                }
                                else
                                {
                                    dob_picker.Value = DateTime.Now; // Default to the current date if DOB is null
                                }

                                // Set gender radio buttons
                                string gender = reader["Gender"].ToString();
                                if (gender == "Male")
                                {
                                    male_Btn.Checked = true;
                                }
                                else if (gender == "Female")
                                {
                                    female_Btn.Checked = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("User data not found. Please try again.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("An error occurred while loading user data: " + ex.Message);
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
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
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
                        cmd.Parameters.AddWithValue("@UserID", userId);  // Use logged-in user ID for updating

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
            Form3 f3 = new Form3(userId);
            f3.Show();
        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
