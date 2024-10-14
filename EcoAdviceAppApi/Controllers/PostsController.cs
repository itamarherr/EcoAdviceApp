using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using EcoAdviceAppApi.DTOs;
using EcoAdviceAppApi.Mappings;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EcoAdviceAppApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController(ContextDAL _context) : ControllerBase
    {

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPost()
        {
            var posts = await _context.Posts.Include(p => p.User).ToListAsync();

            // Use the extension method to convert to PostDto
            var postDtos = posts.Select(p => p.ToDto()).ToList();

            return Ok(postDtos);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            // Use the extension method for mapping
            var postDto = post.ToDto();

            return Ok(postDto);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Only update allowed fields
            post.Content = postDto.Content;
            post.ParentPostId = postDto.ParentPostId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles ="admin")]
  
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] PostDto postDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID");
            }

            var post = new Post
            {
                Content = postDto.Content,
                PostDate = DateTime.UtcNow,
                UserId = int.Parse(userIdClaim),
                ParentPostId = postDto.ParentPostId
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            var createdPost = await _context.Posts.Include(p => p.User).FirstAsync(p => p.Id == post.Id);
            var createdPostDto = createdPost.ToDto();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, postDto);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
