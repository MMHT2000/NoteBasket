﻿using System;
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
    public partial class Note_Manager : Form
    {
        private int userId;
        public Note_Manager(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadNotes();

            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            LoadNotes();
        }

        private void LoadNotes()
        {
            
            panel2.Controls.OfType<Control>()
                .Where(c => c != label2 && c != textBox1 && c != button2)
                .ToList()
                .ForEach(c => panel2.Controls.Remove(c));

            try
            {
                using (SqlConnection con = new SqlConnection("data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI"))
                {
                    string searchQuery = textBox1.Text.Trim();
                    string query = "SELECT NoteID, Title, Status FROM Notes";

                    
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE Title LIKE @SearchText";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            cmd.Parameters.AddWithValue("@SearchText", $"%{searchQuery}%");
                        }

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int y = 106;
                            while (reader.Read())
                            {
                                int noteId = Convert.ToInt32(reader["NoteID"]);
                                string title = reader["Title"].ToString();
                                string status = reader["Status"].ToString();

                                
                                Panel notePanel = new Panel
                                {
                                    Size = new Size(530, 25),
                                    Location = new Point(38, y),
                                    BackColor = Color.White,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                
                                Label titleLabel = new Label
                                {
                                    Text = title,
                                    Font = new Font("Arial", 9, FontStyle.Regular),
                                    AutoSize = true,
                                    Location = new Point(10, 5),
                                    ForeColor = Color.Black
                                };

                                
                                Label statusLabel = new Label
                                {
                                    Text = status,
                                    Font = new Font("Arial", 9, FontStyle.Regular),
                                    AutoSize = true,
                                    Location = new Point(470, 5),
                                    ForeColor = status == "Approved" ? Color.Green :
                                    status == "Rejected" ? Color.Red : Color.Goldenrod
                                };

                                
                                Label manageLabel = new Label
                                {
                                    Text = "Manage",
                                    Font = new Font("Arial", 9, FontStyle.Bold),
                                    AutoSize = true,
                                    Location = new Point(390, 5),
                                    ForeColor = Color.Red,
                                    Cursor = Cursors.Hand,
                                    Tag = noteId
                                };
                                manageLabel.Click += ManageLabel_Click;

                               
                                notePanel.Controls.Add(titleLabel);
                                notePanel.Controls.Add(statusLabel);
                                notePanel.Controls.Add(manageLabel);

                                
                                panel2.Controls.Add(notePanel);

                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManageLabel_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            int noteId = (int)clickedLabel.Tag;

            ManageNotes manageForm = new ManageNotes(userId, noteId);
            manageForm.StartPosition = FormStartPosition.CenterParent;
            manageForm.ShowDialog();

            LoadNotes();
        }

        private void RegisterLabel_Click(object sender, EventArgs e)
        {

        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard form5 = new Admin_Dashboard(userId);
            form5.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
