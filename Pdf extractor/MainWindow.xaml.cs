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

        public MainWindow()
        {
            InitializeComponent();
            BtnDict = new Dictionary<string, Button>();
            ExtractorTest.TestXml();
            AddPdfButton("test", 20);
            AddPdfButton("memes", 80);
            foreach (var memes in ButtonGrid.Children)
            {
                Console.WriteLine(memes);
            }
        }

        private void AddPdfButton(string name, int margin)
        {
            Button PdfButton = new Button
            {
                Content = name,
                Height = 50,
                Width = 120,
                Margin = new Thickness(0, margin, 0, 0)
            };

            ButtonGrid.Children.Add(PdfButton);
        }
    }
}
