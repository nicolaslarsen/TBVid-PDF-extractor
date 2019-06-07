using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;

namespace Pdf_extractor
{
    class Extractor
    {
        private XmlDocument doc;
        private string filename;
        private string[] PdfNames;
        private static string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Encoding.UTF8.GetString(plainTextBytes);
        }

        public static byte[] Base64Decode(string base64EncodedData) {
            return System.Convert.FromBase64String(base64EncodedData);
        } 

        private void InsertPdfNames()
        {
            PdfNames = File.ReadAllLines(path + "/config.cfg");
            for (int i = 0; i < PdfNames.Length; i++)
            {
                PdfNames[i] = PdfNames[i].Trim();
            }
        }

        public bool HasPdfNames()
        {
            try
            {
                InsertPdfNames();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Could not find a config.cfg file in the directory, make sure it exists",
                                "No config file found",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public Extractor(string filename)
        {
            this.filename = filename ?? throw new ArgumentNullException(nameof(filename));
            doc = new XmlDocument();
            var xmlString = File.ReadAllText(filename);
            doc.LoadXml(xmlString);
        }

        public List<XmlNode> GetPdfs()
        {
            XmlNode root            = doc.DocumentElement;
            XmlNodeList children    = root.ChildNodes;

            // list of node names 
            List<string> names      = children.Cast<XmlNode>()
                                       .Select(node => node.Name).ToList();

            // list of nodes contained in the PdfNames list
            return children.Cast<XmlNode>().Where(
                             node => PdfNames.Contains(node.Name))
                             .ToList();
        }

        public string GetTag(string tagName)
        {
            XmlNode root = doc.DocumentElement;

            List<XmlNode> memes = GetPdfs();

            return "Not implemented"; 
        }

        public static string CreateFilename(string name)
        {
            long TS = DateTime.Now.Ticks;
            return path + "/" + name + "_" + TS + ".pdf";
        }
    }
}