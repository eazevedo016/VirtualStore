namespace VirtualStore.VirtualStore.Domain.Entities
{
    public class Produto
    {
        public int Id { get;  set; }
        public string Nome { get;  set; }
        public int Estoque { get;  set; }
        public double Valor { get;  set; }


        public Produto()
        {
        }
        public Produto(int id, string nome, int estoque, double valor)
        {
            Id = id;
            Nome = nome;
            Estoque = estoque;
            Valor = valor;
        }
    }
}
