
using Microsoft.AspNetCore.Mvc;
using QRCode_MVC.Models;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using IronBarCode;
using Image = SixLabors.ImageSharp.Image;
using SixLabors.ImageSharp.Formats.Png;

namespace QRCode_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult GenerateQr()
        {
            byte[] IHDR = { 255, 255, 255 };
            byte[] IDAT = { 0, 0, 0 };
            string str = "http://deneme.com";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
            QRCoder.PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            //QRCode qrCode = new QRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(5, IHDR, IDAT);
            //pictureBox1.Image = qrCodeImage;
            return File(qrCodeImage, "image/png");

        }

        public IActionResult GenerateBarcode()
        {


            GeneratedBarcode barcode = BarcodeWriter.CreateBarcode("https://wissen.com/csharp/barcode/", BarcodeEncoding.Code128);
            barcode.AddAnnotationTextAboveBarcode("Bu Bir Barkodtur.");
            barcode.AddBarcodeValueTextBelowBarcode();
            barcode.ChangeBackgroundColor(IronSoftware.Drawing.Color.White);
            barcode.ChangeBarCodeColor(IronSoftware.Drawing.Color.Black, true);
            Image image = barcode.Image;

            barcode.SaveAsPng("barcode.png");


            using (MemoryStream ms = new MemoryStream())
            {
                image.SaveAsPng(ms, new PngEncoder()
                {




                });
                //image.SaveAsync(ms, System.Drawing.Imaging.ImageFormat.Png);
                //barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return File(ms.ToArray(), "image/png");

            }
        }

        public IActionResult Privacy()
        {
                return View();
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}