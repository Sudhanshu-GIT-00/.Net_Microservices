namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int CouponId { get; internal set; }
        public double DiscountAmount { get; internal set; }
        public string? CouponCode { get; internal set; }
        public int MinAmount { get; internal set; }
    }
}
