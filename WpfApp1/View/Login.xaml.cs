using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp1.Model;



namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

      
        private bool AuthenticateUser(string username, string password)
        {
            UserRepository userRepository = new UserRepository();

            User user = userRepository.GetUserByEmailAndPassword(username, password);

            return user != null;
        }

   

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTextBox.Text;
                string password = PasswordBox.Password;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please fill in both email and password fields.");
                    return;
                }
                if (email.Equals("admin") && password.Equals("123"))
                {
                    AdminPage adminPage = new AdminPage();
                    adminPage.Show();
                    this.Close();
                }
                else
                {
                    UserRepository userRepository = new UserRepository();
                    User user = userRepository.GetUserByEmailAndPassword(email, password);

                    if (user != null)
                    {
                        MessageBox.Show("The user has successfully logged in. Welcome to the Trivia Game!");
                        MainPage mainPage = new MainPage();
                        mainPage.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("The username or password is incorrect. " +
                            "Please correct them to log in to the game.");
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    

        private void Button_NewUser(object sender, RoutedEventArgs e)
        {
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
            this.Close();
        }

       
    }
}
