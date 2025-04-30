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
using System.Diagnostics;
using System.Windows;
using System.Reflection.Emit;


namespace MemoryCards
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateMode : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateMode()
        {
            this.InitializeComponent(); // This must come first
            LoadCards();
            // Then show another window
        }

        public void LoadCards()
        {
            int cardCount = JsonConvert.DeserializeObject<CardList>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "cards.json"))).Cards.Count;
            if (cardCount == 0)
            {
                TextBlock tb = new TextBlock
                {
                    Text = "Nincsenek kártyáid. A létrehozott kártyáid késõbb itt fognak megjelenni.",
                    FontSize = 16,
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                content_SP.Children.Add(tb);
                
            }
            for (int i = 0; i < cardCount; i++)
            {
                TextBlock tb = new TextBlock
                {
                    Text = "Hello from code!",
                    FontSize = 16,
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }

        }

        private void EnterForm(object sender, RoutedEventArgs e)
        {
            // Your code to handle the button click
            CreateForm createForm = new CreateForm(this);
            createForm.Activate();
        }
    }
}
