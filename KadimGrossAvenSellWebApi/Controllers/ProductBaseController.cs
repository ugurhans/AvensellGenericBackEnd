using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using Entity.Concrate;
using Entity.Request;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KadimGrossAvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBaseController : Controller
    {

        public class ProductDbContext : DbContext
        {
            public DbSet<ProductBase> Products { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=2.56.152.68;User ID=aventura;Password=159753Aa!234%;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;MultiSubnetFailover=False;Initial Catalog=ProductRepo;Persist Security Info=True;");
            }
        }

        public class ProductRepository
        {
            private readonly ProductDbContext _dbContext;

            public ProductRepository(ProductDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<ProductBase> GetProductsByName(string productName)
            {
                var filteredProducts = _dbContext.Products.Where(p => p.ProductName.ToLower().Replace(" ", "").Contains(productName.ToLower().Replace(" ", ""))).ToList();
                return filteredProducts;
            }
            public List<ProductBase> GetAll()
            {
                var filteredProducts = _dbContext.Products.ToList();
                return filteredProducts;
            }
            public void UpdateProduct(ProductBase updatedProduct)
            {
                var existingProduct = _dbContext.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

                if (existingProduct != null)
                {
                    existingProduct.ProductCode = updatedProduct.ProductCode;
                    existingProduct.ProductName = updatedProduct.ProductName;
                    existingProduct.ImageUrl = updatedProduct.ImageUrl;
                    existingProduct.UnitPrice = updatedProduct.UnitPrice;
                    existingProduct.CategoryId = updatedProduct.CategoryId;
                    existingProduct.SubCategoryId = updatedProduct.SubCategoryId;
                    existingProduct.BrandId = updatedProduct.BrandId;
                    existingProduct.UnitType = updatedProduct.UnitType;
                    existingProduct.UnitQuantity = updatedProduct.UnitQuantity;
                    existingProduct.UnitCount = updatedProduct.UnitCount;
                    existingProduct.Description = updatedProduct.Description;
                    existingProduct.Manufacturer = updatedProduct.Manufacturer;

                    _dbContext.SaveChanges();
                }
            }
        }

        [HttpPost("UpdateProductName")]
        public IActionResult UpdateProductName()
        {
            try
            {
                using (var dbContext = new ProductDbContext())
                {
                    var productRepository = new ProductRepository(dbContext);
                    var result = productRepository.GetAll();
                    foreach (var item in result)
                    {
                        if (item.ImageUrl != null)
                        {
                            string oldImageUrl = item.ImageUrl;
                            string fileName = Path.GetFileName(oldImageUrl);
                            string fileExtension = Path.GetExtension(oldImageUrl);

                            // Yeni dosya adını oluştur
                            string newFileName = $"{item.Id}{fileExtension}";

                            // Yeni dosya yolu
                            string newImagePath = $"/Api/Assets/productBase/Images/{item.Id}/{newFileName}";

                            // Dosyayı bul ve adını değiştir
                            string oldImagePath = Path.Combine(oldImageUrl);
                            string newImagePathOnDisk = Path.Combine("C:\\inetpub\\wwwroot\\sites\\KadimSite+", newImagePath);

                            // Dosyayı taşı
                            if (System.IO.File.Exists($"C:\\inetpub\\wwwroot\\sites\\KadimSite\\Api\\Assets\\productBase\\Images\\{item.Id}\\{fileName}"))
                            {
                                System.IO.File.Move($"C:\\inetpub\\wwwroot\\sites\\KadimSite\\Api\\Assets\\productBase\\Images\\{item.Id}\\{fileName}", $"C:\\inetpub\\wwwroot\\sites\\KadimSite\\Api\\Assets\\productBase\\Images\\{item.Id}\\{newFileName}");
                            }

                            // ImageUrl'yi güncelle
                            item.ImageUrl = newImagePath;

                            // Veritabanında değişiklikleri kaydet
                            dbContext.SaveChanges();

                        }

                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ürün güncelleme işlemi başarısız oldu." + ex.Message);
            }
        }

        [HttpPost("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductBase updatedProduct)
        {
            try
            {
                using (var dbContext = new ProductDbContext())
                {
                    var productRepository = new ProductRepository(dbContext);
                    productRepository.UpdateProduct(updatedProduct);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ürün güncelleme işlemi başarısız oldu.");
            }
        }

        [HttpGet("UpdateProductUrl")]
        public IActionResult UpdateProductUrl()
        {
            try
            {
                using (var dbContext = new ProductDbContext())
                {
                    var productRepository = new ProductRepository(dbContext);
                    var result = productRepository.GetAll();

                    foreach (var item in result)
                    {
                        if (item.ImageUrl != null)
                        {
                            item.ImageUrl = item.ImageUrl.Replace("/Users/ugurhans/Repositories/Github/AvenSellBackEnd/AvenSellWebApi", "Api/Assets");
                            productRepository.UpdateProduct(item);
                        }
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ürün güncelleme işlemi başarısız oldu.");
            }
        }



        [HttpGet("SearchProductWithName")]
        public IActionResult SearchProductWithName(string productName)
        {
            try
            {
                using (var dbContext = new ProductDbContext())
                {
                    var productRepository = new ProductRepository(dbContext);
                    var products = productRepository.GetProductsByName(productName);
                    return Ok(products);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ürün araması başarısız oldu.");
            }
        }
    }
}

