using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for ReadUser.xaml
    /// </summary>
    public partial class ReadUser : Window
    {
        //private string connectionString = @"Data Source=C:\Users\User\source\repos\WpfApp1\WpfApp1\mynewSql.db";

        public ReadUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string email = txtEmail.Text;

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please fill in email fields.");
                    return;
                }

                UserRepository userRepository = new UserRepository();
                User user = userRepository.GetUserByEmail(email);

                if (user != null)
                {
                    string userDetails = $"ID: {user.Id}\nEmail: {user.Email}\nFirst Name: " +
                        $"{user.FirstName}\nLast Name: {user.LastName}\nPassword: {user.Password}";
                    MessageBox.Show($"The email exists!\n\n{userDetails}");
                }
                else
                {
                    MessageBox.Show("The email does not exist in the list");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtEmail.Text;

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please fill in email fields.");
                    return;
                }

                UserRepository userRepository = new UserRepository();
                bool isDeleted = userRepository.DeleteUserByEmail(email);

                if (isDeleted)
                {
                    MessageBox.Show("The user has been deleted.");
                }
                else
                {
                    MessageBox.Show("The email does not exist in the list");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtEmail.Text;

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please fill in email fields.");
                    return;
                }

                UserRepository userRepository = new UserRepository();
                User user = userRepository.GetUserByEmail(email);

                if (user != null)
                {
                    UpdateUserByAdmin updateUserByAdmin = new UpdateUserByAdmin(user);
                    updateUserByAdmin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The email does not exist in the list");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

            
    }
  }