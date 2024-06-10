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
using System.Data.SQLite;
using WpfApp1.Model;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {

        private string connectionString = @"Data Source=C:\Users\User\source\repos\SQL\mynewSql.db";

        public AdminPage()
        {
            InitializeComponent();
        }

        private void ShowListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM USERS";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            string userList = "Users List:\n\n";
                            while (reader.Read())
                            {
                                string userID = reader["ID"].ToString();
                                string firstName = reader["FIRSTNAME"].ToString();
                                string lastName = reader["LASTNAME"].ToString();
                                string email = reader["EMAIL"].ToString();
                                string password = reader["PASSWORD"].ToString();

                                string userRecord = $"ID: {userID}, First Name: {firstName}, Last Name: " +
                                    $"{lastName}, Email: {email}, Password: {password}\n";

                                userList += userRecord;
                            }
                            MessageBox.Show(userList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReadUser readUser = new ReadUser();
            readUser.Show();
            this.Close();
        }


        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            string question = txtQuestion.Text;

            if (string.IsNullOrEmpty(question))
            {
                MessageBox.Show("Please enter a question.");
                return;
            }

            try
            {
                UserRepository userRepository = new UserRepository();
                bool isAdded = userRepository.AddQuestion(question);

                if (isAdded)
                {
                    MessageBox.Show("Question added successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to add question.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
  }

    
 


