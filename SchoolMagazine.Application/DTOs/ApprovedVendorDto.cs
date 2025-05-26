namespace SchoolMagazine.Application.DTOs
{
    public class ApprovedVendorDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
       // public decimal AmountPaid { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
    }

}
