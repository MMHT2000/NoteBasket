using System;
using System.Data.SqlClient;
using System.Drawing;
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

            CheckBookmarkStatus();
            CheckUserRole();
            LoadNoteDetails();
        }

        private void CheckBookmarkStatus()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT COUNT(*) FROM Bookmarks WHERE UserID = @UserID AND NoteID = @NoteID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@NoteID", noteId);

                        con.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 1)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking bookmark status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckUserRole()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Role FROM Users WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        con.Open();
                        string role = cmd.ExecuteScalar()?.ToString();

                        backtosignin_btn.Visible = role != "Free";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking user role: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                                int uploadedBy = reader["UploadedBy"] != DBNull.Value ? Convert.ToInt32(reader["UploadedBy"]) : 0;

                                if (uploadedBy == 0)
                                {
                                    MessageBox.Show("Invalid uploader for this note.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

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

        private void LoadUploaderDetails(int uploadedBy)
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
                                string gender = reader["Gender"].ToString();

                                if (gender == "Male")
                                {
                                    profilepicture_box.Image = Properties.Resources.Male;
                                }
                                else if (gender == "Female")
                                {
                                    profilepicture_box.Image = Properties.Resources.Female;
                                }
                                else
                                {
                                    profilepicture_box.Image = Properties.Resources.DefaultProfile;
                                }

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
                    string sql = "SELECT COUNT(*) FROM Notes WHERE UploadedBy = @UploadedBy AND Status = 'Approved'";

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

            
            string connectionString = @"data source=Mohaiminul\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI";

            bool isSilverUser = false;
            int userID = userId; 
            int noteID = noteId; 
            int downloadCount = 0;
            bool isDuplicateDownload = false;
            DateTime today = DateTime.Today;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string getUserRoleQuery = "SELECT Role FROM Users WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(getUserRoleQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    string role = (string)command.ExecuteScalar();
                    isSilverUser = role.Equals("Silver", StringComparison.OrdinalIgnoreCase);
                }

                if (isSilverUser)
                {
                    
                    string deleteOldLogsQuery = "DELETE FROM UserDailyActivity WHERE UserID = @UserID AND ActivityDate < @Today";
                    using (SqlCommand command = new SqlCommand(deleteOldLogsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Today", today);
                        command.ExecuteNonQuery();
                    }

                    
                    string checkDuplicateQuery = "SELECT COUNT(*) FROM UserDailyActivity WHERE UserID = @UserID AND NoteID = @NoteID AND ActivityDate = @Today";
                    using (SqlCommand command = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@NoteID", noteID);
                        command.Parameters.AddWithValue("@Today", today);
                        int duplicateCount = (int)command.ExecuteScalar();

                        if (duplicateCount > 0)
                        {
                            isDuplicateDownload = true;
                        }
                    }

                    
                    if (!isDuplicateDownload)
                    {
                        string getDownloadCountQuery = "SELECT COUNT(*) FROM UserDailyActivity WHERE UserID = @UserID AND ActivityDate = @Today";
                        using (SqlCommand command = new SqlCommand(getDownloadCountQuery, connection))
                        {
                            command.Parameters.AddWithValue("@UserID", userID);
                            command.Parameters.AddWithValue("@Today", today);
                            downloadCount = (int)command.ExecuteScalar();
                        }

                        
                        if (downloadCount >= 5)
                        {
                            MessageBox.Show("Your download limit for today is over. Please try again tomorrow or upgrade to Gold.", "Download Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
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

                    
                    if (isSilverUser && !isDuplicateDownload)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string logDownloadQuery = "INSERT INTO UserDailyActivity (UserID, NoteID, ActivityDate) VALUES (@UserID, @NoteID, @Today)";
                            using (SqlCommand command = new SqlCommand(logDownloadQuery, connection))
                            {
                                command.Parameters.AddWithValue("@UserID", userID);
                                command.Parameters.AddWithValue("@NoteID", noteID);
                                command.Parameters.AddWithValue("@Today", today);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
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
                RemoveBookmark();
            }
            else
            {
                AddBookmark();
            }
        }

        private void AddBookmark()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding bookmark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveBookmark()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "DELETE FROM Bookmarks WHERE UserID = @UserID AND NoteID = @NoteID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@NoteID", noteId);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        isBookmarked = false;
                        bookmarkimage.Image = Properties.Resources.bookmark_hollow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing bookmark: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotePreview_Load(object sender, EventArgs e)
        {
            
        }

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {

            Ratings_Review form13 = new Ratings_Review(userId, noteId);
            form13.StartPosition = FormStartPosition.CenterParent;
            form13.ShowDialog();
        }

        private void profilepicture_box_Click(object sender, EventArgs e)
        {
            
           
        }

    }
}
