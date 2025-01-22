using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class NoteDetails : Form
    {
        private int userId;
        private int noteId;
        private string role;

        public NoteDetails(int userId, int noteId)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteId;
            LoadNoteDetails();
            RoleCheck();
        }

        private void RoleCheck()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = "SELECT Role FROM Users WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId); 

                        con.Open();

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            string fetchedRole = result.ToString();

                            
                            if (fetchedRole == "Admin")
                            {
                                button1.Visible = false;
                                button2.Visible = true;
                            }
                            else
                            {
                                button1.Visible = true;
                                button2.Visible = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Role not found for the user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking the role: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNoteDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string sql = @"
                        SELECT Title, Description, FilePath, Category, SubscriptionLevel 
                        FROM Notes 
                        WHERE NoteID = @NoteID";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NoteID", noteId);

                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                label1.Text = reader["Title"].ToString();
                                label2.Text = reader["Description"].ToString();
                                label4.Text = reader["Category"].ToString();
                                label5.Text = reader["SubscriptionLevel"].ToString();

                                string category = reader["Category"].ToString();
                                switch (category)
                                {
                                    case "CSE":
                                        pictureBox2.Image = Properties.Resources.CSE;
                                        break;
                                    case "EEE":
                                        pictureBox2.Image = Properties.Resources.EEE;
                                        break;
                                    case "IPE":
                                        pictureBox2.Image = Properties.Resources.IPE;
                                        break;
                                    case "BBA":
                                        pictureBox2.Image = Properties.Resources.BBA;
                                        break;
                                    case "Economics":
                                        pictureBox2.Image = Properties.Resources.ECO;
                                        break;
                                    case "MMC":
                                        pictureBox2.Image = Properties.Resources.MMC;
                                        break;
                                    case "LAW":
                                        pictureBox2.Image = Properties.Resources.LAW;
                                        break;
                                    case "ENGLISH":
                                        pictureBox2.Image = Properties.Resources.ENGLISH;
                                        break;
                                    case "PHARMACY":
                                        pictureBox2.Image = Properties.Resources.PHARMA;
                                        break;
                                    case "ARCHITECTURE":
                                        pictureBox2.Image = Properties.Resources.ARCH;
                                        break;
                                    case "CNCS":
                                        pictureBox2.Image = Properties.Resources.CNCS;
                                        break;
                                    case "DS":
                                        pictureBox2.Image = Properties.Resources.DS;
                                        break;
                                    default:
                                        pictureBox2.Image = Properties.Resources.NF;
                                        break;
                                }

                                string filePath = reader["FilePath"].ToString();
                                string imagePath = Path.Combine(Application.StartupPath, filePath);
                                if (File.Exists(imagePath))
                                {
                                    pictureBox1.Image = Image.FromFile(imagePath);
                                }
                                else
                                {
                                    pictureBox1.Image = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Note details not found. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }

                    
                    string ratingQuery = "SELECT AVG(CAST(Rating AS FLOAT)) FROM Ratings WHERE NoteID = @NoteID";
                    using (SqlCommand ratingCmd = new SqlCommand(ratingQuery, con))
                    {
                        ratingCmd.Parameters.AddWithValue("@NoteID", noteId);

                        
                        object avgRatingObj = ratingCmd.ExecuteScalar();

                        if (avgRatingObj != DBNull.Value)
                        {
                            double avgRating = Convert.ToDouble(avgRatingObj);
                            label8.Text = $"Rating: {avgRating:F2} Stars";
                        }
                        else
                        {
                            label8.Text = "Rating: N/A";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading note details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
                    
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NotePreview f12 = new NotePreview(userId, noteId);
            f12.StartPosition = FormStartPosition.CenterParent;
            f12.ShowDialog();
        }

        private void GetRole(int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string query = "SELECT Role FROM Users WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        con.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            role = result.ToString(); 
                        }
                        else
                        {
                            MessageBox.Show("Role not found for the specified user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            role = "Unknown"; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving the role: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                role = "Error"; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            GetRole(userId);

            if (role == "Free" || role == "Silver" || role == "Gold")
            {
                this.Close();
                User_Dashboard f12 = new User_Dashboard(userId);
                f12.Show();
            }
            else if (role == "Admin")
            {
                this.Close();
                Admin_Dashboard f12 = new Admin_Dashboard(userId);
                f12.Show();
            }
            else if (role == "NoteMaster")
            {
                this.Close();
                NoteMaster_Dashboard f12 = new NoteMaster_Dashboard(userId);
                f12.Show();
            }
            else
            {
                MessageBox.Show("Role not found. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void NoteDetails_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Reported_Notes f12 = new Reported_Notes(userId);
            f12.Show();
        }

        
    }
}
