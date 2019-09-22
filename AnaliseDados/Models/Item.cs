namespace AnaliseDados.Models
{
    public class Item
    {
        public string ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }

        public decimal Total { get { return ItemQuantity * ItemPrice; } }
    }
}