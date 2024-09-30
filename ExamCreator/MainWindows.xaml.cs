using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace EXAMCREATOR
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadPdf_Click(object sender, RoutedEventArgs e)
        {
            // PDF yükleme diyaloğu aç
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                // PDF dosyasını yükle ve işle
                string pdfFilePath = openFileDialog.FileName;
                ProcessPdf(pdfFilePath); // PDF'yi işlemek için fonksiyonu çağır
            }
        }

        private void ProcessPdf(string filePath)
        {
            try
            {
                PdfDocumentModel pdfDocument = new PdfDocumentModel
                {
                    FilePath = filePath
                };

                string pythonScriptPath = "\\Users\\mert\\Documents\\ExamCreator\\Scripts\\pdf_processor.py"; // Python betiği yolu
                string pythonExePath = "\\opt\\homebrew\\bin\\python3"; // Python yürütülebilir dosyası yolu

                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = pythonExePath;
                start.Arguments = $"\"{pythonScriptPath}\" \"{filePath}\""; // Betiği çalıştır
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = true;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        // PDF'den elde edilen görüntü yollarını ekle
                        pdfDocument.ImagePaths.Add(result.Trim().Split(':')[1].Trim());

                        // Ekrana görüntüyü yükle
                        PdfImage.Source = new BitmapImage(new Uri(pdfDocument.ImagePaths[0], UriKind.Absolute));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
    }
}