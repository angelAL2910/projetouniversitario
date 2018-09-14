﻿using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;
using iTextSharp.text.html.simpleparser;
using System.Drawing;
using System.Web.UI;
using System.Web;

namespace WEB.NewBusiness.Common
{
    /// <summary>
    /// Auhor:Lic. Carlos Ml Lebron
    /// Esta clase fue creada para consumir la libreria ItextSharp
    /// </summary>
    public class ITextSharpService
    {
        #region Manejo de Imagenes a  PDF
        public static string GetTypeImage(Image objImage)
        {
            var result = String.Empty;

            if (objImage.RawFormat.Equals(ImageFormat.Jpeg))
                result = "jpeg";
            else if (objImage.RawFormat.Equals(ImageFormat.Gif))
                result = "gif";
            else if (objImage.RawFormat.Equals(ImageFormat.Bmp))
                result = "bmp";
            else if (objImage.RawFormat.Equals(ImageFormat.Emf))
                result = "emf";
            else if (objImage.RawFormat.Equals(ImageFormat.Exif))
                result = "Exif";
            else if (objImage.RawFormat.Equals(ImageFormat.Png))
                result = "png";
            else if (objImage.RawFormat.Equals(ImageFormat.Tiff))
                result = "tiff";
            else if (objImage.RawFormat.Equals(ImageFormat.Wmf))
                result = "wmf";
            else if (objImage.RawFormat.Equals(ImageFormat.MemoryBmp))
                result = "memorybmp";
            else if (objImage.RawFormat.Equals(ImageFormat.Icon))
                result = "ico";
            return result;
        }


        public static byte[] ConvertImageToPdf(byte[] img)
        {
            iTextSharp.text.Rectangle pageSize = null;

            //var stream = GetImageStream(img);            
            Stream stream = new MemoryStream(img);

            using (var srcImage = new Bitmap(stream))            
                pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);

            byte[] result = new byte[] { };

            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                var image = iTextSharp.text.Image.GetInstance(img);
                document.Add(image);
                document.Close();

                result = ms.ToArray();
            }
            
            return result;
        }


        /// <summary>
        /// Convierte una imagen a PDF
        /// Parametro de entrada ruta de la imagen
        /// </summary>
        /// <param name="bmpFilePaths"></param>
        /// <returns></returns>
        public static byte[] CreatePdfFromImage(string bmpFilePaths)
        {
            using (var ms = new MemoryStream())
            {
                var document = new Document(PageSize.LETTER.Rotate(), 0, 0, 0, 0);

                PdfWriter.GetInstance(document, ms).SetFullCompression();

                document.Open();

                var imgStream = GetImageStream(bmpFilePaths);

                var image = iTextSharp.text.Image.GetInstance(imgStream);

                //Centralizar la imagen
                image.Alignment = Element.ALIGN_CENTER;

                image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);

                document.Add(image);

                document.Close();

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Convierte una imagen a PDF
        /// Parametro de entrada un arreglo de bytes
        /// </summary>
        /// <param name="imgBytes"></param>
        /// <returns></returns>
        public static byte[] CreatePdfFromImage(byte[] imgBytes)
        {
            using (var ms = new MemoryStream())
            {
                var document = new Document(PageSize.LETTER.Rotate(), 0, 0, 0, 0);

                PdfWriter.GetInstance(document, ms).SetFullCompression();

                document.Open();

                var imgStream = GetImageStream(imgBytes);

                var image = iTextSharp.text.Image.GetInstance(imgStream);

                //Centralizar la imagen
                image.Alignment = Element.ALIGN_CENTER;

                image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);

                document.Add(image);

                document.Close();

                return ms.ToArray();
            }
        }

        private static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Gets the image at the specified path, shrinks it, converts to JPG, and returns as a stream
        /// In Parameters Byte[] 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Stream GetImageStream(byte[] image)
        {
            var ms = new MemoryStream(image);

            var typeImg = GetTypeImage(ByteArrayToImage(image));

            using (var img = Image.FromStream(ms))
            {
                var imageCodec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.MimeType == "image/jpeg");

                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)20);               

                const int dpi = 175;
                var thumb = img.GetThumbnailImage((int)(11 * dpi), (int)(8.5 * dpi), null, IntPtr.Zero);
                if (imageCodec != null) thumb.Save(ms, imageCodec, encoderParams);
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        /// <summary>
        /// Gets the image at the specified path, shrinks it, converts to JPG, and returns as a stream
        /// In Parameters String
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private static Stream GetImageStream(string imagePath)
        {
            var ms = new MemoryStream();
            using (var img = Image.FromFile(imagePath))
            {
                var imageCodec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.MimeType == "image/jpeg");

                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)20);

                const int dpi = 175;
                var thumb = img.GetThumbnailImage((int)(11 * dpi), (int)(8.5 * dpi), null, IntPtr.Zero);
                if (imageCodec != null) thumb.Save(ms, imageCodec, encoderParams);
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        #endregion

        #region Manejo de HTML a PDF

        private const String StrSelectUserListBuilder = @"<html><body>
                                <h1>My First Heading</h1>
                                <p>My first paragraph.</p>
                            </body>
                        </html>";

        readonly String _htmlText = StrSelectUserListBuilder.ToString(CultureInfo.InvariantCulture);        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static Byte[] CreatePDFFromHtmlFile(string htmlText)
        {
            try
            {
                var document = new Document();
                var file = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles/" + Guid.NewGuid() + ".pdf");
                PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
                document.Open();
                var hw = new HTMLWorker(document);
                hw.Parse(new StringReader(htmlText));
                document.Close();
                var pdfFile = File.ReadAllBytes(file);

                //Eliminar el archivo temporal
                if (File.Exists(file))
                    File.Delete(file);

                return pdfFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Convierte el body a enviar en una imagen
        /// </summary>
        /// <param name="varImgPath">Path de la nueva imagen</param>
        /// <param name="varBody">Cuerpo del correo a convertir</param>
        /// <param name="varPathFinal">Path final</param>
        /// <returns>string</returns>
        public byte[] mtCreateFinalImage(string varDataPath, string folderTempPath)
        {
            #region Definiciones
            string fImagenFinal = String.Format(@"{1}{0}.png", Guid.NewGuid(), folderTempPath);
            string fResult = string.Empty;
            System.Drawing.Bitmap bmp = null;
            byte[] Result = null;

            #endregion
            #region Proceso

            try
            {
                System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
                wb.ScrollBarsEnabled = false;
                wb.ScriptErrorsSuppressed = true;
                var fUri = new Uri(varDataPath);
                wb.Navigate(fUri.AbsoluteUri, false);

                while (wb.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1500);
                int width = wb.Document.Body.ScrollRectangle.Width + 25;
                int height = wb.Document.Body.ScrollRectangle.Height + 25;
                wb.Width = width;
                wb.Height = height;
                //Le indico la calidad que quiero que tenga la imagen
                ImageCodecInfo fImgEncoder = GetEncoder(ImageFormat.Png);
                System.Drawing.Imaging.Encoder fEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters fEncoderParameters = new EncoderParameters(1);
                EncoderParameter fEncoderParameter = new EncoderParameter(fEncoder, "500L");
                fEncoderParameters.Param[0] = fEncoderParameter;

                //Creo la nueva imagen
                bmp = new System.Drawing.Bitmap(width, height);
                wb.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, width, height));

                bmp.Save(fImagenFinal, fImgEncoder, fEncoderParameters);
                fResult = fImagenFinal;

            }
            catch (System.Threading.ThreadAbortException)
            {


            }
            //catch (Exception jc) { new Exception(String.Format("Ha ocurrido un error generando la imagen a enviar,por favor verifique. Error> {0}", jc.ToString())); }

            #endregion
            #region Salida 
            
            return
               Result;
            #endregion

        }

        /// <summary>
        /// Retorna el encoding de una imagen
        /// </summary>
        /// <param name="format">Formato de la imagen a evaluar</param>
        /// <returns>ImageCodecInfo</returns>
        ImageCodecInfo GetEncoder(ImageFormat format)
        {
            try
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == format.Guid)
                    {
                        return codec;
                    }
                }
            }
            catch { }
            return null;
        }

        public static Byte[] CreatePdfFromHtml(String html)
        {
            var file = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles/" + Guid.NewGuid() + ".pdf");   
            var document = new Document(PageSize.A4, 80, 50, 30, 65);
            PdfWriter.GetInstance(document, new FileStream(file, FileMode.Create));
            document.Open();

            foreach (IElement e in HTMLWorker.ParseToList(new StringReader(html), new StyleSheet()))
                document.Add(e);

            document.Close();

            var pdfFile = File.ReadAllBytes(file);

            //Eliminar el archivo temporal
            if (File.Exists(file))
                File.Delete(file);

            return pdfFile;
        }
        #endregion

        #region Conversion de Imagen

        public static byte[] ConvertImageAsPng(byte[] imgByteArray, String TempFolder)
        {
            byte[] result;

            var PathFile = TempFolder + @"\" + Guid.NewGuid();

            Utility.ByteArrayToFile(PathFile, imgByteArray);
            // Load the image.
            Image image1 = System.Drawing.Image.FromFile(PathFile);

            var FileName = PathFile + ".png";
            image1.Save(FileName, ImageFormat.Png);

            result = File.ReadAllBytes(FileName);

            File.Delete(FileName);

            return result;
        }

        public static byte[] ConvertImageAsPng(Stream stream)
        {
            Image img = Image.FromStream(stream);
            
            byte[] png;

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                png = ms.ToArray();
            }

            return png;
        }

        #endregion

        #region Creacion de PDF Document
        public static Tuple<byte[], string> CreatePDFDocument(String pathFile, string Title, string HeaderText, string AppCreate, string Body)
        {
            var FileName = string.Concat(pathFile, @"\", Guid.NewGuid(), ".pdf");

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(FileName, FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle(Title);
            doc.AddCreator(AppCreate);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph(HeaderText));
            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph(Body));
            doc.Add(Chunk.NEWLINE);

            doc.Close();
            writer.Close();

            var oFile = File.ReadAllBytes(FileName);
            var NameFile = Path.GetFileName(FileName);
            //Eliminar el archivo generado
            File.Delete(FileName);

            return new Tuple<byte[], string>(oFile, NameFile);
        }

        #endregion
    }

}