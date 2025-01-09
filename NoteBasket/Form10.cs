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
    
    public partial class Form10 : Form
    {
        private int userId;
        public Form10(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                Title = "Select an Image",
                CheckFileExists = true,
                CheckPathExists = true
            };
            if
            (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                MessageBox.Show($"You Selected:{openFileDialog1.FileName}");
            }


        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4(userId);
            form4.Show();
        }
    }
}
