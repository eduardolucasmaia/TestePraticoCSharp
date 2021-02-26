using System;
using System.Drawing;
using System.IO;

namespace DTO.Ferramentas
{
    public static class Imagem
    {
        public static string RedimencionarImagem(int largura, string fotoBase64)
        {
            try
            {
                if (!string.IsNullOrEmpty(fotoBase64))
                {
                    var base64String = fotoBase64.Remove(0, fotoBase64.IndexOf(',') + 1);
                    byte[] bytes = Convert.FromBase64String(base64String);

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        var imagem = Image.FromStream(ms);
                        int X = imagem.Width;
                        int Y = imagem.Height;
                        int altura = (int)((largura * Y) / X);
                        var retorno = new Image.GetThumbnailImageAbort(() => false);
                        var imagemMiniatura = imagem.GetThumbnailImage(largura, altura, retorno, IntPtr.Zero);
                        //imagemMiniatura.Save(@"D:\Desktop\" + largura.ToString() + "-test.jpg");
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            imagemMiniatura.Save(imageStream, System.Drawing.Imaging.ImageFormat.Bmp);
                            imageStream.Position = 0;
                            byte[] imagemBytes = imageStream.ToArray();
                            base64String = Convert.ToBase64String(imagemBytes);
                            base64String = fotoBase64.Substring(0, fotoBase64.IndexOf(',') + 1) + base64String;
                            return base64String;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
