using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactNetProyect.BackEnd.API.Utils;
using ReactNetProyect.BackEnd.Data;
using ReactNetProyect.Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactNetProyect.BackEnd.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ReactNetProyectContext _context;
        private readonly IMapper _mapper;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ReactNetProyectContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("listadoUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<List<UserDTO>>> ListUsers([FromQuery] PagerDTO pagerDTO)
        {
            var queryable = _context.Users.AsQueryable();
            await HttpContext.InsertPagerParamsInHeader(queryable);
            var usuarios = await queryable.OrderBy(x => x.Email).Pager(pagerDTO).ToListAsync();
            return _mapper.Map<List<UserDTO>>(usuarios);
        }

        [HttpPost("MakeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> MakeAdmin([FromBody] string usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            await _userManager.AddClaimAsync(usuario, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("RemoverAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> RemoverAdmin([FromBody] string usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            await _userManager.RemoveClaimAsync(usuario, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<AuthResponse>> Create([FromBody] UserCredentials credenciales)
        {
            var usuario = new IdentityUser { UserName = credenciales.Email, Email = credenciales.Email };
            var resultado = await _userManager.CreateAsync(usuario, credenciales.Password);

            if (resultado.Succeeded)
            {
                return await BuildToken(credenciales);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] UserCredentials credenciales)
        {
            var resultado = await _signInManager.PasswordSignInAsync(credenciales.Email, credenciales.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await BuildToken(credenciales);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private async Task<AuthResponse> BuildToken(UserCredentials credenciales)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credenciales.Email)
            };

            var usuario = await _userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await _userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion
            };
        }
    }
}
