using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Ratings_Review : Form
    {
        private int userId;
        private int noteId;
        private int selectedRating = 0; 
        public Ratings_Review(int userId, int noteId)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteId;
            InitializeRatings();
            
        }
        

        private void InitializeRatings()
        {
            rating_image1.Image = Properties.Resources.rating_hollow;
            rating_image2.Image = Properties.Resources.rating_hollow;
            rating_image3.Image = Properties.Resources.rating_hollow;
            rating_image4.Image = Properties.Resources.rating_hollow;
            rating_image5.Image = Properties.Resources.rating_hollow;

            selectedRating = 0;         }

        private void UpdateRating(int rating)
        {
            selectedRating = rating;

            rating_image1.Image = rating >= 1 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image2.Image = rating >= 2 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image3.Image = rating >= 3 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image4.Image = rating >= 4 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image5.Image = rating >= 5 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateRating(1);         }

        private void rating_image2_Click(object sender, EventArgs e)
        {
            UpdateRating(2);         }

        private void rating_image3_Click(object sender, EventArgs e)
        {
            UpdateRating(3);         }

        private void rating_image4_Click(object sender, EventArgs e)
        {
            UpdateRating(4);         }

        private void rating_image5_Click(object sender, EventArgs e)
        {
            UpdateRating(5);         }

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    con.Open();

                    string sql = "SELECT Rating, Review FROM Ratings WHERE NoteID = @NoteID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int existingRating = reader.GetInt32(0); string existingReview = reader.GetString(1);
                                UpdateRating(existingRating);
                                textBox1.Text = existingReview;

                                MessageBox.Show("You have already reviewed this note. Feel free to update your review.", "Existing Review", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();         }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
