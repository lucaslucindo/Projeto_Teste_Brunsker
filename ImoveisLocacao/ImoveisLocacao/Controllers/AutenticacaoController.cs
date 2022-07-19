using ImoveisLocacao.Data;
using ImoveisLocacao.Models;
using ImoveisLocacao.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace ImoveisLocacao.Controllers
{
    [Route("[controller]")]
    [ApiController]


    public class AutenticacacaoController : Controller
    {
        private readonly Context _context;

        public AutenticacacaoController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] Usuario user)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(q => q.Login.ToUpper() == user.Login.ToUpper() && q.Senha == user.Senha);
            
            // Vefirifica se o usuário existe
            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(usuario);

            usuario.Senha = "";

            return new
            {
                user = usuario,
                token = token
            };
        }
    }
}

