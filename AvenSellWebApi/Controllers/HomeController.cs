using System;
using System.Net;
using System.Xml;
using Business.Abstract;
using Business.Concrate;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Entity.Concrate;
using Microsoft.Extensions.Hosting.Internal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvenSellWebApi.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient myHTTPClient; // never use USING with httpclient, this should be application life scope
        private const string baseGoogleImageSearchURL = "https://www.google.com/search?source=lnms&tbm=isch&sa=X&q=";



        private static string GetHtmlCode(string searchString)
        {
            try
            {
                myHTTPClient.DefaultRequestHeaders.Accept.Clear();
                myHTTPClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));
                myHTTPClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                myHTTPClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

                var googleImageQueryHttpResponse = myHTTPClient.GetAsync(new Uri(baseGoogleImageSearchURL + searchString)).Result;
                return googleImageQueryHttpResponse.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "";
        }



        private static List<string> GetUrls(string html)
        {
            try
            {
                var urls = new List<string>();

                int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

                while (ndx >= 0)
                {
                    ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                    ndx++;
                    int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                    string url = html.Substring(ndx, ndx2 - ndx);
                    urls.Add(url);
                    ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
                }

                return urls;

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured attempting to parse urls from html...");
            }

            return null;
        }
        struct ImageObject
        {
            public string ImageURL { get; set; }
            public int ImageNumber { get; set; }
        }
        private static byte[] GetImage(string url)
        {
            try
            {
                var googleImageQueryHttpResponse = myHTTPClient.GetAsync(new Uri(url)).Result;
                if (googleImageQueryHttpResponse.IsSuccessStatusCode)
                    return googleImageQueryHttpResponse.Content.ReadAsByteArrayAsync().Result;
                else
                    return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured attempting to download the image...");
            }
            return null;
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
                    if (productId < 102)
                    {
                        string searchTerm = productName;
                        HttpClientHandler httpHandler = new HttpClientHandler();
                        // code to disable ssl validation (needed only for dev/staging)
                        httpHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                        httpHandler.ServerCertificateCustomValidationCallback = (APIHTTPClient, cert, cetChain, policyErrors) =>
                        {
                            return true;
                        };

                        myHTTPClient = new HttpClient(httpHandler);

                        // headers
                        myHTTPClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux i686; rv:64.0) Gecko/20100101 Firefox/64.0");

                        Console.WriteLine("Grabbing Google search results and parsing out URLs...");
                        List<string> urls = GetUrls(GetHtmlCode(searchTerm));
                        Console.WriteLine("Spinning up background threads to download images...");
                        int t = 0;
                        Task[] tasks = new Task[urls.Count];
                        foreach (string tmpString in urls)
                        {
                            ImageObject tmpImgObj = new ImageObject { ImageNumber = t, ImageURL = tmpString };
                            tasks[t] = Task.Factory.StartNew(async () =>
                            {
                                try
                                {
                                    byte[] image = GetImage(tmpImgObj.ImageURL);
                                    string productFolder = Path.Combine("productBase", "Images", productId.ToString());
                                    if (!Directory.Exists(productFolder))
                                        Directory.CreateDirectory(productFolder);

                                    string filePath = Path.Combine(productFolder, productName);
                                    await System.IO.File.WriteAllBytesAsync(filePath, image);

                                    string updateQuery = "UPDATE Products SET ImageUrl = @ImageUrl WHERE Id = @ProductId";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                                    updateCommand.Parameters.AddWithValue("@ImageUrl", filePath);
                                    updateCommand.Parameters.AddWithValue("@ProductId", productId);
                                    updateCommand.ExecuteNonQuery();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"An error occured downloading or saving the image {tmpImgObj.ImageNumber}");
                                }
                            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                            t++;
                        }
                        Task.WaitAll(tasks);

                        //string imageUrl = await SearchImageAsync(searchTerm);

                        //if (!string.IsNullOrEmpty(imageUrl))
                        //{
                        //    // Beyaz arka planlı bir görsel bulundu, indir
                        //    try
                        //    {
                        //        using (HttpClient httpClient = new HttpClient())
                        //        {
                        //            byte[] imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                        //            string fileName = Path.GetFileName(imageUrl);

                        //            // Görseli kaydet
                        //            string productFolder = Path.Combine(hostingEnvironment.ContentRootPath, "productBase", "Images", productId.ToString());
                        //            if (!Directory.Exists(productFolder))
                        //                Directory.CreateDirectory(productFolder);

                        //            string filePath = Path.Combine(productFolder, fileName);
                        //            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                        //            var imagePath = filePath;

                        //            // Görselin URL'sini veritabanında güncelle
                        //            string updateQuery = "UPDATE Products SET ImageUrl = @ImageUrl WHERE Id = @ProductId";
                        //            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        //            updateCommand.Parameters.AddWithValue("@ImageUrl", imagePath);
                        //            updateCommand.Parameters.AddWithValue("@ProductId", productId);
                        //            updateCommand.ExecuteNonQuery();
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        // Görsel indirme veya kaydetme hatası oluştuğunda yapılacak işlemler
                        //    }
                        //}
                    }
                }
                reader.Close();
                return Ok(200);
            }
        }
    }
}

