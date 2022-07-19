using ImoveisLocacao.Data;
using ImoveisLocacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImoveisLocacao.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class ImovelController : Controller
    {
        private readonly Context _context;

        public ImovelController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetImoveis()
        {
            // Busca todos os imoveis por tipos de locação
            return await _context.Imoveis.Include(q => q.Tipo).ToListAsync();
        }

        // Busca do imovel por ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetImovel(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);

            if (imovel == null)
            {
                return NotFound();
            }

            return imovel;
        }

        // Metodo de edição de imovel, necessário passar o objeto completo para completar a edição.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImovel(int id, Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return BadRequest();
            }

            _context.Entry(imovel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImovelExists(id))
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

        // Metodo de cadastro de Imovel, o retorno é o GetImoveis.
        [HttpPost]
        public async Task<ActionResult<Imovel>> PostImovel(Imovel imovel)
        {
            _context.Imoveis.Add(imovel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImovel", new { id = imovel.Id }, imovel);
        }

        // Deleta um imovel
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImovel(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);

            if (imovel == null)
            {
                return NotFound();
            }

            // verifica se o Id do imovel está sendo usada como chave estrangeira nas tabelas do banco
            if (ImovelUsed(id))
            {
                return NotFound(new { message = "Este Imovel está locado, impossível apaga-lo." });
            }
            _context.Imoveis.Remove(imovel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImovelExists(int id)
        {
            return _context.Imoveis.Any(e => e.Id == id);
        }

        private bool ImovelUsed(int id)
        {
            return _context.LocacoesImoveis.Any(e => e.ImovelId == id);
        }
    }
}
