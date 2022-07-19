using System;
using System.ComponentModel.DataAnnotations;

namespace ImoveisLocacao.Models
{
    public class Imovel
    {
        public Imovel() { }

        public Imovel(string Nome, DateTime DataCriacao, bool Ativo, int TipoId)
        {
            this.Nome = Nome;
            this.DataCriacao = DataCriacao;
            this.Ativo = Ativo;
            this.TipoId = TipoId;
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public void AtualizarImovel(string Nome, bool Ativo)
        {
            this.Nome = Nome;
            this.Ativo = Ativo;
        }
    }
}
