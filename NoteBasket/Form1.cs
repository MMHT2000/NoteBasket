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
using BCrypt.Net;

namespace NoteBasket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel2.Paint += panel2_Paint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = user_textbox.Text.Trim();
            string password = password_textbox.Text.Trim();

            // Check if both fields are filled
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username/email and password.");
                return;
            }

            try
            {
                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=DESKTOP-RS5QGMS\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Query to retrieve the user by either username or email
                    string sql = "SELECT UserID, Username, PasswordHash, Name, Email, DOB, Gender, Role, SubscriptionStartDate, LoyaltyPoints, CreatedAt " +
                                 "FROM Users WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters with their values
                        cmd.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);

                        // Open the connection
                        con.Open();

                        // Execute the query
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if a user was found
                            if (reader.Read())
                            {
                                // Get the user details
                                string username = reader["Username"].ToString();
                                string name = reader["Name"].ToString();
                                string email = reader["Email"].ToString();
                                string dob = Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd");
                                string gender = reader["Gender"].ToString();
                                string role = reader["Role"].ToString();

                                // Check if SubscriptionStartDate is NULL and assign default value if necessary
                                int subscriptions = reader["SubscriptionStartDate"] != DBNull.Value ? Convert.ToInt32(reader["SubscriptionStartDate"]) : 0;

                                // Get loyalty points and account creation date
                                int loyaltyPoints = Convert.ToInt32(reader["LoyaltyPoints"]);
                                DateTime accountCreationDate = Convert.ToDateTime(reader["CreatedAt"]);

                                // Verify password
                                string storedPasswordHash = reader["PasswordHash"].ToString();
                                if (BCrypt.Net.BCrypt.Verify(password, storedPasswordHash))
                                {
                                    // Navigate to Form3 (User Dashboard) and pass user data
                                    Form3 form3 = new Form3(name, username, email, dob, gender, role, subscriptions, loyaltyPoints, accountCreationDate);
                                    form3.Show();

                                    // Hide the login form
                                    this.Hide();
                                }
                                else
                                {
                                    // Incorrect password
                                    MessageBox.Show("Incorrect password. Please try again.");
                                }
                            }
                            else
                            {
                                // User not found
                                MessageBox.Show("User not found. Please check your username/email and try again.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log errors or show user-friendly messages)
                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
