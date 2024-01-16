using Market.Interfaces;
using Market.Model.Entities;
using Market.Model.Request;
using Market.Model.Response;
using Market.Shared;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService service) : ControllerBase
    {
        private readonly IProductService _service = service;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Product> products = await _service.GetAll();

                if (!products.Any())
                    return Ok(Array.Empty<Product>());

                List<ProductResponse> response = new();

                foreach (var item in products)
                {
                    response.Add(new ProductResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Category = item.Category,
                    });
                }

                return Ok(response);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Product product = await _service.GetById(id);

                if (product == null)
                    return NotFound($"An error occurred when searching for the product with the id:{id}, not exixst!");


                var response = new Result<ProductResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new ProductResponse()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Category = product.Category,
                    }
                };

                return Ok(response);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest req)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Price = req.Price,
                Description = req.Description,
                Stock = req.Stock,
                CategoryId = req.CategoryId,
            };

            try
            {
                bool newProduct = await _service.Create(product);

                if (!newProduct)
                    return BadRequest("An error occurred while trying to create a product");

                var response = new Result<ProductResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new ProductResponse()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Category = product.Category
                    }
                };

                return Ok(response);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest req)
        {
            try
            {
                Product product = await _service.GetById(req.Id);

                if (product == null)
                    return NotFound($"An error occurred when searching for the product with the id:{req.Id}, not exixst!");

                product.Name = req.Name;
                product.Price = req.Price;
                product.Description = req.Description;
                product.Stock = req.Stock;

                bool updatedProduct = await _service.Update(product);

                if (!updatedProduct)
                    return BadRequest("An error occurred while trying to update a product");

                var response = new Result<ProductResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new ProductResponse()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                    }
                };

                return Ok(response);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Product product = await _service.GetById(id);

                if (product == null)
                    return NotFound($"An error occurred when searching for the product with the id:{id}, not exixst!");

                bool deletedProduct = await _service.Delete(product);

                if (!deletedProduct)
                    return BadRequest("An error occurred while trying to delete a product");

                var response = new Result<ProductResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new ProductResponse()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Category = product.Category
                    }
                };

                return Ok(response);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }
    }
}
