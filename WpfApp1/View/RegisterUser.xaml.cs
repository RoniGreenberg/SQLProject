using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        public RegisterUser()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!AreAllValuesFilled()) //אם המשתמש לא מילא את אחד מהשדות או שת הסיסמות לא תואמות
            {
                MessageBox.Show("Please fill all the fields & make sure passwords are equal");
                return;
            }

            string email = txtEmail.Text;
            if (IsEmailExists(email)) //אם המייל כבר קיים בדאטה בייס
            {
                MessageBox.Show("Email already exists. Please use a different email.");
                return;
            }
            {
                if (!AreAllValuesFilled())
                {
                    MessageBox.Show("Please fill all the fields & make sure passwords are equal");
                    return;
                }
                else
                {
                    string firstName = txtFname.Text;
                    string lastName = txtLname.Text;
                    string password = txtPassword1.Text;

                   //חיבור לדאטה בייס USERS
                    string connectionString = @"Data Source=C:\Users\User\source\repos\SQL\mynewSql.db;Version=3;";


                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO users (email, firstName, lastName, password)" +
                            " VALUES (@Email, @FirstName, @LastName, @Password)";
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@FirstName", firstName);
                            command.Parameters.AddWithValue("@LastName", lastName);
                            command.Parameters.AddWithValue("@Password", password);
                             command.Parameters.AddWithValue("@IsAdmin", 0);

                            command.ExecuteNonQuery(); // ביצוע השאילתה
                         
                        }
                    }

                    MessageBox.Show("User registered successfully1!");
                    Login login = new Login();
                    login.Show();
                    this.Close();
                    
                }
            }
        }
            private bool AreAllValuesFilled()
            {
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtFname.Text)
                    || string.IsNullOrEmpty(txtLname.Text) || string.IsNullOrEmpty(txtPassword1.Text)
                    || string.IsNullOrEmpty(txtPassword2.Text) || txtPassword1.Text != txtPassword2.Text)
                {
                    return false;
                }
                return true;
            }
     
        //פעולה הבודקת אם המייל כבר קיים בדאטה בייס
        private bool IsEmailExists(string email)
        {
           string connectionString = @"Data Source=C:\Users\User\source\repos\SQL\mynewSql.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE email = @Email";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
 

