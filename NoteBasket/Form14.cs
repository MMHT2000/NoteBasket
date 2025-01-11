using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Form14 : Form
    {
        private int userId;

        public Form14(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadBookmarks();
        }

        private void LoadBookmarks()
        {
            try
            {
                panel2.Controls.Clear(); // Clear existing controls before adding new ones

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = @"
                        SELECT b.BookmarkID, b.BookmarkDate, n.NoteID, n.Title
                        FROM Bookmarks b
                        INNER JOIN Notes n ON b.NoteID = n.NoteID
                        WHERE b.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPosition = 10;

                            while (reader.Read())
                            {
                                int noteId = reader.GetInt32(2); // NoteID
                                string noteTitle = reader.GetString(3); // Title
                                DateTime bookmarkDate = reader.GetDateTime(1); // BookmarkDate

                                // Create a new panel for the bookmark
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(540, 80),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle

                                };

                                // Create a label to display bookmark details
                                Label detailsLabel = new Label
                                {
                                    Text = $"You have bookmarked '{noteTitle}' on {bookmarkDate.ToShortDateString()}",
                                    AutoSize = false,
                                    Size = new Size(480, 40),
                                    Location = new Point(10, 10),
                                    Font = new Font("Arial", 10, FontStyle.Regular),
                                    ForeColor = Color.Black
                                };

                                // Attach a click event to the panel
                                dynamicPanel.Click += (s, e) =>
                                {
                                    Form11 form11 = new Form11(userId, noteId);
                                    this.Hide();
                                    form11.Show();
                                };

                                detailsLabel.Click += (s, e) =>
                                {
                                    Form11 form11 = new Form11(userId, noteId);
                                    this.Hide();
                                    form11.Show();
                                };

                                // Add the label to the panel
                                dynamicPanel.Controls.Add(detailsLabel);

                                // Add the panel to panel2
                                panel2.Controls.Add(dynamicPanel);

                                yPosition += 90; // Adjust yPosition for the next panel
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading bookmarks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Customize the appearance of panel2
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
