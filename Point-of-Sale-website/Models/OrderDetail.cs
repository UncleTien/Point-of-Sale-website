namespace Point_of_Sale_website.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Khóa ngoại đến đơn đặt hàng
        public int ProductId { get; set; } // Khóa ngoại đến sản phẩm
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; } // Khóa ngoại đến đơn đặt hàng
        public Product Product { get; set; } // Khóa ngoại đến sản phẩm
    }
}
