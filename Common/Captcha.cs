using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace G_APIs.Common
{

    public class Captcha
    {
        public Captcha()
        {
        }
        public   Bitmap CreateCaptch(string sImageText)
        {
            Bitmap bmpImage = new Bitmap(1, 1);

            int iWidth = 0;
            int iHeight = 0;

            Font MyFont = new Font("Arial", 36, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            Graphics MyGraphics = Graphics.FromImage(bmpImage);
            iWidth = Convert.ToInt32(MyGraphics.MeasureString(sImageText, MyFont).Width) + 20;
            iHeight = Convert.ToInt32(MyGraphics.MeasureString(sImageText, MyFont).Height) + 4;
            bmpImage = new Bitmap(bmpImage, new Size(iWidth, iHeight));
            MyGraphics = Graphics.FromImage(bmpImage);
            MyGraphics.Clear(Color.Beige);
            MyGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            MyGraphics.DrawString(sImageText, MyFont, new SolidBrush(Color.Brown), 10, 4);
            MyGraphics.Flush();
            return (bmpImage);
        }

    }
}
