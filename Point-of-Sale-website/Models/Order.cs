namespace Point_of_Sale_website.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } // Khóa ngoại đến khách hàng
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        // Các trường dữ liệu khác (nếu cần)

        public List<OrderDetail> OrderDetails { get; set; } // Danh sách chi tiết đơn hàng
        public User Customer { get; set; } // Khóa ngoại đến khách hàng
    }
}
