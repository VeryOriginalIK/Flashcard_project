using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MemoryCards
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateForm : Window
    {
        public int cardCount { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public CreateMode cm { get; set; }
        public TextBox text { get; set; }
        public CreateForm(CreateMode cm, int cardCount)
        {
            this.InitializeComponent();
            this.cardCount = cardCount;
            this.cm = cm;
        }

        RadioButton[] radioButtons = new RadioButton[4];
        TextBox[] textBoxes = new TextBox[4];
        private void soltype_CBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            solution_GR.Children.Clear();
            if(soltype_CBX.SelectedIndex == 1)
            {
                int tbWidth = 315;
                for(int i = 0; i < 4; i++)
                {
                    RadioButton radioButton = new RadioButton
                    {
                        Content = "",
                        FontSize = 16,
                        Margin = new Thickness(10 + i * tbWidth, 0, 0, 0),
                        VerticalAlignment = VerticalAlignment.Center,
                        GroupName = "multiple"

                    };
                    TextBox option_TXB = new TextBox
                    {
                        PlaceholderText = $"{i + 1}. opció",
                        FontSize = 16,
                        Margin = new Thickness(40 + i * tbWidth, 0, 0, 0),
                        Width = tbWidth-40,
                        HorizontalAlignment = HorizontalAlignment.Left

                    };
                    radioButtons[i] = radioButton;
                    textBoxes[i] = option_TXB;
                    solution_GR.Children.Add(radioButton);
                    solution_GR.Children.Add(option_TXB);
                }
            }
            else if(soltype_CBX.SelectedIndex == 2)
            {
                RadioButton rbTrue = new RadioButton
                {
                    Content = "Igaz",
                    FontSize = 16,
                    Margin = new Thickness(10, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    GroupName = "binary"

                };
                RadioButton rbFalse = new RadioButton
                {
                    Content = "Hamis",
                    FontSize = 16,
                    Margin = new Thickness(80, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    GroupName = "binary"

                };
                radioButtons[0] = rbTrue;
                radioButtons[1] = rbFalse;
                solution_GR.Children.Add(rbTrue);
                solution_GR.Children.Add(rbFalse);
            }
            else
            {
                TextBox text = new TextBox
                {
                    PlaceholderText = "Megoldás",
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                solution_GR.Children.Add(text);
            }
        }

        private void add_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            string jsonPath = Path.Combine(AppContext.BaseDirectory, "cards.json");

            // Read the JSON file into a string
            string json = File.ReadAllText(jsonPath);
            string answer;
            if (soltype_CBX.SelectedIndex == 0)
            {
                answer = text.Text;
            }
            else if(soltype_CBX.SelectedIndex == 1)
            {
                for (int i = 0; i < radioButtons.Length; i++)
                {
                    if (radioButtons[i].IsChecked == true)
                    {
                        answer = textBoxes[i].Text;
                        break;
                    }
                }
            }
            else
            {
                answer = radioButtons[0].IsChecked == true ? "1" : "0";
            }
            Card card = new Card(this.cardCount, question_TXB.Text, DateTime.Now, "0", soltype_CBX.SelectedIndex , [textBoxes[0] != null ? textBoxes[0].Text : "", textBoxes[1] != null ? textBoxes[1].Text : "", textBoxes[2] != null ? textBoxes[2].Text : "", textBoxes[3] != null ? textBoxes[3].Text : ""]);
            Cards.Add(card);
            string updatedJson = JsonConvert.SerializeObject(Cards, Formatting.Indented);
            File.WriteAllText(jsonPath, updatedJson);
            System.Diagnostics.Debug.WriteLine(updatedJson);
            cm.LoadCards();
        }
    }
}
