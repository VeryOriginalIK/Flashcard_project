using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;


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

        public void LoadCards()
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
