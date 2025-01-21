using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using System.Data.SqlClient;

using System.IO;

namespace NoteBasket
{
    public partial class User_Dashboard : Form
    {
        private int userId;
        private BookMarks form14Instance;

        string imagePath = Path.Combine(Application.StartupPath, "images");


        public User_Dashboard(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadUserData();
            LoadNotes();

            textBox2.TextChanged += TextBox2_TextChanged;

            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Role FROM Users WHERE UserID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();

                        string role;

                        role = cmd.ExecuteScalar().ToString();

                        if (role == "Free")
                        {
                            button11.Visible = false;
                            button12.Visible = false;
                        }
                        else if (role == "Silver")
                        {
                            button11.Visible = true;
                            button12.Visible = false;
                        }
                        else if (role == "Gold")
                        {
                            button11.Visible = true;
                            button12.Visible = true;


                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox2.Text.Trim();             
            LoadNotes(searchQuery);         
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Name, Username, Email, DOB, Gender, Role, SubscriptionEndDate, LoyaltyPoints, CreatedAt " +
                                 "FROM Users WHERE UserID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader["Name"].ToString();
                                string username = reader["Username"].ToString();
                                string email = reader["Email"].ToString();
                                string dob = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd") : "Not Available";
                                string gender = reader["Gender"].ToString();
                                string role = reader["Role"].ToString();
                                string subscriptions = reader["SubscriptionEndDate"] != DBNull.Value ? Convert.ToDateTime(reader["SubscriptionEndDate"]).ToString("yyyy-MM-dd") : "Not Subscribed";
                                int loyaltyPoints = Convert.ToInt32(reader["LoyaltyPoints"]);
                                string accountCreationDate = Convert.ToDateTime(reader["CreatedAt"]).ToString("yyyy-MM-dd");

                                // Check if SubscriptionEndDate is expired
                                DateTime? subscriptionEndDate = reader["SubscriptionEndDate"] != DBNull.Value ? (DateTime?)reader["SubscriptionEndDate"] : null;
                                if (subscriptionEndDate.HasValue && subscriptionEndDate.Value < DateTime.Today)
                                {
                                    // Update role to Free and clear SubscriptionEndDate
                                    reader.Close();
                                    UpdateRoleToFreeAndClearSubscription(userId, con);
                                }

                                name_label.Text = name;
                                username_label.Text = "@" + username;
                                emaildynamic_label.Text = email;
                                dobdynamic_label.Text = dob;
                                genderdynamiclabel.Text = gender;
                                roledynamic_label.Text = role;
                                subscriptionsdynamic_label.Text = subscriptions;
                                loyaltydynamic_label.Text = loyaltyPoints.ToString();
                                accountcreationdynamic_label.Text = accountCreationDate;

                                if (gender == "Male")
                                {
                                    profilepicture_box.ImageLocation = Path.Combine(imagePath, "Iconarchive-Incognito-Animals-Giraffe-Avatar.128.png");
                                }
                                else if (gender == "Female")
                                {
                                    profilepicture_box.ImageLocation = Path.Combine(imagePath, "Hopstarter-Superhero-Avatar-Avengers-Giant-Man.128.png");
                                }


                            }
                            else
                            {
                                MessageBox.Show("User data not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRoleToFreeAndClearSubscription(int userId, SqlConnection con)
        {
            string updateSql = "UPDATE Users SET Role = 'Free', SubscriptionStartDate = NULL, SubscriptionEndDate = NULL WHERE UserID = @UserId";

            using (SqlCommand cmd = new SqlCommand(updateSql, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        private void LoadNotes(string searchQuery = "", string subscriptionLevel = null)
        {
            try
            {
                foreach (Control control in panel6.Controls.OfType<Panel>().ToList())
                {
                    panel6.Controls.Remove(control);
                }

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string userRole = GetUserRole(userId);

                    string allowedSubscriptionLevels = "";
                    switch (userRole)
                    {
                        case "Gold":
                            allowedSubscriptionLevels = "'Free', 'Silver', 'Gold'";
                            break;
                        case "Silver":
                            allowedSubscriptionLevels = "'Free', 'Silver'";
                            break;
                        default:                             
                            allowedSubscriptionLevels = "'Free'";
                            break;
                    }

                    string sql = @"
                    SELECT NoteID, Title, FilePath, Category, SubscriptionLevel 
                    FROM Notes 
                    WHERE 
                        (@SearchQuery = '' OR Title LIKE '%' + @SearchQuery + '%' OR Category LIKE '%' + @SearchQuery + '%')
                        AND Status = 'Approved'";

                    if (!string.IsNullOrEmpty(subscriptionLevel))
                    {
                        sql += " AND SubscriptionLevel = @SubscriptionLevel";
                    }
                    else
                    {
                        sql += $" AND SubscriptionLevel IN ({allowedSubscriptionLevels})";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);

                        if (!string.IsNullOrEmpty(subscriptionLevel))
                        {
                            cmd.Parameters.AddWithValue("@SubscriptionLevel", subscriptionLevel);
                        }

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                if (!string.IsNullOrEmpty(searchQuery))
                                {
                                    MessageBox.Show("No notes found for the given search query.", "No Notes Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                return;
                            }

                            int yPosition = 60; 
                            while (reader.Read())
                            {
                                int noteId = reader.GetInt32(0);                                 
                                string title = reader.GetString(1);                                 
                                string filePath = reader.GetString(2);                                 
                                string category = reader.GetString(3); 
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(525, 80),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                PictureBox dpictureBox = new PictureBox
                                {
                                    Size = new Size(60, 60),
                                    Location = new Point(10, 10),
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                string imagePath = Path.Combine(Application.StartupPath, filePath);
                                if (File.Exists(imagePath))
                                {
                                    dpictureBox.Image = Image.FromFile(imagePath);
                                }
                                else
                                {
                                    dpictureBox.Image = Properties.Resources.bookmark_filled;                                 
                                }

                                Label titleLabel = new Label
                                {
                                    Text = title,
                                    AutoSize = false,
                                    Size = new Size(300, 20),
                                    Location = new Point(80, 10),
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    ForeColor = Color.Black
                                };

                                Label categoryLabel = new Label
                                {
                                    Text = category,
                                    AutoSize = false,
                                    Size = new Size(300, 20),
                                    Location = new Point(80, 35),
                                    Font = new Font("Arial", 9, FontStyle.Italic),
                                    ForeColor = Color.Gray
                                };

                                Button viewButton = new Button
                                {
                                    Text = "View",
                                    Size = new Size(80, 30),
                                    Location = new Point(430, 25),
                                    BackColor = Color.White,
                                    FlatStyle = FlatStyle.Popup
                                };

                                viewButton.Click += (s, e) =>
                                {
                                    NoteDetails form11 = new NoteDetails(userId, noteId);
                                    this.Hide();
                                    form11.Show();
                                };

                                dynamicPanel.Controls.Add(dpictureBox);
                                dynamicPanel.Controls.Add(titleLabel);
                                dynamicPanel.Controls.Add(categoryLabel);
                                dynamicPanel.Controls.Add(viewButton);

                                panel6.Controls.Add(dynamicPanel);

                                yPosition += 90;                             }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading notes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private string GetUserRole(int userId)
        {
            using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
            {
                string sql = "SELECT Role FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    con.Open();

                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "Free";                 
                }
            }
        }

        private string GetAllowedSubscriptionLevels(string userRole)
        {
            switch (userRole)
            {
                case "Gold":
                    return "'Free', 'Silver', 'Gold'";
                case "Silver":
                    return "'Free', 'Silver'";
                default:                     
                    return "'Free'";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form f1 = new Login_Form();
            f1.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            EditProfile editProfileForm = new EditProfile(userId);
           
            editProfileForm.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }


        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        

        private void profilepicture_box_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            LoadNotes(subscriptionLevel: "Free");

        }
        private void ShowForm14()
        {
            if (form14Instance == null || form14Instance.IsDisposed)
            {
                Point panel6LocationOnScreen = panel6.PointToScreen(Point.Empty);

                form14Instance = new BookMarks(userId)
                {
                    StartPosition = FormStartPosition.Manual,                     
                    Location = panel6LocationOnScreen                        
                };
                form14Instance.Show();
            }
            else
            {
                form14Instance.BringToFront();             
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            ShowForm14();

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadNotes();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadNotes(subscriptionLevel: "Silver");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            LoadNotes(subscriptionLevel: "Gold");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Subscription_Tiers subscription_Tiers = new Subscription_Tiers(userId);
            subscription_Tiers.Show();
        }

        private void name_label_Click(object sender, EventArgs e)
        {

        }
    }
}
