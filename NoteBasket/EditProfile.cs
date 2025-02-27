﻿using System;
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
    public partial class EditProfile : Form
    {
        private int userId;

        public EditProfile(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadUserData();

        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Name, Username, Email, DOB, Gender, Role FROM Users WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name_textbox.Text = reader["Name"].ToString();
                                username_textbox.Text = reader["Username"].ToString();
                                email_textbox.Text = reader["Email"].ToString();

                                if (reader["DOB"] != DBNull.Value)
                                {
                                    dob_picker.Value = Convert.ToDateTime(reader["DOB"]);
                                }
                                else
                                {
                                    dob_picker.Value = DateTime.Now;                                 
                                }

                                string gender = reader["Gender"].ToString();
                                if (gender == "Male")
                                {
                                    male_Btn.Checked = true;
                                }
                                else if (gender == "Female")
                                {
                                    female_Btn.Checked = true;
                                }

                                string role = reader["Role"].ToString();
                                button1.Visible = role != "Admin";
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
                MessageBox.Show("An error occurred while loading user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editprofile_label_Click(object sender, EventArgs e)
        {

        }

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            string name = name_textbox.Text.Trim();
            string username = username_textbox.Text.Trim();
            string email = email_textbox.Text.Trim();
            string dob = dob_picker.Value.ToString("yyyy-MM-dd");
            string gender = male_Btn.Checked ? "Male" : "Female";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all the fields.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "UPDATE Users SET Name = @Name, Username = @Username, Email = @Email, DOB = @DOB, Gender = @Gender WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DOB", dob);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@UserID", userId);  
                        con.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {

            try
            {
                string userRole = "";

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = "SELECT Role FROM Users WHERE UserID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        con.Open();

                        object result = cmd.ExecuteScalar();
                        userRole = result?.ToString();
                    }
                }

                this.Close();

                if (userRole == "Free" || userRole == "Silver" || userRole == "Gold")
                {
                    User_Dashboard f3 = new User_Dashboard(userId);
                    f3.Show();
                }
                else if (userRole == "Notemaster")
                {
                    NoteMaster_Dashboard f4 = new NoteMaster_Dashboard(userId);
                    f4.Show();
                }
                else if (userRole == "Admin")
                {
                    Admin_Dashboard f5 = new Admin_Dashboard(userId);
                    f5.Show();
                }
                else
                {
                    MessageBox.Show("Invalid role. Unable to navigate to the dashboard.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            ChangePassword f9 = new ChangePassword(userId);
            f9.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show
            (
                "Are you sure you want to delete your account? This action cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        string sql = "DELETE FROM Users WHERE UserID = @UserID";

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Your account has been deleted successfully.", "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Login_Form form1 = new Login_Form();
                    this.Hide();
                    form1.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting your account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
