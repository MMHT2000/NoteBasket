using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class BookMarks : Form
    {
        private int userId;

        public BookMarks(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadBookmarks();
        }

        private void LoadBookmarks()
        {
            try
            {
                panel2.Controls.Clear(); 
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
                                int noteId = reader.GetInt32(2);                                 
                                string noteTitle = reader.GetString(3);                                 
                                DateTime bookmarkDate = reader.GetDateTime(1); 
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(540, 80),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle

                                };

                                Label detailsLabel = new Label
                                {
                                    Text = $"You have bookmarked '{noteTitle}' on {bookmarkDate.ToShortDateString()}",
                                    AutoSize = false,
                                    Size = new Size(480, 70),
                                    Location = new Point(10, 10),
                                    Font = new Font("Times New Roman", 14, FontStyle.Regular),
                                    ForeColor = Color.Black
                                };

                                dynamicPanel.Click += (s, e) =>
                                {
                                    NoteDetails form11 = new NoteDetails(userId, noteId);
                                    this.Hide();
                                    form11.Show();
                                };

                                detailsLabel.Click += (s, e) =>
                                {
                                    NoteDetails form11 = new NoteDetails(userId, noteId);
                                    this.Hide();
                                    form11.Show();
                                };

                                dynamicPanel.Controls.Add(detailsLabel);

                                panel2.Controls.Add(dynamicPanel);

                                yPosition += 90;                             }
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
                    
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
