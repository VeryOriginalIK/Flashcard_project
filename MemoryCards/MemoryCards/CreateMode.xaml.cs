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
        private ObservableCollection<Card> Cards;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreateMode()
        {
            this.InitializeComponent();
            LoadCards();
        }

        private void LoadCards()
        {
            try
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, "cards.json");
                Cards = JArray.Parse(File.ReadAllText(filePath)).ToObject<ObservableCollection<Card>>();
            }
            catch
            {
                Cards = new ObservableCollection<Card>();
            }
        }

        private void SaveCards()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "cards.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(Cards));
        }

        private void EnterForm(object sender, RoutedEventArgs e)
        {
            CreateForm createForm = new CreateForm(this);
            createForm.Activate();
        }
    }
}
