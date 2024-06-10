using System;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace WpfApp1.Model
{
    public class UserRepository
    {
        private string connectionString = @"Data Source=C:\Users\User\source\repos\SQL\mynewSql.db;";
        //private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\SQL\mynewSql.mdf;Integrated Security=True";
        //private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\Users\User\source\repos\SQL\mynewSql.mdf;";



        public User GetUserByEmailAndPassword(string email, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

         public User GetUserByEmail(string email)
        {
            User user = null;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM USERS WHERE EMAIL = @Email";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    Id = reader["ID"].ToString(), 
                                    Email = reader["EMAIL"].ToString(),
                                    FirstName = reader["FIRSTNAME"].ToString(),
                                    LastName = reader["LASTNAME"].ToString(),
                                    Password = reader["PASSWORD"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while getting user by email: " + ex.Message);
                }
            }
            return user;
        }
        public bool DeleteUserByEmail(string email)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM USERS WHERE EMAIL = @Email";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while deleting user by email: " + ex.Message);
                }
            }
        }
        public bool UpdateUserByEmail(string email, string firstName, string lastName, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Password = @Password WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while updating user by email: " + ex.Message);
                }
            }
        }

        public bool AddQuestion(string question)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Questions (QuestionText) VALUES (@QuestionText)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@QuestionText", question);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while adding question: " + ex.Message);
                }
            }
        }

    }
}
