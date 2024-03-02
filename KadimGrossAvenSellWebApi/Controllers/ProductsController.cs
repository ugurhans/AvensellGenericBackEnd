using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Request;
using Entity.Concrate;
using Core.Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private IFileStorageService _fileStorageService;
        private IUserFavoriteService _userFavoriteService;
        public ProductsController(IProductService productService, IFileStorageService fileStorageService, IUserFavoriteService userFavoriteService)
        {
            _productService = productService;
            _fileStorageService = fileStorageService;
            _userFavoriteService = userFavoriteService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("GetImageBase64ForUpdate")]
        public IActionResult GetImageBase64ForUpdate(int id)
        {
            var result = _productService.GetImageBase64ForUpdate(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdtobyid")]
        public IActionResult GetDtoById(int id)
        {
            var result = _productService.GetDtoById(id);
            if (result.Success && result.Data != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetDetailWithId")]
        public IActionResult GetDetailWithId(int id, int? userId)
        {
            var result = _productService.GetDetailWithId(id, userId);
            if (result.Success && result.Data != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("SearchProduct")]
        public IActionResult SearchProduct(string productName)
        {
            var result = _productService.SearchProduct(productName.ToLower());

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldto")]
        public IActionResult GetAllDto()
        {
            var result = _productService.GetAllDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getalldtoWithUri")]
        public IActionResult GetalldtoWithUri()
        {
            var result = _productService.GetAllDto();
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetAllCampaignProducts")]
        public IActionResult getAllCampaignProducts(int productCount)
        {
            var result = _productService.GetAllCampaignProducts(productCount);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetRandomRecommendations")]
        public IActionResult GetRandomRecommendations(int ProductCount)
        {
            var result = _productService.GetRandomRecommendations(ProductCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllRecommendedSixProducts")]
        public IActionResult GetAllRecommendedSixProducts(int basketId,int ProductCount)
        {
            var result = _productService.GetAllRecommendedSixProducts(basketId, ProductCount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getTopFiveFeatured")]
        public IActionResult GetTopFiveFeatured(int userId)
        {
            var result = _productService.GetAllTopFiveDto(userId);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(ProductAdd productAdd)
        {
            var product = productAdd.Product;
            var result = _productService.Add(product);
            if (result.Success)
            {
                productAdd.fileUpload.OwnerId = product.Id;
                productAdd.fileUpload.Collection = "AvenSellProducts";

                var fileResult = _fileStorageService.AddUri(productAdd.fileUpload);
                if (fileResult.Success)
                {
                    product.ImageUrl = fileResult.Data.Url;
                    return Ok(result);
                }
                else
                {
                    _productService.Delete(product.Id);
                    return BadRequest("Ürün eklendi ama resimde sorun çıktı, ürün tekrar silindi.");
                }
            }
            else
            {
                return BadRequest("Ürün Eklenemedi");
            }
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {

            var result = _productService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetAllByCategoryId")]
        public IActionResult GetAllByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllByCategoryIdForUser")]
        public IActionResult GetAllByCategoryIdForUser(int categoryId, int userId)
        {
            var result = _productService.GetAllByCategoryIdForUser(categoryId, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(ProductAdd product)
        {

            if (product.fileUpload != null)
            {
                product.fileUpload.OwnerId = product.Product.Id;
                product.fileUpload.Collection = "AvenSellProducts";
                var fileResult = _fileStorageService.UpdateUri(product.fileUpload);
                if (fileResult.Success)
                {
                    var exProd = _productService.GetById(product.Product.Id);
                    product.Product.ImageUrl = exProd.Data.ImageUrl ?? " ";
                    var result = _productService.Update(product.Product);
                    if (result.Success)
                    {
                        return Ok(result);
                    }
                }

            }
            else
            {
                var exProd = _productService.GetById(product.Product.Id);
                product.Product.ImageUrl = exProd.Data.ImageUrl ?? " ";
                var result = _productService.Update(product.Product); if (result.Success)
                {
                    return Ok(result);
                }
            }

            return BadRequest();
        }

        [HttpPatch("ReOrder")]
        public IActionResult ReOrder(int from, int to, int categoryId)
        {
            var result = _productService.ReOrder(from, to, categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }

}
