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
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class NoteMaster_Dashboard : Form
    {
        private int userId;
        private int noteId;

        string imagePath = Path.Combine(Application.StartupPath, "images");

        public NoteMaster_Dashboard(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadUserData();
            LoadNotes();
            textBox1.TextChanged += TextBox2_TextChanged;

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
            LoadNotes(searchQuery);
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

                profilepicture_box.ImageLocation = Path.Combine(imagePath, "pngegg.png");
            }

        }

        private void LoadNotes(string searchQuery = "", string subscriptionLevel = null)
        {
            try
            {
                
                foreach (Control control in panel5.Controls.OfType<Panel>().ToList())
                {
                    panel5.Controls.Remove(control);
                }

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = @"
                    SELECT NoteID, Title, FilePath, Category, SubscriptionLevel 
                    FROM Notes 
                    WHERE 
                    (@SearchQuery = '' OR Title LIKE '%' + @SearchQuery + '%' OR Category LIKE '%' + @SearchQuery + '%')
                    AND Status = 'Approved' 
                    AND UploadedBy = @UserId"; 

                    
                    if (!string.IsNullOrEmpty(subscriptionLevel))
                    {
                        sql += " AND SubscriptionLevel = @SubscriptionLevel";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
                        cmd.Parameters.AddWithValue("@UserId", userId);  

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
                                    Size = new Size(520, 80),
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
                                    Text = "Manage",
                                    Size = new Size(80, 30),
                                    Location = new Point(430, 25),
                                    BackColor = Color.White,
                                    FlatStyle = FlatStyle.Popup
                                };

                                viewButton.Click += (s, e) =>
                                {
                                    this.Close();
                                    EditNoteDetails editNoteDetailsForm = new EditNoteDetails(userId,noteId);
                                    editNoteDetailsForm.Show();
                                };

                                dynamicPanel.Controls.Add(dpictureBox);
                                dynamicPanel.Controls.Add(titleLabel);
                                dynamicPanel.Controls.Add(categoryLabel);
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
                MessageBox.Show($"An error occurred while loading notes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            UploadNote form10 = new UploadNote(userId);
            form10.Show();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadNotes(subscriptionLevel: "Free");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadNotes(subscriptionLevel: "Silver");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadNotes(subscriptionLevel: "Gold");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadNotes();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            ManageMyNotes manageMyNotesForm = new ManageMyNotes(userId);
            manageMyNotesForm.Show();
        }
    }
}
