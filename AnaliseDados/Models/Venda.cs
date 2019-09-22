using System.Collections.Generic;
using System.Linq;

namespace AnaliseDados.Models
{
    public class Venda
    {
        public string SaleId { get; set; }
        public List<Item> Itens { get; set; }
        public string SalesmanName { get; set; }

        public Venda()
        {
            this.Itens = new List<Item>();
        }

        public decimal TotalDosItens
        {
            get { return Itens.Count > 0 ? (from i in Itens select i.Total).Sum() : 0; }
        }
    }
}