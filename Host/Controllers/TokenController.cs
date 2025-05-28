using Dominio.Configuration;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public TokenController(AppSettings appSettings, IUsuariosRepositorio usuariosRepositorio)
        {
            _appSettings = appSettings;
            _usuariosRepositorio = usuariosRepositorio;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _usuariosRepositorio.ObterUsuario(dto.Usuario);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, user.SenhaHash))
                return Unauthorized();

            var token = GerarToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDTO dto)
        {
            var existingUser = await _usuariosRepositorio.ObterUsuario(dto.Usuario);
            if (existingUser != null)
                return BadRequest("Usuário já existe.");

            var user = new UsuarioModel
            {
                Usuario = dto.Usuario,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            await _usuariosRepositorio.RegistrarUsuario(user);

            return Ok();
        }

        private string GerarToken(UsuarioModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, usuario.Usuario)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
