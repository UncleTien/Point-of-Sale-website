namespace Point_of_Sale_website.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public string RequestedBy { get; set; } // Người gửi yêu cầu
        public DateTime RequestedDate { get; set; } // Thời gian gửi yêu cầu
        public bool RequestApproved { get; set; } = false;// Trạng thái yêu cầu

    }
}
