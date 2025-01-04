using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace NoteBasket
{
    public partial class Form3 : Form
    {
        private int loggedInUserId;
        private string name;
        private string username;
        private string email;
        private string dob;
        private string gender;



        public Form3(int userId, string name, string username, string email, string dob, string gender, string role, int subscriptions, int loyaltyPoints, DateTime accountCreationDate)
        {
            InitializeComponent();
            this.loggedInUserId = userId;
            this.dob = dob;

            // Set the labels with the passed data
            name_label.Text = name;
            username_label.Text = "@" + username;
            emaildynamic_label.Text = email;
            dobdynamic_label.Text = dob ?? "Not Available";
            genderdynamiclabel.Text = gender;
            roledynamic_label.Text = role;

            // Convert subscriptions and loyalty points to string before assigning to labels
            subscriptionsdynamic_label.Text = subscriptions.ToString();  // subscriptions is an int
            loyaltydynamic_label.Text = loyaltyPoints.ToString();       // loyaltyPoints is an int

            accountcreationdynamic_label.Text = accountCreationDate.ToString("yyyy-MM-dd");

            // Set the profile picture based on gender
            if (gender == "Male")
            {
                profilepicture_box.ImageLocation = @"F:\Project Files\NoteBasket\images\Iconarchive-Incognito-Animals-Giraffe-Avatar.128.png";
            }
            else if (gender == "Female")
            {
                profilepicture_box.ImageLocation = @"F:\Project Files\NoteBasket\images\Hopstarter-Superhero-Avatar-Avengers-Giant-Man.128.png";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form8 editProfileForm = new Form8(loggedInUserId, name_label.Text, username_label.Text.TrimStart('@'), emaildynamic_label.Text, dob, genderdynamiclabel.Text);
           
            editProfileForm.Show();
        }
    }
}
