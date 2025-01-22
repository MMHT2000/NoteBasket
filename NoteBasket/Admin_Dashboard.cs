using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Admin_Dashboard : Form
    {
        private int userId;

        private string imagePath = Path.Combine(Application.StartupPath, "images");


        public Admin_Dashboard(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadUserData();
            LoadUsers();
            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Name, Username, Email, DOB, Gender, Role, SubscriptionStartDate, LoyaltyPoints, CreatedAt " +
                                 "FROM Users WHERE UserID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                name_label.Text = reader["Name"]?.ToString() ?? "Not Set";
                                username_label.Text = "@" + (reader["Username"]?.ToString() ?? "Not Set");
                                emaildynamic_label.Text = reader["Email"]?.ToString() ?? "Not Set";
                                dobdynamic_label.Text = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd") : "Not Set";
                                genderdynamiclabel.Text = reader["Gender"]?.ToString() ?? "Not Set";
                                roledynamic_label.Text = reader["Role"]?.ToString() ?? "Not Set";
                                subscriptionsdynamic_label.Text = reader["SubscriptionStartDate"] != DBNull.Value ? Convert.ToDateTime(reader["SubscriptionStartDate"]).ToString("yyyy-MM-dd") : "Not Subscribed";
                                loyaltydynamic_label.Text = reader["LoyaltyPoints"] != DBNull.Value ? reader["LoyaltyPoints"].ToString() : "0";
                                accountcreationdynamic_label.Text = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]).ToString("yyyy-MM-dd") : "Not Available";

                                string gender = reader["Gender"]?.ToString() ?? "Not Set";
                                if (gender == "Male")
                                    profilepicture_box.ImageLocation = Path.Combine(imagePath, "Iconarchive-Incognito-Animals-Giraffe-Avatar.128.png");
                                else if (gender == "Female")
                                    profilepicture_box.ImageLocation = Path.Combine(imagePath, "Hopstarter-Superhero-Avatar-Avengers-Giant-Man.128.png");
                                else
                                    profilepicture_box.ImageLocation = Path.Combine(imagePath, "DefaultProfileImage.png");
                            }
                            else
                            {
                                name_label.Text = "Not Set";
                                username_label.Text = "Not Set";
                                emaildynamic_label.Text = "Not Set";
                                dobdynamic_label.Text = "Not Set";
                                genderdynamiclabel.Text = "Not Set";
                                roledynamic_label.Text = "Not Set";
                                subscriptionsdynamic_label.Text = "Not Set";
                                loyaltydynamic_label.Text = "0";
                                accountcreationdynamic_label.Text = "Not Set";

                                profilepicture_box.ImageLocation = Path.Combine(imagePath, "DefaultProfileImage.png");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                name_label.Text = "Not Set";
                username_label.Text = "Not Set";
                emaildynamic_label.Text = "Not Set";
                dobdynamic_label.Text = "Not Set";
                genderdynamiclabel.Text = "Not Set";
                roledynamic_label.Text = "Not Set";
                subscriptionsdynamic_label.Text = "Not Set";
                loyaltydynamic_label.Text = "0";
                accountcreationdynamic_label.Text = "Not Set";

                profilepicture_box.ImageLocation = Path.Combine(imagePath, "DefaultProfileImage.png");
            }

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
            LoadUsers(searchQuery);
        }

        private void LoadUsers(string searchQuery = "", string[] roles = null)
        {
            try
            {
                
                foreach (Control control in panel5.Controls.OfType<Panel>().ToList())
                {
                    panel5.Controls.Remove(control);
                }

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    
                    string roleCondition = roles != null && roles.Length > 0 ? "AND Role IN ('" + string.Join("', '", roles) + "')" : "";

                    string sql = @"
                    SELECT UserID, Name, Username, Email, Gender 
                    FROM Users 
                     WHERE 
                    (@SearchQuery = '' OR Name LIKE '%' + @SearchQuery + '%' OR Username LIKE '%' + @SearchQuery + '%') 
                    AND Role != 'Admin' " + roleCondition;

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                if (!string.IsNullOrEmpty(searchQuery))
                                {
                                    MessageBox.Show("No users found for the given search query.", "No Users Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                return;
                            }

                            int yPosition = 60; 
                            while (reader.Read())
                            {
                                int userId = reader.GetInt32(0); 
                                string name = reader.GetString(1); 
                                string username = reader.GetString(2); 
                                string email = reader.IsDBNull(3) ? "Not Available" : reader.GetString(3); 
                                string gender = reader.IsDBNull(4) ? "Not Available" : reader.GetString(4); 

                              
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(525, 80),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                               
                                Label nameLabel = new Label
                                {
                                    Text = $"Name: {name}",
                                    AutoSize = false,
                                    Size = new Size(230, 20),
                                    Location = new Point(10, 10),
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    ForeColor = Color.Black
                                };

                               
                                Label usernameLabel = new Label
                                {
                                    Text = $"Username: {username}",
                                    AutoSize = false,
                                    Size = new Size(150, 20),
                                    Location = new Point(10, 35),
                                    Font = new Font("Arial", 9, FontStyle.Regular),
                                    ForeColor = Color.Black
                                };

                                
                                Label genderLabel = new Label
                                {
                                    Text = $"Gender: {gender}",
                                    AutoSize = false,
                                    Size = new Size(150, 20),
                                    Location = new Point(250, 10),
                                    Font = new Font("Arial", 9, FontStyle.Regular),
                                    ForeColor = Color.DarkBlue
                                };

                                
                                Label emailLabel = new Label
                                {
                                    Text = $"Email: {email}",
                                    AutoSize = false,
                                    Size = new Size(260, 20),
                                    Location = new Point(160, 35),
                                    Font = new Font("Arial", 9, FontStyle.Italic),
                                    ForeColor = Color.DarkGreen
                                };

                               
                                Button viewButton = new Button
                                {
                                    Text = "Manage",
                                    Size = new Size(80, 30),
                                    Location = new Point(430, 25),
                                    BackColor = Color.White,
                                    FlatStyle = FlatStyle.Popup
                                };

                                
                                viewButton.Click += (s, e) =>
                                {
                                    Manage_User manageUserForm = new Manage_User(userId);
                                    this.Hide();
                                    manageUserForm.Show();
                                };

                               
                                dynamicPanel.Controls.Add(nameLabel);
                                dynamicPanel.Controls.Add(usernameLabel);
                                dynamicPanel.Controls.Add(genderLabel);
                                dynamicPanel.Controls.Add(emailLabel);
                                dynamicPanel.Controls.Add(viewButton);

                                
                                panel5.Controls.Add(dynamicPanel);

                                yPosition += 90; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void createanaccount_label_Click(object sender, EventArgs e)
        {

        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form form1 = new Login_Form();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditProfile editProfileForm = new EditProfile(userId);
            editProfileForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Note_Approval note_Approval = new Note_Approval(userId);
            note_Approval.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Note_Manager noteManager = new Note_Manager(userId);
            noteManager.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadUsers(roles: new string[] { "Free", "Silver", "Gold" });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadUsers(roles: new string[] { "Notemaster" });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadUsers(roles: new string[] { "Free", "Silver", "Gold", "Notemaster" });
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
            Reported_Notes reportedNotesForm = new Reported_Notes(userId);
            reportedNotesForm.Show();

        }
    }
}
