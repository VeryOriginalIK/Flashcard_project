using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MemoryCards
{
    public sealed partial class StudyMode : Window, INotifyPropertyChanged
    {

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
        }

    }
}