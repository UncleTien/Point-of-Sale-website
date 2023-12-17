namespace Point_of_Sale_website.Models
{
    public class ReportsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
