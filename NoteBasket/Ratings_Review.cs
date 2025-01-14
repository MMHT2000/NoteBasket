using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Ratings_Review : Form
    {
        private int userId;
        private int noteId;
        private int selectedRating = 0; // Variable to store the selected rating

        public Ratings_Review(int userId, int noteId)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteId;
            InitializeRatings();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    con.Open();

                    // Query to check if the user has already reviewed this note
                    string sql = "SELECT Rating, Review FROM Ratings WHERE NoteID = @NoteID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // User has already reviewed this note
                                int existingRating = reader.GetInt32(0); // Existing rating
                                string existingReview = reader.GetString(1); // Existing review

                                // Update the UI with existing data
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

        private void InitializeRatings()
        {
            // Set all rating images to hollow by default
            rating_image1.Image = Properties.Resources.rating_hollow;
            rating_image2.Image = Properties.Resources.rating_hollow;
            rating_image3.Image = Properties.Resources.rating_hollow;
            rating_image4.Image = Properties.Resources.rating_hollow;
            rating_image5.Image = Properties.Resources.rating_hollow;

            selectedRating = 0; // Reset selectedRating
        }

        private void UpdateRating(int rating)
        {
            // Update the selected rating value
            selectedRating = rating;

            // Fill the appropriate number of stars based on the rating
            rating_image1.Image = rating >= 1 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image2.Image = rating >= 2 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image3.Image = rating >= 3 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image4.Image = rating >= 4 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
            rating_image5.Image = rating >= 5 ? Properties.Resources.rating_filled : Properties.Resources.rating_hollow;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateRating(1); // Set rating to 1 when the first star is clicked
        }

        private void rating_image2_Click(object sender, EventArgs e)
        {
            UpdateRating(2); // Set rating to 2 when the second star is clicked
        }

        private void rating_image3_Click(object sender, EventArgs e)
        {
            UpdateRating(3); // Set rating to 3 when the third star is clicked
        }

        private void rating_image4_Click(object sender, EventArgs e)
        {
            UpdateRating(4); // Set rating to 4 when the fourth star is clicked
        }

        private void rating_image5_Click(object sender, EventArgs e)
        {
            UpdateRating(5); // Set rating to 5 when the fifth star is clicked
        }

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            // Validate if a rating has been selected
            if (selectedRating == 0)
            {
                MessageBox.Show("Please select a rating before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string review = textBox1.Text.Trim(); // Get the review from the textbox

            if (string.IsNullOrEmpty(review))
            {
                MessageBox.Show("Please provide a review before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Insert the rating and review into the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "INSERT INTO Ratings (NoteID, UserID, Rating, Review, RatingDate) " +
                                 "VALUES (@NoteID, @UserID, @Rating, @Review, @RatingDate)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);        // Current NoteID
                        cmd.Parameters.AddWithValue("@UserID", userId);        // Logged-in UserID
                        cmd.Parameters.AddWithValue("@Rating", selectedRating); // Selected rating
                        cmd.Parameters.AddWithValue("@Review", review);        // Review from the textbox
                        cmd.Parameters.AddWithValue("@RatingDate", DateTime.Now); // Current date and time

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                // Show success message and reset the UI
                MessageBox.Show("Rating and review submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI and variables
                InitializeRatings();
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while submitting the rating: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
