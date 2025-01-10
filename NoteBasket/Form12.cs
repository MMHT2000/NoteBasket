﻿using System;
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
    public partial class Form12 : Form
    {
        private int userId;
        public Form12(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void updateprofile_btn_Click(object sender, EventArgs e)
        {
            
            Form13 form13 = new Form13(userId);
            form13.StartPosition = FormStartPosition.CenterParent;
            form13.ShowDialog();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
