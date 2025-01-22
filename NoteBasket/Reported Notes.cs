using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Reported_Notes : Form
    {
        private int userId;
        
        public Reported_Notes(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            
            LoadReportedNotes();
        }

        private void Reported_Notes_Load(object sender, EventArgs e)
        {

        }

        private void LoadReportedNotes()
        {
            try
            {
                // Clear all dynamic panels from panel5
                foreach (Control control in panel5.Controls.OfType<Panel>().ToList())
                {
                    panel5.Controls.Remove(control);
                }

                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = @"
    SELECT 
        r.ReportID, r.ReportDate, 
        n.Title, 
        u.Name, 
        r.NoteID
    FROM Reports r
    INNER JOIN Notes n ON r.NoteID = n.NoteID
    INNER JOIN Users u ON r.UserID = u.UserID
    ORDER BY r.ReportDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int yPosition = 10; // Initial Y position for dynamic panels

                            while (reader.Read())
                            {
                                int noteId = Convert.ToInt32(reader["NoteID"]);
                                string noteTitle = reader["Title"].ToString();
                                string reportedBy = reader["Name"].ToString();
                                DateTime reportDate = Convert.ToDateTime(reader["ReportDate"]);

                                // Create a dynamic panel
                                Panel dynamicPanel = new Panel
                                {
                                    Size = new Size(520, 80),
                                    Location = new Point(10, yPosition),
                                    BackColor = Color.LightBlue,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                // Create a label for the report details
                                Label detailsLabel = new Label
                                {
                                    Text = $"{noteTitle} was reported by {reportedBy} on {reportDate}",
                                    AutoSize = false,
                                    Size = new Size(300, 40),
                                    Location = new Point(10, 10),
                                    Font = new Font("Times New Roman", 10, FontStyle.Regular),
                                    ForeColor = Color.Black
                                };

                                // Create a "View" button
                                Button viewButton = new Button
                                {
                                    Text = "View",
                                    Size = new Size(80, 30),
                                    Location = new Point(320, 25),
                                    BackColor = Color.White,
                                    FlatStyle = FlatStyle.Popup
                                };

                                // Attach event to open NoteDetails form
                                viewButton.Click += (s, e) =>
                                {
                                    this.Close();
                                    NoteDetails noteDetailsForm = new NoteDetails(userId, noteId);
                                    noteDetailsForm.Show();
                                };

                                // Create a "Delete" button
                                Button deleteButton = new Button
                                {
                                    Text = "Delete",
                                    Size = new Size(80, 30),
                                    Location = new Point(410, 25),
                                    BackColor = Color.Red,
                                    FlatStyle = FlatStyle.Popup,
                                    ForeColor = Color.White
                                };

                                // Attach event to delete the note from the database
                                deleteButton.Click += (s, e) =>
                                {
                                    var confirmation = MessageBox.Show(
                                        "Are you sure you want to delete this note?",
                                        "Confirm Delete",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);

                                    if (confirmation == DialogResult.Yes)
                                    {
                                        try
                                        {
                                            using (SqlConnection deleteCon = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                                            {
                                                string deleteQuery = "DELETE FROM Notes WHERE NoteID = @NoteID";
                                                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, deleteCon))
                                                {
                                                    deleteCmd.Parameters.AddWithValue("@NoteID", noteId);
                                                    deleteCon.Open();
                                                    deleteCmd.ExecuteNonQuery();
                                                }
                                            }

                                            MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            LoadReportedNotes();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                };

                                
                                dynamicPanel.Controls.Add(detailsLabel);
                                dynamicPanel.Controls.Add(viewButton);
                                dynamicPanel.Controls.Add(deleteButton);

                                // Add the panel to panel5
                                panel5.Controls.Add(dynamicPanel);

                                yPosition += 90; // Adjust Y position for the next panel
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reported notes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Admin_Dashboard adminDashboard = new Admin_Dashboard(userId);
            adminDashboard.Show();
        }
    }
}
