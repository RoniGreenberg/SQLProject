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
using System.Data.SqlClient;
using System.Data.SQLite;

namespace WpfApp1.View
{
    public partial class MainPage : Window
    {
        private string connectionString = @"Data Source=C:\Users\User\source\repos\WpfApp1\WpfApp1\mynewSql.db;Initial Catalog=YourDatabaseName;Integrated Security=True";

        private SQLiteConnection connection;
        private SQLiteCommand command;
        private SQLiteDataReader reader;
        private string currentQuestion;
        private string[] possibleAnswers;
        private List<int> askedQuestionIds = new List<int>(); // רשימה של שאלות שנשאלו
        private int totalQuestions = 9; // מספר השאלות המקסימלי במשחק
        private int currentQuestionIndex = 0;
        private int score = 1; // ציון המשתמש

        public MainPage()
        {
            InitializeComponent();
            LoadQuestion();
            PlayBackgroundMusic();

        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= totalQuestions)
            {
                MessageBox.Show($"Game over! Your score is {score}/10");

                this.Close();
                return;
            }

            try
            {
                using (connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // שאילתת SQL לבחירת השאלה הבאה שלא נשאלה עדיין
                    string query = @"
                        SELECT QuestionID, QuestionText 
                        FROM Questions 
                        WHERE QuestionID NOT IN (" + string.Join(",", askedQuestionIds) + @") 
                        LIMIT 1";

                    command = new SQLiteCommand(query, connection);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int questionId = reader.GetInt32(0);
                        currentQuestion = reader.GetString(1);
                        askedQuestionIds.Add(questionId); // הוספת מספר השאלה לרשימה של שאלות שנשאלו

                        QuestionTextBlock.Content = currentQuestion;

                        // בחירת תשובות אפשריות לשאלה הנוכחית
                        query = "SELECT AnswerText FROM Answers WHERE QuestionID = @QuestionID ORDER BY AnswerID";
                        command = new SQLiteCommand(query, connection);
                        command.Parameters.AddWithValue("@QuestionID", questionId);
                        reader = command.ExecuteReader();

                        possibleAnswers = new string[4];
                        int i = 0;
                        while (reader.Read() && i < 4)
                        {
                            possibleAnswers[i] = reader["AnswerText"].ToString();
                            i++;
                        }
                        // ערבוב תשובות אפשריות
                        ShuffleArray(possibleAnswers);

                        // הצגת תשובות אפשריות בתיבות סימון רדיו
                        AnswerRadioButton1.Content = possibleAnswers[0];
                        AnswerRadioButton2.Content = possibleAnswers[1];
                        AnswerRadioButton3.Content = possibleAnswers[2];
                        AnswerRadioButton4.Content = possibleAnswers[3];

                        // איפוס תיבות הסימון
                        AnswerRadioButton1.IsChecked = false;
                        AnswerRadioButton2.IsChecked = false;
                        AnswerRadioButton3.IsChecked = false;
                        AnswerRadioButton4.IsChecked = false;

                        currentQuestionIndex++;
                    }
                    else
                    {
                        MessageBox.Show("No more questions available.");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        
    }
        private void ShuffleArray(string[] array)
        {
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the selected answer is correct
                    string selectedAnswer = null;
                    if (AnswerRadioButton1.IsChecked == true)
                    {
                        selectedAnswer = AnswerRadioButton1.Content.ToString();
                    }
                    else if (AnswerRadioButton2.IsChecked == true)
                    {
                        selectedAnswer = AnswerRadioButton2.Content.ToString();
                    }
                    else if (AnswerRadioButton3.IsChecked == true)
                    {
                        selectedAnswer = AnswerRadioButton3.Content.ToString();
                    }
                    else if (AnswerRadioButton4.IsChecked == true)
                    {
                        selectedAnswer = AnswerRadioButton4.Content.ToString();
                    }

                    if (selectedAnswer != null)
                    {
                        string query = "SELECT IsCorrect FROM Answers WHERE AnswerText = @AnswerText AND QuestionID" +
                            " = (SELECT QuestionID FROM Questions WHERE QuestionText = @QuestionText)";
                        command = new SQLiteCommand(query, connection);
                        command.Parameters.AddWithValue("@AnswerText", selectedAnswer);
                        command.Parameters.AddWithValue("@QuestionText", currentQuestion);

                        var result = command.ExecuteScalar();
                        bool isCorrect = result != null && Convert.ToInt32(result) == 1;

                        if (isCorrect)
                        {
                            CorrectAnswerImage.Visibility = Visibility.Visible; // הצגת תמונת תשובה נכונה
                            WrongAnswerImage.Visibility = Visibility.Collapsed; // הסתרת תמונה של טעות

                            MessageBox.Show("Correct!");
                            score++;
                        }
                        else
                        {
                            CorrectAnswerImage.Visibility = Visibility.Collapsed; // הסתרת תמונת תשובה נכונה 
                            WrongAnswerImage.Visibility = Visibility.Visible; // הצגת תמונה של טעות

                            MessageBox.Show("Wrong!");
                        }

                        // Load a new question
                        LoadQuestion();
                    }
                    else
                    {
                        MessageBox.Show("Please select an answer.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void PlayBackgroundMusic() //הפעלת השיר
        {
            SongMediaElement.Play();
        }
    }   
}
