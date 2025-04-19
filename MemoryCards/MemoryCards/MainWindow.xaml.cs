using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MemoryCards
{
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        /*Login page basically.
        A Study mode sorolja fel a kártyákat és editel. Megkapja a loginolt user kártyáit.
        Meg Regisztrálni tud új felhasználókat.
         */

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Card>? Users { get; set; } = new ObservableCollection<Card>();
        public Card SelectedUser { get; set; }

        private ObservableCollection<Card> usersToShow;

        public ObservableCollection<Card> UsersToShow
        {
            get { return usersToShow; }
            set { usersToShow = value; OnPropertyChanged(nameof(UsersToShow)); }
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