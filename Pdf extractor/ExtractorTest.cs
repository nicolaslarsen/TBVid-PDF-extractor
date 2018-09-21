using System;
using System.Text;
using System.Xml;

namespace Pdf_extractor
{
    class ExtractorTest
    {
        private static Extractor Ext;

        // Actually fails right now
        public static void Base64Test()
        {
            Ext = new Extractor("C:/Users/nr/Desktop/extractor/test.xml");
            foreach (XmlNode pdf in Ext.GetPdfs())
            {
                string decodeString = Encoding.Default.GetString(
                    Extractor.Base64Decode(pdf.InnerText));
                Console.WriteLine("Base64Test: " + 
                    (pdf.InnerText == Extractor.Base64Encode(decodeString)));
            }
        }

        public static void TestXml(){
            Extractor ext = new Extractor("C:/Users/nr/Desktop/extractor/test.xml");
        }

    }
}
