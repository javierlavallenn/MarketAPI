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
    public class CategoryController(ICategoryService service) : ControllerBase
    {
        private readonly ICategoryService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Category> categories = await _service.GetAll();

                if (!categories.Any())
                    return Ok(Array.Empty<Category>());

                List<CategoryResponse> response = new();

                foreach (var item in categories)
                {
                    response.Add(new CategoryResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Products = item.Products,
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
                Category category = await _service.GetById(id);

                if (category == null)
                    return NotFound($"An error has occurred, the category with the id: {id} does not exist");

                var response = new Result<CategoryResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new CategoryResponse()
                    {
                        Id = id,
                        Name = category.Name,
                        Description = category.Description,
                        Products = category.Products
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
        public async Task<IActionResult> Create(CreateCategoryRequest req)
        {

            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
            };

            try
            {
                bool newCategory = await _service.Create(category);

                if (!newCategory)
                    return BadRequest("An error occurred while trying to create a Category");

                var response = new Result<CategoryResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new CategoryResponse()
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Products = category.Products
                    }
                };

                return StatusCode(201, response);

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
                Category category = await _service.GetById(id);

                if (category == null)
                    return NotFound($"An error has occurred, the category with the id: {id} does not exist");

                bool deletedCategory = await _service.Delete(category);

                if (!deletedCategory)
                    return BadRequest("An error occurred when trying to delete the category, please try again");

                var response = new Result<CategoryResponse>()
                {
                    HasError = false,
                    Message = "",
                    Data = new CategoryResponse()
                    {
                        Id = category.Id,
                        Description = category.Description,
                        Name = category.Name,
                        Products = category.Products
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
