using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Pdf_extractor
{
    class Extractor
    {
        private XmlDocument doc;
        private string filename;
        private List<string> PdfNames;

        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        } 

        private void InsertPdfNames()
        {
            PdfNames = new List<string> {
                "BBR_MEDDELELSE",
                "VURDERINGS_PRINT"
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


            return ""; 
        }
    }
}
