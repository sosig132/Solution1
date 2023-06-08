﻿using Azure.Core.Pipeline;
using Back_End.Models.Reviews;
using Back_End.Services.BookService;
using Back_End.Services.ReviewService;
using Back_End.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        public ReviewController(IReviewService reviewService, IBookService bookService, IUserService userService)
        {
            _reviewService = reviewService;
            _bookService = bookService;
            _userService = userService;
        }

        [HttpPost("addReview")]
        public IActionResult AddReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review is null");
            }

            if (review.Rating == 0)
                return BadRequest("Rating is null");

            if (review.Content == null)
                return BadRequest("Comment is null");

            review.Id = Guid.NewGuid();
            review.DateCreated = DateTime.Now;
            Debug.WriteLine(review.Content);
            Debug.WriteLine(review.UserId);
            Debug.WriteLine(review.BookId);
            Debug.WriteLine(review.Id);
            Debug.WriteLine(review.Rating);


            _reviewService.AddReview(review);
            _reviewService.SaveChanges();
            return Ok(review);
        }

        [HttpGet("byBookId/{id}")]
        public IActionResult GetReviewsByBookId(Guid id)
        {
            var reviews = _reviewService.GetReviewsByBookId(id);
            if (reviews == null)
            {
                return NotFound("Reviews not found");
            }
            foreach(Review review in reviews)
            {
                review.User = _userService.GetUserMappedById(review.UserId);
            }
            return Ok(reviews);
        }

    }
}