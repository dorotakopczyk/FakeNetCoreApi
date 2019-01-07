using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleMVCApp.Api.Models;
using SampleMVCApp.Api.Repositories;
using SampleMVCApp.Api.Utilities;

namespace SampleMVCApp.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IUrlHelper _urlHelper;

        public FoodController(IUrlHelper urlHelper, IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetAllFoods))]
        public ActionResult GetAllFoods([FromQuery] QueryParameters queryParameters)
        {
            List<Food> foodItems = _foodRepository.GetAll(queryParameters).ToList();

            var allItemCount = _foodRepository.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            return Ok(foodItems);
        }



        [HttpGet]
        [Route("{id:int}", Name = nameof(GetFoodItem))]
        public ActionResult GetFoodItem(int id)
        {
            Food foodItem = _foodRepository.GetSingleFood(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return Ok(foodItem);
        }


        [HttpPost(Name = nameof(AddFood))]
        public ActionResult<Food> AddFood([FromBody] Food newFood)
        {
            if (newFood == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _foodRepository.Add(newFood);

            if (!_foodRepository.Save())
            {
                throw new Exception("Creating a fooditem failed on save.");
            }

            Food newFoodItem = _foodRepository.GetSingleFood(newFood.Id);

            return CreatedAtRoute(nameof(GetFoodItem), new { id = newFoodItem.Id });
        }

        [HttpPatch("{id:int}", Name = nameof(PartiallyUpdateFood))]
        public ActionResult<Food> PartiallyUpdateFood(int id, [FromBody] JsonPatchDocument<Food> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            Food existingEntity = _foodRepository.GetSingleFood(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Food updated = _foodRepository.Update(id, existingEntity);

            if (!_foodRepository.Save())
            {
                throw new Exception("Updating a fooditem failed on save.");
            }

            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(RemoveFood))]
        public ActionResult RemoveFood(int id)
        {
            Food foodItem = _foodRepository.GetSingleFood(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            _foodRepository.Delete(id);

            if (!_foodRepository.Save())
            {
                throw new Exception("Deleting a fooditem failed on save.");
            }

            return NoContent();
        }

    }
}
