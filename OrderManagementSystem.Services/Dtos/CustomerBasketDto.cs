namespace Talabate.PL.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }

    }
}
