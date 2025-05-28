using PdfSharp.Fonts;
using System.Drawing.Text;

namespace PDFSharpLab_HelloWorld2;


public class CustomFontResolver : IFontResolver
{
  //DirectoryInfo fontsFolder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Fonts)); // 取得字型資料夾路徑
  readonly DirectoryInfo _fontsFolder = new DirectoryInfo(@"assets\pdfsharp-6.x\fonts"); // 取得字型資源路徑
  
  public byte[]? GetFont(string faceName)
  {
    string fontFileName = faceName;

    //※ PDFsharp 只支援 TrueType 字型(.ttf)。
    foreach (var fontFolder in _fontsFolder.GetDirectories())
    {
      foreach (FileInfo file in fontFolder.GetFiles("*.ttf"))
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

    //※ PDFsharp 只支援 TrueType 字型(.ttf)。
    foreach (var fontFolder in _fontsFolder.GetDirectories())
    {
      foreach (FileInfo file in fontFolder.GetFiles("*.ttf"))
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