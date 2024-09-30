import fitz  # PyMuPDF

def extract_first_page(pdf_path):
    # PDF dosyasını aç
    doc = fitz.open(pdf_path)
    
    # İlk sayfayı al
    page = doc.load_page(0)  # Sayfa numarası 0'dan başlar

    # Sayfadan ekran görüntüsü al
    pix = page.get_pixmap()
    
    # Görüntüyü PNG olarak kaydet
    output_path = "output.png"  # Çıktı olarak kaydedilecek görüntü
    pix.save(output_path)
    
    return output_path

# Komut satırından çağırıldığında
if __name__ == "__main__":
    import sys
    pdf_path = sys.argv[1]  # Komut satırından PDF dosyası yolu al
    output_image = extract_first_page(pdf_path)
    print(f"Görüntü kaydedildi: {output_image}")