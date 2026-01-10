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
        public async Task<IActionResult> Register([FromBody] RegistroUsuarioDTO dto)
        {
            // validação básica
            if (string.IsNullOrWhiteSpace(dto.Usuario) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Senha))
            {
                return BadRequest("Usuário, e-mail e senha são obrigatórios.");
            }

            // verifica duplicidade de usuário
            var existingUser = await _usuariosRepositorio.ObterUsuario(dto.Usuario);
            if (existingUser != null)
                return BadRequest("Usuário já existe.");

            // verifica duplicidade de e-mail (precisa de método no repositório)
            var existingByEmail = await _usuariosRepositorio.ObterPorEmail(dto.Email);
            if (existingByEmail != null)
                return BadRequest("E-mail já está em uso.");

            var user = new UsuarioModel
            {
                Usuario = dto.Usuario.Trim(),
                Nome = dto.Nome?.Trim(),
                Email = dto.Email.Trim(),
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Perfil = "aluno",
                Ativo = true,
                CriadoEm = DateTime.UtcNow
            };

            await _usuariosRepositorio.RegistrarUsuario(user);

            return Ok();
        }

        private string GerarToken(UsuarioModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),  // ← ID NUMÉRICO
                new Claim(ClaimTypes.Name, usuario.Usuario),
                new Claim(ClaimTypes.Role, usuario.Perfil ?? "aluno")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
