using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class NotePreview : Form
    {
        private bool isBookmarked;
        private int userId;
        private int noteId;

        public NotePreview(int userId, int noteId)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteId;

            // Original code logic
            using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
            {
                string sql = $"select count(*) from Bookmarks where USERID = {userId} and NoteID ={noteId}";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int x = (int)cmd.ExecuteScalar();
                    if (x == 1)
                    {
                        bookmarkimage.Image = Properties.Resources.bookmark_filled;
                        isBookmarked = true;
                    }
                    else
                    {
                        bookmarkimage.Image = Properties.Resources.bookmark_hollow;
                        isBookmarked = false;
                    }
                }
            }

            using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
            {
                string sql = "SELECT Role FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    con.Open();
                    string role = (string)cmd.ExecuteScalar();

                    if (role == "Free")
                    {
                        backtosignin_btn.Visible = false;
                    }
                    else
                    {
                        backtosignin_btn.Visible = true;
                    }
                }
            }

            
            LoadNoteDetails();
            LoadUploaderDetails();
        }

        private void LoadNoteDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Title, Description, FilePath, UploadedBy FROM Notes WHERE NoteID = @NoteID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string filePath = reader["FilePath"].ToString();
                                int uploadedBy = Convert.ToInt32(reader["UploadedBy"]);

                                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                                {
                                    pictureBox1.Image = Image.FromFile(filePath);
                                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pictureBox1.BackColor = Color.LightGray;
                                }
                                else
                                {
                                    pictureBox1.Image = null;
                                    pictureBox1.BackColor = Color.LightGray;
                                }

                                LoadUploaderDetails(uploadedBy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading note details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUploaderDetails(int uploadedBy = 0)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = @"
                        SELECT 
                            Username, Email, DOB, Gender, Role, CreatedAt 
                        FROM Users 
                        WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", uploadedBy);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                username_label.Text = reader["Username"].ToString();
                                emaildynamic_label.Text = reader["Email"].ToString();

                                if (reader["DOB"] != DBNull.Value)
                                    dobdynamic_label.Text = Convert.ToDateTime(reader["DOB"]).ToShortDateString();

                                genderdynamiclabel.Text = reader["Gender"].ToString();
                                roledynamic_label.Text = reader["Role"].ToString();

                                if (reader["CreatedAt"] != DBNull.Value)
                                    accountcreationdynamic_label.Text = Convert.ToDateTime(reader["CreatedAt"]).ToShortDateString();
                            }
                        }
                    }
                }

                LoadTotalNotesCount(uploadedBy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading uploader details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTotalNotesCount(int uploadedBy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT COUNT(*) FROM Notes WHERE UploadedBy = @UploadedBy";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UploadedBy", uploadedBy);
                        con.Open();

                        int totalNotes = (int)cmd.ExecuteScalar();
                        number_ofNotes.Text = totalNotes.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading total notes count: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backtosignin_btn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No image is available to save.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp|All Files|*.*",
                Title = "Save Note Image",
                FileName = "NoteImage"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = saveFileDialog1.FileName;

                    System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                    string extension = System.IO.Path.GetExtension(filePath).ToLower();

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = System.Drawing.Imaging.ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = System.Drawing.Imaging.ImageFormat.Bmp;
                            break;
                        case ".png":
                            format = System.Drawing.Imaging.ImageFormat.Png;
                            break;
                        default:
                            MessageBox.Show("Unsupported file format. Saving as PNG by default.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }

                    pictureBox1.Image.Save(filePath, format);
                    MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (isBookmarked)
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = $"DELETE FROM Bookmarks WHERE UserID = {userId} AND NoteID = {noteId}";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        isBookmarked = false;
                        bookmarkimage.Image = Properties.Resources.bookmark_hollow;
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "INSERT INTO Bookmarks (UserID, NoteID, BookmarkDate) VALUES (@UserID, @NoteID, @BookmarkDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        cmd.Parameters.AddWithValue("@BookmarkDate", DateTime.Now);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        isBookmarked = true;
                        bookmarkimage.Image = Properties.Resources.bookmark_filled;
                    }
                }
            }
        }

        private void profilepicture_box_Click(object sender, EventArgs e)
        {
            // Logic for handling the click event
        }

        private void NotePreview_Load(object sender, EventArgs e)
        {
            // Event handling logic (if any)
        }
        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            Ratings_Review form13 = new Ratings_Review(userId, noteId);
            form13.StartPosition = FormStartPosition.CenterParent;
            form13.ShowDialog();

        }


    }
}
