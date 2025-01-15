using System;
using System.Data.SqlClient;
using System.Drawing;
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

            // Load the note details when the form loads
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
                    string query = "SELECT Title, Category, UploadedBy FROM Notes WHERE NoteID = @NoteID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the labels with note details
                                label2.Text = reader["Title"].ToString(); // Note Title
                                label4.Text = reader["Category"].ToString(); // Category
                                int uploadedBy = Convert.ToInt32(reader["UploadedBy"]);

                                // Fetch uploader's username
                                label6.Text = GetUploaderName(uploadedBy);
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
            // Approve the note
            try
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

                // Close this form and refresh the previous form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            // Delete the note
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

                    // Close this form and refresh the previous form
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }
    }
}
