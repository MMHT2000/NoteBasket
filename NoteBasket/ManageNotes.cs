using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class ManageNotes : Form
    {
        private int userId;
        private int noteId;

        public ManageNotes(int userId, int noteId)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteId;

            LoadNoteDetails();
        }

        private void ManageNotes_Load(object sender, EventArgs e)
        {
        }

        private void LoadNoteDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = "SELECT Title, Category, FilePath, UploadedBy, Status FROM Notes WHERE NoteID = @NoteID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label2.Text = reader["Title"].ToString(); 
                                label4.Text = reader["Category"].ToString();

                                
                                string categoryName = reader["Category"].ToString();
                                pictureBox2.Image = GetCategoryImage(categoryName);

                                
                                string filePath = reader["FilePath"].ToString();
                                pictureBox3.Image = GetNoteImage(filePath);

                                
                                int uploadedBy = Convert.ToInt32(reader["UploadedBy"]);
                                label6.Text = GetUploaderName(uploadedBy);

                                
                                string status = reader["Status"].ToString();
                                if (status == "Pending")
                                {
                                    label9.Visible = false;  
                                    label7.Visible = true; 
                                    label10.Visible = false;
                                }
                                else if (status == "Approved")
                                {
                                    label9.Visible = true; 
                                    label7.Visible = false; 
                                    label10.Visible = false;
                                }
                                else
                                {
                                    label9.Visible = false; 
                                    label7.Visible = true; 
                                    label8.Visible = false;
                                    label10.Visible = true;
                                }
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

        private Image GetCategoryImage(string categoryName)
        {
            
            try
            {
                return (Image)Properties.Resources.ResourceManager.GetObject(categoryName) ?? Properties.Resources.NF;
            }
            catch
            {
                return Properties.Resources.NF;
            }
        }

        private Image GetNoteImage(string filePath)
        {
          
            try
            {
                if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                {
                    return Image.FromFile(filePath);
                }
                else
                {
                    return Properties.Resources.NF;
                }
            }
            catch
            {
                return Properties.Resources.NF;
            }
        }

        private string GetUploaderName(int uploadedBy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = "SELECT Username FROM Users WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", uploadedBy);
                        con.Open();

                        return cmd.ExecuteScalar()?.ToString() ?? "Unknown";
                    }
                }
            }
            catch
            {
                return "Unknown";
            }
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            try
            {
                var confirmation = MessageBox.Show(
                    "Are you sure you want to approve this note?",
                    "Confirm Approval",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        string query = "UPDATE Notes SET Status = 'Approved' WHERE NoteID = @NoteID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@NoteID", noteId);
                            con.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Note approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show(
    "Are you sure you want to reject this note?",
    "Confirm Rejection",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        string query = "UPDATE Notes SET Status = 'Rejected' WHERE NoteID = @NoteID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@NoteID", noteId);
                            con.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Note rejected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmation = MessageBox.Show(
                    "Are you sure you want to change the status of this note to Pending?",
                    "Confirm Status Change",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        string query = "UPDATE Notes SET Status = 'Pending' WHERE NoteID = @NoteID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@NoteID", noteId);
                            con.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Note status changed to Pending successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                   
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing note status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you sure you want to delete this note?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        string query = "DELETE FROM Notes WHERE NoteID = @NoteID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@NoteID", noteId);
                            con.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
