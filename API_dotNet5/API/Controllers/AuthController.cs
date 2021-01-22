using API.Context;
using Lib.DTO;
using Lib.Entites;
using Lib.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AssignmentDbContext dbContext;
        private IConfiguration configuration;

        public AuthController(AssignmentDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user = await dbContext.ApplicationUsers.SingleOrDefaultAsync(user => user.Username == loginModel.Username);

            if (user == null)
                return NotFound(new LoginResponseDTO { Message = "User not found", HasError = true });

            if (!CryptoHelper.VerifyHashedPassword(loginModel.Password, user.Password))
                return BadRequest(new LoginResponseDTO { Message = "Invalid password", HasError = true });

            return Ok(new LoginResponseDTO
            {
                Message = "Logged in successfully",
                AccessToken = GenerateToken(user, configuration)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDTO>> Register([FromBody] RegisterRequestDTO registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            registerModel.Name = registerModel.Name.Trim();
            registerModel.Username = registerModel.Username.Trim();

            if (await dbContext.ApplicationUsers.AnyAsync(user => user.Username == registerModel.Username))
                return BadRequest(new RegisterResponseDTO { Message = "Username already exists", HasError = true });

            ApplicationUser user = new ApplicationUser()
            {
                Name = registerModel.Name,
                Username = registerModel.Username,

                // this is not safe just for demo purpose
                // Ideally only admins can register new admins
                Role = registerModel.Role,

                CreationDateTime = DateTime.Now,
                Password = CryptoHelper.HashPassword(registerModel.Password)
            };

            try
            {
                dbContext.ApplicationUsers.Add(user);
                await dbContext.SaveChangesAsync();
                return Ok(new RegisterResponseDTO { Message = "User created successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private string GenerateToken(ApplicationUser user, IConfiguration configuration)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.GivenName,user.Name),
            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(Convert.ToInt32(configuration["JWT:ExpiryDurationHours"])),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}

