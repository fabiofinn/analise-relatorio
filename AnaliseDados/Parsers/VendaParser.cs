using AnaliseDados.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AnaliseDados.Parsers
{
    public class VendaParser
    {
        int saleIdIndex = 1;
        int itensIndex = 2;
        int salesmanNameIndex = 3;

        int itemIdIndex = 0;
        int itemQuantityIndex = 1;
        int itemPriceIndex = 2;

        public Venda Parse(string[] split)
        {
            if (split.Length != Constants.SplitLength)
                throw new Exception("Dados inválidos");

            return new Venda
            {
                SaleId = split[saleIdIndex],
                Itens = ParseItens(split[itensIndex]),
                SalesmanName = split[salesmanNameIndex]
            };
        }

        private List<Item> ParseItens(string itensData)
        {
            var split = itensData.Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var itens = new List<Item>();

            foreach (var item in split)
            {
                itens.Add(ParseItem(item));
            }

            return itens;
        }

        private Item ParseItem(string itemData)
        {
            var split = itemData.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != Constants.ItemSplitLength)
                throw new Exception("Dados de item inválidos");

            var itemQuantityParseOk = int.TryParse(split[itemQuantityIndex], out int itemQuantity);
            if (!itemQuantityParseOk)
                throw new Exception("Erro ao ler a quantidade do Item");

            var itemPriceParseOk = decimal.TryParse(split[itemPriceIndex], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal itemPrice);
            if (!itemPriceParseOk)
                throw new Exception("Erro ao ler o preço do Item");

            return new Item
            {
                ItemId = split[itemIdIndex],
                ItemQuantity = itemQuantity,
                ItemPrice = itemPrice
            };
        }
    }
}