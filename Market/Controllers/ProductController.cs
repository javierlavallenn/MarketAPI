using Market.Entities;
using Market.Interfaces;
using Market.Models.Request.Product;
using Market.Models.Response;
using Market.Shared;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Market.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController(IProductService service) : ControllerBase
    {
        private readonly IProductService _service = service;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Product> products = _service.GetAll();

                if (!products.Any())
                    return Ok(Array.Empty<Product>());

                return Ok(products);
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Product product = _service.GetById(id);

                if (product == null)
                    return NotFound($"An error occurred, the product with id: {id}, was not found");

                return Ok(new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new ProductResponse()
                    {
                        Id = id,
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                    }
                });
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(CreateProductRequest req)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Brand = req.Brand,
                Price = req.Price,
                Stock = req.Stock,
                BarCode = req.BarCode,
                Description = req.Description,
                IsPublished = req.IsPublished,
                CreateAt = DateTime.Now,
            };

            try
            {
                bool newProduct = _service.Create(product);

                if (!newProduct)
                    return BadRequest("An error occurred while trying to create a product, please try again");

                return Ok(new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new ProductResponse()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                    }
                });
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpPut]
        public IActionResult Update(UpdateProductRequest req)
        {
            try
            {
                Product product = _service.GetById(req.Id);

                if (product == null)
                    return NotFound($"An error occurred when searching for the product with the id: {req.Id}, please verify the id and try again");

                product.Name = req.Name;
                product.Stock = req.Stock;
                product.Price = req.Price;
                product.IsPublished = req.IsPublished;

                bool updatedProduct = _service.Update(product);

                if (!updatedProduct)
                    return BadRequest("An error occurred while trying to update a product");

                return Ok(new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new ProductResponse()
                    {
                        Id = req.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                    }
                });
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                Product product = _service.GetById(id);

                if (product == null)
                    return NotFound($"An error occurred, the product with id: {id}, was not found");

                bool result = _service.Delete(product);

                if (!result)
                    return BadRequest("An error occurred while deleting a product");

                return Ok(new Result<ProductResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new ProductResponse()
                    {
                        Id = id,
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock,
                    }
                });
            }
            catch (Exception error)
            {
                Log.Error(error.Message);

                throw new Exception($"An unexpected error occurred: {error.Message}");
            }
        }
    }
}
