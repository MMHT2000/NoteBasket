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
    public partial class Form5 : Form
    {
        private int userId;

        private string imagePath = Path.Combine(Application.StartupPath, "images");


        public Form5(int userId)
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
                                // Inline default values
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
                // Handle exceptions and set default values inline
                MessageBox.Show("An error occurred while retrieving user data: " + ex.Message);

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

        private void createanaccount_label_Click(object sender, EventArgs e)
        {

        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 editProfileForm = new Form8(userId);
            editProfileForm.Show();
        }
    }
}
