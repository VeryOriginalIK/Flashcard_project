using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
        public CreateMode cm { get; set; }
        public CreateForm(CreateMode cm)
        {
            this.InitializeComponent();
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
                        VerticalAlignment = VerticalAlignment.Center

                    };
                    TextBox option_TXB = new TextBox
                    {
                        PlaceholderText = $"{i + 1}. opció",
                        FontSize = 16,
                        Margin = new Thickness(40 + i * tbWidth, 0, 0, 0),
                        Width = tbWidth-40,
                        HorizontalAlignment = HorizontalAlignment.Left

                    };
                    solution_GR.Children.Add(radioButton);
                    solution_GR.Children.Add(option_TXB);

                }
            }
            else if(soltype_CBX.SelectedIndex == 2)
            {
                RadioButton rbTrue = new RadioButton
                {
                    Content = "Igaz",
                    Margin = new Thickness(10, 0, 0, 0)
                };
                RadioButton rbFalse = new RadioButton
                {
                    Content = "Hamis",
                    Margin = new Thickness(80, 0, 0, 0)
                };
                solution_GR.Children.Add(rbTrue);
                solution_GR.Children.Add(rbFalse);
            }
            else
            {
                TextBox text = new TextBox
                {
                    PlaceholderText = "Megoldás",
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                solution_GR.Children.Add (text);
            }
        }

        private void showInfo(string text)
        {
            TextBlock info_TB = new TextBlock
            {
                Text = text,
                FontSize = 16,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            solution_GR.Children.Add(info_TB);
        }

        private void add_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            cm.LoadCards();
        }
    }
}
