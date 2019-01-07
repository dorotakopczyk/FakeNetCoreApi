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
            List<FoodItem> foodItems = _foodRepository.GetAll(queryParameters).ToList();

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
            FoodItem foodItem = _foodRepository.GetSingle(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return Ok(foodItem);
        }


        [HttpPost(Name = nameof(AddFood))]
        public ActionResult<FoodItem> AddFood([FromBody] FoodItem newFood)
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

            FoodItem newFoodItem = _foodRepository.GetSingle(newFood.Id);

            return CreatedAtRoute(nameof(GetFoodItem), new { id = newFoodItem.Id });
        }

        [HttpPatch("{id:int}", Name = nameof(PartiallyUpdateFood))]
        public ActionResult<FoodItem> PartiallyUpdateFood(int id, [FromBody] JsonPatchDocument<FoodItem> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            FoodItem existingEntity = _foodRepository.GetSingle(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       
            FoodItem updated = _foodRepository.Update(id, existingEntity);

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
            FoodItem foodItem = _foodRepository.GetSingle(id);

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
