using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf.Annotations;
using PdfSharp.Quality;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSharpLab_HelloWorld2;

class SignatureAppearanceHandler : IAnnotationAppearanceHandler
{
  private string _signatureText = "簽名";

  public SignatureAppearanceHandler(string signatureText)
  {
    _signatureText = signatureText;
  }

  public void DrawAppearance(XGraphics gfx, XRect rect)
  {
    DirectoryInfo imageFolder = new DirectoryInfo(@"assets/pdfsharp-6.x/signatures"); // 取得字型資源路徑

    var image = XImage.FromFile(Path.Combine(imageFolder.FullName, "JohnDoe.png"));

    //string text = "John Doe\nSeattle, " + DateTime.Now.ToString(CultureInfo.GetCultureInfo("EN-US"));
    string text = $"{_signatureText}, {DateTime.Now:F}";

    //var font = new XFont("Verdana", 7.0, XFontStyleEx.Regular);
    var font = new XFont("標楷體", 8.0, XFontStyleEx.Regular);
    var textFormatter = new XTextFormatter(gfx);
    double num = (double)image.PixelWidth / image.PixelHeight;
    double signatureHeight = rect.Height * .4;
    var point = new XPoint(rect.Width / 10, rect.Height / 10);

    // Draw image.
    gfx.DrawImage(image, point.X, point.Y, signatureHeight * num, signatureHeight);
    // Adjust position for text. We draw it below image.
    point = new XPoint(point.X, rect.Height / 2d);

    //textFormatter.DrawString(text, font, new XSolidBrush(XColor.FromKnownColor(XKnownColor.Black)), new XRect(point.X, point.Y, rect.Width, rect.Height - point.Y), XStringFormats.TopLeft);
    textFormatter.DrawString(text, font, XBrushes.Black, new XRect(point.X, point.Y, rect.Width, rect.Height - point.Y), XStringFormats.TopLeft);
  }
}