using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.SqlClient;
using System.Drawing;
using System.IO; // Required for Path and File operations

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Form10 : Form
    {

        private int userId;
        private string filepath; // To store the relative file path

        public Form10(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                Title = "Select an Image",
                CheckFileExists = true,
                CheckPathExists = true
            };

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
                label2.Text = $"{filepath}"; // Update the label text
                label2.ForeColor = Color.Green; // Set label text color
                label2.Visible = true; // Make the label visible

                MessageBox.Show($"File successfully saved to: {fullFilePath}");
            }
            else
            {
                // If no file is selected, hide the label
                label2.Visible = false;
            }
        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4(userId);
            form4.Show();
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
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }

                // Establish the connection to the database
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Use a parameterized query to prevent SQL injection
                    string sql = "INSERT INTO Notes (Title, Description, Filepath, Category, UploadedBy, UploadDate, SubscriptionLevel, Status) " +
                                 "VALUES (@Title, @Description, @Filepath, @Category, @UploadedBy, @UploadDate, @SubscriptionLevel, @Status)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters with their values
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Filepath", filepath); // Use the relative filepath
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@UploadedBy", userId); // Pass the userId of the logged-in user
                        cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now); // Use current date and time
                        cmd.Parameters.AddWithValue("@SubscriptionLevel", subscription);
                        cmd.Parameters.AddWithValue("@Status", "Pending");


                        // Open the connection
                        con.Open();

                        // Execute the query
                        cmd.ExecuteNonQuery();

                        // Inform the user of success
                        MessageBox.Show("Details inserted successfully");

                        // Navigate to the relevant form (e.g., dashboard or another form)
                        this.Hide();
                        Form4 dashboard = new Form4(userId); // Pass userId to maintain session
                        dashboard.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
    }
}
