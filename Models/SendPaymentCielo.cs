
namespace Models
{
    public class SendPaymentCielo
    {
        public string PaymentId { get; set; }
        public int Installments { get; set; }
        public int CapturedAmount { get; set; }
        public int OrderId { get; set; }
    }
}
