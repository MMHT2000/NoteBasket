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
    public partial class Note_Approval : Form
    {
        private int userId;
        public Note_Approval(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard form5 = new Admin_Dashboard(userId);
            form5.Show();
        }
    }
}
