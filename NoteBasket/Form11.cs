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
    public partial class Form11 : Form
    {
        private int userId;
        public Form11(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            Form12 f12 = new Form12(userId);

            f12.StartPosition = FormStartPosition.CenterParent;
            f12.ShowDialog();
        }
    }
}
