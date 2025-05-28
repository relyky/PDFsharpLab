using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PDFSharpLab_HelloWorld2;

// 註冊自訂字型解析器
GlobalFontSettings.FontResolver = new CustomFontResolver();
//GlobalFontSettings.FontResolver = new SamplesFontResolver();
GlobalFontSettings.UseWindowsFontsUnderWindows = true;

// 建立 PDF 文件
using PdfDocument document = new PdfDocument();
document.Info.Title = "PDFsharp 中文練習";

PdfPage page = document.AddPage();
XGraphics gfx = XGraphics.FromPdfPage(page);

// 使用註冊過的微軟正黑體字型
//XFont font = new XFont("標楷體", 20);
XFont font = new XFont("微軟正黑體", 20);

gfx.DrawString($"你好，PDFsharp at {DateTime.Now:HH:mm:ss} ！", font, XBrushes.Black,
    new XRect(0, 0, page.Width, page.Height),
    XStringFormats.Center);

XFont fontJhengHei = new XFont("微軟正黑體", 36);
gfx.DrawString($"這是微軟正黑體 36 PDFsharp at {DateTime.Now:HH:mm:ss} ！", fontJhengHei, XBrushes.Black, 10, 120);

XFont fontKaiu = new XFont("標楷體", 36);
gfx.DrawString($"這是標楷體 36 PDFsharp at {DateTime.Now:HH:mm:ss} ！", fontKaiu, XBrushes.Black, 10, 160);

// 儲存 PDF
string filename = "output.pdf";
document.Save(filename);
Console.WriteLine($"PDF 已儲存為 {filename}");
