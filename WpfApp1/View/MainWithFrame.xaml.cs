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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for MainWithFrame.xaml
    /// </summary>
    public partial class MainWithFrame : Window
    {
        public MainWithFrame()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Login());
            Application.Current.Windows[0].Height = 530;
            Application.Current.Windows[0].Width = 500;
        }
        private void UsrTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (UsrTxtBox.Text == "Enter your email or username")
            //{
            //    UsrTxtBox.Text = string.Empty;
            //}
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            RegisterUser registerUserWindow = new RegisterUser();
            registerUserWindow.Show();
            this.Close();
        }

        private void logoutMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
