using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private Question currentQuestion;
        private List<Answer> possibleAnswers;
        private int selectedAnswerIndex;

        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                if (currentQuestion != value)
                {
                    currentQuestion = value;
                    OnPropertyChanged("CurrentQuestion");
                }
            }
        }

        public List<Answer> PossibleAnswers
        {
            get { return possibleAnswers; }
            set
            {
                if (possibleAnswers != value)
                {
                    possibleAnswers = value;
                    OnPropertyChanged("PossibleAnswers");
                }
            }
        }

        public int SelectedAnswerIndex
        {
            get { return selectedAnswerIndex; }
            set
            {
                if (selectedAnswerIndex != value)
                {
                    selectedAnswerIndex = value;
                    OnPropertyChanged("SelectedAnswerIndex");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
