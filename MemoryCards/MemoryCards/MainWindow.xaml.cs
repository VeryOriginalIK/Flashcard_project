using Microsoft.UI.Xaml;
using System;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.Security.Cryptography.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;

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
                ContentDialog loginUserDialog = new ContentDialog
                {
                    Title = "M�dv�laszt�",
                    Content = "Mit szeretne csin�lni?",
                    PrimaryButtonText = "K�rty�k szerkeszt�se",
                    SecondaryButtonText = "Tanul�s",
                    XamlRoot = this.Content.XamlRoot
                };

                loginUserDialog.PrimaryButtonClick += (sender, e) => {Create(); };
                loginUserDialog.SecondaryButtonClick += (sender, e) => {Study(user.cardIds);};

                _ = loginUserDialog.ShowAsync();
            }
            else
            {
                ContentDialog WrongPasswordDialog = new ContentDialog
                {
                    Title = "Helytelen jelsz�",
                    Content = "A felhaszn�l�n�v, vagy a jelsz� nem egyezik!\nK�rem, pr�b�lja �jra!",
                    CloseButtonText = "Ok",
                };
                _ = WrongPasswordDialog.ShowAsync();
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
                        ContentDialog usernameDialog = new ContentDialog
                        {
                            Title = "Hib�s felhaszn�l�n�v",
                            Content = $"A felhaszn�l�n�v legal�bb 3 karakter hossz�nak kell lennie!",
                            CloseButtonText = "Ok",
                            XamlRoot = this.Content.XamlRoot
                        };
                        _ = usernameDialog.ShowAsync();
                    }
                    else
                    {
                        Users.Add(newUser);
                        ContentDialog registered = new ContentDialog
                        {
                            Title = "Sikeres regisztr�ci�!",
                            Content = $"Jelentkezz be!",
                            CloseButtonText = "Ok",
                            XamlRoot = this.Content.XamlRoot
                        };
                        _ = registered.ShowAsync();
                    }
                }
                else
                {
                    ContentDialog passwordDialog = new ContentDialog
                    {
                        Title = "Hib�s jelsz�",
                        Content = $"A jelsz�nak legal�bb 5 karakter hossz�nak kell lennie �s tartalmaznia kell sz�mot is!",
                        CloseButtonText = "Ok",
                        XamlRoot = this.Content.XamlRoot
                    };
                    _ = passwordDialog.ShowAsync();
                }
            }
            else
            {
                ContentDialog userTakenDialog = new ContentDialog
                {
                    Title = "A felhaszn�l�n�v foglalt",
                    Content = "K�rem, v�lasszon m�sikat!",
                    CloseButtonText = "Ok",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = userTakenDialog.ShowAsync();
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

        public void SaveUsers(object sender, WindowEventArgs e)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "users.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(Users));
        }


    }
}