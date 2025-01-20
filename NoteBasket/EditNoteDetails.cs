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

            // Load the existing note details when the form loads
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
                                // Populate the fields with the existing note data
                                name_textbox.Text = reader.GetString(0); // Title
                                description_textbox.Text = reader.GetString(1); // Description

                                // Retrieve FilePath and handle potential null or empty value
                                string filePath = reader.IsDBNull(2) ? string.Empty : reader.GetString(2); // FilePath
                                label3.Text = string.IsNullOrEmpty(filePath) ? "No file path available" : filePath;

                                // Ensure label3 is visible
                                label3.Visible = true;

                                // Populate comboBoxes
                                comboBox1.SelectedItem = reader.GetString(3); // Category
                                comboBox2.SelectedItem = reader.GetString(4); // SubscriptionLevel
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
            // Open file dialog to select a new file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string selectedFilePath = openFileDialog1.FileName;

                // Define the application image directory
                string imagePath = Path.Combine(Application.StartupPath, "images");

                // Ensure the directory exists
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath); // Create the "images" folder if it doesn't exist
                }

                // Copy the selected file into the "images" directory
                string fileName = Path.GetFileName(selectedFilePath);
                filepath = Path.Combine("images", fileName); // Use a relative path
                string fullFilePath = Path.Combine(imagePath, fileName);

                File.Copy(selectedFilePath, fullFilePath, true); // Overwrite if file exists

                // Display the relative file path in the label and make it visible
                label3.Text = $"{filepath}"; // Update the label text
                label3.ForeColor = Color.Green; // Set label text color
                label3.Visible = true; // Make the label visible

                MessageBox.Show($"File successfully saved to: {fullFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data from input fields
                string title = name_textbox.Text.Trim();
                string description = description_textbox.Text.Trim();
                string category = comboBox1.Text.Trim();
                string subscription = comboBox2.Text.Trim();

                // Check if all fields are filled
                if (string.IsNullOrEmpty(title) ||
                    string.IsNullOrEmpty(description) ||
                    string.IsNullOrEmpty(category) ||
                    string.IsNullOrEmpty(subscription) ||
                    string.IsNullOrEmpty(filepath))
                {
                    MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Use a parameterized query to prevent SQL injection
                    string sql = "UPDATE Notes SET Title = @Title, Description = @Description, Filepath = @Filepath, " +
                                 "Category = @Category, SubscriptionLevel = @SubscriptionLevel " +
                                 "WHERE NoteID = @NoteID";  // Update the record where NoteID matches

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters with their values
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Filepath", filepath); // Use the relative filepath
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@SubscriptionLevel", subscription);
                        cmd.Parameters.AddWithValue("@NoteID", noteId); // Use the current noteId to find the record

                        // Open the connection
                        con.Open();

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Inform the user of success
                            MessageBox.Show("Note details updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Navigate to the relevant form (e.g., dashboard or another form)
                            this.Hide();
                            NoteMaster_Dashboard dashboard = new NoteMaster_Dashboard(userId); // Pass userId to maintain session
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
                // Handle exceptions and display an error message
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
                // Ask the user to confirm the deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this note?",
                                                      "Confirm Deletion", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Establish the connection to the database
                    using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                    {
                        // Use a parameterized query to prevent SQL injection
                        string sql = "DELETE FROM Notes WHERE NoteID = @NoteID";  // Delete the record where NoteID matches

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            // Add the NoteID parameter
                            cmd.Parameters.AddWithValue("@NoteID", noteId); // Use the current noteId to identify the note

                            // Open the connection
                            con.Open();

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Inform the user of success
                                MessageBox.Show("Note deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Navigate to the relevant form (e.g., dashboard or another form)
                                this.Hide();
                                NoteMaster_Dashboard dashboard = new NoteMaster_Dashboard(userId); // Pass userId to maintain session
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
                // Handle exceptions and display an error message
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
