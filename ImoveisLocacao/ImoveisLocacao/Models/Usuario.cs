using System.ComponentModel.DataAnnotations;

namespace ImoveisLocacao.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
