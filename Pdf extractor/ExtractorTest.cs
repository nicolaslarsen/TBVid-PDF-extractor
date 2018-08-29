namespace Pdf_extractor
{
    class ExtractorTest
    { 
        public static void TestXml(){
            Extractor ext = new Extractor("C:/Users/nr/Desktop/extractor/test.xml");
            ext.GetTag("test");
        }
    }
}
