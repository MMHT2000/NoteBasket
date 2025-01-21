using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.SqlClient;
using System.Drawing;
using System.IO;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class UploadNote : Form
    {

        private int userId;
        private string filepath;

        public UploadNote(int userId)
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
                Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png",
                Title = "Select an Image",
                CheckFileExists = true,
                CheckPathExists = true
            };

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

                
                label2.Text = $"{filepath}"; 
                label2.ForeColor = Color.Green; 
                label2.Visible = true; 

                MessageBox.Show($"File successfully saved to: {fullFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                
                label2.Visible = false;
            }
        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            NoteMaster_Dashboard form4 = new NoteMaster_Dashboard(userId);
            form4.Show();
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
                    
                    string sql = "INSERT INTO Notes (Title, Description, Filepath, Category, UploadedBy, UploadDate, SubscriptionLevel, Status) " +
                                 "VALUES (@Title, @Description, @Filepath, @Category, @UploadedBy, @UploadDate, @SubscriptionLevel, @Status)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Filepath", filepath); 
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@UploadedBy", userId); 
                        cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@SubscriptionLevel", subscription);
                        cmd.Parameters.AddWithValue("@Status", "Pending");


                       
                        con.Open();

                        
                        cmd.ExecuteNonQuery();

                       
                        MessageBox.Show("Details inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      
                        this.Hide();
                        NoteMaster_Dashboard dashboard = new NoteMaster_Dashboard(userId);
                        dashboard.Show();
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
