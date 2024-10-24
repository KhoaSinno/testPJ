namespace ThienAnFuni.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public List<Goods>? Goods { get; set; }
    }
}
