using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        }
    }
}