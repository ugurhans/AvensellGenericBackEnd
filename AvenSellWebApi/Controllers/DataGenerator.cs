using System;
using System.Net;
using System.Xml;
using Business.Abstract;
using Business.Concrate;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HtmlAgilityPack;
using MailKit.Search;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.IO;
using OfficeOpenXml;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.ColorSpaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;
using Microsoft.OpenApi.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataGenerator : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IBrandService brnadService;
        private readonly IReviewService reviewService;
        private readonly ISubCategoryService subCategoryService;

        public DataGenerator(IProductService productService, ICategoryService categoryService, IBrandService brnadService, IReviewService reviewService, ISubCategoryService subCategoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.brnadService = brnadService;
            this.reviewService = reviewService;
            this.subCategoryService = subCategoryService;
        }

        //[HttpPost("generateProducts")]
        //public async Task<IActionResult> generateProducts(int count)
        //{
        //    var products = FakeDataGenerator.GenerateProducts(count);
        //    foreach (var item in products)
        //    {
        //        productService.Add(item);
        //        if (item.Brand != null)
        //        {
        //            brnadService.Add(item.Brand);
        //        }
        //        if (item.Reviews != null)
        //        {
        //            foreach (var review in item.Reviews)
        //            {
        //                reviewService.Add(review);
        //            }
        //        }
        //    }
        //    return Ok(200);
        //}

        private void CreateTable(SqlConnection connection, string tableName)
        {
            string createQuery = $@"CREATE TABLE {tableName}
                            (
                                ProductCode VARCHAR(50) NOT NULL,
                                ProductName VARCHAR(100) NOT NULL
                            )";

            using (SqlCommand command = new SqlCommand(createQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        [HttpPost("ExcelToDatabase")]
        public void ExcelToDatabase(string excelFilePath, string connectionString, string tableName)
        {
            // Excel dosyasını oku
            FileInfo fileInfo = new FileInfo(excelFilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                // SQL bağlantı dizesini ve tablo adını güncelle


                // SQL bağlantısını oluştur
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tabloyu temizle
                    //TruncateTable(connection, tableName);
                    // CREATE TABLE sorgusu
                    string createTableQuery = @"
                    CREATE TABLE Products (
                        Id INT PRIMARY KEY IDENTITY,
                        ProductCode VARCHAR(50),
                        ProductName VARCHAR(100)
                    )";

                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    int rowCount = worksheet.Dimension.End.Row;
                    int colCount = worksheet.Dimension.End.Column;

                    // Excel verilerini SQL tablosuna ekle
                    for (int row = 2; row <= rowCount; row++)
                    {
                        string productCode = worksheet.Cells[row, 1].Value?.ToString();
                        string productName = worksheet.Cells[row, 2].Value?.ToString();

                        // SQL sorgusu oluştur
                        string insertQuery = $"INSERT INTO {tableName} (ProductCode, ProductName) VALUES (@ProductCode, @ProductName)";

                        // SQL komutunu hazırla
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Parametreleri ekle
                            command.Parameters.AddWithValue("@ProductCode", productCode);
                            command.Parameters.AddWithValue("@ProductName", productName);

                            // Komutu çalıştır
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void TruncateTable(SqlConnection connection, string tableName)
        {
            string truncateQuery = $"TRUNCATE TABLE {tableName}";
            using (SqlCommand command = new SqlCommand(truncateQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }


        private bool IsWhiteBackground(byte[] imageBytes)
        {
            using (Image<Rgba32> image = Image.Load<Rgba32>(imageBytes))
            {
                int whitePixelCount = 0;
                int totalPixelCount = 0;

                // Loop through all pixels in the image
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        Rgba32 pixel = image[x, y];

                        // Check if the pixel is white or close to white
                        if (IsWhiteOrCloseToWhite(pixel))
                        {
                            whitePixelCount++;
                        }

                        totalPixelCount++;
                    }
                }

                // Calculate the ratio of white pixels to total pixels
                double whitePixelRatio = (double)whitePixelCount / totalPixelCount;

                // If the ratio is above a certain threshold, consider it as a white background
                double whiteThreshold = 0.9;
                return whitePixelRatio >= whiteThreshold;
            }
        }

        private bool IsWhiteOrCloseToWhite(Rgba32 pixel)
        {
            // Define a threshold for the RGB values to determine if the pixel is white or close to white
            byte threshold = 220;

            // Check if each RGB component is above the threshold
            return pixel.R >= threshold && pixel.G >= threshold && pixel.B >= threshold;
        }

        private async Task<string> FindWhiteBackgroundImageAsync(JArray imageResults)
        {
            foreach (JToken imageResult in imageResults)
            {
                string imageUrl = imageResult["link"]?.ToString();
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    // Görseli indir
                    using (HttpClient client = new HttpClient())
                    {
                        //byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                        // Arka planı beyaz mı kontrol et
                        //bool isWhiteBackground = IsWhiteBackground(imageBytes);

                        //if (isWhiteBackground)
                        //{
                        // Beyaz arka planlı bir görsel bulundu
                        return imageUrl;
                        //}
                    }
                }
            }

            // Beyaz arka planlı bir görsel bulunamadı
            return null;
        }

        struct SearchResult
        {
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

        private string GetFirstImageFromGoogle(string searchTerm)
        {
            // Chrome WebDriver'ı başlat
            ChromeDriverService service = ChromeDriverService.CreateDefaultService("/Users/ugurhans/Downloads/chromedriver_mac_arm64", "chromedriver");
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Arka planda çalıştırmak için
            options.AddArgument("--no-sandbox"); // Sandbox modunu devre dışı bırakmak için
            IWebDriver driver = new ChromeDriver(service, options);

            try
            {
                // Google Resimler sayfasını aç
                driver.Navigate().GoToUrl($"https://www.google.com.tr/search?q={Uri.EscapeDataString(searchTerm)}&tbm=isch");

                // İlk resim elementini bulmak için bekleyin
                IWebElement imageElement = driver.FindElement(By.CssSelector("img.rg_i"));

                // Resim URL'sini alın
                string imageUrl = imageElement.GetAttribute("src");

                return imageUrl;
            }
            finally
            {
                // WebDriver'ı kapat
                driver.Quit();
                driver.Dispose();
            }
        }

        private async Task<string> SearchImageAsync(string searchTerm)
        {
            try
            {
                // Resim URL'sini al
                string imageUrl = GetFirstImageFromGoogle(searchTerm);
                return imageUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.Message);
            }

            return string.Empty;
        }

        [HttpPost("takepictures")]
        public async Task<IActionResult> TakePictures([FromServices] IWebHostEnvironment hostingEnvironment)
        {
            var cs = "Data Source=2.56.152.68;User ID=aventura;Password=159753Aa!234%;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;MultiSubnetFailover=False;Initial Catalog=ProductRepo;Persist Security Info=True;MultipleActiveResultSets=True;";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                // Ürünleri veritabanından al
                string selectQuery = "SELECT Id, ProductName, ProductCode FROM Products";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    int productId = (int)reader["Id"];
                    string productCode = (string)reader["ProductCode"];
                    string productName = (string)reader["ProductName"];
                    if (productId > 5099)
                    {
                        string searchTerm = productName;
                        string imageUrl = await SearchImageAsync(searchTerm);

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            // Beyaz arka planlı bir görsel bulundu, indir
                            try
                            {
                                using (HttpClient httpClient = new HttpClient())
                                {
                                    // Base64 dönüşümünü gerçekleştir
                                    string base64Data = imageUrl.Substring(imageUrl.IndexOf(',') + 1);
                                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                                    // Dosya adını belirle
                                    string fileName = productName; // İstediğiniz bir dosya adı verebilirsiniz

                                    // Dosya yolunu oluştur
                                    string productFolder = Path.Combine(hostingEnvironment.ContentRootPath, "productBase", "Images", productId.ToString());
                                    if (!Directory.Exists(productFolder))
                                        Directory.CreateDirectory(productFolder);

                                    string filePath = Path.Combine(productFolder, fileName + ".jpeg");

                                    // Base64 veriyi dosyaya yaz
                                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                                    var imagePath = filePath;

                                    // Görselin URL'sini veritabanında güncelle
                                    string updateQuery = "UPDATE Products SET ImageUrl = @ImageUrl WHERE Id = @ProductId";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                                    updateCommand.Parameters.AddWithValue("@ImageUrl", imagePath);
                                    updateCommand.Parameters.AddWithValue("@ProductId", productId);
                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                // Görsel indirme veya kaydetme hatası oluştuğunda yapılacak işlemler
                            }
                        }
                    }
                }
                reader.Close();
                return Ok(200);
            }
        }



        static string GetHtmlFromUrl(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        static string GetFirstImageUrl(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            // İlk a etiketini bul
            HtmlNode aNode = document.DocumentNode.SelectSingleNode("//a");
            if (aNode != null)
            {
                // href özelliğini al
                string href = aNode.GetAttributeValue("href", "");
                string imageUrl = ExtractImageUrlFromGoogleLink(href);
                return imageUrl;
            }

            return null;
        }

        static string ExtractImageUrlFromGoogleLink(string link)
        {
            if (link.StartsWith("/imgres?imgurl="))
            {
                // Görsel URL'sini çıkart
                link = link.Replace("/imgres?imgurl=", "");
                link = Uri.UnescapeDataString(link);
                link = WebUtility.HtmlDecode(link);

                // Ek parametreleri kaldır
                int ampIndex = link.IndexOf('&');
                if (ampIndex >= 0)
                {
                    link = link.Substring(0, ampIndex);
                }

                return link;
            }

            return null;
        }

        static void DownloadImageFromSource(string imageUrl, string fileName)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(imageUrl, fileName);
            }
        }


        [HttpPost("generateCategories")]
        public async Task<IActionResult> generateCategories(int count, int subCount)
        {
            Random _random = new Random();

            var categories = FakeDataGenerator.GenerateCategories(count);
            foreach (var category in categories)
            {
                categoryService.Add(category);
                var subCategoreis = FakeDataGenerator.GenerateSubCategories(_random.Next(3, 7), category.Id);
                foreach (var sub in subCategoreis)
                {
                    subCategoryService.Add(sub);
                    var products = FakeDataGenerator.GenerateProducts(_random.Next(5, 15), sub.Id, sub.CategoryId);
                    foreach (var product in products)
                    {
                        productService.Add(product);
                        var reviews = FakeDataGenerator.GenerateReviews(_random.Next(1, 5), product.Id);
                        foreach (var review in reviews)
                        {
                            reviewService.Add(review);
                        }
                    }
                }
            }
            return Ok(200);
        }
    }
}

