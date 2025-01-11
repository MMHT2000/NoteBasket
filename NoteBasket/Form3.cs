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
    public partial class Form3 : Form
    {
        private int userId;
        private Form14 form14Instance;

        string imagePath = Path.Combine(Application.StartupPath, "images");
        private int noteID=2;


        public Form3(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadUserData();
            LoadNotes();

            textBox2.TextChanged += TextBox2_TextChanged;

            try
            {
                // Connect to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Query to retrieve user details
                    string sql = "SELECT Role FROM Users WHERE UserID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add the parameter for userId
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        // Open the connection
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
                // Handle exceptions
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox2.Text.Trim(); // Get the current text from textBox2
            LoadNotes(searchQuery); // Call the LoadNotes method with the search query
        }

        private void LoadUserData()
        {
            try
            {
                // Connect to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Query to retrieve user details
                    string sql = "SELECT Name, Username, Email, DOB, Gender, Role, SubscriptionStartDate, LoyaltyPoints, CreatedAt " +
                                 "FROM Users WHERE UserID = @UserId";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add the parameter for userId
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        // Open the connection
                        con.Open();

                        // Execute the query and retrieve data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extract data from the reader
                                string name = reader["Name"].ToString();
                                string username = reader["Username"].ToString();
                                string email = reader["Email"].ToString();
                                string dob = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd") : "Not Available";
                                string gender = reader["Gender"].ToString();
                                string role = reader["Role"].ToString();
                                string subscriptions = reader["SubscriptionStartDate"] != DBNull.Value ? Convert.ToDateTime(reader["SubscriptionStartDate"]).ToString("yyyy-MM-dd") : "Not Subscribed";
                                int loyaltyPoints = Convert.ToInt32(reader["LoyaltyPoints"]);
                                string accountCreationDate = Convert.ToDateTime(reader["CreatedAt"]).ToString("yyyy-MM-dd");

                                // Set the labels with retrieved data
                                name_label.Text = name;
                                username_label.Text = "@" + username;
                                emaildynamic_label.Text = email;
                                dobdynamic_label.Text = dob;
                                genderdynamiclabel.Text = gender;
                                roledynamic_label.Text = role;
                                subscriptionsdynamic_label.Text = subscriptions;
                                loyaltydynamic_label.Text = loyaltyPoints.ToString();
                                accountcreationdynamic_label.Text = accountCreationDate;

                                // Set the profile picture based on gender
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
                // Handle exceptions
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNotes(string searchQuery = "")
        {
            try
            {
                // Clear only dynamically added panels
                foreach (Control control in panel6.Controls.OfType<Panel>().ToList())
                {
                    panel6.Controls.Remove(control);
                }

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string userRole = GetUserRole(userId); // Get the user's role (Free, Silver, or Gold)

                    // Build the SQL query with filtering based on the user's role
                    string sql = @"
            SELECT NoteID, Title, FilePath, Category, SubscriptionLevel 
            FROM Notes 
            WHERE 
                (@SearchQuery = '' OR Title LIKE '%' + @SearchQuery + '%' OR Category LIKE '%' + @SearchQuery + '%')
                AND Status = 'Approved'
                AND SubscriptionLevel IN (" + GetAllowedSubscriptionLevels(userRole) + ")";

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
                                    MessageBox.Show("No notes found for the given search query.", "No Notes Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                return;
                            }

                            int yPosition = 50; // Start below the search box

                            while (reader.Read())
                            {
                                int noteId = reader.GetInt32(0); // NoteID
                                string title = reader.GetString(1); // Title
                                string filePath = reader.GetString(2); // FilePath
                                string category = reader.GetString(3); // Category

                                // Create a dynamic panel
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(520, 80),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                // Create a PictureBox for the thumbnail
                                PictureBox dpictureBox = new PictureBox
                                {
                                    Size = new Size(60, 60),
                                    Location = new Point(10, 10),
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                // Load the thumbnail image
                                string imagePath = Path.Combine(Application.StartupPath, filePath);
                                if (File.Exists(imagePath))
                                {
                                    dpictureBox.Image = Image.FromFile(imagePath);
                                }
                                else
                                {
                                    dpictureBox.Image = Properties.Resources.bookmark_filled; // Fallback image
                                }

                                // Create a label for the title
                                Label titleLabel = new Label
                                {
                                    Text = title,
                                    AutoSize = false,
                                    Size = new Size(300, 20),
                                    Location = new Point(80, 10),
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    ForeColor = Color.Black
                                };

                                // Create a label for the category
                                Label categoryLabel = new Label
                                {
                                    Text = category,
                                    AutoSize = false,
                                    Size = new Size(300, 20),
                                    Location = new Point(80, 35),
                                    Font = new Font("Arial", 9, FontStyle.Italic),
                                    ForeColor = Color.Gray
                                };

                                // Create a button to view the note
                                Button viewButton = new Button
                                {
                                    Text = "View",
                                    Size = new Size(80, 30),
                                    Location = new Point(430, 25),
                                    BackColor = Color.White,
                                    FlatStyle = FlatStyle.Popup
                                };

                                // Attach click event for the View button
                                viewButton.Click += (s, e) =>
                                {
                                    Form11 form11 = new Form11(userId, noteId);
                                    this.Hide();
                                    form11.Show();
                                };

                                // Add controls to the dynamic panel
                                dynamicPanel.Controls.Add(dpictureBox);
                                dynamicPanel.Controls.Add(titleLabel);
                                dynamicPanel.Controls.Add(categoryLabel);
                                dynamicPanel.Controls.Add(viewButton);

                                // Add the dynamic panel to panel6
                                panel6.Controls.Add(dynamicPanel);

                                yPosition += 90; // Adjust position for the next panel
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

        private string GetUserRole(int userId)
        {
            // Fetch the user's role from the database
            using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
            {
                string sql = "SELECT Role FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    con.Open();

                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "Free"; // Default to "Free" if role is not found
                }
            }
        }

        private string GetAllowedSubscriptionLevels(string userRole)
        {
            // Define allowed subscription levels for each user role
            switch (userRole)
            {
                case "Gold":
                    return "'Free', 'Silver', 'Gold'";
                case "Silver":
                    return "'Free', 'Silver'";
                default: // Free users
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
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form8 editProfileForm = new Form8(userId);
           
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
            Form11 form11 = new Form11(userId, noteID);
            form11.Show();

        }
        private void ShowForm14()
        {
            if (form14Instance == null || form14Instance.IsDisposed)
            {
                // Get the screen coordinates of panel6
                Point panel6LocationOnScreen = panel6.PointToScreen(Point.Empty);

                // Create a new instance of Form14 with the userId
                form14Instance = new Form14(userId)
                {
                    StartPosition = FormStartPosition.Manual, // Set the position manually
                    Location = panel6LocationOnScreen        // Position it at panel6's location
                };
                form14Instance.Show();
            }
            else
            {
                form14Instance.BringToFront(); // Bring the existing instance to the front
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            ShowForm14();

        }
    }
}
