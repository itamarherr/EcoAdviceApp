using DAL.Models;
using EcoAdviceAppApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EcoAdviceAppApi.Mappings;
using EcoAdviceAppApi.Services;

namespace EcoAdviceAppApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController(
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager,
        JwtService jwtService
        ) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(dto.ToUser(), dto.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if(user is null)
                {
                    return Unauthorized();
                }
                var result = await signInManager.PasswordSignInAsync(
                    user.Email, dto.Password, false, false
                    );
                if (result.Succeeded)
                {
                    var token = await jwtService.CreateToken(user);
                    return Ok(new { token });
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}
