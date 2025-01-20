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
    public partial class Payment_Confirmation : Form
    {
        private int userId;
        private int amount;
        public Payment_Confirmation(int userId, int amount)
        {
            InitializeComponent();
            this.userId = userId;
            this.amount = amount;

            label2.Text = $"BDT {amount}";
            textBox1.Text = amount.ToString();
            textBox1.TextAlign = HorizontalAlignment.Center;

        }

        private void label5_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int amount))
            {
                // Establish a SQL connection
                string connectionString = "data source=Mohaiminul\\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create the SQL query
                    string updateQuery = @"
                DECLARE @CurrentRole NVARCHAR(50);
                SELECT @CurrentRole = [Role] FROM [NoteBasketDB].[dbo].[Users] WHERE [UserID] = @UserID;

                -- Prevent downgrades for Gold users
                IF @CurrentRole = 'Gold' AND @Amount = 49
                BEGIN
                    RAISERROR('Gold users cannot downgrade to Silver.', 16, 1);
                    RETURN;
                END

                -- Update user role and subscription dates
                UPDATE [NoteBasketDB].[dbo].[Users]
                SET 
                    [Role] = CASE 
                        WHEN @Amount = 49 AND @CurrentRole != 'Silver' THEN 'Silver'
                        WHEN @Amount = 99 AND @CurrentRole != 'Gold' THEN 'Gold'
                        ELSE [Role]
                    END,
                    [SubscriptionStartDate] = CASE 
                        WHEN [SubscriptionStartDate] IS NULL THEN @Today
                        ELSE [SubscriptionStartDate]
                    END,
                    [SubscriptionEndDate] = CASE 
                        WHEN [SubscriptionEndDate] IS NULL THEN DATEADD(DAY, 30, @Today)
                        ELSE DATEADD(DAY, 30, [SubscriptionEndDate])
                    END
                WHERE [UserID] = @UserID;

                SELECT @CurrentRole AS PreviousRole;
            ";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Set parameters
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@Today", DateTime.Now);
                        command.Parameters.AddWithValue("@UserID", userId); // Use the passed userId

                        // Execute the command and retrieve the previous role
                        try
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string previousRole = reader["PreviousRole"].ToString();
                                    string newRole = amount == 49 ? "Silver" : "Gold";

                                    // Determine the appropriate message
                                    if (previousRole == newRole)
                                    {
                                        MessageBox.Show($"Payment Successful! Your {newRole} subscription has been extended by 30 days.");
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Payment Successful! You are upgraded to {newRole}.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Payment failed. Please try again.");
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            // Handle the exception if the RAISERROR triggers
                            MessageBox.Show(ex.Message); 
                        }
                    }
                }
            }
            else
            {
                // Invalid numeric input
                MessageBox.Show("Please enter a valid numeric value in the amount field.");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
