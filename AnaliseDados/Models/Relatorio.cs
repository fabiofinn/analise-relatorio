using System.Collections.Generic;
using System.Linq;

namespace AnaliseDados.Models
{
    public class Relatorio
    {
        public List<Vendedor> Vendedores { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Venda> Vendas { get; set; }

        public Relatorio()
        {
            this.Vendedores = new List<Vendedor>();
            this.Clientes = new List<Cliente>();
            this.Vendas = new List<Venda>();
        }

        public string GetIdVendaMaisCara()
        {
            if (Vendas.Count == 0) return string.Empty;

            return Vendas.OrderByDescending(x => x.TotalDosItens).FirstOrDefault().SaleId;
        }

        public string GetPiorVendedor()
        {
            if (Vendas.Count == 0) return string.Empty;

            return Vendas.OrderBy(x => x.TotalDosItens).FirstOrDefault().SalesmanName;
        }
    }
}