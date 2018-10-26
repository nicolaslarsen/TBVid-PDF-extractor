using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Pdf_extractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BUTTON_HEIGHT = 50;
        private const int BUTTON_WIDTH  = 150;

        public MainWindow()
        {
            InitializeComponent();
            
            ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            ButtonGrid.RowDefinitions.Add(new RowDefinition());
        }

        private List<int> CheckAndAddSpace()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < ButtonGrid.ColumnDefinitions.Count; i++)
            {
                int btnPerRow = (int) ButtonGrid.Height / BUTTON_HEIGHT;

                for (int j = 0; j < ButtonGrid.RowDefinitions.Count; j++)
                {
                    if (ButtonGrid.RowDefinitions.Count != btnPerRow)
                    {
                        ButtonGrid.RowDefinitions.Add(new RowDefinition());
                        list.Add(0);
                        list.Add(j);
                    }
                    else if (ButtonGrid.Children.Count < btnPerRow * i + j + 1)
                    {
                        list.Add(i);
                        list.Add(j);
                    }
                    else if (j+1 == btnPerRow && i+1 == ButtonGrid.ColumnDefinitions.Count)
                    { 
                        ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        list.Add(i+1);
                        list.Add(0);
                    }
                }
            }
            return list;
        }

        private void CreateAndOpenPdf(XmlNode pdf)
        {
            byte[] pdfBytes = Extractor.Base64Decode(pdf.InnerText);
            string filename = Extractor.CreateFilename(pdf.Name);
            File.WriteAllBytes(filename, pdfBytes);
            Process.Start(filename);
        }

        private Button AddPdfButton(XmlNode pdf)
        {
            Button PdfButton = new Button
            {
                Name = pdf.Name,
                Content = pdf.Name,
                Height = BUTTON_HEIGHT,
                Width = BUTTON_WIDTH,
                FontSize = 14
            };

            PdfButton.Click += (s, e) => 
            {
                CreateAndOpenPdf(pdf);
            };

            List<int> colRow = CheckAndAddSpace();
            ButtonGrid.Children.Add(PdfButton);
            Grid.SetColumn(PdfButton, colRow[0]);
            Grid.SetRow(PdfButton, colRow[1]);

            return PdfButton;
        }

        private void InputSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            if (opf.ShowDialog() == true)
            {
                InputFile.Text = opf.FileName;
                Extractor ext;
                try
                {
                    ext = new Extractor(InputFile.Text);
                }
                catch (XmlException)
                {
                    MessageBox.Show("Selected file is not an XML file",
                                    "Not XML file",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }
                ButtonGrid.Children.Clear();
                
                if (ext.HasPdfNames())
                {
                    List<XmlNode> pdfs = ext.GetPdfs();

                    if (pdfs.Count < 1)
                    {
                        MessageBox.Show("No PDF's found for this file, " +
                                        "perhaps the XML tag needs to be added to the config file",
                                        "No PDF found",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                    foreach (XmlNode pdf in pdfs)
                    {
                        Button pdfButton = AddPdfButton(pdf);
                    }
                }
            }
        }
    }
}
