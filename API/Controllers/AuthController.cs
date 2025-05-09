using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using MyApi.Helpers;
using MyApi.Application.DTOs.UserDTOs;
using MyApi.Application.Services.Interfaces;


namespace MyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #nullable disable
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            // Rechercher l'utilisateur dans la base de données par email
            var user = await _userService.GetUserByEmailAsync(userLogin.Email);
            
            // Si l'utilisateur n'existe pas ou si le mot de passe ne correspond pas, renvoyer une erreur
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            {
                return Unauthorized("Email ou mot de passe incorrect.");
            }

            // Si l'utilisateur est trouvé et que le mot de passe est correct, générer le token JWT
            var token = GenerateJwtToken(user.Email, user.Role, user.Id);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string email, string role, int id)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role), // Ajout du rôle ici
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.ExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
   

        [HttpPost("verifyToken")]
        public IActionResult VerifyToken()
        {
            var authorizationHeader = Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Token non fourni ou invalide.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length);

            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = key,
                    ValidateLifetime = true // Vérifie si le token est expiré
                }, out SecurityToken validatedToken);

                return Ok(new { IsValid = true });
            }
            catch (Exception)
            {
                return Unauthorized("Token invalide ou expiré.");
            }
        }
    }
}
