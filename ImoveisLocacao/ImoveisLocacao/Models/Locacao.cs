using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImoveisLocacao.Models
{
    public class Locacao
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter {1} caractere. ")]
        public string CPF { get; set; }
        public DateTime DataLocacao { get; set; }
        public virtual ICollection<LocacaoImovel> LocacaoImoveis { get; set; }
    }
}
