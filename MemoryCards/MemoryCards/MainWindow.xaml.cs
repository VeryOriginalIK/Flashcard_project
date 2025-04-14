using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MemoryCards
{
    public sealed partial class MainWindow : Window
    {
        public ObservableCollection<Card>? Cards { get; set; } = new ObservableCollection<Card>();
        public Card selectedCard { get; set;}

        private ObservableCollection<Card> cardsToShow;

        public ObservableCollection<Card> CardsToShow
        {
            get { return cardsToShow; }
            set { cardsToShow = value; OnPropertyChanged(nameof(CardsToShow)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void login_BTN_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Registration_BTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
