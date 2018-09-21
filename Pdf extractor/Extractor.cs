using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Pdf_extractor
{
    class Extractor
    {
        private XmlDocument doc;
        private string filename;
        private List<string> PdfNames;

        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Encoding.UTF8.GetString(plainTextBytes);
        }

        public static byte[] Base64Decode(string base64EncodedData) {
            return System.Convert.FromBase64String(base64EncodedData);
        } 

        private void InsertPdfNames()
        {
            PdfNames = new List<string> {
                "BBR_MEDDELELSE",
                "VURDERINGS_PRINT",
                "EJSKAT_PRINT2"
            };
        }

        public Extractor(string filename)
        {
            this.filename = filename ?? throw new ArgumentNullException(nameof(filename));
            doc = new XmlDocument();
            doc.Load(filename);
            InsertPdfNames();
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
            return name + "_" + TS + ".pdf";
        }
    }
}