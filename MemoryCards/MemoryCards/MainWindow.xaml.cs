using Microsoft.UI.Xaml;
using System;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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

        public ObservableCollection<User>? Users { get; set; } = new ObservableCollection<User>();
        public User SelectedUser { get; set; }

        private ObservableCollection<User> usersToShow;

        public ObservableCollection<User> UsersToShow
        {
            get { return usersToShow; }
            set { usersToShow = value; OnPropertyChanged(nameof(UsersToShow)); }
        }

        private void Study()
        {
            StudyMode studyMode = new StudyMode();
            studyMode.Show();
            this.Close();
        }

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void login_BTN_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog newUserDialog = new ContentDialog
            {
                Title = "A felhasználónév foglalt",
                Content = "Kérem, válasszon másikat!",
                PrimaryButtonText = "Tanulás",
                PrimaryButtonCommand = Study,
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot
            };
            _ = newUserDialog.ShowAsync();
        }
        private void Registration_BTN_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User(Usrnm.Text.ToString(), Pswrd.ToString());
            if (!Users.Any(x => x.name.ToLower() == Usrnm.Text.ToString().ToLower()))
            {
                Users.Add(newUser);
                login_BTN_Click(sender, e);
            }
            else
            {
                ContentDialog userTakenDialog = new ContentDialog
                {
                    Title = "A felhasználónév foglalt",
                    Content = "Kérem, válasszon másikat!",
                    CloseButtonText = "Ok",
                    XamlRoot = this.Content.XamlRoot
                };
                _ = userTakenDialog.ShowAsync();
            }
        }
    } 
    }