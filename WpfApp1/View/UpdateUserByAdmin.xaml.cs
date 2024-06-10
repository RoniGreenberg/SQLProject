using System;
using System.Collections.Generic;
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
    /// Interaction logic for UpdateUserByAdmin.xaml
    /// </summary>
    public partial class UpdateUserByAdmin : Window
    {
        private User _user;

        public UpdateUserByAdmin(User user)
        {
            InitializeComponent();
            _user = user;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InformationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                string userDetails = $"ID: {_user.Id}\nEmail: {_user.Email}\nFirst Name: {_user.FirstName}\nLast Name: {_user.LastName}\nPassword: {_user.Password}";
                MessageBox.Show(userDetails, "User Details");
            }
            else
            {
                MessageBox.Show("User details not available.");
            }
        }

     

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
        
                try
                {
                    string email = txtEmail.Text;
                    string firstName = txtFname.Text;
                    string lastName = txtLname.Text;
                    string password = txtPassword1.Text;

                    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Please fill in all fields.");
                        return;
                    }

                    UserRepository userRepository = new UserRepository();
                    bool isUpdated = userRepository.UpdateUserByEmail(email, firstName, lastName, password);

                    if (isUpdated)
                    {
                        MessageBox.Show("User details updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user details.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }

        }
    }

