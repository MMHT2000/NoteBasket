using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NoteBasket
{
    public partial class EditNoteDetails : Form
    {
        private int userId;
        private int noteId;
        private string filepath;

        public EditNoteDetails(int userId, int noteId)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteId;

            
            LoadNoteDetails();
        }

        private void LoadNoteDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = "SELECT Title, Description, FilePath, Category, SubscriptionLevel FROM Notes WHERE NoteID = @NoteID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                name_textbox.Text = reader.GetString(0); 
                                description_textbox.Text = reader.GetString(1);

                                
                                string filePath = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                label3.Text = string.IsNullOrEmpty(filePath) ? "No file path available" : filePath;

                                
                                label3.Visible = true;


                                comboBox1.SelectedItem = reader.GetString(3);
                                comboBox2.SelectedItem = reader.GetString(4);
                            }
                            else
                            {
                                MessageBox.Show("Note not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                string selectedFilePath = openFileDialog1.FileName;

                
                string imagePath = Path.Combine(Application.StartupPath, "images");

                
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                
                string fileName = Path.GetFileName(selectedFilePath);
                filepath = Path.Combine("images", fileName);
                string fullFilePath = Path.Combine(imagePath, fileName);

                File.Copy(selectedFilePath, fullFilePath, true);

                
                label3.Text = $"{filepath}";
                label3.ForeColor = Color.Green; 
                label3.Visible = true;

                MessageBox.Show($"File successfully saved to: {fullFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {
            try
            {
                
                string title = name_textbox.Text.Trim();
                string description = description_textbox.Text.Trim();
                string category = comboBox1.Text.Trim();
                string subscription = comboBox2.Text.Trim();

               
                if (string.IsNullOrEmpty(title) ||
                    string.IsNullOrEmpty(description) ||
                    string.IsNullOrEmpty(category) ||
                    string.IsNullOrEmpty(subscription) ||
                    string.IsNullOrEmpty(filepath))
                {
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    
                    string sql = "UPDATE Notes SET Title = @Title, Description = @Description, Filepath = @Filepath, " +
                                 "Category = @Category, SubscriptionLevel = @SubscriptionLevel " +
                                 "WHERE NoteID = @NoteID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                       
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Filepath", filepath); 
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@SubscriptionLevel", subscription);
                        cmd.Parameters.AddWithValue("@NoteID", noteId); 

                        
                        con.Open();

                        
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            
                            MessageBox.Show("Note details updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                            this.Hide();
                            NoteMaster_Dashboard dashboard = new NoteMaster_Dashboard(userId);
                            dashboard.Show();
                        }
                        else
                        {
                            MessageBox.Show("No record was updated. Please check the NoteID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            NoteMaster_Dashboard form4 = new NoteMaster_Dashboard(userId);
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                DialogResult result = MessageBox.Show("Are you sure you want to delete this note?",
                                                      "Confirm Deletion", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        
                        string sql = "DELETE FROM Notes WHERE NoteID = @NoteID";  

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            
                            cmd.Parameters.AddWithValue("@NoteID", noteId); 

                            
                            con.Open();

                            
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                
                                MessageBox.Show("Note deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                
                                this.Hide();
                                NoteMaster_Dashboard dashboard = new NoteMaster_Dashboard(userId); 
                                dashboard.Show();
                            }
                            else
                            {
                                MessageBox.Show("No record was deleted. Please check the NoteID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
