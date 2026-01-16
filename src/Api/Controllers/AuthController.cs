using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Users;
using DigitalProductsApi.src.Application.Interfaces;
using DigitalProductsApi.src.Application.Services;
using DigitalProductsApi.src.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalProductsApi.src.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtTokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = _mapper.Map<ApplicationUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Set standard role "User"
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));

            var token = _tokenService.GenerateToken(user.Id.ToString(), "User");

            return Ok(new AuthResultDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                UserId = user.Id.ToString(),
                Role = "User"
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid username or password");

            // Take role from claim
            var claims = await _userManager.GetClaimsAsync(user);
            var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "User";

            var token = _tokenService.GenerateToken(user.Id.ToString(), roleClaim);

            return Ok(new AuthResultDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                UserId = user.Id.ToString(),
                Role = roleClaim
            });
        }
    }
}
