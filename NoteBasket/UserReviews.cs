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
    
    public partial class UserReviews : Form
    {
        private int userId;
        public UserReviews(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserReviews();
        }

        private void LoadUserReviews()
        {
            try
            {
                
                foreach (Control control in panel5.Controls.OfType<Panel>().ToList())
                {
                    panel5.Controls.Remove(control);
                }

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = @"
                SELECT 
                    r.Rating, 
                    r.Review, 
                    n.Title AS NoteTitle, 
                    u.Name AS UserName 
                FROM Ratings r
                INNER JOIN Notes n ON r.NoteID = n.NoteID
                INNER JOIN Users u ON r.UserID = u.UserID
                WHERE n.UploadedBy = @UploadedBy
                ORDER BY r.RatingDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@UploadedBy", userId);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPosition = 10; 

                            while (reader.Read())
                            {
                                string userName = reader["UserName"].ToString();
                                string noteTitle = reader["NoteTitle"].ToString();
                                int rating = Convert.ToInt32(reader["Rating"]);
                                string review = reader["Review"].ToString();

                                
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(520, 100),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                
                                Label detailsLabel = new Label
                                {
                                    Text = $"{userName} reviewed \"{noteTitle}\"",
                                    AutoSize = false,
                                    Size = new Size(500, 20),
                                    Location = new Point(10, 10),
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    ForeColor = Color.Black
                                };

                                
                                Label ratingLabel = new Label
                                {
                                    Text = $"Rating: {rating} star(s)",
                                    AutoSize = false,
                                    Size = new Size(500, 20),
                                    Location = new Point(10, 35),
                                    Font = new Font("Arial", 9, FontStyle.Regular),
                                    ForeColor = Color.DarkGoldenrod
                                };

                                
                                Label reviewLabel = new Label
                                {
                                    Text = $"Review: {review}",
                                    AutoSize = false,
                                    Size = new Size(500, 40),
                                    Location = new Point(10, 60),
                                    Font = new Font("Arial", 9, FontStyle.Italic),
                                    ForeColor = Color.DarkGreen
                                };

                                
                                dynamicPanel.Controls.Add(detailsLabel);
                                dynamicPanel.Controls.Add(ratingLabel);
                                dynamicPanel.Controls.Add(reviewLabel);

                               
                                panel5.Controls.Add(dynamicPanel);

                                yPosition += 110; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user reviews: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UserReviews_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            NoteMaster_Dashboard noteMasterDashboard = new NoteMaster_Dashboard(userId);
            noteMasterDashboard.Show();
        }
    }
}
