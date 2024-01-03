using Market.Entities;
using Market.Interfaces;
using Market.Models.Request.Category;
using Market.Models.Response;
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
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Category> categories = _service.GetAll();

                if (!categories.Any())
                    return Ok(Array.Empty<Category>());

                return Ok(categories);
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
                Category category = _service.GetById(id);

                if (category == null)
                    return NotFound($"An error has occurred, the category with the id:{id} does not exist");

                return Ok(new Result<CategoryResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new CategoryResponse()
                    {
                        Id = id,
                        Name = category.Name,
                        Description = category.Description
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
        public IActionResult Create(CreateCategoryResponse req)
        {
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };

            try
            {
                bool newCategory = _service.Create(category);

                if (!newCategory)
                    return BadRequest("An error occurred when trying to create a category, please try again");

                return Ok(new Result<CategoryResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new CategoryResponse()
                    {
                        Id = category.Id,
                        Name = req.Name,
                        Description = req.Description,
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
        public IActionResult Update(UpdateCategoryResponse req)
        {
            try
            {
                Category category = _service.GetById(req.Id);

                if (category == null)
                    return NotFound($"An error has occurred, the category with the id:{req.Id} does not exist");

                category.Name = req.Name;
                category.Description = req.Description;
                category.CreateAt = DateTime.Now;
                category.UpdateAt = DateTime.Now;

                bool updateCategory = _service.Update(category);

                if (!updateCategory)
                    return BadRequest("Error when trying to update a category, please try again");

                return Ok(new Result<CategoryResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new CategoryResponse()
                    {
                        Id = category.Id,
                        Description = req.Description,
                        Name = req.Name,
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
                Category category = _service.GetById(id);

                if (category == null)
                    return NotFound("$\"An error has occurred, the category with the id:{req.Id} does not exist\"");

                bool deletedCategory = _service.Delete(category);

                if (!deletedCategory)
                    return BadRequest($"An error occurred while trying to delete a category, please try again");

                return Ok(new Result<CategoryResponse>()
                {
                    Failure = false,
                    Message = "No errors found",
                    Data = new CategoryResponse()
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
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
