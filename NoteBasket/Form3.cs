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
        string imagePath = Path.Combine(Application.StartupPath, "images");



        public Form3(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            LoadUserData();
            


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
                                MessageBox.Show("User data not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message);
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
    }
}
