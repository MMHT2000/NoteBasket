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
            using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
            {
                // Corrected query with proper column names
                string sql = $"select count(*) from Bookmarks where USERID = {userId} and NoteID ={noteId}";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    int x = (int)cmd.ExecuteScalar();
                    if(x == 1)
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

            using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
            {
                // Use parameterized query to prevent syntax errors and SQL injection
                string sql = "SELECT Role FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);



                    con.Open();
                    

                    string role = (string)cmd.ExecuteScalar();

                    if (role == "Free")
                    {
                        backtosignin_btn.Visible = false;
                    }
                    else
                    {
                        backtosignin_btn.Visible = true;
                    }


                }
            }
        }
       

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            
            Ratings_Review form13 = new Ratings_Review(userId, noteId);
            form13.StartPosition = FormStartPosition.CenterParent;
            form13.ShowDialog();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(isBookmarked)
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Corrected query with proper column names
                    string sql = $"DELETE FROM Bookmarks WHERE UserID = {userId} AND NoteID = {noteId}";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        isBookmarked = false;
                        bookmarkimage.Image = Properties.Resources.bookmark_hollow;
                        
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    // Use parameterized query to prevent syntax errors and SQL injection
                    string sql = "INSERT INTO Bookmarks (UserID, NoteID, BookmarkDate) VALUES (@UserID, @NoteID, @BookmarkDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Add parameters with their respective values
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@NoteID", noteId);
                        cmd.Parameters.AddWithValue("@BookmarkDate", DateTime.Now);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Update UI and logic
                        isBookmarked = true;
                        bookmarkimage.Image = Properties.Resources.bookmark_filled;
                    }
                }

            }


        }



        private void profilepicture_box_Click(object sender, EventArgs e)
        {
            
           
        }

        private void backtosignin_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
