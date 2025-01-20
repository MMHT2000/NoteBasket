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
    public partial class Bkash_Payment : Form
    {
        private int userId;
        private int amount;
        public Bkash_Payment(int userId, int amount)
        {
            InitializeComponent();
            this.userId = userId;
            this.amount = amount;

            label2.Text = $"BDT {amount}";
            textBox1.TextAlign = HorizontalAlignment.Center;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            string phoneNumber = textBox1.Text;

            // Check if the phone number is exactly 11 digits and starts with valid prefixes
            if (phoneNumber.Length == 11 &&
                (phoneNumber.StartsWith("013") || phoneNumber.StartsWith("014") || phoneNumber.StartsWith("015") ||
                 phoneNumber.StartsWith("016") || phoneNumber.StartsWith("017") || phoneNumber.StartsWith("018") ||
                 phoneNumber.StartsWith("019")))
            {
                // If valid, open the Payment_Confirmation form
                this.Close();
                Payment_Confirmation paymentConfirmation = new Payment_Confirmation(userId, amount);
                paymentConfirmation.StartPosition = FormStartPosition.CenterParent;
                paymentConfirmation.ShowDialog();
            }
            else
            {
                // If invalid, show an error message
                MessageBox.Show("Invalid phone number. Please enter a valid Bangladeshi phone number.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.bkash.com/en/page/terms-and-conditions";
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Ensures the URL opens in the default browser
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open link. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
