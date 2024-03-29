﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Services;

namespace WebshopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CategoryResponse> categoryResponse = await _categoryService.GetAllCategories();
                if (categoryResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (categoryResponse.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }
        // https://localhost:5001/api/Category/derp
        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.GetCategoryById(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        // https://localhost:5001/api/Category/derp
        [HttpGet("WithoutProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategoriesWithoutProducts()
        {
            try
            {
                List<CategoryResponse> categoryResponse = await _categoryService.GetAllCategoriesWithoutProducts();
                if (categoryResponse == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }
                if (categoryResponse.Count == 0)
                {
                    return NoContent();
                }
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }


        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest newCategory)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.CreateCategory(newCategory);

                if (categoryResponse == null)
                {
                    return Problem("Category Was not created, something went wrong");
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] CategoryRequest updateAuthor)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.UpdateCategory(categoryId, updateAuthor);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.DeleteCategory(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }



    }
}
