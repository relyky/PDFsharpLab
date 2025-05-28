using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Signatures;
using PDFSharpLab_HelloWorld2;
using System.Security.Cryptography.X509Certificates;

static X509Certificate2 GetCertificate()
{
  DirectoryInfo certFolder = new DirectoryInfo(@"assets/pdfsharp-6.x/signatures"); // 取得字型資源路徑

  var pfxFile = Path.Combine(certFolder.FullName , "test-cert_rsa_1024.pfx");
  var rawData = File.ReadAllBytes(pfxFile);

  // This code is for demonstration only. Do not use password literals for real certificates in source code.
  var certificatePassword = "Seecrit1243";

  var certificate = new X509Certificate2(rawData,
      certificatePassword,
      X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

  return certificate;
}

// 註冊自訂字型解析器
GlobalFontSettings.FontResolver = new CustomFontResolver();
//GlobalFontSettings.FontResolver = new SamplesFontResolver();
GlobalFontSettings.UseWindowsFontsUnderWindows = true;

// 建立 PDF 文件
using PdfDocument document = new PdfDocument();
document.Info.Title = "PDFsharp 中文練習";
document.Info.Subject = "PDFsharp 中文練習主旨";

PdfPage page = document.AddPage();
XGraphics gfx = XGraphics.FromPdfPage(page);

// 使用註冊過的微軟正黑體字型
//XFont font = new XFont("標楷體", 20);
XFont font = new XFont("微軟正黑體", 20);

gfx.DrawString($"你好，PDFsharp at {DateTime.Now:HH:mm:ss} ！", font, XBrushes.Black,
    new XRect(0, 0, page.Width, page.Height),
    XStringFormats.Center);

XFont fontJhengHei = new XFont("微軟正黑體", 36);
gfx.DrawString($"這是微軟正黑體 36 PDFsharp at {DateTime.Now:HH:mm:ss} ！", fontJhengHei, XBrushes.Black, 10, 60);

XFont fontKaiu = new XFont("標楷體", 36);
gfx.DrawString($"這是標楷體 36 PDFsharp at {DateTime.Now:HH:mm:ss} ！", fontKaiu, XBrushes.Black, 10, 100);

XFont fontArial = new XFont("Arial", 36);
gfx.DrawString($"This is Arial font, 36 PDFsharp at {DateTime.Now:HH:mm:ss} ！", fontArial, XBrushes.Black, 10, 140);

//## 試著簽章
var signPosition = gfx.Transformer.WorldToDefaultPage(new XPoint(144, 600));
var pdfSignatureHandler = DigitalSignatureHandler.ForDocument(document,
    new PdfSharpDefaultSigner(GetCertificate(), PdfMessageDigestType.SHA256),
    new DigitalSignatureOptions
    {
      ContactInfo = "Sky-Walker Kao",
      Location = "新北市",
      Reason = "測試 PDF 簽章",
      Rectangle = new XRect(signPosition.X, signPosition.Y, 200, 50), // 簽章位置
      AppearanceHandler = new SignatureAppearanceHandler("高天賜\n新北市")
    });

// 儲存 PDF
string filename = "output.pdf";
document.Save(filename);
Console.WriteLine($"PDF 已儲存為 {filename}");


