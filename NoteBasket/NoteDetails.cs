using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class NoteDetails : Form
    {
        private int userId;
        private int noteId;
        public NoteDetails(int userId, int noteID)
        {
            InitializeComponent();
            this.userId = userId;
            this.noteId = noteID;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            NotePreview f12 = new NotePreview(userId, noteId);

            f12.StartPosition = FormStartPosition.CenterParent;
            f12.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            User_Dashboard form3 = new User_Dashboard(userId);
            form3.Show();
        }
    }
}
