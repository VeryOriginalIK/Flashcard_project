using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Windows.System;

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
            foreach (string answer in SelectedCard.FakeAnswers)
            {
                answers.Add(answer);
            }
            answers.Add(SelectedCard.Answer);
            answers = answers.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (string answer in answers)
            {
                Button button = new Button();
                button.Content = answer;
                button.Click += AnswerButton_Click(object sender, RoutedEventArgs e, answer);
                BTNs_Place.Children.Add(button);
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e, string answer)
        {
            Button button = (Button)sender;
            string answer = button.Content.ToString();
            if (answer == sender.Answer)
            {
                SelectedCard.TimesRead++;
                SelectedCard.LastRead = DateTime.Now;
                button.Background = Brushes.Green;
                Cards[Cards.IndexOf(SelectedCard)] = SelectedCard;
                // Show correct message
            }
            else {

                SelectedCard.LastRead = DateTime.Now;
                SelectedCard.TimesRead++;
                button.Background = Brushes.Red;

            }
        }
    }
}