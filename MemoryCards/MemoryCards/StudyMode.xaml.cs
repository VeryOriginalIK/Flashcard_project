using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.System;

namespace MemoryCards
{
    public sealed partial class StudyMode : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Card> Cards { get; set; }
        private List<int> CardIDs { get; set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Card>? Card { get; set; } = new ObservableCollection<Card>();
        public Card SelectedCard { get; set; }

        private ObservableCollection<Card> cardsToShow;

        public ObservableCollection<Card> CardsToShow
        {
            get { return cardsToShow; }
            set { cardsToShow = value; OnPropertyChanged(nameof(CardsToShow)); }
        }

        public StudyMode(List<int> cardIDs)
        {
            this.InitializeComponent();
            CardIDs = cardIDs;
            this.Cards = GetCards();
        }

        private ObservableCollection<Card> GetCards()
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, "cards.json");
                var Cards = JArray.Parse(File.ReadAllText(filePath))
                                     .ToObject<List<Card>>()
                                     .Where(card => CardIDs.Contains(card.id))
                                     .ToList();
                return new ObservableCollection<Card>(Cards);
            }
            catch
            {
                ContentDialog NoCards = new ContentDialog
                {
                    Title = "Még nincsenek kártyák létrehozva!",
                    Content = "No cards found for the selected user.",
                    PrimaryButtonText = "Új kártyák hozzáadása",
                    CloseButtonText = "Kilépés",
                };

               /* NoCards.PrimaryButtonClick += (sender, e) =>
                {
                    Edi mainWindow = new MainWindow();
                    EditMode.Activate();
                    this.Close();
                };*/
                NoCards.CloseButtonClick += (sender, e) =>
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Activate();
                    this.Close();
                };
                _ = NoCards.ShowAsync();

                return new ObservableCollection<Card>();
            }
        }
    }
}