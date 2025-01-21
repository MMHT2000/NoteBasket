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
    public partial class Manage_User : Form
    {
        private int userId;
        public Manage_User(int userId)
        {
            InitializeComponent();
            this.userId = userId;

        }

        private void Manage_User_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = @"
            SELECT Name, Username, Email, DOB, Gender 
            FROM Users 
            WHERE UserID = @UserID";

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

                                if (!reader.IsDBNull(reader.GetOrdinal("DOB")))
                                {
                                    dob_picker.Value = reader.GetDateTime(reader.GetOrdinal("DOB"));
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
                                else
                                {
                                    male_Btn.Checked = false;
                                    female_Btn.Checked = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("User details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    MessageBox.Show("The account has been deleted successfully.", "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Admin_Dashboard admin_Dashboard = new Admin_Dashboard(18);
                    this.Hide();
                    admin_Dashboard.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting your account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard(18);
            admin_Dashboard.Show();
        }
    }
}
