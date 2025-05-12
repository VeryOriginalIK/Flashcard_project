using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MemoryCards
{
    public sealed partial class StudyMode : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Card>? Cards { get; set; }
        public Card SelectedCard { get; set; }

        public StudyMode(ObservableCollection<Card> cards)
        {
            this.InitializeComponent();
            Cards = cards;
            GenerateButtons();
        }

        private void GenerateButtons()
        {
            List<string> answers = new List<string>();
            foreach (string answer in SelectedCard.options)
            {
                answers.Add(answer);
            }
            answers = answers.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (string answer in answers)
            {
                Button button = new Button();
                button.Content = answer;
                button.Click += (s, e) => AnswerButton_Click(s, e, answer);
                BTNs_Place.Children.Add(button);
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e, string answer)
        {
            Button button = (Button)sender;
            string choice = button.Content.ToString();
            if (choice == SelectedCard.answer)
            {
                SelectedCard.timesRead++;
                SelectedCard.lastRead = DateTime.Now;
                button.Background = Brushes.Green;
                Cards[Cards.IndexOf(SelectedCard)] = SelectedCard;
                MessageBox.Show("Helyes válasz!", "Answer", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SelectedCard.lastRead = DateTime.Now;
                SelectedCard.timesRead++;

            }
        }
    }
}