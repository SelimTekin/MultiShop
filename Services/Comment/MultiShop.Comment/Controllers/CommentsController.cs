using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;
        public CommentsController(CommentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _context.UserComments.ToList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            var value = _context.UserComments.Find(id);
            return Ok(value);
        }
        [HttpPost]
        public IActionResult CreateComment(UserComment userComment)
        {
            _context.UserComments.Add(userComment);
            _context.SaveChanges();
            return Ok("Yorum Başarıyla Eklendi");
        }
        [HttpDelete]
        public IActionResult DeleteComment(int id) {
            var value = _context.UserComments.Find(id);
            _context.UserComments.Remove(value);
            _context.SaveChanges();
            return Ok("Yorum Başarıyla Silindi");
        }
        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            _context.UserComments.Update(userComment);
            _context.SaveChanges();
            return Ok("Yorum Başarıyla Güncellendi");
        }
        [HttpGet("CommentListByProductId/{id}")]
        public IActionResult CommentListByProductId(string id)
        {
            var value = _context.UserComments.Where(x => x.ProductId == id).ToList();
            return Ok(value);
        }
    }
}
