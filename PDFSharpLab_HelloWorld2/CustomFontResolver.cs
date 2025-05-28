using PdfSharp.Fonts;
using System.Drawing.Text;

namespace PDFSharpLab_HelloWorld2;


public class CustomFontResolver : IFontResolver
{
  public byte[]? GetFont(string faceName)
  {
    string fontFileName = faceName;

    //DirectoryInfo fontsFolder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Fonts));
    DirectoryInfo sampleFolder = new DirectoryInfo(@"D:\Temp\PDFSharpLab_HelloWorld2\assets\pdfsharp-6.x\fonts"); // 取得字型資料夾路徑
    foreach (var fontsFolder in sampleFolder.GetDirectories())
    {
      foreach (FileInfo file in fontsFolder.GetFiles("*.ttf"))
      {
        if(file.Name == fontFileName)
        {
          return File.ReadAllBytes(file.FullName);
        }
      }
    }

    throw new FileNotFoundException($"字型檔案 {fontFileName} 未找到！");
  }

  public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
  {
    //DirectoryInfo fontsFolder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Fonts)); // 取得字型資料夾路徑
    DirectoryInfo sampleFolder = new DirectoryInfo(@"D:\Temp\PDFSharpLab_HelloWorld2\assets\pdfsharp-6.x\fonts"); // 取得字型資料夾路徑

    //※ PDFsharp 只支援 TrueType 字型(.ttf)。
    foreach (var fontsFolder in sampleFolder.GetDirectories())
    {
      foreach (FileInfo file in fontsFolder.GetFiles("*.ttf"))
      {
        var pfc = new PrivateFontCollection();
        pfc.AddFontFile(file.FullName);
        if (pfc.Families.Length > 0)
        {
          foreach (var family in pfc.Families)
          {
            if (family.Name == familyName)
            {
              return new FontResolverInfo(file.Name, bold, italic); // 此處 faceName 等於字型檔案名稱。
            }
          }
        }
      }
    }

    // 回傳 null 表示不支援該字型。
    return null;
  }
}