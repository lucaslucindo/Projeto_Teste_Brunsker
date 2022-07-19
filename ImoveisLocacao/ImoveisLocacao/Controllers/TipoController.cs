using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImoveisLocacao.Data;
using ImoveisLocacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ImoveisLocacao.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TipoController : Controller
    {
        private readonly Context _context;

        public TipoController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            // Busca todos os Tipos de imoveis cadastrados
            return await _context.Tipos.ToListAsync();
        }

        // Busca um tipo de imovel especifico por Id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo>> GetTipo(int id)
        {
            var tipo = await _context.Tipos.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            return tipo;
        }

        // Edição do tipo de imovel
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo(int id, Tipo tipo)
        {
            if (id != tipo.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExists(id))
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

        // Metodo Cadastrar tipo do imovel
        [HttpPost]
        public async Task<ActionResult<Tipo>> PostTipo(Tipo tipo)
        {
            _context.Tipos.Add(tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo", new { id = tipo.Id }, tipo);
        }

        // Metodo Deletar tipo do imovel
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo(int id)
        {
            var tipo = await _context.Tipos.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            // Verifica se o tipo está sendo utilizado em alguma outra tabela
            if (TipoUsed(id))
            {
                return NotFound(new { message = "Esse Tipo de imóvel já está está sendo usado, impossível apaga-lo." });
            }

            _context.Tipos.Remove(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoExists(int id)
        {
            return _context.Tipos.Any(e => e.Id == id);
        }
        // Verifica se o tipo de imóvel está sendo usado pra poder excluir
        private bool TipoUsed(int id)
        {
            return _context.Imoveis.Any(e => e.TipoId == id);
        }
    }
}
