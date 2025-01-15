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

        public NoteDetails(int userId, int noteId)
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

                    // Calculate the average rating
                    string ratingQuery = "SELECT AVG(CAST(Rating AS FLOAT)) FROM Ratings WHERE NoteID = @NoteID";
                    using (SqlCommand ratingCmd = new SqlCommand(ratingQuery, con))
                    {
                        ratingCmd.Parameters.AddWithValue("@NoteID", noteId);

                        // No need to open the connection again, as it's already open
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            User_Dashboard form3 = new User_Dashboard(userId);
            form3.Show();
        }

        private void NoteDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
