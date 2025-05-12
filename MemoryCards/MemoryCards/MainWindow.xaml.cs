using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace MemoryCards
{
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        /*Login page basically.
        A Study mode sorolja fel a k�rty�kat �s editel. Megkapja a loginolt user k�rty�it.
        Meg Regisztr�lni tud �j felhaszn�l�kat.
         */

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<User>? Users { get; set; } = new ObservableCollection<User>();
        public User SelectedUser { get; set; }

        private ObservableCollection<User> usersToShow;

        public ObservableCollection<User> UsersToShow
        {
            get { return usersToShow; }
            set { usersToShow = value; OnPropertyChanged(nameof(UsersToShow)); }
        }

        private void Study(List<int> cardIDs)
        {
            StudyMode studyMode = new StudyMode(GetCards(cardIDs));
            studyMode.Activate();
            this.Close();
        }

        private void Create()
        {
            CreateMode createMode = new CreateMode();
            createMode.Activate();
            this.Close();
        }

        private void ReadUsers()
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, "users.json");
                Users = JArray.Parse(File.ReadAllText(filePath)).ToObject<ObservableCollection<User>>();
            }
            catch
            {
                Users = new ObservableCollection<User>();
            }
        }

        public MainWindow()
        {
            this.InitializeComponent();
            ReadUsers();
        }

        private void login_BTN_Click(object sender, RoutedEventArgs e)
        {
            User? user = Users.FirstOrDefault(x => x.name.ToLower() == Username.Text.ToString().ToLower());

            if (user != null && user.Login(Password.Password) != null)
            {
                var result = MessageBox.Show(
                    "Mit szeretne csinálni?",
                    "Módrválasztó",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Create();
                }
                else if (result == MessageBoxResult.No)
                {
                    Study(user.cardIds);
                }
            }
            else
            {
                MessageBox.Show(
                    "A felhasználónév, vagy a jelszó nem egyezik!\nKérem, próbálja újra!",
                    "Helytelen jelszó",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private bool PasswordCheck()
        {
            bool good = true;
            if (Password.Password.Length < 5) good = false;
            if (!Password.Password.Any(char.IsDigit)) good = false;

            return good;
        }

        private bool UsernameCheck()
        {
            bool good = true;
            if (Username.Text.ToString().Length < 3) good = false;

            return good;
        }
        private void Registration_BTN_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User(Username.Text.ToString(), Password.Password);
            if (!Users.Any(x => x.name.ToLower() == Username.Text.ToString().ToLower()))
            {
                if (PasswordCheck())
                {
                    if (!UsernameCheck())
                    {
                        MessageBox.Show(
                            "A felhasználónév legalább 3 karakter hosszúnak kell lennie!",
                            "Hibás felhasználónév",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        Users.Add(newUser);
                        MessageBox.Show(
                            "Jelentkezz be!",
                            "Sikeres regisztráció!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "A jelszónak legalább 5 karakter hosszúnak kell lennie és tartalmaznia kell számot is!",
                        "Hibás jelszó",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(
                    "Kérem, válasszon másikat!",
                    "A felhasználónév foglalt",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private ObservableCollection<Card> GetCards(List<int> CardIDs)
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
                /*ContentDialog NoCards = new ContentDialog
                {
                    Title = "M�g nincsenek k�rty�k l�trehozva!",
                    Content = "Mit szeretn�l csin�lni?",
                    PrimaryButtonText = "�j k�rty�k hozz�ad�sa",
                    CloseButtonText = "Kil�p�s",
                };
                
                NoCards.PrimaryButtonClick += (sender, e) =>
                {

                    CreateMode createMode = new CreateMode();
                    createMode.Activate();
                    this.Close();
                };

                NoCards.CloseButtonClick += (sender, e) =>
                {

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Activate();
                    this.Close();
                };

                NoCards.XamlRoot = this.Content.XamlRoot;
                _ = NoCards.ShowAsync();*/

                return new ObservableCollection<Card>();
            }
        }

        public void SaveUsers(object sender, EventArgs e)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "users.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(Users));
        }


    }
}