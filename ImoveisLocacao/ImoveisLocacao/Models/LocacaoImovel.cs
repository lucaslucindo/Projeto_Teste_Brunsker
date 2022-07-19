namespace ImoveisLocacao.Models
{
    public class LocacaoImovel
    {
        public LocacaoImovel(int LocacaoId, int ImovelId)
        {
            this.LocacaoId = LocacaoId;
            this.ImovelId = ImovelId;
        }
        public int Id { get; set; }
        public Locacao Locacao { get; set; }
        public int LocacaoId { get; set; }
        public int ImovelId { get; set; }
        public Imovel Imovel { get; set; }
    }
}
