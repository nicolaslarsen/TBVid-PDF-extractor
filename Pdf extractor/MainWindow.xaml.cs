using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pdf_extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Button> BtnDict;
        private const int BUTTON_HEIGHT = 50;
        private const int BUTTON_WIDTH  = 120;

        public MainWindow()
        {
            InitializeComponent();
            BtnDict = new Dictionary<string, Button>();
            ExtractorTest.TestXml();


            AddPdfButton("test");
            Button test = AddPdfButton("memes");
            AddPdfButton("third");
            CalcButtonMargins();
            foreach (Button btn in ButtonGrid.Children)
            {
                Console.WriteLine(btn.Content + ": " + btn.Margin);
            }
            test.Margin = new Thickness(0,0,0,-145);

        }

        // Makes room for a new button and returns the margin for this button
        private void CalcButtonMargins()
        {
            int numButtons  = ButtonGrid.Children.Count;

            if (numButtons <= 1)
            {
                return;
            }

            int buttonMargin = (int) ButtonGrid.Height / numButtons;
            int marginTop = 0;
            int marginBot = (int) (buttonMargin) - (BUTTON_HEIGHT / 2);
            
            foreach (Button child in ButtonGrid.Children)
            {
                child.Margin = new Thickness(0, marginTop, 0, marginBot);
                marginBot -= (int) (buttonMargin * 1.5);
            }
        }

        private Button AddPdfButton(string name)
        {
            Button PdfButton = new Button
            {
                Content = name,
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
            };

            ButtonGrid.Children.Add(PdfButton);

            return PdfButton;
        }
    }
}
