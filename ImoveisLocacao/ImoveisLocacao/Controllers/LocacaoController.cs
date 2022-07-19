using ImoveisLocacao.Data;
using ImoveisLocacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImoveisLocacao.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LocacaoController : ControllerBase
    {
        private readonly Context _context;

        public LocacaoController(Context context)
        {
            _context = context;
        }

        // Metodo listar todas as locações.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locacao>>> GetLocacoes()
        {
            return await _context.Locacoes.Include(q => q.LocacaoImoveis).ThenInclude(q => q.Imovel).ToListAsync();
        }

        // Lista somente uma locação.
        [HttpGet("{id}")]
        public async Task<ActionResult<Locacao>> GetLocacao(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);

            if (locacao == null)
            {
                return NotFound();
            }

            return locacao;
        }

        // Cadastro da locacao
        // Criando uma View Model, para realizar o cadastro de locação.
        [HttpPost]
        public async Task<ActionResult<Locacao>> PostLocacao(LocacaoVM locacaoVM)
        {

            _context.Locacoes.Add(locacaoVM.Locacao);
            await _context.SaveChangesAsync();

            // Percorre toda a lista de imoveis que chegou no array viewmodel e cadastra como imovel locado
            foreach (var item in locacaoVM.ImovelId)
            {
                LocacaoImovel locacaoImovel = new LocacaoImovel(locacaoVM.Locacao.Id, item);
                _context.Add(locacaoImovel);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocacao", new { id = locacaoVM.Locacao.Id }, locacaoVM.Locacao);
        }
    }
}
