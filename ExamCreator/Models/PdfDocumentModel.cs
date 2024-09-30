using System.Collections.Generic;

namespace EXAMCREATOR
{
    public class PdfDocumentModel
    {
        public string FilePath { get; set; } // PDF dosyasının yolu
        public List<string> ImagePaths { get; set; } // PDF'den elde edilen görüntü yolları

        public PdfDocumentModel()
        {
            ImagePaths = new List<string>(); // Görüntü yollarını tutmak için listeyi başlat
        }

        // PDF ile ilgili diğer özellikler ve metotlar eklenebilir
    }
}