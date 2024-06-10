using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            get { return questions; }
            set
            {
                questions = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Questions)));
            }
        }

        private string connectionString = @"Data Source=C:\Users\User\source\repos\WpfApp1\QUESTIONS.sql;Version=3;";

        //string connectionString = @"Data Source=C:\Users\User\source\repos\פרויקט 5 יחידות\MVVMWithAdd\myfirstsql.db;Version=3;";
        //זה מה שגיא שם

        public UserViewModel()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            Questions = new ObservableCollection<Question>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM QUESTIONS";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Question question = new Question
                    {
                        QuestionID = (int)reader["QuestionID"],
                        QuestionText = (string)reader["QuestionText"]
                    };

                    Questions.Add(question);
                }

                reader.Close();
            }
        }

        public void CheckAnswer(Question question, string userAnswer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT COUNT(*) FROM ANSWERS WHERE QuestionID = {question.QuestionID} AND AnswerText = '{userAnswer}' AND IsCorrect = 1";
                SqlCommand command = new SqlCommand(query, connection);
                int correctAnswerCount = (int)command.ExecuteScalar();

                if (correctAnswerCount > 0)
                {
                    MessageBox.Show("Correct answer!");
                }
                else
                {
                    MessageBox.Show("Wrong answer!");
                }
            }
        }
    }

    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
    }
}
