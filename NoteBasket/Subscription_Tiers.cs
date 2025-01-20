using System;
using System.Windows.Forms;

namespace NoteBasket
{
    public partial class Subscription_Tiers : Form
    {
        private int userId;
        private int amount;

        public Subscription_Tiers(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void changepassword_btn_Click(object sender, EventArgs e)
        {
            amount = 99;
            OpenBkashPaymentForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            amount = 49;
            OpenBkashPaymentForm();
        }

        private void OpenBkashPaymentForm()
        {
            // Pass userId and amount to the Bkash_Payment form
            Bkash_Payment bkash_Payment = new Bkash_Payment(userId, amount);
            bkash_Payment.StartPosition = FormStartPosition.CenterParent;
            bkash_Payment.ShowDialog();

        }

        private void returntodashboard_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            User_Dashboard user_Dashboard = new User_Dashboard(userId);
            user_Dashboard.Show();
        }
    }
}
